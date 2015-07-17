using System;
using System.Text;

namespace Infotecs.GangOfFour.Interpreter
{
    internal sealed class BracketExpression : IExpression
    {
        private IExpression Operand1 { get; set; }
        private IExpression Operand2 { get; set; }
        private string Operation { get; set; }

        public int Evaluate()
        {
            return Operations.Instance.OperationDictionary[Operation](Operand1.Evaluate(), Operand2.Evaluate());
        }

        public int Interpret(StringBuilder context, int startIndex)
        {
            if (context[startIndex + 1] == '(')
            {
                Operand1 = new BracketExpression();
            }
            else
            {
                Operand1 = new VariableExpression();
            }
            int i = Operand1.Interpret(context, startIndex + 1);
            var operationBuilder = new StringBuilder();
            while ((context[i] < '0' || context[i] > '9')
                && context[i] != ')'
                && context[i] != '(')
            {
                operationBuilder.Append(context[i]);
                i++;
            }
            int operationEnd = i;
            Operation = operationBuilder.ToString();

            if (context[operationEnd] == '(')
            {
                Operand2 = new BracketExpression();
            }
            else
            {
                Operand2 = new VariableExpression();
            }
            return Operand2.Interpret(context, operationEnd) + 1;
        }
    }
}
