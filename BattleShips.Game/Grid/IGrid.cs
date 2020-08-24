using BattleShips.Game.Grid;
using BattleShips.Game.Ships;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShips.Grid
{
    public interface IGrid
    {
        int Size { get; }
        void AddShip(IShip ship);
        IEnumerable<IShip> Ships { get; }
        IEnumerable<IGridPoint> UsedGridPoints { get; }
    }
}
