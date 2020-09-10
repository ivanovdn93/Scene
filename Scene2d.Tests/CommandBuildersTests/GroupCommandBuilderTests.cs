namespace Scene2d.Tests.CommandBuildersTests
{
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;

    [TestFixture]
    public class GroupCommandBuilderTests
    {
        [Test]
        public void AppendLine_IncorrectGroupCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate { commandProducer.AppendLine("group 123"); },
                "Incorrect group command did not throw an exception");
        }
    }
}
