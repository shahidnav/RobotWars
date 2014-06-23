using Moq;
using NUnit.Framework;
using RobotWars.Arena;
using RobotWars.Command;

namespace RobotWars.UnitTests.Command
{
    public class SetBattleArenaDimensionsCommandTests
    {
        [TestFixture]
        public class SetBattleArenaDimensionsCommand_Constructor
        {
            [Test]
            public void Given_a_dimension_sets_property()
            {
                var dimension = new Dimension(1, 2);
                var setBattleArenaDimensionsCommand = new SetBattleArenaDimensionsCommand(dimension);
                Assert.AreEqual(dimension, setBattleArenaDimensionsCommand.Dimension);
            }
        }

        [TestFixture]
        public class SetBattleArenaDimensionsCommand_SetReceiver
        {
            [Test]
            public void Accepts_Receiver()
            {
                var dimension = new Dimension(0, 0);
                var mockBattleArena = new Mock<IBattleArena>();
                var setBattleArenaDimensionsCommand = new SetBattleArenaDimensionsCommand(dimension);
                Assert.DoesNotThrow(() =>
                    setBattleArenaDimensionsCommand.SetReceiver(mockBattleArena.Object));
            }
        }

        [TestFixture]
        public class SetBattleArenaDimensionsCommand_Execute
        {
            [Test]
            public void Sets_battlearena_dimension()
            {
                var mockBattleArena = new Mock<IBattleArena>();
                var dimension = new Dimension(0, 0);
                var setBattleArenaDimensionsCommand = new SetBattleArenaDimensionsCommand(dimension);
                setBattleArenaDimensionsCommand.SetReceiver(mockBattleArena.Object);

                setBattleArenaDimensionsCommand.Execute();

                mockBattleArena.Verify(x => x.SetDimension(dimension), Times.Once());
            }
        }

        [TestFixture]
        public class SetBattleArenaDimensionsCommand_GetCommandType
        {
            [Test]
            public void Returns_SetBattleArenaDimensionsCommand_type()
            {
                var dimension = new Dimension(0, 0);
                var setBattleArenaDimensionsCommand = new SetBattleArenaDimensionsCommand(dimension);
                Assert.AreEqual(setBattleArenaDimensionsCommand.GetCommandType(), CommandType.SetBattleArenaDimensions);
            }
        }
    }
}
