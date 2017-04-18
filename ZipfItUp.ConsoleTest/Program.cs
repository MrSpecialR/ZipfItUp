using ZipfItUp.Data;
using ZipfItUp.Models;
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
            // OccuranceContext context = new OccuranceContext();
            // //context.DocumentWords.Count();
            //TextManipulator.GetWords.WordList();
            // string path = "../../../InputFiles/80day10.txt";
            /*using (OccuranceContext context = new OccuranceContext())
            {
                DocumentWord word = Query.Document.GetMostUsedWord(context.Documents.First(), context);
                Console.WriteLine($"{word.Word.WordString} - {word.Occurances}");
            }*/
            UploadFileToDatabase("../../../InputFiles/alcott-little-261.txt");
        }

       
    }
}
