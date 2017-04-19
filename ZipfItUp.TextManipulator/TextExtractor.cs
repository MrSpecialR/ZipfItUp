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

        public static string GetTimeDiffernace(DateTime date)
        {
            
            long minutes = (long)Math.Floor((DateTime.Now - date).TotalMinutes);
            if (minutes > 60)
            {
                int hours = (int) Math.Floor((DateTime.Now - date).TotalHours);
                if (hours > 24)
                {
                    int days = (int) Math.Floor((DateTime.Now - date).TotalDays);
                    return $"Uploaded {days} days ago.";
                }
                return $"Uploaded {hours} hours ago.";
            }
            return $"Uploaded {minutes} minutes ago."; ;
        }

        public static string GetFileName(string path)
        {
            int lastIndex = path.LastIndexOf('/') > path.LastIndexOf('\\')
                ? path.LastIndexOf('/')
                : path.LastIndexOf('\\');
            return path.Substring(lastIndex + 1, path.Length - lastIndex - 1);
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