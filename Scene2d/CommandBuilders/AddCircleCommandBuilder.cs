namespace Scene2d.CommandBuilders
{
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;
    using Scene2d.Figures;

    public class AddCircleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex =
            new Regex("^add circle [-_ёЁА-Яа-яA-Za-z0-9]+ \\(-?[0-9]+\\, -?[0-9]+\\) radius -?[0-9]+$");

        private IFigure _circle;

        private string _name;

        public bool IsCommandReady => true;

        public void AppendLine(string line)
        {
            var match = RecognizeRegex.Match(line);

            if (match.Success)
            {
                var commandParts = line.Split(' ');

                _name = commandParts[2];

                var x = int.Parse(commandParts[3].Trim('(', ','));
                var y = int.Parse(commandParts[4].Trim(')'));
                var radius = int.Parse(commandParts[6]);

                if (radius <= 0)
                    throw new BadCircleRadiusException();

                _circle = new CircleFigure(new ScenePoint(x, y), radius);
            }
            else
                throw new BadFormatException();
        }

        public ICommand GetCommand() => new AddFigureCommand(_name, _circle);
    }
}