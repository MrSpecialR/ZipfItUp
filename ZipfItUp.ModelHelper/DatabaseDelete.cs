using System.Collections.Generic;
using System.Linq;
using ZipfItUp.Data;
using ZipfItUp.Models;

namespace ZipfItUp.ModelHelper
{
    public static class DatabaseDelete
    {
        public static void DeleteDocumentWords(List<DocumentWord> words, OccuranceContext context)
        {
            List<Word> AllWords = context.Words.ToList();
            foreach (DocumentWord word in words)
            {
                AllWords.First(x => x.WordString == word.Word.WordString).Occurances -= word.Occurances;
            }
            context.SaveChanges();
        }
    }
}