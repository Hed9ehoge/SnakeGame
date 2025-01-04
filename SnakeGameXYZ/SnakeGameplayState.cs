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
        public SnakeDir currentDir { get; private set; } = SnakeDir.Right;
        public const float FPS = 0.75f;

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
        public void SetDirection(Cell dir)
        {
            currentDir = DirToCell.GetValueOrDefault(dir);
        }
        public Cell ShiftTo()
        {
            var result = SnakeBody.First();
            foreach (var item in DirToCell)
            {
                if (item.Value == currentDir) result= result.Sum(item.Key);
            }
            //return new Cell(result.X, -result.Y);
            return result;
        }
        public override void Reset()
        {
            SnakeBody.Clear();
            var middleY = fieldHeight/2;
            var middleX = fieldWidth/2;
            currentDir = SnakeDir.Right;
            SnakeBody.Add(new Cell(middleX, middleY));
            timeToMove = 0;
        }

        public override void Update(float deltaTime)
        {
            timeToMove -= deltaTime;
            if (timeToMove > 0) return;

            timeToMove = FPS;
            var head = SnakeBody.First();
            var nextCell = ShiftTo();
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
         foreach(var cell in SnakeBody)
            {
                renderer.SetPixel(cell.X, cell.Y, snakeIcon, 0);
            }   
        }
    }
    
}
