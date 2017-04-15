namespace ZipfItUp.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class OccuranceContext : DbContext
    {
        public OccuranceContext()
            : base("name=OccuranceContext")
        {
        }
        public virtual DbSet<Word> Words { get;set; }
        public virtual  DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentWord> DocumentWords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>().HasMany(x=>x.Documents).WithRequired(x=>x.Word).WillCascadeOnDelete(false);
            modelBuilder.Entity<Document>()
                .HasMany(x => x.Words)
                .WithRequired(x => x.Document)
                .HasForeignKey(x => x.DocumentId);
            base.OnModelCreating(modelBuilder);
        }
    }
}