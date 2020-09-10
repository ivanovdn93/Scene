namespace Scene2d.Tests.CommandBuildersTests
{
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;

    [TestFixture]
    public class CopyCommandBuilderTests
    {
        [Test]
        public void AppendLine_IncorrectCopyCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate { commandProducer.AppendLine("copy 123"); },
                "Incorrect copy command did not throw an exception");
        }
    }
}
