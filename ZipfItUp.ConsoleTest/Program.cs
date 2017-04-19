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
            Console.WriteLine(TextExtractor.GenerateFileName(".jpg", "jASDooo"));
        }

       
    }
}
