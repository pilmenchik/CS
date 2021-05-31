using System;

namespace division
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите первое число:");
            int x = int.Parse(Console.ReadLine());
            Console.Write("Введите второе число:");
            int y = int.Parse(Console.ReadLine());
            Division(x, y);
        }
        private static void Division(int dd, int dr)
        {
            long RemaiderQuotient = dd;
            long divisor = dr;
            int c = 1;
            bool ch = false;
            divisor <<= 32;
            for (int i = 0; i < 33; i++)
            {
                if (divisor <= RemaiderQuotient)
                {
                    RemaiderQuotient -= divisor;
                    ch = true;
                }
                RemaiderQuotient <<= 1;
                if (ch)
                {
                    ch = false;
                    RemaiderQuotient |= 1;
                }
                c++;
                Console.Write($"{c}\n Divisor: ");
                PrintB64(divisor);
                Console.WriteLine();
                Console.Write("Remainder + quotient: ");
                PrintB64(RemaiderQuotient);
                Console.WriteLine();
            }
            long m = 4294967295;                                               //11111111111111111111111111111111
            Console.WriteLine(Convert.ToString(m,2));
            long quotient = RemaiderQuotient & (m);
            long remainder = RemaiderQuotient >> 33;


            Console.Write($" Quotient: ");
            PrintB32(quotient);
            Console.Write($" Remainder: " );
            PrintB32(remainder);
            Console.WriteLine("частное="+quotient +"\nостача="+ remainder);
        }
        public static void PrintB32(long n)
        {
            string p = Convert.ToString(n, 2);
            int pl = p.Length;
            if (p.Length < 32)
            {
                for (int i = 0; i < 32 - pl; i++)
                {
                    p = "0" + p;
                }
            }
            for (int i = 0; i < 32; i++)
            {
                if ((i + 1) % 4 == 0)
                {
                    Console.Write(p[i]);
                    Console.Write(" ");
                }
                else Console.Write(p[i]);
                if (i == 63) Console.WriteLine("");
            }
            Console.WriteLine();
        }
        public static void PrintB64(long n)
        {
            string p = Convert.ToString(n, 2);
            int pl = p.Length;
            if (p.Length < 63)
            {
                for (int i = 0; i < 64 - pl; i++)
                {
                    if (i < 64 - pl) p = "0" + p;
                }
            }
            for (int i = 0; i < 64; i++)
            {
                if ((i + 1) % 4 == 0)
                {
                    Console.Write(p[i]);
                    Console.Write(" ");
                }
                else Console.Write(p[i]);
            }
        }
    }
}
