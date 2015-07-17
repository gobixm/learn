using System;
using System.Collections.Generic;

namespace Infotecs.GangOfFour.Interpreter
{
    internal sealed class Operations
    {
        private static readonly Operations _instance = new Operations();

        static Operations()
        {
        }

        private Operations()
        {
            OperationDictionary = new Dictionary<string, Func<int, int, int>> { { "+", (leftOperand, rightOperand) => leftOperand + rightOperand } };
        }

        public static Operations Instance
        {
            get { return _instance; }
        }

        public Dictionary<string, Func<int, int, int>> OperationDictionary { get; private set; }
    }
}
