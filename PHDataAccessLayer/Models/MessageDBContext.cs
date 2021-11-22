using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PHDataAccessLayer.Models
{
    public partial class MessageDBContext : DbContext
    {
        public MessageDBContext()
        {
        }

        public MessageDBContext(DbContextOptions<MessageDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MessageArchive> MessageArchive { get; set; }
        public virtual DbSet<MessageMaster> MessageMaster { get; set; }
        public virtual DbSet<PurgeMaster> PurgeMaster { get; set; }
        public virtual DbSet<SystemConfig> SystemConfig { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer("Server=tcp:peacehealth-poc-sql.database.windows.net,1433;Initial Catalog=MessageDB;Persist Security Info=False;User ID=ph-user;Password=Win@peace1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MessageArchive>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__MessageA__C87C037C5DDC2868");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Details).IsRequired();

                entity.Property(e => e.From)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Mode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RetryDate).HasColumnType("datetime");

                entity.Property(e => e.RetryNumber).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.To)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MessageMaster>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                    .HasName("PK__MessageM__C87C037C40B77EB9");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Details).IsRequired();

                entity.Property(e => e.From)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MessageDate).HasColumnType("datetime");

                entity.Property(e => e.Mode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RetryDate).HasColumnType("datetime");

                entity.Property(e => e.RetryNumber).HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.To)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PurgeMaster>(entity =>
            {
                entity.HasKey(e => e.PurgeId)
                    .HasName("PK__PurgeMas__8AC0B077F2FFE850");

                entity.Property(e => e.PurgeId).HasColumnName("PurgeID");

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PurgeDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SystemConfig>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.Key)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
