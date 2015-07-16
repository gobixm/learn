using System;
using System.Text;

namespace Infotecs.GangOfFour.Interpreter
{
    internal interface IExpression
    {
        int Evaluate();
        int Interpret(StringBuilder context, int startIndex);
    }
}
