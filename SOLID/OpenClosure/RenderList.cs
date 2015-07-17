using System.Collections.Generic;

namespace Infotecs.SOLID.OpenClosure
{
    /// <summary>
    ///     Closure class
    /// </summary>
    internal sealed class RenderList
    {
        private readonly List<IDrawable> _drawings;

        public RenderList()
        {
            _drawings = new List<IDrawable>();
        }

        public void AddDrawing(IDrawable drawable)
        {
            _drawings.Add(drawable);
        }

        public void Render()
        {
            int i = 0;
            foreach (IDrawable drawable in _drawings)
            {
                drawable.Draw(i++*32);
            }
        }
    }
}