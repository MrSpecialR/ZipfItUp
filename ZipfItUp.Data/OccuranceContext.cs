namespace ZipfItUp.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class OccuranceContext : DbContext
    {
        public OccuranceContext()
            : base("name=OccuranceContext")
        {
        }
    }
}