namespace ZipfItUp.Models
{
    public class WordRanking
    {
        public int DocumentId { get; set; }
        public int Rank { get; set; }
        public string FileName { get; set; }
        public long Occurances { get; set; }
    }
}