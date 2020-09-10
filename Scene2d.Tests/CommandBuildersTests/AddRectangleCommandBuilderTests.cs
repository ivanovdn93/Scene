namespace Scene2d.Tests.CommandBuildersTests
{
    using System.Linq;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    [TestFixture]
    public class AddRectangleCommandBuilderTests
    {
        [Test]
        public void AppendLine_OppositeVertices_CanNotBeEqual()
        {
            var commandProducer = new CommandProducer();
            const string addRectangle = "add rectangle TEST (0, 0) (0, 0)";

            Assert.Throws<BadRectanglePointException>(
                delegate { commandProducer.AppendLine(addRectangle); },
                "Adding rectangle with equal opposite vertices did not throw an exception");
        }

        [Test]
        public void AppendLine_ParsingGivenString_ShouldMakeNewRectangleFigure()
        {
            var commandProducer = new CommandProducer();
            var scene = new Scene();
            const string exampleString = "add rectangle TEST (0, 0) (1, 1)";

            commandProducer.AppendLine(exampleString);
            var command = commandProducer.GetCommand();
            command.Apply(scene);

            var sceneFigure = scene.ListDrawableFigures().First();
            var rectangleFigure = new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1));

            var jsonSceneFigure = JsonConvert.SerializeObject(sceneFigure);
            var jsonRectangleFigure = JsonConvert.SerializeObject(rectangleFigure);

            Assert.That(
                jsonRectangleFigure,
                Is.EqualTo(jsonSceneFigure),
                "Parsed RectangleFigure is incorrect");
        }

        [Test]
        public void AppendLine_IncorrectAddRectangleCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate { commandProducer.AppendLine("add rectangle 123"); },
                "Incorrect add rectangle command did not throw an exception");
        }
    }
}
