namespace Scene2d.CommandBuilders
{
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    public class RotateCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex =
            new Regex("^rotate [-_ёЁА-Яа-яA-Za-z0-9]+ -?[0-9]+$");

        private double _angle;

        private string _name;

        public bool IsCommandReady => true;

        public void AppendLine(string line)
        {
            var match = RecognizeRegex.Match(line);

            if (match.Success)
            {
                var commandParts = line.Split(' ');

                _name = commandParts[1];

                _angle = double.Parse(commandParts[2]);
            }
            else
                throw new BadFormatException();
        }

        public ICommand GetCommand() => new RotateCommand(_name, _angle);
    }
}