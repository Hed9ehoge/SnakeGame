using System.Collections.ObjectModel;

namespace SnakeGame.Interfaces
{
    public interface IArrowListener
    {
        public abstract void OnArrowLeft();
        public abstract void OnArrowUp();
        public abstract void OnArrowRight();
        public abstract void OnArrowDown();

    }
}