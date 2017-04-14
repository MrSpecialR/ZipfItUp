using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZipfItUp.Models
{     
    public class Word
    {
        public static long WordsCount { get; set; }
        public static int CurrentBestWordId { get; set; }
        public static long CurrentBestWordOccurances { get; set; }
        public Word(string word, int times)
        {
            WordsCount += times;
        }
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Key, Column(Order = 1)]
        public string WordString { get; set; }
        public long Occurances { get; set; }
        public decimal Percent { get; set; }

        public void AddOccurances(int times)
        {
            this.Occurances += times;
        }

        public void UpdateRankAndPercentage()
        {
            
        }
    }
}