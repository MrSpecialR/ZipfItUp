using ZipfItUp.TextManipulator;

namespace ZipfItUp.ConsoleTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Program
    {
        static void Main(string[] args)
        {
            //TextManipulator.GetWords.WordList();
           // string path = "../../../InputFiles/80day10.txt";

            string path = "../../../InputFiles/rtf.rtf";
            var wordlist = GetWords.WordList(path);
            foreach (KeyValuePair<string, long> wordInfo in wordlist)
            {
                Console.WriteLine($"{wordInfo.Key} - {wordInfo.Value}");
            }
            Console.WriteLine();
        }
    }
}
