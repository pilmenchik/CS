using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Base64true
{
    class Program
    {
        public static int FillDict(string text, Dictionary<char, double> symbols) 
        {
            int textLength = text.Length;
            for (int i = 0; i < textLength; i++)
            {
                if (symbols.ContainsKey(text[i]))
                {
                    symbols[text[i]]++;
                }
                else
                {
                    symbols.Add(text[i], 1);
                }
            }
            return textLength;
        }
        public static void SymbolProbability(Dictionary<char, double> symbols, int SymbolsQuantity)
        {
            int ElementQuantity = symbols.Keys.Count;
            char[] keysDict = new char[ElementQuantity];                 
            symbols.Keys.CopyTo(keysDict, 0);
            for (int iter = 0; iter < ElementQuantity; iter++)
            {
                symbols[keysDict[iter]] /= SymbolsQuantity;
            }
        }
        public static double Entropy(Dictionary<char, double> symbols)
        {
            int ElementQuantity = symbols.Keys.Count;
            char[] keysDict = new char[ElementQuantity]; 
            symbols.Keys.CopyTo(keysDict, 0);
            double x = 0;
            double entropy = 0;
            for (int i = 0; i < ElementQuantity; i++)
            {
                x = symbols[keysDict[i]];
                entropy -= x * Math.Log(x, 2);
            }
            return entropy;
        }
        public static string ToBinaryString(string text)
        {
            Encoding encoding = Encoding.UTF8;
            return string.Join("", encoding.GetBytes(text).Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
        }
        public static string Base64Encode(string text)
        {

            int RewritedB = 0;
            string SixBit = "";


            string base64 = "";
            char[] Table = new char[64]
               {   'A','B','C','D','E','F','G','H','I','J','K','L','M',
                'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                'a','b','c','d','e','f','g','h','i','j','k','l','m',
                'n','o','p','q','r','s','t','u','v','w','x','y','z',
                '0','1','2','3','4','5','6','7','8','9','+','/'
               };
            string BitText = ToBinaryString(text);
            int textLength = BitText.Length;


            int count = 0;
            while (RewritedB < textLength)
            {
                if (textLength - RewritedB < 6)
                {
                    for (int ic = 0; ic < RewritedB + 6 - textLength; ic++)
                    {
                        BitText += "0";
                    }
                }

                int i = RewritedB;
                while (i < RewritedB + 6)
                {
                    SixBit += Convert.ToString(BitText[i]);
                    i++;
                }

                int number = Convert.ToInt32(SixBit, 2);
                SixBit = "";
                base64 += Table[number];
                RewritedB = RewritedB + 6;
                count++;
            }
            for (int i = 0; i < (textLength % 3); i++)
            {
                base64 += "=";
            }
            return base64;
        }
        static void Main(string[] args)
        {
            string textFile;
            int SymbolsQuantity, SymbolsQuantityb;
            double entropy, entropyb;
            string path = @"D:\учеба\cs\textfiles\";
            Dictionary<char, double> SymbolsDict = new Dictionary<char, double>();
            Dictionary<char, double> SymbolsDictb = new Dictionary<char, double>();
            try
            {
                Console.Write("Введите название файла: ");
                textFile = Console.ReadLine();
                Console.WriteLine("_____________________________");
                path += textFile;
                string text = File.ReadAllText(path);
                string final = Base64Encode(text);
                SymbolsQuantity = FillDict(text, SymbolsDict);
                SymbolProbability(SymbolsDict, SymbolsQuantity);
                entropy = Entropy(SymbolsDict);
                Console.WriteLine("Колличество информации:{0} bytes", SymbolsQuantity * entropy / 8);
                FileInfo fileb = new FileInfo(final);
                SymbolsQuantityb = FillDict(final, SymbolsDictb);
                SymbolProbability(SymbolsDictb, SymbolsQuantityb);
                entropyb = Entropy(SymbolsDictb);
                Console.WriteLine("Колличество информации в закодированом файле:{0} bytes", SymbolsQuantityb * entropyb / 8);
                Console.WriteLine("_________________________________________________________________________________________");
                Console.WriteLine(final);
            }
            catch (FileNotFoundException fnfexc)
            {
                Console.WriteLine(fnfexc.Message);
            }
            catch (IOException ioexc)
            {
                Console.WriteLine(ioexc.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }
    }
}
