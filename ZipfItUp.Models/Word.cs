using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZipfItUp.Models
{
    public class Word
    {
        public Word()
        {
            this.Documents = new HashSet<DocumentWord>();
        }
        [Key]
        public int Id { get; set; }
        [Index(IsUnique = true), Required, StringLength(450)]
        public string WordString { get; set; }
        public long Occurances { get; set; }
        public virtual ICollection<DocumentWord> Documents { get; set; }
    }
}