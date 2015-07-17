using System;

namespace Infotecs.GangOfFour.Adapter
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
