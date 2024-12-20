using SnakeGame;
using System    ;


namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SnakeGameLogic gameLogic = new SnakeGameLogic();
            ConsoleInput Input = new ConsoleInput();
            Console.CursorVisible = false;
            gameLogic.InitializeInput(Input);

            float lastFrameTime = DateTime.Now.Minute* 60+DateTime.Now.Second;
            gameLogic.GotoGameplay();
            while (true) 
            {
                Input.Update();
                float frameStartTime = DateTime.Now.Minute * 60 + DateTime.Now.Second;
                float deltaTime = MathF.Abs(lastFrameTime - frameStartTime);
                gameLogic.Update(deltaTime);
                lastFrameTime = frameStartTime;
            }
        }
    }
}