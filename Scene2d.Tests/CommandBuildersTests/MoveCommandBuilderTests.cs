namespace Scene2d.Tests.CommandBuildersTests
{
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;

    [TestFixture]
    public class MoveCommandBuilderTests
    {
        [Test]
        public void AppendLine_IncorrectMoveCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate { commandProducer.AppendLine("move 123"); },
                "Incorrect move command did not throw an exception");
        }
    }
}
