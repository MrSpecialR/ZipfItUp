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
            //TextManipulator.GetWords.WordList();
           // string path = "../../../InputFiles/80day10.txt";

            string path = "../../../InputFiles/80day10.txt";
            var wordlist = GetWords.WordList(path);
            List<DocumentWord> words = ModelHelper.Initialize.TransformWords(wordlist.ToList());
            
            foreach (KeyValuePair<string, long> wordInfo in wordlist)
            {
                Console.WriteLine($"{wordInfo.Key} - {wordInfo.Value}");
            }
            OccuranceContext context = new OccuranceContext();
            UserInput input = new UserInput()
            {
                DateUploaded = DateTime.Now,
                Document = new Document()
                {
                    FileName = "80day10.txt",
                    FilePath = path,
                    WordCount = wordlist.Sum(x=>x.Value),
                    DocumentType = DocumentType.Text,
                    MostUsedWord = words[0].Word,
                    Words = words
                }
            };

            context.UserInputs.Add(input);
            context.SaveChanges();
        }
    }
}
