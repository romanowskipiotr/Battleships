using BattleShips.Game;
using BattleShips.Game.Ships;
using System;

namespace BattleShips.App
{
    class Program
    {
        private static ViewAdapter _viewAdapter;
        private static int _lastShotPointRow;
        private static int _lastShotPointColumn;

        static void Main(string[] args)
        {
            var gameSize = 10;
            var grid = new Grid.Grid(gameSize);
            var game = new BattleShipsGame(grid);

            game.ShipSunk += ShipShunk;
            game.MissedShot += MissShot;
            game.ShipHit += ShipHit;

            //TODO: read ships size from the configuration file
            game.TryAddShip(ShipType.Destroyer, 4);
            game.TryAddShip(ShipType.Destroyer, 4);
            game.TryAddShip(ShipType.Battleship, 5);

            _viewAdapter = new ViewAdapter(gameSize);
            while (!game.IsGameFinished)
            {
                try
                {
                    Console.WriteLine(_viewAdapter.ToString());

                    Console.WriteLine("Provide position to shoot");
                    var position = Console.ReadLine();

                    InputInterpreter.InterpretCell(position, out int rowNumber, out int columnNumber);

                    Console.WriteLine($"Shooting position: {position}.");

                    _lastShotPointRow = rowNumber;
                    _lastShotPointColumn = columnNumber;

                    game.MakeShot(rowNumber, columnNumber);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine("Game finished !");
        }

        private static void ShipHit(object sender, IShip ship)
        {
            _viewAdapter.MarkHit(_lastShotPointRow, _lastShotPointColumn);
            Console.WriteLine($"Hit. {ship.Type}.");
        }

        private static void MissShot(object sender, EventArgs e)
        {
            Console.WriteLine("Miss.");
        }

        private static void ShipShunk(object sender, IShip ship)
        {
            _viewAdapter.MarkHit(_lastShotPointRow, _lastShotPointColumn);
            Console.WriteLine($"Ship {ship.Type} shunk!");
        }
    }
}
