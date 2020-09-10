namespace Scene2d.Tests.FiguresTests
{
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Scene2d.Figures;

    [TestFixture]
    public class RectangleFigureTests
    {
        [Test]
        public void Clone_ShouldReturn_SameFigure()
        {
            var rectangleFigure = new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1));

            var rectangleFigureClone = rectangleFigure.Clone();

            var jsonRectangleFigure = JsonConvert.SerializeObject(rectangleFigure);
            var jsonRectangleFigureClone = JsonConvert.SerializeObject(rectangleFigureClone);

            Assert.That(
                jsonRectangleFigureClone,
                Is.EqualTo(jsonRectangleFigure),
                "Clone and original RectangleFigure are not same");
        }

        [Test]
        public void CalculateCircumscribingRectangle_ShouldReturn_ItsOppositeVertices()
        {
            var rectangleFigure = new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1));

            var circumscribingRectangle = rectangleFigure.CalculateCircumscribingRectangle();

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
            var rectangleFigure = new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1));

            rectangleFigure.Move(new ScenePoint(1, 1));

            var movedRectangleFigure = new RectangleFigure(new ScenePoint(1, 1), new ScenePoint(2, 2));

            var jsonRectangleFigure = JsonConvert.SerializeObject(rectangleFigure);
            var jsonMovedRectangleFigure = JsonConvert.SerializeObject(movedRectangleFigure);

            Assert.That(
                jsonMovedRectangleFigure,
                Is.EqualTo(jsonRectangleFigure),
                "RectangleFigure was moved incorrectly");
        }

        [Test]
        public void Rotate_ShouldRotateAllPoints_ByGivenAngle()
        {
            var rectangleFigure = new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1));

            rectangleFigure.Rotate(90);

            var rotatedRectangleFigure = new RectangleFigure(new ScenePoint(0, 1), new ScenePoint(1, 0));

            var jsonRectangleFigure = JsonConvert.SerializeObject(rectangleFigure);
            var jsonRotatedRectangleFigure = JsonConvert.SerializeObject(rotatedRectangleFigure);

            Assert.That(
                jsonRotatedRectangleFigure,
                Is.EqualTo(jsonRectangleFigure),
                "RectangleFigure was rotated incorrectly");
        }

        [Test]
        public void Reflect_ShouldReflectAllPoints_ByGivenOrientation()
        {
            var rectangleFigure = new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1));

            rectangleFigure.Reflect(ReflectOrientation.Vertical);

            var reflectedRectangleFigure = new RectangleFigure(new ScenePoint(0, 1), new ScenePoint(1, 0));

            var jsonRectangleFigure = JsonConvert.SerializeObject(rectangleFigure);
            var jsonReflectedRectangleFigure = JsonConvert.SerializeObject(reflectedRectangleFigure);

            Assert.That(
                jsonReflectedRectangleFigure,
                Is.EqualTo(jsonRectangleFigure),
                "RectangleFigure was reflected incorrectly");
        }
    }
}
