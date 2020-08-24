using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShips.Game.Grid
{
    public class GridPoint : IGridPoint
    {
        public int X { get; }

        public int Y { get; }

        public bool Alive { get; private set; }

        public GridPoint(int x, int y)
        {
            Alive = true;
            X = x;
            Y = y;
        }

        public void HitGridPoint() => Alive = false;
    }
}
