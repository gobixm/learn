using System;

namespace redis_test
{
    public class MatrixGenerator
    {
        public void Generate(int side, Action<int, byte[]> row)
        {
            for (int i = 0; i < side; i++)
            {
                var buff = new byte[0xffff / 8];
                buff[i % 8] = 1;
                row(i, buff);
            }
        }
    }
}