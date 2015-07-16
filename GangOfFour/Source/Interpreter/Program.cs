using System;
using System.Text;

namespace Infotecs.GangOfFour.Interpreter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var expression = new StringBuilder("(((1+1)*(2+(2*(3+3))))+14)");
            Operations.Instance.OperationDictionary.Add("*", (left, right) => left * right);
            var expressionTree = new BracketExpression();
            expressionTree.Interpret(expression, 0);
            Console.WriteLine(expression.ToString());
            Console.WriteLine("The ultimate answer for life and universe is {0}", expressionTree.Evaluate());
            Console.ReadKey();
        }
    }
}
