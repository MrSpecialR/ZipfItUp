namespace ZipfItUp.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using ZipfItUp.Models;

    public class OccuranceContext : DbContext
    {
        public OccuranceContext()
            : base("name=OccuranceContext")
        {
        }
        public virtual DbSet<Word> Words { get;set; }
    }
}