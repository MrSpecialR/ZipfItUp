using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [Required]
        public string FileName { get; set; }

        public DocumentType DocumentType { get; set; }
        [Required]
        public string FilePath { get; set; }
        public long WordCount { get; set; }
        
        public virtual ICollection<DocumentWord> Words { get; set; }
        public DateTime DateUploaded { get; set; }
    }
}