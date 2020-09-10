namespace Scene2d.Tests.CommandBuildersTests
{
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;

    [TestFixture]
    public class CommandProducerTests
    {
        [Test]
        public void AppendLine_UnexpectedEndOfPolygon_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<UnexpectedEndOfPolygonException>(
                delegate
                {
                    commandProducer.AppendLine("add polygon TEST");
                    commandProducer.AppendLine(" add point (0, 0)");
                    commandProducer.AppendLine("add rectangle TEST2 (0, 0) (1, 1)");
                },
                "Unexpected end of polygon did not throw an exception");
        }

        [Test]
        public void AppendLine_IncorrectCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate { commandProducer.AppendLine(""); },
                "Incorrect command did not throw an exception");
        }
    }
}
