using System;
using System.Text;

namespace Infotecs.GangOfFour.Interpreter
{
    internal class VariableExpression : IExpression
    {
        public int Variable { get; private set; }

        public int Evaluate()
        {
            return Variable;
        }

        public int Interpret(StringBuilder context, int startIndex)
        {
            var variable = new StringBuilder();
            int i = startIndex;
            while ((context[i] >= '0') && (context[i] <= '9'))
            {
                variable.Append(context[i]);
                i++;
            }
            int realVariable;
            int.TryParse(variable.ToString(), out realVariable);
            Variable = realVariable;
            return i;
        }
    }
}
