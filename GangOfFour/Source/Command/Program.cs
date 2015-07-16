using System;
using System.Text;

namespace Infotecs.GangOfFour.Command
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var stringBuilder = new StringBuilder();

            var addShortWord = new AddWordCommand(stringBuilder, "short | ");
            var addLongWord = new AddWordCommand(stringBuilder, "veryLongCamelCaseWord | ");

            var commandProvider = new CommandProvider<AddWordCommand>();
            commandProvider.PerformCommand(addShortWord);
            commandProvider.PerformCommand(addLongWord);
            Console.WriteLine("Builder now is " + stringBuilder);
            commandProvider.PerformCommand(addShortWord);
            commandProvider.PerformCommand(addLongWord);
            commandProvider.PerformCommand(addLongWord);
            Console.WriteLine("Builder now is " + stringBuilder);
            commandProvider.UndoCommand();
            commandProvider.UndoCommand();
            Console.WriteLine("Builder now is " + stringBuilder);
            Console.ReadKey();
        }
    }
}
