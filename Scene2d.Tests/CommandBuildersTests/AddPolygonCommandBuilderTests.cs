namespace Scene2d.Tests.CommandBuildersTests
{
    using System.Linq;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Scene2d.CommandBuilders;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    [TestFixture]
    public class AddPolygonCommandBuilderTests
    {
        [Test]
        public void AppendLine_AddingSamePoints_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadPolygonPointException>(
                delegate
                {
                    commandProducer.AppendLine("add polygon TEST");
                    commandProducer.AppendLine(" add point (0, 0)");
                    commandProducer.AppendLine(" add point (0, 0)");
                },
                "Adding same points of polygon did not throw an exception");
        }

        [Test]
        public void AppendLine_SelfIntersecting_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadPolygonPointException>(
                delegate
                {
                    commandProducer.AppendLine("add polygon TEST");
                    commandProducer.AppendLine(" add point (-1, 1)");
                    commandProducer.AppendLine(" add point (1, 1)");
                    commandProducer.AppendLine(" add point (-1, -1)");
                    commandProducer.AppendLine(" add point (1, -1)");
                },
                "Self-intersecting polygon did not throw an exception");
        }

        [Test]
        public void AppendLine_FewPoints_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadPolygonPointNumberException>(
                delegate
                {
                    commandProducer.AppendLine("add polygon TEST");
                    commandProducer.AppendLine(" add point (-1, 1)");
                    commandProducer.AppendLine(" add point (1, 1)");
                    commandProducer.AppendLine("end polygon");
                },
                "Less than 3 points in polygon did not throw an exception");
        }

        [Test]
        public void AppendLine_ParsingGivenString_ShouldMakeNewPolygonFigure()
        {
            var commandProducer = new CommandProducer();
            var scene = new Scene();

            commandProducer.AppendLine("add polygon TEST");
            commandProducer.AppendLine(" add point (-1, 1)");
            commandProducer.AppendLine(" add point (1, 1)");
            commandProducer.AppendLine(" add point (0, 0)");
            commandProducer.AppendLine("end polygon");

            var command = commandProducer.GetCommand();
            command.Apply(scene);

            var sceneFigure = scene.ListDrawableFigures().First();
            var polygonFigure = new PolygonFigure(new[]
            {
                new ScenePoint(-1, 1), 
                new ScenePoint(1, 1),
                new ScenePoint(0, 0) 
            });

            var jsonSceneFigure = JsonConvert.SerializeObject(sceneFigure);
            var jsonPolygonFigure = JsonConvert.SerializeObject(polygonFigure);

            Assert.That(
                jsonPolygonFigure,
                Is.EqualTo(jsonSceneFigure),
                "Parsed PolygonFigure is incorrect");
        }

        [Test]
        public void AppendLine_IncorrectAddPolygonCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate
                {
                    commandProducer.AppendLine("add polygon TEST 123");
                    commandProducer.AppendLine(" add point (-1, 1)");
                    commandProducer.AppendLine(" add point (1, 1)");
                    commandProducer.AppendLine(" add point (0, 0)");
                    commandProducer.AppendLine("end polygon");
                },
                "Incorrect add polygon command did not throw an exception");
        }

        [Test]
        public void AppendLine_IncorrectAddPointCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate
                {
                    commandProducer.AppendLine("add polygon TEST");
                    commandProducer.AppendLine(" add point (-1, 1)");
                    commandProducer.AppendLine(" add point 123");
                    commandProducer.AppendLine(" add point (0, 0)");
                    commandProducer.AppendLine("end polygon");
                },
                "Incorrect add point command did not throw an exception");
        }

        [Test]
        public void AppendLine_IncorrectEndPolygonCommand_ShouldThrowAnException()
        {
            var commandProducer = new CommandProducer();

            Assert.Throws<BadFormatException>(
                delegate
                {
                    commandProducer.AppendLine("add polygon TEST");
                    commandProducer.AppendLine(" add point (-1, 1)");
                    commandProducer.AppendLine(" add point (1, 1)");
                    commandProducer.AppendLine(" add point (0, 0)");
                    commandProducer.AppendLine("end polygon 123");
                },
                "Incorrect end polygon command did not throw an exception");
        }
    }
}
