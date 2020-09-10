namespace Scene2d.Tests.CommandBuildersTests
{
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;

    [TestFixture]
    public class PrintCircumscribingRectangleCommandBuilderTests
    {
        [Test]
        public void AppendLine_IncorrectPrintCircumscribingRectangleCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate { commandProducer.AppendLine("print circumscribing rectangle for 123 123"); },
                "Incorrect print circumscribing rectangle command did not throw an exception");
        }
    }
}
