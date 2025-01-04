using Shared;
using SnakeGame.Struct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class SnakeGameLogic : BaseGameLogic
    {
        public float GetAllTime => time;
        SnakeGameplayState gameplayState = new SnakeGameplayState();
        public override void OnArrowDown()
        {
            if(currentState!=gameplayState) return;
            gameplayState.SetDirection(new Cell(0, 1));
        }

        public override void OnArrowLeft()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(new Cell(-1, 0));
        }

        public override void OnArrowRight()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(new Cell(1, 0));

        }
        public override void OnArrowUp()
        {
            if (currentState != gameplayState) return;
            gameplayState.SetDirection(new Cell(0, -1));
        }
        public override void Update(float deltaTime)
        {
            if(currentState!=gameplayState) GotoGameplay();
        }
        public void GotoGameplay()
        {
            gameplayState.fieldWidth = screenWidth;
            gameplayState.fieldHeight = screenHight;
            ChangeState(gameplayState);
            gameplayState.Reset();
        }

        public override ConsoleColor[] CreatePallet()
        {
            return new ConsoleColor[] { ConsoleColor.DarkGreen };
        }
    }
}
