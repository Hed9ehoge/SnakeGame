using SnakeGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public abstract class BaseGameLogic : IArrowListener
    {
        public virtual void OnArrowDown() { }
        public virtual void OnArrowLeft() { }
        public virtual void OnArrowRight() { }
        public virtual void OnArrowUp() { }
        public void InitializeInput(ConsoleInput input)
        {
            input.Subscribe(this);
        }
        public abstract void Update(float deltaTime);
    }
}
