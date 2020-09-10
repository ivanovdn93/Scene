namespace Scene2d.CommandBuilders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Scene2d.Commands;
    using Scene2d.Exceptions;

    public class CommandProducer : ICommandBuilder
    {
        private static readonly Dictionary<Regex, Func<ICommandBuilder>> Commands =
            new Dictionary<Regex, Func<ICommandBuilder>>
            {
                { new Regex("^add rectangle .*"), () => new AddRectangleCommandBuilder() },
                { new Regex("^add circle .*"), () => new AddCircleCommandBuilder() },
                { new Regex("^add polygon .*"), () => new AddPolygonCommandBuilder() },
                { new Regex("^move .*"), () => new MoveCommandBuilder() },
                { new Regex("^rotate .*"), () => new RotateCommandBuilder() },
                { new Regex("^reflect .*"), () => new ReflectCommandBuilder() },
                { new Regex("^group .*"), () => new GroupCommandBuilder() },
                { new Regex("^copy .*"), () => new CopyCommandBuilder() },
                { new Regex("^delete .*"), () => new DeleteCommandBuilder() },
                { new Regex("^print circumscribing rectangle for .*"), () => new PrintCircumscribingRectangleCommandBuilder() }
            };

        public ICommandBuilder CurrentBuilder { get; private set; }

        public bool IsCommandReady
        {
            get
            {
                if (CurrentBuilder == null)
                {
                    return false;
                }

                return CurrentBuilder.IsCommandReady;
            }
        }

        public void AppendLine(string line)
        {
            if (CurrentBuilder is AddPolygonCommandBuilder)
            {
                if (Commands.Keys.Any(key => key.IsMatch(line)))
                {
                    throw new UnexpectedEndOfPolygonException();
                }
            }

            if (CurrentBuilder == null)
            {
                foreach (var pair in Commands.Where(pair => pair.Key.IsMatch(line)))
                {
                    CurrentBuilder = pair.Value();
                    break;
                }

                if (CurrentBuilder == null)
                {
                    throw new BadFormatException();
                }
            }
            
            CurrentBuilder.AppendLine(line);
        }

        public ICommand GetCommand()
        {
            if (CurrentBuilder == null)
            {
                return null;
            }

            var command = CurrentBuilder.GetCommand();
            CurrentBuilder = null;

            return command;
        }
    }
}
