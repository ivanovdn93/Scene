namespace Scene2d.CommandBuilders
{
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class AddRectangleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex = 
            new Regex("^add rectangle [-_ёЁА-Яа-яA-Za-z0-9]+ \\(-?[0-9]+\\, -?[0-9]+\\) \\(-?[0-9]+\\, -?[0-9]+\\)$");

        private IFigure _rectangle;

        private string _name;

        public bool IsCommandReady => true;

        public void AppendLine(string line)
        {
            var match = RecognizeRegex.Match(line);

            if (match.Success) 
            {                
                var commandParts = line.Split(' ');

                _name = commandParts[2];

                var x1 = int.Parse(commandParts[3].Trim('(', ','));
                var y1 = int.Parse(commandParts[4].Trim(')'));
                var x2 = int.Parse(commandParts[5].Trim('(', ','));
                var y2 = int.Parse(commandParts[6].Trim(')'));

                if (x1 == x2 || y1 == y2)
                    throw new BadRectanglePointException();

                _rectangle = new RectangleFigure(new ScenePoint(x1, y1), new ScenePoint(x2, y2));
            }
            else
                throw new BadFormatException();
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _rectangle);
    }
}