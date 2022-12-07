using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DAL.Context
{
    public partial class CoctailsContext : DbContext
    {
        public CoctailsContext()
            : base("name=CoctailsContext")
        {
        }

        public virtual DbSet<Coctail> Coctails { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coctail>()
                .HasOptional(e => e.Favorite1)
                .WithRequired(e => e.Coctail);
        }
    }
}
