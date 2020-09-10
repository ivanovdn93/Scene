namespace Scene2d.Tests.CommandBuildersTests
{
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;

    [TestFixture]
    public class DeleteCommandBuilderTests
    {
        [Test]
        public void AppendLine_IncorrectDeleteCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate { commandProducer.AppendLine("delete 123 123"); },
                "Incorrect delete command did not throw an exception");
        }
    }
}
