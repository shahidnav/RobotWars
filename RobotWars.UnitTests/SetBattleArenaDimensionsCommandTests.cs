using NUnit.Framework;
using RobotWars.Arena;
using RobotWars.Command;

namespace RobotWars.UnitTests
{
    public class SetBattleArenaDimensionsCommandTests
    {
        [TestFixture]
        public class SetBattleArenaDimensionsCommand_Contructor
        {
            [Test]
            public void Accepts_dimension_and_assigns_to_property()
            {
                var expectedDimension = new Dimension(5, 5);
                
                var command = new SetBattleArenaDimensionsCommand(expectedDimension);
                
                Assert.AreEqual(expectedDimension, command.Dimension);
            }
        }
    }
}
