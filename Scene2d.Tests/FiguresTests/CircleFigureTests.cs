namespace Scene2d.Tests.FiguresTests
{
    using Newtonsoft.Json;
    using NUnit.Framework;
    using Scene2d.Figures;

    [TestFixture]
    public class CircleFigureTests
    {
        [Test]
        public void Clone_ShouldReturn_SameFigure()
        {
            var circleFigure = new CircleFigure(new ScenePoint(0, 0), 1);
            
            var circleFigureClone = circleFigure.Clone();

            var jsonCircleFigure = JsonConvert.SerializeObject(circleFigure);
            var jsonCircleFigureClone = JsonConvert.SerializeObject(circleFigureClone);

            Assert.That(
                jsonCircleFigureClone,
                Is.EqualTo(jsonCircleFigure),
                "Clone and original CircleFigure are not same");
        }

        [Test]
        public void CalculateCircumscribingRectangle_ShouldReturn_ItsOppositeVertices()
        {
            var circleFigure = new CircleFigure(new ScenePoint(0, 0), 1);

            var circumscribingRectangle = circleFigure.CalculateCircumscribingRectangle();

            Assert.That(
                circumscribingRectangle.Vertex1.X,
                Is.EqualTo(-1).Within(0.00001),
                "Vertex1's x is not correct");

            Assert.That(
                circumscribingRectangle.Vertex1.Y,
                Is.EqualTo(-1).Within(0.00001),
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
        public void Move_ShouldMoveCenter_ByGivenVector()
        {
            var circleFigure = new CircleFigure(new ScenePoint(0, 0), 1);

            circleFigure.Move(new ScenePoint(1, 1));

            var movedCircleFigure = new CircleFigure(new ScenePoint(1, 1), 1);

            var jsonCircleFigure = JsonConvert.SerializeObject(circleFigure);
            var jsonMovedCircleFigure = JsonConvert.SerializeObject(movedCircleFigure);

            Assert.That(
                jsonMovedCircleFigure,
                Is.EqualTo(jsonCircleFigure),
                "CircleFigure was moved incorrectly");
        }
    }
}
