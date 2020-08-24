using BattleShips.Game.Grid;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShips.Game.Ships
{
    public interface IShip
    {
        int Size { get; }
        bool IsDead { get; }
        ShipType Type { get; }
        IEnumerable<IGridPoint> Body { get; }
    }
}
