using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuickFile.WebApi.Models
{
    public partial class QuickFileContext : DbContext
    {
        public QuickFileContext()
        {
        }

        public QuickFileContext(DbContextOptions<QuickFileContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FileStore> FileStore { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(local);Database=QuickFile;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<FileStore>(entity =>
            {
                entity.HasIndex(e => e.UniqueId)
                    .HasName("AK_FileStore_UniqueId")
                    .IsUnique();

                entity.Property(e => e.FileData).IsRequired();

                entity.Property(e => e.UniqueId).HasDefaultValueSql("(newid())");
            });
        }
    }
}
