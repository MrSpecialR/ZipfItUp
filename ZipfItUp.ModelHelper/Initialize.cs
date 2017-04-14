namespace ZipfItUp.ModelHelper
{
    using ZipfItUp.Models;
    using ZipfItUp.Data;
    using System.Linq;
    public static class Initialize
    {
        public static void SetProperties()
        {
            using (OccuranceContext context = new OccuranceContext())
            {
                Word.WordsCount = context.Words.Sum(x => x.Occurances);
                Word bestWord = context.Words.FirstOrDefault(x=>x.Occurances == context.Words.Max(y=>y.Occurances));
                if (bestWord != null) { 
                    Word.CurrentBestWordId = bestWord.Id;
                    Word.CurrentBestWordOccurances = bestWord.Occurances;
                }
            }
        }
    }
}