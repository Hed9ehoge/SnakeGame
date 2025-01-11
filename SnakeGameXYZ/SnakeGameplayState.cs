using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using SnakeGame.Struct;

namespace SnakeGame
{
    public class SnakeGameplayState : BaseGameState
    {
        public List<Cell> SnakeBody = new List<Cell>();
        private float timeToMove;
        public int fieldWidth;
        public int fieldHeight;
        const char snakeIcon = '■';
        const char circleSymbol = '@';
        Cell apple = new();
        Random random = new();
        int countOfApple => SnakeBody.Count()-1;
        const int countOfAppleForWin = 3;
        int countOfAppleForWinAtCurrentLevel => countOfAppleForWin + level;
        public bool gameOver;
        public bool hasWon;
        public int level;
        Cell SnakeHead => SnakeBody.First();
        public SnakeDir currentDir { get; private set; } = SnakeDir.Right;

        private Dictionary<Cell, SnakeDir> DirToCell = new()
        {
            {new Cell(0,1), SnakeDir.Down},
            {new Cell(0,-1), SnakeDir.Up},
            {new Cell(-1,0), SnakeDir.Left},
            {new Cell(1,0), SnakeDir.Right}
        };
        public enum SnakeDir
        {
            Left, 
            Up,
            Right,
            Down
        }
        private void GenerateApple()
        {
            Random random = new Random();
            Cell cell = new(random.Next(fieldWidth), random.Next(fieldHeight));
            if (cell.Equals(SnakeHead)) 
            {
                var middleY = fieldHeight / 2;
                if (cell.Y > middleY)
                    cell.Y -= 1;
                else 
                    cell.Y += 1;
            }
            apple = cell;
        }
        public void SetDirection(Cell dir)
        {
            var newDir = DirToCell.GetValueOrDefault(dir);
            var ItIsOppositeWay = false;
            foreach (var item in DirToCell)
            {
                if (item.Value == currentDir&& new Cell(item.Key.X * -1, item.Key.Y * -1).Equals(dir))
                    ItIsOppositeWay = true;
            }

            if (ItIsOppositeWay) return;
            currentDir = newDir;
        }
        public Cell ShiftTo()
        {
            var result = SnakeHead;
            foreach (var item in DirToCell)
            {
                if (item.Value == currentDir) result= result.Sum(item.Key);
            }
            return result;
        }
        public override void Reset()
        {
            SnakeBody.Clear();
            gameOver = false;
            hasWon = false;
            var middleY = fieldHeight/2;
            var middleX = fieldWidth/2;
            currentDir = SnakeDir.Right;
            apple = new(middleX, middleY);
            SnakeBody.Add(new Cell(middleX, middleY));
            timeToMove = 0;
        }

        public override void Update(float deltaTime)
        {
            timeToMove -= deltaTime;
            if (timeToMove > 0 || gameOver) return;
            timeToMove = 1f/ 4f;
            if (level != 0) timeToMove/=level;
            var head = SnakeHead;
            var nextCell = ShiftTo();
            if (nextCell.Equals(apple)) 
            {
                SnakeBody.Insert(0, apple);
                if(countOfApple >= countOfAppleForWinAtCurrentLevel) hasWon= true;
                GenerateApple();
                return;
            }
            if (nextCell.X<0|| nextCell.Y < 0|| nextCell.Y> fieldHeight|| nextCell.X > fieldWidth)
            {
                gameOver = true;
                return;
            }
            SnakeBody = AddHeadToBody(nextCell);
        }

        private List<Cell> AddHeadToBody(Cell nextCell)
        {
            var NewSnakeBody = new List<Cell>() { nextCell };
            SnakeBody.Remove(SnakeBody.Last());
            NewSnakeBody.AddRange(SnakeBody);
            return NewSnakeBody;
        }

        public override void Draw(ConsoleRenderer renderer)
        {
            renderer.DrawString($"Score: {countOfApple} apples",3,2,ConsoleColor.DarkMagenta);
            renderer.DrawString($"Goal is {countOfAppleForWinAtCurrentLevel} apples",3,3,ConsoleColor.White);
            renderer.DrawString($"Level {level}",3,4,ConsoleColor.Cyan);
            renderer.SetPixel(apple.X, apple.Y, circleSymbol, 2);
            foreach (var cell in SnakeBody)
            {
                renderer.SetPixel(cell.X, cell.Y, snakeIcon, 0);
            }
        }

        public override bool IsDone()
        {
            return gameOver|| hasWon;
        }
    }
    
}
