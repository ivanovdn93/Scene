namespace Scene2d.Commands
{
    using System.Collections.Generic;

    public class GroupCommand : ICommand
    {
        private readonly string _name;

        private readonly IList<string> _childFigures;

        public GroupCommand(string name, IList<string> childFigures)
        {
            _name = name;
            _childFigures = childFigures;
        }

        public void Apply(Scene scene)
        {
            scene.CreateCompositeFigure(_name, _childFigures);
        }

        public string FriendlyResultMessage => "Figures " + string.Join(", ", _childFigures) + " grouped as " + _name;
    }
}