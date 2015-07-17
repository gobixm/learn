using System;

namespace Infotecs.GangOfFour.TemplateMethod
{
    internal class RoundBracketRenderer : StringRenderer
    {
        protected override string GetEndSequence()
        {
            return ")))";
        }

        protected override string GetStartSequence()
        {
            return "(((";
        }
    }
}
