using System;
using System.Threading.Tasks;

namespace Publisher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            EmitLoop();
            
        }

        private static void EmitLoop()
        {
            using (var emitter = new Emitter(Guid.NewGuid().ToString()))
            {
                Parallel.For(0,
                    100000000,
                    i => emitter.Emit($"message {i}"));
            }
        }
    }
}
