namespace Scene2d.CommandBuilders
{
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    public class GroupCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex =
            new Regex("^group [-_ёЁА-Яа-яA-Za-z0-9]+(, [-_ёЁА-Яа-яA-Za-z0-9]+)* as [-_ёЁА-Яа-яA-Za-z0-9]+$");

        private readonly IList<string> _childFigures = new List<string>();

        private string _name;

        public bool IsCommandReady => true;

        public void AppendLine(string line)
        {
            var match = RecognizeRegex.Match(line);

            if (match.Success)
            {
                var commandParts = line.Split(' ');

                _name = commandParts[commandParts.Length - 1];

                for (var i = 1; i < commandParts.Length - 2; i++)
                {
                    _childFigures.Add(commandParts[i].Trim(','));
                }
            }
            else
                throw new BadFormatException();
        }

        public ICommand GetCommand() => new GroupCommand(_name, _childFigures);
    }
}