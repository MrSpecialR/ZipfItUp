using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TikaOnDotNet.TextExtraction;

namespace ZipfItUp.TextManipulator
{
    using System.IO;
    using System.Text.RegularExpressions;
    public static class TextExtractor
    {
        public static string GetExtension(string path)
        {
            return path.Substring(path.LastIndexOf('.') + 1, path.Length - path.LastIndexOf('.') - 1);
        }

        public static string GetFileName(string path)
        {
            return path.Substring(path.LastIndexOf('/') + 1, path.Length - path.LastIndexOf('/') - 1);
        }
        public static string FromTXT(string path)
        {
            return File.ReadAllText(path);
        }

        public static string FromDOC(string path)
        {
            var TextExtractor = new TikaOnDotNet.TextExtraction.TextExtractor();
            return TextExtractor.Extract(path).Text;
        }

        public static string FromPDF(string path)
        {
            var TextExtractor = new TikaOnDotNet.TextExtraction.TextExtractor();
            return TextExtractor.Extract(path).Text;
        }
        public static string FromSRT(string path)
        {
            List<string> text = File.ReadAllLines(path).ToList();
            StringBuilder textBuilder = new StringBuilder();
            string timeStampPattern = @"(\d{2}:\d{2}:\d{2},\d{3}?.*)";

            foreach (string line in text)
            {

                if (!Regex.IsMatch(line, timeStampPattern) && !Regex.IsMatch(line, @"^\d+$"))
                {
                    textBuilder.Append(line.Trim());
                }
            }
            return textBuilder.ToString();
        }
    }
}