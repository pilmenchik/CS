using System;
using System.Collections.Generic;
using System.IO;
namespace Lab11
{
    class Program
    {
        public static int FillDict(string path, Dictionary<char, double> symbols) 
        {
            string text = File.ReadAllText(path);
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
        public static void Print(Dictionary<char, double> symbols, int SymbolsQuantity, double entropy, long fileSize)
        {
            Console.WriteLine("Размер файла:{0} bytes", fileSize);
            Console.WriteLine("Колличество информации:{0} bytes", SymbolsQuantity * entropy / 8);
            Console.WriteLine("Энтропия:{0}", entropy);
            Console.WriteLine("_____________________________");
            SortedDictionary<char, double> sorted = new SortedDictionary<char, double>(symbols);
            Console.WriteLine("Вероятность появления символа:");
            foreach (KeyValuePair<char, double> k in sorted)
            {
                if (k.Key == '\r') Console.WriteLine("/r" + " - " + k.Value + "%");
                else if (k.Key == '\n') Console.WriteLine("/n" + " - " + k.Value + "%");
                else Console.WriteLine(k.Key + " - " + k.Value + "%");

            }
        }
        static void Main(string[] args)
        {
            string path = @"D:\учеба\cs\textfiles\";
            string textFile;
            int SymbolsQuantity;
            double entropy;
            Dictionary<char, double> SymbolsDict = new Dictionary<char, double>();
            try
            {
                Console.Write("Введите название файла: ");
                textFile = Console.ReadLine();
                Console.WriteLine("_____________________________");
                path += textFile + ".txt";
                FileInfo file = new FileInfo(path);
                long FSize = file.Length;
                SymbolsQuantity = FillDict(path, SymbolsDict);
                SymbolProbability(SymbolsDict, SymbolsQuantity);
                entropy = Entropy(SymbolsDict);
                Print(SymbolsDict, SymbolsQuantity, entropy, FSize);
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