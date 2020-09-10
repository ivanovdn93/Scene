namespace Scene2d.Commands
{

    public class CopyCommand : ICommand
    {
        private readonly string _originalName;

        private readonly string _copyName;

        public CopyCommand(string originalName, string copyName)
        {
            _originalName = originalName;
            _copyName = copyName;
        }

        public void Apply(Scene scene)
        {
            scene.Copy(_originalName, _copyName);
        }

        public string FriendlyResultMessage => _originalName + " has been copied to figure " + _copyName;
    }
}