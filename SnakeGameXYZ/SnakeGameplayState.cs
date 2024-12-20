using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Struct;

namespace SnakeGame
{
    public class SnakeGameplayState : BaseGameState
    {
        public List<Cell> SnakeBody = new List<Cell>();
        private float timeToMove;
        public SnakeDir currentDir { get; private set; } = SnakeDir.Right;

        public const float FPS = 1/5;

        private Dictionary<Cell, SnakeDir> DirToCell = new()
        {
            {new Cell(0,1), SnakeDir.Right},
            {new Cell(0,-1), SnakeDir.Left},
            {new Cell(1,0), SnakeDir.Up},
            {new Cell(-1,0), SnakeDir.Down}
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
            return result;
        }
        public override void Reset()
        {
            SnakeBody.Clear();
            currentDir = SnakeDir.Right;
            SnakeBody.Add(new Cell(0,0));
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
            Console.WriteLine($"XY:{SnakeBody.First().X},{SnakeBody.First().Y}");
        }

        private List<Cell> AddHeadToBody(Cell nextCell)
        {
            var NewSnakeBody = new List<Cell>() { nextCell };
            SnakeBody.Remove(SnakeBody.Last());
            NewSnakeBody.AddRange(SnakeBody);
            return NewSnakeBody;
        }
    }
    
}
