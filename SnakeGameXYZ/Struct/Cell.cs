using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Struct
{
    public struct Cell
    {
        public int X;
        public int Y;
        public bool Equals(Cell cell)
        {
            if (cell.X == X && cell.Y == Y)
                return true;
            return false;
        }
        public Cell Sum(Cell cell)
        {
            return new Cell(cell.X + X, cell.Y + Y);
        }
        public static Cell Sum(Cell cell1, Cell cell2)
        {
            return new Cell(cell1.X + cell2.X, cell1.Y + cell2.Y);
        }
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
