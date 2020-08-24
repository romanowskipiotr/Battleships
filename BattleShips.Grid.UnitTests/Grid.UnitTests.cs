using BattleShips.Game.Grid;
using BattleShips.Game.Ships;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShips.Game.UnitTests
{
    public class GridUnitTests
    {
        [Test]
        public void Grid_ShouldBeCreatedSuccessfully()
        {
            // Arrange
            var gridSize = 10;

            // Act
            var grid = new BattleShips.Grid.Grid(gridSize);

            // Assert
            Assert.IsNotNull(grid.Ships);
            Assert.IsNotNull(grid.UsedGridPoints);
            Assert.AreEqual(gridSize, grid.Size);
        }

        [Test]
        public void Grid_ShouldCollectUsedGridPoint_WhenAddedShip()
        {
            // Arrange
            var gridSize = 10;
            var grid = new BattleShips.Grid.Grid(gridSize);

            var gridPoint1 = new Mock<IGridPoint>();
            var gridPoint2 = new Mock<IGridPoint>();

            var ship = new Mock<IShip>();
            ship.Setup(x => x.Body).Returns(new[] { gridPoint1.Object, gridPoint2.Object });

            // Act
            grid.AddShip(ship.Object);

            // Assert
            Assert.AreEqual(2, grid.UsedGridPoints.Count());
            Assert.AreEqual(1, grid.Ships.Count());
        }
    }
}
