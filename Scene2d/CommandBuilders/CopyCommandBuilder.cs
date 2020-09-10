namespace Scene2d.CommandBuilders
{
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    public class CopyCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex =
            new Regex("^copy [-_ёЁА-Яа-яA-Za-z0-9]+ to [-_ёЁА-Яа-яA-Za-z0-9]+$");

        private string _originalName;

        private string _copyName;

        public bool IsCommandReady => true;

        public void AppendLine(string line)
        {
            var match = RecognizeRegex.Match(line);

            if (match.Success)
            {
                var commandParts = line.Split(' ');

                _originalName = commandParts[1];

                _copyName = commandParts[3];
            }
            else
                throw new BadFormatException();
        }

        public ICommand GetCommand() => new CopyCommand(_originalName, _copyName);
    }
}