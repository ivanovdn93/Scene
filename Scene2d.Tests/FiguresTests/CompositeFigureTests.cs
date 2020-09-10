namespace Scene2d.Tests.FiguresTests
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using NUnit.Framework;
    using Scene2d.Figures;

    [TestFixture]
    public class CompositeFigureTests
    {
        [Test]
        public void Clone_ShouldReturn_SameCompositeFigure()
        {
            var compositeFigure = new CompositeFigure(
                new List<IFigure>()
                {
                    new CircleFigure(new ScenePoint(0, 0), 1),
                    new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(1, 1))
                });

            var compositeFigureClone = compositeFigure.Clone();

            var jsonCompositeFigure = JsonConvert.SerializeObject(compositeFigure);
            var jsonCompositeFigureClone = JsonConvert.SerializeObject(compositeFigureClone);

            Assert.That(
                jsonCompositeFigureClone,
                Is.EqualTo(jsonCompositeFigure),
                "Clone and original CompositeFigure are not same");

        }

        [Test]
        public void CalculateCircumscribingRectangle_ShouldReturn_ItsOppositeVertices()
        {
            var compositeFigure = new CompositeFigure(
                new List<IFigure>()
                {
                    new CircleFigure(new ScenePoint(0, 0), 1),
                    new RectangleFigure(new ScenePoint(0, 0), new ScenePoint(2, 2))
                });

            var circumscribingRectangle = compositeFigure.CalculateCircumscribingRectangle();

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
                Is.EqualTo(2).Within(0.00001),
                "Vertex2's x is not correct");

            Assert.That(
                circumscribingRectangle.Vertex2.Y,
                Is.EqualTo(2).Within(0.00001),
                "Vertex2's y is not correct");
        }
    }
}
