using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.Interfaces;

namespace SnakeGame
{
    public class ConsoleInput 
    { 
        //
        List<IArrowListener> arrowListeners = new List<IArrowListener>();
        Dictionary<Dir,List<ConsoleKey>> DirectionAndButton = new() 
        {
            {Dir.Left,new List<ConsoleKey>{ConsoleKey.A,ConsoleKey.LeftArrow} },
            {Dir.Up,new List<ConsoleKey>{ConsoleKey.W,ConsoleKey.UpArrow} },
            {Dir.Right,new List<ConsoleKey>{ConsoleKey.D,ConsoleKey.RightArrow} },
            {Dir.Down,new List<ConsoleKey>{ConsoleKey.S,ConsoleKey.DownArrow} },
        };
        enum Dir
        {
            Left,
            Up,
            Right,
            Down
        }

        public void Subscribe(IArrowListener listener) 
        {
            arrowListeners.Add(listener);
        }
        public void Update()
        {
            if (!Console.KeyAvailable)
                return;
            var key = StealthReadKey();
            

            
            Dir direction = KeyDirection(key);
            switch (direction)
            {
                case Dir.Left:
                    foreach (var item in arrowListeners)
                        item.OnArrowLeft();
                    break;
                case Dir.Up:
                    foreach (var item in arrowListeners)
                        item.OnArrowUp();
                    break;
                case Dir.Right:
                    foreach (var item in arrowListeners)
                        item.OnArrowRight();
                    break;
                case Dir.Down:
                    foreach (var item in arrowListeners)
                        item.OnArrowDown();
                    break;
            }
        }

        private ConsoleKeyInfo StealthReadKey()
        {
            var curretCursorDir = Console.GetCursorPosition();
            Console.SetCursorPosition(0, 0);
            var key = Console.ReadKey();
            Console.SetCursorPosition(0, 0);
            Console.Write(' ');
            Console.SetCursorPosition(curretCursorDir.Left, curretCursorDir.Top);
            return key;
        }

        private Dir KeyDirection(ConsoleKeyInfo key)
        {
            foreach (var dir in DirectionAndButton)
            {
                foreach (var item1 in dir.Value)
                {
                    if (item1.ToString() == key.Key.ToString())
                    {
                        return dir.Key;
                    }
                }
            }
            return Dir.Right;
        }
    }
}
