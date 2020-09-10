namespace Scene2d.CommandBuilders
{
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    public class MoveCommandBuilder : ICommandBuilder
    {
        private static readonly Regex RecognizeRegex =
            new Regex("^move [-_ёЁА-Яа-яA-Za-z0-9]+ \\(-?[0-9]+\\, -?[0-9]+\\)$");

        private ScenePoint _vector;

        private string _name;

        public bool IsCommandReady => true;

        public void AppendLine(string line)
        {
            var match = RecognizeRegex.Match(line);

            if (match.Success)
            {
                var commandParts = line.Split(' ');

                _name = commandParts[1];

                var x = int.Parse(commandParts[2].Trim('(', ','));
                var y = int.Parse(commandParts[3].Trim(')'));

                _vector = new ScenePoint(x, y);
            }
            else
                throw new BadFormatException();
        }

        public ICommand GetCommand() => new MoveCommand(_name, _vector);
    }
}