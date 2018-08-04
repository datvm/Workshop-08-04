using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Workshop.Api.Models.Entities
{
    public partial class WorkshopContext : DbContext
    {
        public WorkshopContext()
        {
        }

        public WorkshopContext(DbContextOptions<WorkshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tweet> Tweet { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tweet>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.CreatedTime })
                    .HasName("IX_Tweet_ByUser");

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedTime).HasColumnType("datetime");

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tweet)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tweet_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email);

                entity.HasIndex(e => e.GoogleId);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.GoogleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
