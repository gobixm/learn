using System;

namespace Infotecs.GangOfFour.TemplateMethod
{
    internal class StringRenderer
    {
        public void Render(string text)
        {
            Console.WriteLine("{0}{1}{2}", GetStartSequence(), text, GetEndSequence());
        }

        protected virtual string GetEndSequence()
        {
            return default(string);
        }

        protected virtual string GetStartSequence()
        {
            return default(string);
        }
    }
}
