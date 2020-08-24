using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BattleShips.Game.Grid;

namespace BattleShips.Game.Ships
{
    public class Ship : IShip
    {
        public int Size { get; }

        public bool IsDead => !Body.Any(x => x.Alive);

        public ShipType Type { get; }

        public IEnumerable<IGridPoint> Body { get; }


        public Ship(ShipType shipType, IEnumerable<IGridPoint> body)
        {
            Type = shipType;
            Size = body.Count();
            Body = body;
        }
    }
}
