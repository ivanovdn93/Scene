namespace Scene2d.Tests.FiguresTests
{
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Scene2d.Figures;

    [TestFixture]
    public class PolygonFigureTests
    {
        [Test]
        public void Clone_ShouldReturn_SameFigure()
        {
            var polygonFigure = new PolygonFigure(new[]
            {
                new ScenePoint(0, 0),
                new ScenePoint(0, 1),
                new ScenePoint(1, 1) 
            });

            var polygonFigureClone = polygonFigure.Clone();

            var jsonPolygonFigure = JsonConvert.SerializeObject(polygonFigure);
            var jsonPolygonFigureClone = JsonConvert.SerializeObject(polygonFigureClone);

            Assert.That(
                jsonPolygonFigureClone,
                Is.EqualTo(jsonPolygonFigure),
                "Clone and original PolygonFigure are not same");
        }

        [Test]
        public void CalculateCircumscribingRectangle_ShouldReturn_ItsOppositeVertices()
        {
            var polygonFigure = new PolygonFigure(new[]
            {
                new ScenePoint(0, 0),
                new ScenePoint(0, 1),
                new ScenePoint(1, 1)
            });

            var circumscribingRectangle = polygonFigure.CalculateCircumscribingRectangle();

            Assert.That(
                circumscribingRectangle.Vertex1.X,
                Is.EqualTo(0).Within(0.00001),
                "Vertex1's x is not correct");

            Assert.That(
                circumscribingRectangle.Vertex1.Y,
                Is.EqualTo(0).Within(0.00001),
                "Vertex1's y is not correct");

            Assert.That(
                circumscribingRectangle.Vertex2.X,
                Is.EqualTo(1).Within(0.00001),
                "Vertex2's x is not correct");

            Assert.That(
                circumscribingRectangle.Vertex2.Y,
                Is.EqualTo(1).Within(0.00001),
                "Vertex2's y is not correct");
        }

        [Test]
        public void Move_ShouldMoveAllPoints_ByGivenVector()
        {
            var polygonFigure = new PolygonFigure(new[]
            {
                new ScenePoint(0, 0),
                new ScenePoint(0, 1),
                new ScenePoint(1, 1)
            });

            polygonFigure.Move(new ScenePoint(1, 1));

            var movedPolygonFigure = new PolygonFigure(new[]
            {
                new ScenePoint(1, 1),
                new ScenePoint(1, 2),
                new ScenePoint(2, 2)
            });

            var jsonPolygonFigure = JsonConvert.SerializeObject(polygonFigure);
            var jsonMovedPolygonFigure = JsonConvert.SerializeObject(movedPolygonFigure);

            Assert.That(
                jsonMovedPolygonFigure,
                Is.EqualTo(jsonPolygonFigure),
                "PolygonFigure was moved incorrectly");
        }

        [Test]
        public void Rotate_ShouldRotateAllPoints_ByGivenAngle()
        {
            var polygonFigure = new PolygonFigure(new[]
            {
                new ScenePoint(0, 0),
                new ScenePoint(0, 1),
                new ScenePoint(1, 1)
            });

            polygonFigure.Rotate(90);

            var rotatedPolygonFigure = new PolygonFigure(new[]
            {
                new ScenePoint(0, 0),
                new ScenePoint(1, 0),
                new ScenePoint(1, -1)
            });

            var jsonPolygonFigure = JsonConvert.SerializeObject(polygonFigure);
            var jsonRotatedPolygonFigure = JsonConvert.SerializeObject(rotatedPolygonFigure);

            Assert.That(
                jsonRotatedPolygonFigure,
                Is.EqualTo(jsonPolygonFigure),
                "PolygonFigure was rotated incorrectly");
        }

        [Test]
        public void Reflect_ShouldReflectAllPoints_ByGivenOrientation()
        {
            var polygonFigure = new PolygonFigure(new[]
            {
                new ScenePoint(0, 0),
                new ScenePoint(0, 1),
                new ScenePoint(1, 1)
            });

            polygonFigure.Reflect(ReflectOrientation.Vertical);

            var reflectedPolygonFigure = new PolygonFigure(new[]
            {
                new ScenePoint(0, 1),
                new ScenePoint(0, 0),
                new ScenePoint(1, 0)
            });

            var jsonPolygonFigure = JsonConvert.SerializeObject(polygonFigure);
            var jsonReflectedPolygonFigure = JsonConvert.SerializeObject(reflectedPolygonFigure);

            Assert.That(
                jsonReflectedPolygonFigure,
                Is.EqualTo(jsonPolygonFigure),
                "PolygonFigure was reflected incorrectly");
        }
    }
}
