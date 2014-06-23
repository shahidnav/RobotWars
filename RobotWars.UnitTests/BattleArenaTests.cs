using NUnit.Framework;
using RobotWars.Arena;

namespace RobotWars.UnitTests
{
    public class BattleArenaTests
    {
        [TestFixture]
        public class BattleArena_SetDimension
        {
            [TestCase(5, 5)]
            [TestCase(10, 10)]
            [TestCase(20, 20)]
            public void When_dimension_has_been_set_the_dimension_returned_has_the_same_values(int expectedX, int expectedY)
            {
                var battleArena = new BattleArena();
                var expectedDimension = new Dimension(expectedX, expectedY);
                
                battleArena.SetDimension(expectedDimension);

                Assert.AreEqual(expectedDimension, battleArena.GetDimension());
            }
        }
    
        [TestFixture]
        public class BattleArena_IsValid
        {
            [TestCase(1, 1, 0, 0)]
            [TestCase(1, 1, 1, 1)]
            [TestCase(5, 3, 5, 3)]
            public void When_point_is_within_size_boundary_returns_true(int boundaryX, int boundaryY, int attemptedPointX, int attemptedPointY)
            {
                var dimension = new Dimension(boundaryX, boundaryY);
                var point = new Point(attemptedPointX, attemptedPointY);
                var battleArena = new BattleArena();

                battleArena.SetDimension(dimension);

                var isValid = battleArena.IsValid(point);
                Assert.That(isValid);
            }

            [TestCase(1, 1, 2, 0)]
            [TestCase(1, 1, 0, 2)]
            [TestCase(1, 1, -1, 1)]
            [TestCase(1, 1, 1, -1)]
            [TestCase(5, 3, 3, 5)]
            public void When_point_is_outside_size_boundary_returns_false(int boundaryX, int boundaryY, int attemptedX, int attemptedY)
            {
                var dimension = new Dimension(boundaryX, boundaryY);
                var point = new Point(attemptedX, attemptedY);
                var battleArena = new BattleArena();

                battleArena.SetDimension(dimension);

                var isValid = battleArena.IsValid(point);
                Assert.That(!isValid);
            }
        }
    }
}