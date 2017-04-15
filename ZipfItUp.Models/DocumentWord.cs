using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZipfItUp.Models
{
    public class DocumentWord
    {
        [Key, Column(Order=0)]
        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }
        [Key, Column(Order=1)]
        public int WordId { get; set; }
        public virtual Word Word { get; set; }
        
        public long Occurances { get; set; }
    }
}