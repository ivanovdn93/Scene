namespace Scene2d.CommandBuilders
{
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    public class PrintCircumscribingRectangleCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex =
            new Regex("^print circumscribing rectangle for [-_ёЁА-Яа-яA-Za-z0-9]+$");

        private string _name;

        public bool IsCommandReady => true;

        public void AppendLine(string line)
        {
            var match = RecognizeRegex.Match(line);

            if (match.Success)
            {
                var commandParts = line.Split(' ');

                _name = commandParts[4];
            }
            else
                throw new BadFormatException();
        }

        public ICommand GetCommand() => new PrintCircumscribingRectangleCommand(_name);
    }
}