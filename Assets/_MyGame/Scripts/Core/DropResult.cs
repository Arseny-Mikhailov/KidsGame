namespace _MyGame.Scripts.Services.Cube.Strategies
{
    public class DropResult
    {
        public bool Success { get; }
        public string Message { get; }

        public DropResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}