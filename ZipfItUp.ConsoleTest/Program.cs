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
            Console.WriteLine(TextExtractor.GetTimeDiffernace(DateTime.Parse("4/12/2017 6:32:16 PM")));
        }

       
    }
}
