namespace Scene2d.Tests.CommandBuildersTests
{
    using System.Linq;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    [TestFixture]
    public class AddCircleCommandBuilderTests
    {
        [Test]
        public void AppendLine_NonPositiveRadius_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();
            const string exampleString = "add circle TEST (0, 0) radius 0";

            Assert.Throws<BadCircleRadiusException>(
                delegate { commandProducer.AppendLine(exampleString); },
                "Non-positive radius did not throw an exception");
        }

        [Test]
        public void AppendLine_ParsingGivenString_ShouldMakeNewCircleFigure()
        {
            var commandProducer = new CommandProducer();
            var scene = new Scene();
            const string exampleString = "add circle TEST (0, 0) radius 1";

            commandProducer.AppendLine(exampleString);
            var command = commandProducer.GetCommand();
            command.Apply(scene);

            var sceneFigure = scene.ListDrawableFigures().First();
            var circleFigure = new CircleFigure(new ScenePoint(0, 0), 1);

            var jsonSceneFigure = JsonConvert.SerializeObject(sceneFigure);
            var jsonCircleFigure = JsonConvert.SerializeObject(circleFigure);

            Assert.That(
                jsonCircleFigure,
                Is.EqualTo(jsonSceneFigure),
                "Parsed CircleFigure is incorrect");
        }

        [Test]
        public void AppendLine_IncorrectAddCircleCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate { commandProducer.AppendLine("add circle 123"); },
                "Incorrect add circle command did not throw an exception");
        }
    }
}
