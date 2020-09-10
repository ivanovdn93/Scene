namespace Scene2d.Tests.CommandBuildersTests
{
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;

    [TestFixture]
    public class ReflectCommandBuilderTests
    {
        [Test]
        public void AppendLine_IncorrectReflectCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate { commandProducer.AppendLine("reflect 123"); },
                "Incorrect reflect command did not throw an exception");
        }
    }
}
