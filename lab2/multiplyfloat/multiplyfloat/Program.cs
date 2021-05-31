using System;

namespace multiplyfloat
{
    class Program
    {
        static void Main(string[] args)
        {
            float x = 0;
            float y = 0;
            string xs = "";
            string ys = "";
            Console.Write("Введите первый множитель: ");
            xs = Console.ReadLine();
            float.TryParse(xs, out x);
            Console.Write("Введите второй множитель: ");
            ys = Console.ReadLine();
            float.TryParse(ys, out y);
            MultiplicateF(x, y);
            Console.ReadKey();
        }
        public static void PrintB(long value, int b)
        {
            string p = Convert.ToString(value, 2);
            int pl = p.Length;
            if (p.Length < b)
            {
                for (int i = 0; i < b - pl; i++)
                {
                    if (i < b - pl) p = "0" + p;
                }
            }
            for (int i = 0; i < b; i++)
            {
                if ((i + 1) % 4 == 0)
                {
                    Console.Write(p[i]);
                    Console.Write(" ");
                }
                else Console.Write(p[i]);
                if (i == b) Console.WriteLine("");
            }
            Console.WriteLine();
        }

        public static int ConvertFloat(float value)
        {
            byte[] bitsn = BitConverter.GetBytes(value);
            int floatint = 0;
            floatint += bitsn[0];
            floatint += bitsn[1] << 8;
            floatint += bitsn[2] << 16;
            floatint += bitsn[3] << 24;
            return floatint;
        }

        public static void MultiplicateF(float x, float y)
        {
            int xbits = ConvertFloat(x);
            int ybits = ConvertFloat(y);
            int xysign = (xbits >> 31) ^ (ybits >> 31);
            Console.WriteLine();
            Console.WriteLine("Знак произведения:");
            Console.WriteLine((xbits >> 31) + " XOR " + (ybits >> 31) + " = " + xysign);
            long xmantissa = xbits & ((int)Math.Pow(2, 23) - 1);
            Console.WriteLine();
            Console.WriteLine("Мантисса первого числа: ");
            PrintB(xmantissa, 24);
            long ymantissa = ybits & ((int)Math.Pow(2, 23) - 1);
            Console.WriteLine("Мантисса второго числа: ");
            PrintB(ymantissa, 24);
            long xymantissa = ((1 << 23) | xmantissa) * ((1 << 23) | ymantissa);
            Console.WriteLine("Мантисса произведения : ");
            PrintB(xymantissa, 48);
            xymantissa >>= 23;
            int normalizer = 0;
            if (((1 << 24) & xymantissa) > 0)
            {
                normalizer = 1;
                xymantissa >>= 1;
                xymantissa &= ~(1 << 23);
            }
            else
            {
                xymantissa &= ~(3 << 23);
            }
            Console.WriteLine("Нормализованая мантисса: ");
            PrintB(xymantissa, 23);
            int xexponent = (int)((xbits >> 23) & 255);
            int yexponent = (int)((ybits >> 23) & 255);
            int xyexponent = xexponent + yexponent - 127 + normalizer;
            Console.WriteLine();
            Console.WriteLine("Экспонента первого числа:: ");
            PrintB(xexponent, 8);
            Console.WriteLine("В десятичном виде: " + xexponent);
            Console.WriteLine("Экспонента второго числа : ");
            PrintB(yexponent, 8);
            Console.WriteLine("В десятичном виде: " + yexponent);
            Console.WriteLine("Экспонента произведения: " + xexponent + " + " + yexponent + " - 127 + " + normalizer + " = " + xyexponent);
            Console.WriteLine("В двоичном виде: ");
            PrintB(xyexponent, 8);
            Console.WriteLine();
            int xybits = (int)xymantissa;
            xybits |= xyexponent << 23;
            xybits |= xysign << 31;
            byte[] floatbits = new byte[4];
            floatbits[0] = (byte)(xybits & 255);
            floatbits[1] = (byte)((xybits >> 8) & 255);
            floatbits[2] = (byte)((xybits >> 16) & 255);
            floatbits[3] = (byte)((xybits >> 24) & 255);
            float xyfloatresult = BitConverter.ToSingle(floatbits, 0);
            Console.WriteLine(+x +" * " +y +" = " + xyfloatresult);
            Console.WriteLine("В двоичном виде:");
            PrintB(xybits, 32);
        }

    }
}
