using SnakeGame.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class SnakeGameLogic : BaseGameLogic
    {
        SnakeGameplayState gameplayState = new SnakeGameplayState();
        public override void OnArrowDown()
        {
            gameplayState.SetDirection(new Cell(0,-1));
        }

        public override void OnArrowLeft()
        {
            gameplayState.SetDirection(new Cell(-1, 0));
        }

        public override void OnArrowRight()
        {
            gameplayState.SetDirection(new Cell(1, 0));

        }
        public override void OnArrowUp()
        {
            gameplayState.SetDirection(new Cell(0, 1));
        }
        public override void Update(float deltaTime)
        {
            gameplayState.Update(deltaTime);
        }
        public void GotoGameplay()
        {
            gameplayState.Reset();
        }
    }
}
