using BattleShips.Game.Grid;
using BattleShips.Game.Ships;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShips.Game
{
    public interface IBattleShipsGame
    {
        bool IsGameFinished { get; }

        void TryAddShip(ShipType shipType, int shipSize);

        void MakeShot(int rowIndex, int columnIndex);

        event EventHandler<IShip> ShipSunk;
    }
}
