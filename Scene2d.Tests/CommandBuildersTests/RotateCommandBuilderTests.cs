namespace Scene2d.Tests.CommandBuildersTests
{
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;

    [TestFixture]
    public class RotateCommandBuilderTests
    {
        [Test]
        public void AppendLine_IncorrectRotateCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate { commandProducer.AppendLine("rotate 123"); },
                "Incorrect rotate command did not throw an exception");
        }
    }
}
