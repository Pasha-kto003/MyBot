using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MyBot.db
{
    public partial class MyBot_DBContext : DbContext
    {

        static MyBot_DBContext db;

        public static MyBot_DBContext GetDB()
        {
            if (db == null)
                db = new MyBot_DBContext();
            return db;
        }

        public MyBot_DBContext()
        {
        }

        public MyBot_DBContext(DbContextOptions<MyBot_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Drug> Drugs { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-2KIP198\\SQLEXPRESS;Database=MyBot_DB;Trusted_Connection=True; User=dbo");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Drug>(entity =>
            {
                entity.ToTable("Drug");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DateOrder).HasColumnType("datetime");

                entity.Property(e => e.DrugId).HasColumnName("DrugID");

                entity.Property(e => e.NumberOfOrder)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Drug)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DrugId)
                    .HasConstraintName("FK_Order_Drug");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
