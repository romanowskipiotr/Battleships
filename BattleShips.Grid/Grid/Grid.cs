using System;
using System.Collections.Generic;
using System.Linq;
using BattleShips.Game.Grid;
using BattleShips.Game.Ships;

namespace BattleShips.Grid
{
    public class Grid : IGrid
    {
        public int Size { get; }

        public IEnumerable<IShip> Ships { get; private set; }

        public IEnumerable<IGridPoint> UsedGridPoints => Ships.SelectMany(x => x.Body);

        public Grid(int size)
        {
            Size = size;
            Ships = new List<IShip>();
        }

        public void AddShip(IShip ship)
        {
            Ships = Ships.Append(ship);
        }
    }
}
