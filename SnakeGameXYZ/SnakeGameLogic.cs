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
        bool newGamePending = false;
        int currentLevel;
        ShowTextState showTextState = new(2);
        private void GotoNextLevel()
        {
            currentLevel++;
            newGamePending = false;
            showTextState.text = $"Level {currentLevel}";
            ChangeState(showTextState);
        }
        private void GotoGameOver()
        {
            currentLevel = 0;
            newGamePending = true;
            showTextState.text = "Game Over";
            ChangeState(showTextState);
        }
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
            if(currentState!=null&&!currentState.IsDone())return;


            if (currentState == null || currentState == gameplayState && !gameplayState.gameOver) GotoNextLevel();

            else if (currentState == gameplayState && gameplayState.gameOver) GotoGameOver();

            else if (currentState != gameplayState && newGamePending) GotoNextLevel();

            else if (currentState != gameplayState && !newGamePending) GotoGameplay();
        }
        public void GotoGameplay()
        {
            gameplayState.level = currentLevel;
            gameplayState.fieldWidth = screenWidth;
            gameplayState.fieldHeight = screenHight;
            ChangeState(gameplayState);
            gameplayState.Reset();
        }

        public override ConsoleColor[] CreatePallet()
        {
            return new ConsoleColor[] { ConsoleColor.DarkGreen, ConsoleColor.DarkRed, ConsoleColor.Yellow, ConsoleColor.DarkMagenta, ConsoleColor.White,ConsoleColor.Cyan };
        }
    }
}
