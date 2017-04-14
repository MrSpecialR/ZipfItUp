using System;
using System.ComponentModel.DataAnnotations;

namespace ZipfItUp.Models
{
    public class UserInput
    {
        [Key]
        public int Id { get; set; }
        public int DocumentId { get; set; }
        public virtual Document Document { get; set; }
        public DateTime DateUploaded { get; set; }
    }
}