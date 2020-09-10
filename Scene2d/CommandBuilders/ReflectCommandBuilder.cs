namespace Scene2d.CommandBuilders
{
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    public class ReflectCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex =
            new Regex("^reflect (vertically|horizontally) [-_ёЁА-Яа-яA-Za-z0-9]+$");

        private ReflectOrientation _orientation;

        private string _name;

        public bool IsCommandReady => true;

        public void AppendLine(string line)
        {
            var match = RecognizeRegex.Match(line);

            if (match.Success)
            {
                var commandParts = line.Split(' ');

                _name = commandParts[2];

                _orientation = commandParts[1] == "vertically" ? ReflectOrientation.Vertical : ReflectOrientation.Horizontal;
            }
            else
                throw new BadFormatException();
        }

        public ICommand GetCommand() => new ReflectCommand(_name, _orientation);
    }
}