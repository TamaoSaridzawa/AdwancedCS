using System;

namespace CleanCode_Task1
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        public static int GetValidNumber(int a, int b, int c)
        {
            if (a < b)
                return b;
            else if (a > c)
                return c;
            else
                return a;
        }
    }
}
