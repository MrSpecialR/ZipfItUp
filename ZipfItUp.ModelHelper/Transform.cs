using System;
using System.Collections.Generic;

namespace ZipfItUp.ModelHelper
{
    using ZipfItUp.Models;
    using ZipfItUp.Data;
    using System.Linq;
    public static class Transform
    {
        public static List<Word> ToWords(List<KeyValuePair<string, long>> words)
        {
            List<Word> WordObjects = new List<Word>(words.Select(x=> new Word()
            {
                WordString = x.Key,
                Occurances = x.Value
            }));
            return WordObjects;
        }
        public static Document ToDocument(string path)
        {
            return new Document()
            {
                DocumentType = GetType(path),
                FileName = TextManipulator.TextExtractor.GetExtension(path),
                FilePath = path,
                Words = new HashSet<DocumentWord>()
            };
        }

        public static DocumentType GetType(string path)
        {
            string extension = TextManipulator.TextExtractor.GetExtension(path);
            DocumentType type;
            switch (extension)
            {
                case "doc":
                case "docx":
                    type = DocumentType.WordDoc;
                    break;
                case "pdf":
                    type = DocumentType.PortableDoc;
                    break;
                case "rtf":
                    type = DocumentType.Rich;
                    break;
                case "txt":
                    type = DocumentType.Text;
                    break;
                case "lyr":
                    type = DocumentType.Lyric;
                    break;
                case "srt":
                    type = DocumentType.Subtitle;
                    break;
                default:
                    type = DocumentType.Other;
                    break;
            }
            return type;
        }


        public static long EstimateOccurances(long MaxOccuringWord, int Rank)
        {
            long Occurances = (long) Math.Ceiling((1.0m / Rank) * MaxOccuringWord);
            return Occurances;
        }
    }
}