using BattleShips.Game.Grid;
using BattleShips.Grid;
using NUnit.Framework;

namespace Tests
{
    public class GridPointUnitTests
    {

        [Test]
        public void GridPoint_ShouldNotBeAlive_WhenItGotShot()
        {
            // Arrange
            var gridPoint = new GridPoint(0, 0);

            // Act
            gridPoint.HitGridPoint();

            // Assert
            Assert.IsFalse(gridPoint.Alive);
        }
    }
}