using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZipfItUp.Models
{
    public enum DocumentType
    {
        Text,
        Lyric,
        Subtitle,
        Rich,
        WordDoc,
        PortableDoc,
        Other
    }
    public class Document
    {
        public Document()
        {
            this.Words = new HashSet<DocumentWord>();
        }

        [Key]
        public int Id { get; set; }

        public string FileName { get; set; }

        public DocumentType DocumentType { get; set; }
        public string FilePath { get; set; }
        public long WordCount { get; set; }

        public int MostUsedWordId { get; set; }
        public virtual Word MostUsedWord { get; set; }
        
        public virtual ICollection<DocumentWord> Words { get; set; }

        public int UserInputId { get; set; }
        public virtual UserInput UserInput { get; set; }
    }
}