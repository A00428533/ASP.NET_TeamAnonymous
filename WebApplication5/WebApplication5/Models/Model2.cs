namespace WebApplication5.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model21")
        {
        }

        public virtual DbSet<RegisterUser> RegisterUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegisterUser>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterUser>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<RegisterUser>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);
        }
    }
}
