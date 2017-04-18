using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZipfItUp.Data;
using ZipfItUp.Models;

namespace ZipfItUp.Query
{
    public static class Document
    {
        public static DocumentWord GetMostUsedWord(Models.Document doc, OccuranceContext context)
        {
            return context.Documents.First(x => x.Id == doc.Id).Words.First();
        }

        public static int NumberOfWordsUsedJustOnce(Models.Document doc, OccuranceContext context)
        {
            return context.Documents.First(x => x.Id == doc.Id).Words.Count(x => x.Occurances == 1);
        }
    }
}