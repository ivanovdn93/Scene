namespace Scene2d.Figures
{
    using System;
    using System.Drawing;
    using System.Linq;

    public class PolygonFigure : IFigure
    {
        private readonly ScenePoint[] _points;

        public PolygonFigure(ScenePoint[] points)
        {
            _points = points;
        }

        public object Clone()
        {
            return new PolygonFigure(_points.ToArray());
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            if (_points.Length == 0)
            {
                return new SceneRectangle();
            }

            return new SceneRectangle
            {
                Vertex1 = new ScenePoint(_points.Min(p => p.X), _points.Min(p => p.Y)),
                Vertex2 = new ScenePoint(_points.Max(p => p.X), _points.Max(p => p.Y))
            };
        }

        public void Move(ScenePoint vector)
        {
            for (var i = 0; i < _points.Length; i++)
            {
                _points[i].X += vector.X;
                _points[i].Y += vector.Y;
            }
        }

        public void Rotate(double angle)
        {
            var p1 = CalculateCircumscribingRectangle().Vertex1;
            var p2 = CalculateCircumscribingRectangle().Vertex2;

            var center = new ScenePoint((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);

            for (var i = 0; i < _points.Length; i++)
            {
                var p = _points[i];

                _points[i].X = center.X + (p.X - center.X) * Math.Cos(angle * Math.PI / 180) - (p.Y - center.Y) * Math.Sin(angle * Math.PI / 180);
                _points[i].Y = center.Y + (p.X - center.X) * Math.Sin(angle * Math.PI / 180) + (p.Y - center.Y) * Math.Cos(angle * Math.PI / 180);
            }
        }

        public void Reflect(ReflectOrientation orientation)
        {
            var p1 = CalculateCircumscribingRectangle().Vertex1;
            var p2 = CalculateCircumscribingRectangle().Vertex2;

            var center = new ScenePoint((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);

            for (var i = 0; i < _points.Length; i++)
            {
                if (orientation == ReflectOrientation.Horizontal)
                {
                    _points[i].X = (_points[i].X < center.X) ? _points[i].X + (center.X - _points[i].X) * 2 : 
                        _points[i].X - (_points[i].X - center.X) * 2;
                }
                else
                {
                    _points[i].Y = (_points[i].Y < center.Y) ? _points[i].Y + (center.Y - _points[i].Y) * 2 :
                        _points[i].Y - (_points[i].Y - center.Y) * 2;
                }
            }
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            using (var pen = new Pen(Color.DarkOrchid))
            {
                for (var i = 0; i < _points.Length; i++)
                {
                    ScenePoint firstPoint = _points[i];
                    ScenePoint secondPoint = i >= _points.Length - 1 ? _points.First() : _points[i + 1];

                    drawing.DrawLine(
                        pen,
                        (float)(firstPoint.X - origin.X),
                        (float)(firstPoint.Y - origin.Y),
                        (float)(secondPoint.X - origin.X),
                        (float)(secondPoint.Y - origin.Y));
                }
            }
        }
    }
}