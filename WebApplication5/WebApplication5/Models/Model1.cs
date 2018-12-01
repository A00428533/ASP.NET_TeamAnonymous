namespace WebApplication5.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=User")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
               .Property(e => e.Street_Number);
              
            modelBuilder.Entity<User>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Province_State)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Postal_Code)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone_Number)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.E_mail_Address)
                .IsUnicode(false);
        }
    }
}
