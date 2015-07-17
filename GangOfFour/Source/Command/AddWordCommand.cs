using System;
using System.Text;

namespace Infotecs.GangOfFour.Command
{
    internal sealed class AddWordCommand : ICommand
    {
        private readonly StringBuilder _builder;
        private readonly string _word;

        public AddWordCommand(StringBuilder builder, string theWord)
        {
            _builder = builder;
            _word = theWord;
        }

        public void Do()
        {
            _builder.Append(_word);
            Console.WriteLine("Command {0} executed", GetType().Name);
        }

        public void Undo()
        {
            _builder.Remove(_builder.Length - _word.Length, _word.Length);
            Console.WriteLine("Command {0} undone", GetType().Name);
        }
    }
}
