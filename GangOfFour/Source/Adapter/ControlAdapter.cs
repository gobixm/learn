using System;

namespace Infotecs.GangOfFour.Adapter
{
    internal class ControlAdapter : ILibraryControl, IFancyControl
    {
        private bool _visible;

        public Visibility Visibility
        {
            get
            {
                if (Visible)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Collapsed;
                }
            }
            set
            {
                if (value == Visibility.Visible)
                {
                    _visible = true;
                }
                else
                {
                    _visible = false;
                }
            }
        }

        public bool Visible
        {
            get { return _visible; }
            set { _visible = value; }
        }
    }
}
