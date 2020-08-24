using BattleShips.Game.Grid;
using BattleShips.Game.Ships;
using BattleShips.Grid;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShips.Game.UnitTests
{
    public class BattleShipsGameUnitTests
    {
        private Mock<IGrid> _grid;
        private BattleShipsGame _game;

        [SetUp]
        public void SetUp()
        {
            _grid = new Mock<IGrid>();
            _game = new BattleShipsGame(_grid.Object);
        }

        [Test]
        public void BattleShipGame_ShouldBeFinished_WhenAllShipsSunk()
        {
            // Arrange
            var ship1 = new Mock<IShip>();
            ship1.SetupGet(x => x.IsDead).Returns(true);

            var ship2 = new Mock<IShip>();
            ship2.SetupGet(x => x.IsDead).Returns(true);

            _grid.SetupGet(x => x.Ships).Returns(new[] { ship1.Object, ship2.Object });

            // Act && Assert
            Assert.IsTrue(_game.IsGameFinished);
        }

        [Test]
        public void BattleShipGame_ShouldNotBeFinished_WhenThereAreShipsAlive()
        {
            // Arrange
            var ship1 = new Mock<IShip>();
            ship1.SetupGet(x => x.IsDead).Returns(true);

            var ship2 = new Mock<IShip>();
            ship2.SetupGet(x => x.IsDead).Returns(false);

            _grid.SetupGet(x => x.Ships).Returns(new[] { ship1.Object, ship2.Object });

            // Act && Assert
            Assert.IsFalse(_game.IsGameFinished);
        }

        [Test]
        public void BattleShipGame_ShouldAddNewShip_WhenThereIsEnoughSpaceInTheGrid()
        {
            // Arrange
            _grid.SetupGet(x => x.Size).Returns(10);

            // Act
            _game.TryAddShip(ShipType.Battleship, 5);

            // Assert
            _grid.Verify(x => x.AddShip(It.IsAny<IShip>()), Times.Once);
        }

        [Test]
        public void BattleShipGame_ShouldTrowExceptionWhileAddingNewShip_WhenThereIsNotEnoughSpaceInTheGrid()
        {
            // Arrange
            _grid.SetupGet(x => x.Size).Returns(6);

            var usedGridPointList = new List<IGridPoint>();

            for (int i = 0; i < 6 * 6 - 4; i++)
            {
                usedGridPointList.Add(new Mock<IGridPoint>().Object);
            }
            _grid.SetupGet(x => x.UsedGridPoints).Returns(usedGridPointList);

            // Act && Assert
            Assert.Throws<Exception>(() =>
            {
                _game.TryAddShip(ShipType.Battleship, 5);
            });

            _grid.Verify(x => x.AddShip(It.IsAny<IShip>()), Times.Never);
        }

        [Test]
        public void BattleShipGame_ShipShouldSink_WhenShotLastGridPoint()
        {
            // Arrange
            var ship = new Mock<IShip>();
            var gridPoint = new Mock<IGridPoint>();
            gridPoint.SetupGet(x => x.X).Returns(0);
            gridPoint.SetupGet(x => x.Y).Returns(0);
            ship.SetupGet(x => x.Body).Returns(new[] { gridPoint.Object });
            ship.SetupGet(x => x.IsDead).Returns(true);
            _grid.SetupGet(x => x.UsedGridPoints).Returns(new[] { gridPoint.Object });
            _grid.SetupGet(x => x.Ships).Returns(new[] { ship.Object });

            var shipSunk = false;
            _game.ShipSunk += (sender, e) =>
            {
                shipSunk = true;
            };

            // Act
            _game.MakeShot(0, 0);

            // Assert
            Assert.IsTrue(shipSunk);
        }

        [Test]
        public void BattleShipGame_ShipShouldNotSink_WhenShotNotLastGridPoint()
        {
            // Arrange
            var ship = new Mock<IShip>();
            var gridPoint = new Mock<IGridPoint>();
            gridPoint.SetupGet(x => x.X).Returns(0);
            gridPoint.SetupGet(x => x.Y).Returns(0);
            ship.SetupGet(x => x.Body).Returns(new[] { gridPoint.Object });
            ship.SetupGet(x => x.IsDead).Returns(false);
            _grid.SetupGet(x => x.UsedGridPoints).Returns(new[] { gridPoint.Object });
            _grid.SetupGet(x => x.Ships).Returns(new[] { ship.Object });

            var shipHit = false;
            _game.ShipHit += (sender, e) =>
            {
                shipHit = true;
            };

            // Act
            _game.MakeShot(0, 0);

            // Assert
            Assert.IsTrue(shipHit);
        }

        [Test]
        public void BattleShipGame_ShipShouldBeUntouched_WhenShotMissed()
        {
            // Arrange
            var ship = new Mock<IShip>();
            var gridPoint = new Mock<IGridPoint>();
            gridPoint.SetupGet(x => x.X).Returns(0);
            gridPoint.SetupGet(x => x.Y).Returns(0);
            ship.SetupGet(x => x.Body).Returns(new[] { gridPoint.Object });
            ship.SetupGet(x => x.IsDead).Returns(false);
            _grid.SetupGet(x => x.UsedGridPoints).Returns(new[] { gridPoint.Object });
            _grid.SetupGet(x => x.Ships).Returns(new[] { ship.Object });

            var missedShot = false;
            _game.MissedShot += (sender, e) =>
            {
                missedShot = true;
            };

            // Act
            _game.MakeShot(99, 99);

            // Assert
            Assert.IsTrue(missedShot);
        }
    }
}

