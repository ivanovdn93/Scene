namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class AddPolygonCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex1 =
            new Regex("^add polygon [-_ёЁА-Яа-яA-Za-z0-9]+$");

        private static readonly Regex RecognizeRegex2 =
            new Regex("^ add point \\(-?[0-9]+\\, -?[0-9]+\\)$");

        private static readonly Regex RecognizeRegex3 =
            new Regex("^end polygon$");

        private IFigure _polygon;

        private string _name;

        private readonly List<ScenePoint> _points = new List<ScenePoint>();

        public bool IsCommandReady { get; private set; }

        public void AppendLine(string line)
        {
            var commandParts = line.Split(' ');

            if (RecognizeRegex1.IsMatch(line))
            {
                _name = commandParts[2];

                IsCommandReady = false;
            }
            else if (RecognizeRegex2.IsMatch(line))
            {
                var x = int.Parse(commandParts[3].Trim('(', ','));
                var y = int.Parse(commandParts[4].Trim(')'));

                if (_points.Any(point => x == (int)point.X && y == (int)point.Y))
                {
                    throw new BadPolygonPointException();
                }

                if (_points.Count > 1)
                {
                    for (var i = 1; i < _points.Count; i++)
                    {
                        if (!Intersect(_points[i - 1], _points[i], _points[_points.Count - 1], new ScenePoint(x, y)))
                            throw new BadPolygonPointException();
                    }
                }

                _points.Add(new ScenePoint(x, y));
            }
            else if (RecognizeRegex3.IsMatch(line))
            {
                if (_points.Count < 3)
                    throw new BadPolygonPointNumberException();

                _polygon = new PolygonFigure(_points.ToArray());

                IsCommandReady = true;
            }
            else
                throw new BadFormatException();
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _polygon);

        private const double Eps = 1E-9;

        private static double Det(double a, double b, double c, double d)
        {
            return a * d - b * c;
        }

        private static bool Between(double a, double b, double c)
        {
            return Math.Min(a, b) <= c + Eps && c <= Math.Max(a, b) + Eps;
        }

        private static bool Intersect1(double a, double b, double c, double d)
        {
            if (a > b)
            {
                var x = a;
                a = b;
                b = x;
            }
            if (c > d)
            {
                var x = c;
                c = d;
                d = x;
            }
            return Math.Max(a, c) <= Math.Min(b, d);
        }

        private static bool Intersect(ScenePoint a, ScenePoint b, ScenePoint c, ScenePoint d)
        {
            double a1 = a.Y - b.Y, b1 = b.X - a.X, c1 = -a1 * a.X - b1 * a.Y;
            double a2 = c.Y - d.Y, b2 = d.X - c.X, c2 = -a2 * c.X - b2 * c.Y;
            var zn = Det(a1, b1, a2, b2);

            if (Math.Abs(zn) >= Eps)
            {
                var x = -Det(c1, b1, c2, b2) / zn;
                var y = -Det(a1, c1, a2, c2) / zn;
                return Between(a.X, b.X, x) && Between(a.Y, b.Y, y)
                                            && Between(c.X, d.X, x) && Between(c.Y, d.Y, y);
            }
            
            return Math.Abs(Det(a1, c1, a2, c2)) < Eps && Math.Abs(Det(b1, c1, b2, c2)) < Eps
                                                && Intersect1(a.X, b.X, c.X, d.X)
                                                && Intersect1(a.Y, b.Y, c.Y, d.Y);
        }
    }
}