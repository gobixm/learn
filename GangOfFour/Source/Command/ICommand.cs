using System;

namespace Infotecs.GangOfFour.Command
{
    internal interface ICommand
    {
        void Do();
        void Undo();
    }
}
