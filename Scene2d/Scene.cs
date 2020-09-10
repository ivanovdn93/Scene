namespace Scene2d
{
    using System.Collections.Generic;
    using System.Linq;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class Scene
    {
        private readonly Dictionary<string, IFigure> _figures = new Dictionary<string, IFigure>();

        private readonly Dictionary<string, ICompositeFigure> _compositeFigures = new Dictionary<string, ICompositeFigure>();

        public void AddFigure(string name, IFigure figure)
        {
            if (_figures.ContainsKey(name) || _compositeFigures.ContainsKey(name))
            {
                throw new ExistNameException();
            }

            if (name == "scene")
            {
                throw new BadNameException();
            }
            
            _figures[name] = figure;
        }

        public SceneRectangle CalculateSceneCircumscribingRectangle()
        {
            var allFigures = ListDrawableFigures()
                .Select(f => f.CalculateCircumscribingRectangle())
                .SelectMany(a => new[] { a.Vertex1, a.Vertex2 })
                .ToList();

            if (allFigures.Count == 0)
            {
                return new SceneRectangle();
            }

            return new SceneRectangle
            {
                Vertex1 = new ScenePoint(allFigures.Min(p => p.X), allFigures.Min(p => p.Y)),
                Vertex2 = new ScenePoint(allFigures.Max(p => p.X), allFigures.Max(p => p.Y))
            };
        }

        public void CreateCompositeFigure(string name, IEnumerable<string> childFigures)
        {
            if (_figures.ContainsKey(name) || _compositeFigures.ContainsKey(name))
            {
                throw new ExistNameException();
            }

            if (name == "scene")
            {
                throw new BadNameException();
            }

            var childFigureValues = new List<IFigure>();

            foreach (var childFigure in childFigures)
            {
                if (_figures.ContainsKey(childFigure))
                {
                    childFigureValues.Add(_figures[childFigure]);
                }
                else
                {
                    throw new BadNameException();
                }
            }

            _compositeFigures[name] = new CompositeFigure(childFigureValues);
        }

        public SceneRectangle CalculateCircumscribingRectangle(string name)
        {
            if (name == "scene")
            {
                return CalculateSceneCircumscribingRectangle();
            }

            if (_figures.ContainsKey(name))
            {
                return _figures[name].CalculateCircumscribingRectangle();
            }

            if (_compositeFigures.ContainsKey(name))
            {
                return _compositeFigures[name].CalculateCircumscribingRectangle();
            }

            throw new BadNameException();
        }

        public void MoveScene(ScenePoint vector)
        {
            foreach (var figure in ListDrawableFigures())
            {
                figure.Move(vector);
            }
        }

        public void Move(string name, ScenePoint vector)
        {
            if (name == "scene")
            {
                MoveScene(vector);
                return;
            }

            if (_figures.ContainsKey(name))
            {
                _figures[name].Move(vector);
                return;
            }

            if (_compositeFigures.ContainsKey(name))
            {
                _compositeFigures[name].Move(vector);
                return;
            }
                        
            throw new BadNameException();
        }

        public void RotateScene(double angle)
        {
            foreach (var figure in ListDrawableFigures())
            {
                figure.Rotate(angle);
            }
        }

        public void Rotate(string name, double angle)
        {
            if (name == "scene")
            {
                RotateScene(angle);
                return;
            }

            if (_figures.ContainsKey(name))
            {
                _figures[name].Rotate(angle);
                return;
            }

            if (_compositeFigures.ContainsKey(name))
            {
                _compositeFigures[name].Rotate(angle);
                return;
            }
            
            throw new BadNameException();
        }

        public IEnumerable<IFigure> ListDrawableFigures()
        {
            return _figures
                .Values
                .Concat(_compositeFigures.SelectMany(x => x.Value.ChildFigures))
                .Distinct();
        }

        public void CopyScene(string copyName)
        {
            _compositeFigures[copyName] = new CompositeFigure(ListDrawableFigures().ToList());
        }

        public void Copy(string originalName, string copyName)
        {
            if (_figures.ContainsKey(copyName) || _compositeFigures.ContainsKey(copyName))
            {
                throw new ExistNameException();
            }

            if (copyName == "scene")
            {
                throw new BadNameException();
            }

            if (originalName == "scene")
            {
                CopyScene(copyName);
                return;
            }

            if (_figures.ContainsKey(originalName))
            {
                _figures[copyName] = (IFigure)_figures[originalName].Clone();
                return;
            }

            if (_compositeFigures.ContainsKey(originalName))
            {
                _compositeFigures[copyName] = (ICompositeFigure)_compositeFigures[originalName].Clone();
                return;
            }
            
            throw new BadNameException();
        }

        public void DeleteScene()
        {
            _figures.Clear();
            _compositeFigures.Clear();
        }

        public void Delete(string name)
        {
            if (name == "scene")
            {
                DeleteScene();
                return;
            }

            if (_figures.ContainsKey(name))
            {
                _figures.Remove(name);
                return;
            }

            if (_compositeFigures.ContainsKey(name))
            {
                _compositeFigures.Remove(name);
                return;
            }
            
            throw new BadNameException();
        }

        public void ReflectScene(ReflectOrientation reflectOrientation)
        {
            foreach (var figure in ListDrawableFigures())
            {
                figure.Reflect(reflectOrientation);
            }
        }

        public void Reflect(string name, ReflectOrientation orientation)
        {
            if (name == "scene")
            {
                ReflectScene(orientation);
                return;
            }

            if (_figures.ContainsKey(name))
            {
                _figures[name].Reflect(orientation);
                return;
            }

            if (_compositeFigures.ContainsKey(name))
            {
                _compositeFigures[name].Reflect(orientation);
                return;
            }
            
            throw new BadNameException();
        }
    }
}
