namespace Scene2d.Figures
{
    using System.Drawing;

    public class CircleFigure : IFigure
    {
        private ScenePoint _center;

        private readonly double _radius;

        public CircleFigure(ScenePoint center, double radius)
        {
            _center = center;
            _radius = radius;
        }

        public object Clone()
        {
            return new CircleFigure(_center, _radius);
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            return new SceneRectangle
            {
                Vertex1 = new ScenePoint(_center.X - _radius, _center.Y - _radius),
                Vertex2 = new ScenePoint(_center.X + _radius, _center.Y + _radius)
            };
        }

        public void Move(ScenePoint vector)
        {
            _center.X += vector.X;
            _center.Y += vector.Y;
        }

        public void Rotate(double angle)
        {
        }

        public void Reflect(ReflectOrientation orientation)
        {
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            using (var pen = new Pen(Color.Green))
            {
                drawing.DrawEllipse(
                    pen,
                    (int)(_center.X - _radius - origin.X),
                    (int)(_center.Y - _radius - origin.Y),
                    (int)(_radius * 2),
                    (int)(_radius * 2));
            }
        }
    }
}