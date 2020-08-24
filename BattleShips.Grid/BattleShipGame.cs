using BattleShips.Game.Grid;
using BattleShips.Game.Ships;
using BattleShips.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips.Game
{
    public class BattleShipsGame : IBattleShipsGame
    {
        private IGrid _grid;

        public bool IsGameFinished => !_grid.Ships.Any(ship => !ship.IsDead);
        public event EventHandler<IShip> ShipSunk;
        public event EventHandler<IShip> ShipHit;
        public event EventHandler MissedShot;

        public BattleShipsGame(IGrid grid)
        {
            _grid = grid;
        }
        
        public void TryAddShip(ShipType shipType, int shipSize)
        {
            var startingPoint = GenerateStartingPoint(shipSize);

            bool isVerticalFree;
            bool isHorizontalFree;

            while (!ValidateStartingPoint(startingPoint, shipSize, out isHorizontalFree, out isVerticalFree))
            {
                startingPoint = GenerateStartingPoint(shipSize);
            }

            AddShip(shipType, shipSize, startingPoint, isHorizontalFree, isVerticalFree);
        }

        public void MakeShot(int rowIndex, int columnIndex)
        {
            var usedGridPoint = _grid.UsedGridPoints.FirstOrDefault(point => point.X == rowIndex && point.Y == columnIndex);

            if (usedGridPoint != null)
            {
                usedGridPoint.HitGridPoint();

                CheckShot(rowIndex, columnIndex);
            }
            else
                MissedShot?.Invoke(this, new EventArgs());
        }

        private IShip GetShipByGridPoint(int rowIndex, int columnIndex)
        {
            return _grid.Ships.First(sh => sh.Body.Any(gP => gP.X == rowIndex && gP.Y == columnIndex));
        }

        private void CheckShot(int rowIndex, int columnIndex)
        {
            var ship = GetShipByGridPoint(rowIndex, columnIndex);

            if (ship.IsDead)
                ShipSunk?.Invoke(this, ship);
            else
                ShipHit?.Invoke(this, ship);
        }

        private void AddShip(ShipType shipType, int shipSize, GridPoint startingPoint, bool isHorizontalFree, bool isVerticalFree)
        {
            bool horizontalDirection;
            if (isHorizontalFree && isVerticalFree)
                horizontalDirection = new Random().Next(0, 2) == 0;
            else if (isHorizontalFree)
                horizontalDirection = true;
            else
                horizontalDirection = false;

            var shipBody = new List<IGridPoint>();
            for (var i = 0; i < shipSize; i++)
            {
                int x, y;

                x = horizontalDirection ? startingPoint.X : startingPoint.X + i;
                y = horizontalDirection ? startingPoint.Y + i : startingPoint.Y;

                shipBody.Add(new GridPoint(x, y));
            }

            var ship = new Ship(shipType, shipBody);

            _grid.AddShip(ship);
        }

        private bool ValidateStartingPoint(GridPoint startingPoint, int shipSize, out bool isHorizontalFree, out bool isVerticalFree)
        {
            if (_grid.Size * _grid.Size - _grid.UsedGridPoints.Count() < shipSize)
                throw new Exception("No more place on the game grid");

            isHorizontalFree = !_grid.UsedGridPoints.Any(point => point.X == startingPoint.X
            && point.Y >= startingPoint.Y
            && point.Y < _grid.Size - shipSize + point.Y);

            isVerticalFree = !_grid.UsedGridPoints.Any(point => point.Y == startingPoint.Y
            && point.X >= startingPoint.X
            && point.X < _grid.Size - shipSize + point.X);

            return isHorizontalFree || isVerticalFree;
        }

        private GridPoint GenerateStartingPoint(int shipSize)
        {
            var startX = new Random().Next(0, _grid.Size - shipSize - 1);
            var startY = new Random().Next(0, _grid.Size - shipSize - 1);

            return new GridPoint(startX, startY);
        }
    }
}
