namespace Scene2d.Figures
{
    using System;
    using System.Drawing;

    public class RectangleFigure : IFigure
    {
        private ScenePoint _p1;
        private ScenePoint _p2;
        private ScenePoint _p3;
        private ScenePoint _p4;

        public RectangleFigure(ScenePoint p1, ScenePoint p2)
        {
            _p1 = p1;
            _p2 = new ScenePoint { X = p2.X, Y = p1.Y };
            _p3 = p2;
            _p4 = new ScenePoint { X = p1.X, Y = p2.Y };
        }

        public object Clone()
        {
            return new RectangleFigure(_p1, _p3) { _p2 = _p2, _p4 = _p4 };
        }

        public SceneRectangle CalculateCircumscribingRectangle()
        {
            return new SceneRectangle
            {
                Vertex1 = new ScenePoint(Math.Min(Math.Min(_p1.X, _p2.X), Math.Min(_p3.X, _p4.X)), 
                                            Math.Min(Math.Min(_p1.Y, _p2.Y), Math.Min(_p3.Y, _p4.Y))),
                Vertex2 = new ScenePoint(Math.Max(Math.Max(_p1.X, _p2.X), Math.Max(_p3.X, _p4.X)),
                                            Math.Max(Math.Max(_p1.Y, _p2.Y), Math.Max(_p3.Y, _p4.Y)))
            };
        }

        public void Move(ScenePoint vector)
        {
            _p1.X += vector.X;
            _p1.Y += vector.Y;
            _p2.X += vector.X;
            _p2.Y += vector.Y;
            _p3.X += vector.X;
            _p3.Y += vector.Y;
            _p4.X += vector.X;
            _p4.Y += vector.Y;
        }

        public void Rotate(double angle)
        {
            var center = new ScenePoint((_p1.X + _p3.X)/2, (_p1.Y + _p3.Y) / 2);

            var p1 = _p1;
            var p2 = _p2;
            var p3 = _p3;
            var p4 = _p4;

            _p1.X = center.X + (p1.X - center.X) * Math.Cos(angle * Math.PI / 180) - (p1.Y - center.Y) * Math.Sin(angle * Math.PI / 180);
            _p1.Y = center.Y + (p1.X - center.X) * Math.Sin(angle * Math.PI / 180) + (p1.Y - center.Y) * Math.Cos(angle * Math.PI / 180);
            _p2.X = center.X + (p2.X - center.X) * Math.Cos(angle * Math.PI / 180) - (p2.Y - center.Y) * Math.Sin(angle * Math.PI / 180);
            _p2.Y = center.Y + (p2.X - center.X) * Math.Sin(angle * Math.PI / 180) + (p2.Y - center.Y) * Math.Cos(angle * Math.PI / 180);
            _p3.X = center.X + (p3.X - center.X) * Math.Cos(angle * Math.PI / 180) - (p3.Y - center.Y) * Math.Sin(angle * Math.PI / 180);
            _p3.Y = center.Y + (p3.X - center.X) * Math.Sin(angle * Math.PI / 180) + (p3.Y - center.Y) * Math.Cos(angle * Math.PI / 180);
            _p4.X = center.X + (p4.X - center.X) * Math.Cos(angle * Math.PI / 180) - (p4.Y - center.Y) * Math.Sin(angle * Math.PI / 180);
            _p4.Y = center.Y + (p4.X - center.X) * Math.Sin(angle * Math.PI / 180) + (p4.Y - center.Y) * Math.Cos(angle * Math.PI / 180);
        }

        public void Reflect(ReflectOrientation orientation)
        {
            var center = new ScenePoint((_p1.X + _p3.X) / 2, (_p1.Y + _p3.Y) / 2);

            if (orientation == ReflectOrientation.Horizontal)
            {
                _p1.X = (_p1.X < center.X) ? _p1.X + (center.X - _p1.X) * 2 : _p1.X - (_p1.X - center.X) * 2;
                _p2.X = (_p2.X < center.X) ? _p2.X + (center.X - _p2.X) * 2 : _p2.X - (_p2.X - center.X) * 2;
                _p3.X = (_p3.X < center.X) ? _p3.X + (center.X - _p3.X) * 2 : _p3.X - (_p3.X - center.X) * 2;
                _p4.X = (_p4.X < center.X) ? _p4.X + (center.X - _p4.X) * 2 : _p4.X - (_p4.X - center.X) * 2;
            }
            else 
            {
                _p1.Y = (_p1.Y < center.Y) ? _p1.Y + (center.Y - _p1.Y) * 2 : _p1.Y - (_p1.Y - center.Y) * 2;
                _p2.Y = (_p2.Y < center.Y) ? _p2.Y + (center.Y - _p2.Y) * 2 : _p2.Y - (_p2.Y - center.Y) * 2;
                _p3.Y = (_p3.Y < center.Y) ? _p3.Y + (center.Y - _p3.Y) * 2 : _p3.Y - (_p3.Y - center.Y) * 2;
                _p4.Y = (_p4.Y < center.Y) ? _p4.Y + (center.Y - _p4.Y) * 2 : _p4.Y - (_p4.Y - center.Y) * 2;
            }
        }

        public void Draw(ScenePoint origin, Graphics drawing)
        {
            using (var pen = new Pen(Color.Blue))
            {
                drawing.DrawLine(
                    pen, 
                    (float) (_p1.X - origin.X), 
                    (float) (_p1.Y - origin.Y),
                    (float) (_p2.X - origin.X),
                    (float) (_p2.Y - origin.Y));

                drawing.DrawLine(
                    pen, 
                    (float) (_p2.X - origin.X), 
                    (float) (_p2.Y - origin.Y),
                    (float) (_p3.X - origin.X),
                    (float) (_p3.Y - origin.Y));

                drawing.DrawLine(
                    pen, 
                    (float) (_p3.X - origin.X), 
                    (float) (_p3.Y - origin.Y),
                    (float) (_p4.X - origin.X),
                    (float) (_p4.Y - origin.Y));

                drawing.DrawLine(
                    pen, 
                    (float) (_p4.X - origin.X), 
                    (float) (_p4.Y - origin.Y),
                    (float) (_p1.X - origin.X),
                    (float) (_p1.Y - origin.Y));
            }
        }
    }
}
