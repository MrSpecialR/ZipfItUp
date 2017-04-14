using System;
using System.Collections.Generic;

namespace ZipfItUp.ModelHelper
{
    using ZipfItUp.Models;
    using ZipfItUp.Data;
    using System.Linq;
    public static class Initialize
    {
        public static List<DocumentWord> TransformWords(List<KeyValuePair<string, long>> words)
        {
            List<DocumentWord> WordObjects = new List<DocumentWord>();
            long MostOccuringWord = words[0].Value;
            for(int i = 0; i < words.Count; i++)
            {
                WordObjects.Add(
                new DocumentWord()
                {
                    Word = new Word()
                    {
                        WordString = words[i].Key,
                        Rank = i + 1,
                        Occurances = words[i].Value,
                        EstimatedOccurances = EstimateOccurances(MostOccuringWord, i + 1)
                    },
                    Rank = i + 1,
                    Occurances = words[i].Value,
                    EstimatedOccurances = EstimateOccurances(MostOccuringWord,i+1)
                });
        }
            return WordObjects;
        }

        public static long EstimateOccurances(long MaxOccuringWord, int Rank)
        {
            long Occurances = (long) Math.Ceiling((1.0m / Rank) * MaxOccuringWord);
            return Occurances;
        }
    }
}