namespace Scene2d.Figures
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    public class CompositeFigure : ICompositeFigure
    {
        public IList<IFigure> ChildFigures { get; }

        public CompositeFigure(IList<IFigure> childFigures)
        {
            ChildFigures = childFigures;
        }

        public object Clone()
        {
            var cloneList = ChildFigures
                .Select(figure => (IFigure) figure.Clone())
                .ToList();

            return new CompositeFigure(cloneList);
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            var allPoints = ChildFigures
                .Select(f => f.CalculateCircumscribingRectangle())
                .SelectMany(a => new[] { a.Vertex1, a.Vertex2 })
                .ToList();
            
            return new SceneRectangle
            {
                Vertex1 = new ScenePoint(allPoints.Min(p => p.X), allPoints.Min(p => p.Y)),
                Vertex2 = new ScenePoint(allPoints.Max(p => p.X), allPoints.Max(p => p.Y))
            };
        }

        public void Move(ScenePoint vector)
        {
            foreach (var figure in ChildFigures)
            {
                figure.Move(vector);
            }
        }

        public void Rotate(double angle)
        {
            foreach (var figure in ChildFigures)
            {
                figure.Rotate(angle);
            }
        }

        public void Reflect(ReflectOrientation orientation)
        {
            foreach (var figure in ChildFigures)
            {
                figure.Reflect(orientation);
            }
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
        }
    }
}
