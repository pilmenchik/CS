using System;

namespace multiplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите первое число:");
            int x = int.Parse(Console.ReadLine());
            Console.Write("Введите второе число:");
            int y = int.Parse(Console.ReadLine());
            Multiply(x,y);
        }
        public static void Multiply(int multiplicand, int multiplier)
        {
            int c = 0;
            long product = 0;
            for (int i = 0; i < 32; i++)
            {
                c++;
                Console.Write($" Product:  ");
                PrintB64(product);
                Console.Write($"{c}\n Multiplicand: ");
                PrintB32(multiplicand);
                Console.Write($" Multiplier: ");
                PrintB32(multiplier);
                if ((multiplier & 1) == 1)
                {
                    product += multiplicand;
                }
                multiplicand <<= 1;
                Console.Write($" Multiplicand Shift left: ");
                PrintB32(multiplicand);
                multiplier >>= 1;
                Console.Write($" Multiplier Shift right: ");
                PrintB32(multiplier);
            }
            Console.WriteLine($"произведение: " + product);
            PrintB64(product);
        }
        public static void PrintB32(int n)
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
                if (i == 63) Console.WriteLine("");
            }
            Console.WriteLine();
        }

    }
}
