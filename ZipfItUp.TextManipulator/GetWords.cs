using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZipfItUp.TextManipulator
{
    public static class GetWords
    {

        public static HashSet<KeyValuePair<string, long>> WordList(string path)
        {
            string text = GetText(path);
            List<string> words = ToList(text);
            return WordList(words);
        }

        public static HashSet<KeyValuePair<string, long>> WordList(List<string> words)
        {
            HashSet<KeyValuePair<string, long>> Words = 
                new HashSet<KeyValuePair<string, long>>(
                    words.GroupBy(x => x)
                    .Select(x=> 
                        new KeyValuePair<string, long>( 
                            x.Key, 
                            x.Count()))
                    .OrderByDescending(x=>x.Value)
                    .Distinct()
                    .ToList()
                );
            
            return Words;
        }

        public static string GetText(string path)
        {
            string extension = TextExtractor.GetExtension(path);
            string text = "";
            switch (extension)
            {
                case "txt":
                    text = TextExtractor.FromTXT(path);
                    break;
                case "srt":
                    text = TextExtractor.FromSRT(path);
                    break;
                case "doc":
                case "docx":
                case "rtf":
                    text = TextExtractor.FromDOC(path);
                    break;
                case "pdf":
                    text = TextExtractor.FromPDF(path);
                    break;
            }
            text = new string(text.Where(x=>char.IsWhiteSpace(x) || char.IsLetterOrDigit(x)).ToArray());
            text = text.ToLower().Replace("\r", string.Empty).Replace("\n", string.Empty);
            return text;
        }

        public static List<string> ToList(string text)
        {
            return text.Split(new char[] {' ', '\n'}, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
