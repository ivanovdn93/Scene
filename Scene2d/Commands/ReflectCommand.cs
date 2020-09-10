namespace Scene2d.Commands
{

    public class ReflectCommand : ICommand
    {
        private readonly string _name;

        private readonly ReflectOrientation _orientation;

        public ReflectCommand(string name, ReflectOrientation orientation)
        {
            _name = name;
            _orientation = orientation;
        }

        public void Apply(Scene scene)
        {
            scene.Reflect(_name, _orientation);
        }

        public string FriendlyResultMessage => _name + " has been reflected " + _orientation + "ly";
    }
}