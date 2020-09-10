namespace Scene2d.Commands
{

    public class PrintCircumscribingRectangleCommand : ICommand
    {
        private readonly string _name;

        private SceneRectangle _circumscribingRectangle;

        public PrintCircumscribingRectangleCommand(string name)
        {
            _name = name;
        }

        public void Apply(Scene scene)
        {
            scene.CalculateCircumscribingRectangle(_name);

            _circumscribingRectangle = scene.CalculateCircumscribingRectangle(_name);
        }

        public string FriendlyResultMessage =>
            "(" + _circumscribingRectangle.Vertex1.X + ", " + _circumscribingRectangle.Vertex1.Y + ") ("
            + _circumscribingRectangle.Vertex2.X + ", " + _circumscribingRectangle.Vertex2.Y + ")";
    }
}