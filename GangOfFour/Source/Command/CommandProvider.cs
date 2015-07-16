using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Command
{
    internal class CommandProvider<T>
        where T : ICommand
    {
        private readonly Stack<T> _stack = new Stack<T>();

        public void PerformCommand(T command)
        {
            command.Do();
            _stack.Push(command);
        }

        public void UndoCommand()
        {
            _stack.Pop().Undo();
        }
    }
}
