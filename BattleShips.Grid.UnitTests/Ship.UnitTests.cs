using BattleShips.Game.Grid;
using BattleShips.Game.Ships;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShips.Game.UnitTests
{
    public class ShipUnitTests
    {
        [Test]
        public void Ship_ShouldBeCreatedSuccessfully()
        {
            // Arrange
            var shipType = ShipType.Battleship;
            var shipBody = new GridPoint[] { new GridPoint(0, 0), new GridPoint(0, 1), new GridPoint(0, 2), new GridPoint(0, 3), new GridPoint(0, 4) };

            // Act
            var ship = new Ship(shipType, shipBody);

            // Assert
            Assert.AreEqual(ShipType.Battleship, shipType);
            Assert.AreEqual(shipBody.Length, ship.Size);
            Assert.IsFalse(ship.IsDead);
            Assert.AreEqual(shipBody, ship.Body);
        }

        [Test]
        public void Ship_ShouldBeDead_WhenAllGridPointsAreNotAlive()
        {
            // Arrange
            var shipType = ShipType.Battleship;
            var gridPoint1 = new GridPoint(0, 0);
            var gridPoint2 = new GridPoint(0, 1);
            var gridPoint3 = new GridPoint(0, 2);
            var ship = new Ship(shipType, new[] { gridPoint1, gridPoint2, gridPoint3 });

            // Act
            gridPoint1.HitGridPoint();
            gridPoint2.HitGridPoint();
            gridPoint3.HitGridPoint();

            // Assert
            Assert.IsTrue(ship.IsDead);
        }
    }
}
