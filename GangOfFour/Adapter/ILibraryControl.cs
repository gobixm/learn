using System;

namespace Infotecs.GanfOfFour.Adapter
{
    internal enum Visibility
    {
        None = 0,
        Visible,
        Hidden,
        Collapsed
    }

    internal interface ILibraryControl
    {
        Visibility Visibility { get; set; }
    }
}
