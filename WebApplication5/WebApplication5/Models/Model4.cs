namespace WebApplication5.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model4 : DbContext
    {
        public Model4()
            : base("name=ModelYan")
        {
        }

        public virtual DbSet<reservation_table> reservation_table { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<reservation_table>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.reservation_table)
                .HasForeignKey(e => e.Rid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.Tid)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.CardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.NameOnCard)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.UnitPrice)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.TotalPrice)
                .HasPrecision(10, 0);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.CardCatergoryName)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.ExpDate)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.First_Name)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Last_Name)
                .IsUnicode(false);

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

            modelBuilder.Entity<User>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.Uid)
                .WillCascadeOnDelete(false);
        }
    }
}
