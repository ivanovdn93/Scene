namespace Scene2d.Tests
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    [TestFixture]
    public class SceneTests
    {
        [Test]
        public void AddFigure_ExistingName_ShouldThrowAnException()
        {
            var scene = new Scene();
            
            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));

            Assert.Throws<ExistNameException>(
                delegate { scene.AddFigure("circle", 
                    new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1))); },
                "Adding figure with existing name did not throw an exception");
        }
        
        [Test]
        public void AddFigure_SceneName_ShouldThrowAnException()
        {
            var scene = new Scene();

            Assert.Throws<BadNameException>(
                delegate { scene.AddFigure("scene", 
                    new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1))); },
                "Adding figure with name 'scene' did not throw an exception");
        }

        [Test]
        public void CalculateSceneCircumscribingRectangle_NoFigures_ShouldReturnEmptySceneRectangle()
        {
            var scene = new Scene();

            var sceneCircumscribingRectangle = scene.CalculateSceneCircumscribingRectangle();
            var emptyRectangle = new SceneRectangle();

            var jsonSceneCircumscribingRectangle = JsonConvert.SerializeObject(sceneCircumscribingRectangle);
            var jsonEmptyRectangle = JsonConvert.SerializeObject(emptyRectangle);

            Assert.That(
                jsonEmptyRectangle,
                Is.EqualTo(jsonSceneCircumscribingRectangle),
                "Method does not return empty rectangle when there is no figures in the scene");
        }

        [Test]
        public void CalculateSceneCircumscribingRectangle_ShouldReturn_ItsOppositeVertices()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));
            scene.AddFigure("rectangle", new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(2, 2)));

            var sceneCircumscribingRectangle = scene.CalculateSceneCircumscribingRectangle();

            Assert.That(
                sceneCircumscribingRectangle.Vertex1.X,
                Is.EqualTo(-1).Within(0.00001),
                "Vertex1's x is not correct");

            Assert.That(
                sceneCircumscribingRectangle.Vertex1.Y,
                Is.EqualTo(-1).Within(0.00001),
                "Vertex1's y is not correct");

            Assert.That(
                sceneCircumscribingRectangle.Vertex2.X,
                Is.EqualTo(2).Within(0.00001),
                "Vertex2's x is not correct");

            Assert.That(
                sceneCircumscribingRectangle.Vertex2.Y,
                Is.EqualTo(2).Within(0.00001),
                "Vertex2's y is not correct");
        }

        [Test]
        public void CreateCompositeFigure_ExistingName_ShouldThrowAnException()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));
            scene.AddFigure("rectangle", new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1)));

            Assert.Throws<ExistNameException>(
                delegate { scene.CreateCompositeFigure("circle", new List<string> { "circle", "rectangle" }); },
                "Creating composite figure with existing name did not throw an exception");
        }

        [Test]
        public void CreateCompositeFigure_SceneName_ShouldThrowAnException()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));
            scene.AddFigure("rectangle", new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1)));

            Assert.Throws<BadNameException>(
                delegate { scene.CreateCompositeFigure("scene", new List<string> { "circle", "rectangle" }); },
                "Creating composite figure with name 'scene' did not throw an exception");
        }

        [Test]
        public void CreateCompositeFigure_IncorrectFigureNames_ShouldThrowAnException()
        {
            var scene = new Scene();

            Assert.Throws<BadNameException>(
                delegate { scene.CreateCompositeFigure("compositeFigure", 
                    new List<string> { "circle", "rectangle" }); },
                "Creating composite figure with incorrect figure names did not throw an exception");
        }

        [Test]
        public void CalculateCircumscribingRectangle_IncorrectName_ShouldThrowAnException()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));

            Assert.Throws<BadNameException>(
                delegate { scene.CalculateCircumscribingRectangle("rectangle"); },
                "Calculating circumscribing rectangle for figure with incorrect name did not throw an exception");
        }

        [Test]
        public void Move_IncorrectName_ShouldThrowAnException()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));

            Assert.Throws<BadNameException>(
                delegate { scene.Move("rectangle", new ScenePoint(1, 1)); },
                "Moving figure with incorrect name did not throw an exception");
        }

        [Test]
        public void Rotate_IncorrectName_ShouldThrowAnException()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));

            Assert.Throws<BadNameException>(
                delegate { scene.Rotate("rectangle", 90); },
                "Rotating figure with incorrect name did not throw an exception");
        }

        [Test]
        public void ListDrawableFigures_ShouldReturn_DrawableFigures()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));
            scene.AddFigure("rectangle", new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1)));

            var drawableFigures = new List<IFigure>
            {
                new CircleFigure(new ScenePoint(0, 0), 1),
                new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1))
            };

            var jsonListDrawableFigures = JsonConvert.SerializeObject(scene.ListDrawableFigures());
            var jsonDrawableFigures = JsonConvert.SerializeObject(drawableFigures);

            Assert.That(
                jsonDrawableFigures,
                Is.EqualTo(jsonListDrawableFigures),
                "ListDrawableFigures is incorrect");
        }

        [Test]
        public void Copy_ExistingName_ShouldThrowAnException()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));
            scene.AddFigure("circle2", new CircleFigure(new ScenePoint(0, 0), 1));

            Assert.Throws<ExistNameException>(
                delegate { scene.Copy("circle", "circle2"); },
                "Copying figure with existing name did not throw an exception");
        }

        [Test]
        public void Copy_SceneName_ShouldThrowAnException()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));

            Assert.Throws<BadNameException>(
                delegate { scene.Copy("circle", "scene"); },
                "Copying figure into figure with name 'scene' did not throw an exception");
        }

        [Test]
        public void Copy_IncorrectName_ShouldThrowAnException()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));

            Assert.Throws<BadNameException>(
                delegate { scene.Copy("rectangle", "rectangle2"); },
                "Copying figure with incorrect name did not throw an exception");
        }

        [Test]
        public void DeleteScene_ShouldDo_It()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));
            scene.AddFigure("rectangle", new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1)));
            scene.CreateCompositeFigure("compositeFigure", new List<string>{ "circle", "rectangle" });

            var emptyScene = new Scene();
            scene.DeleteScene();

            var jsonScene = JsonConvert.SerializeObject(scene);
            var jsonEmptyScene = JsonConvert.SerializeObject(emptyScene);
            
            Assert.That(
                jsonEmptyScene,
                Is.EqualTo(jsonScene),
                "Scene was deleted is incorrectly");
        }

        [Test]
        public void Delete_IncorrectName_ShouldThrowAnException()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));

            Assert.Throws<BadNameException>(
                delegate { scene.Delete("rectangle"); },
                "Deleting figure with incorrect name did not throw an exception");
        }

        [Test]
        public void Reflect_IncorrectName_ShouldThrowAnException()
        {
            var scene = new Scene();

            scene.AddFigure("circle", new CircleFigure(new ScenePoint(0, 0), 1));

            Assert.Throws<BadNameException>(
                delegate { scene.Reflect("rectangle", ReflectOrientation.Horizontal); },
                "Reflecting figure with incorrect name did not throw an exception");
        }
    }
}
