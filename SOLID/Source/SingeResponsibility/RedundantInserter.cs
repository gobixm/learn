namespace Infotecs.SOLID.SingeResponsibility
{
    internal static class RedundantInserter
    {
        public static string[] InsertYouKnow(string[] words)
        {
            var compose = new string[words.Length*2 - 1];
            for (int i = 0; i < words.Length; i++)
            {
                compose[i*2] = words[i];
                if (i < words.Length - 1)
                    compose[i*2 + 1] = ",you know,";
            }
            return compose;
        }
    }
}