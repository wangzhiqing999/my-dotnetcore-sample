using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using A0008_EF_ExistingDb.Model;

namespace A0008_EF_ExistingDb.DataAccess
{
    public partial class MyCustomActionContext : DbContext
    {
        public MyCustomActionContext()
        {
        }

        public MyCustomActionContext(DbContextOptions<MyCustomActionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<McaCustomAction> McaCustomAction { get; set; }
        public virtual DbSet<McaServiceModule> McaServiceModule { get; set; }

        //public virtual DbSet<MigrationHistory> MigrationHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Initial Catalog=MyCustomAction;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<McaCustomAction>(entity =>
            {
                entity.ToTable("mca_custom_action");

                entity.HasIndex(e => e.ModuleCode)
                    .HasName("IX_module_code");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessCount).HasColumnName("access_count");

                entity.Property(e => e.CustomId).HasColumnName("custom_id");

                entity.Property(e => e.EnterTime)
                    .HasColumnName("enter_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExpData).HasColumnName("exp_data");

                entity.Property(e => e.LastAccessTime)
                    .HasColumnName("last_access_time")
                    .HasColumnType("datetime");

                entity.Property(e => e.ModuleCode)
                    .IsRequired()
                    .HasColumnName("module_code")
                    .HasMaxLength(32);

                entity.HasOne(d => d.ModuleCodeNavigation)
                    .WithMany(p => p.McaCustomAction)
                    .HasForeignKey(d => d.ModuleCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dbo.mca_custom_action_dbo.mca_service_module_module_code");
            });

            modelBuilder.Entity<McaServiceModule>(entity =>
            {
                entity.HasKey(e => e.ModuleCode)
                    .HasName("PK_dbo.mca_service_module");

                entity.ToTable("mca_service_module");

                entity.Property(e => e.ModuleCode)
                    .HasColumnName("module_code")
                    .HasMaxLength(32)
                    .ValueGeneratedNever();

                entity.Property(e => e.ModuleName)
                    .IsRequired()
                    .HasColumnName("module_name")
                    .HasMaxLength(64);
            });


            /*
            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");

                entity.ToTable("__MigrationHistory");

                entity.Property(e => e.MigrationId).HasMaxLength(150);

                entity.Property(e => e.ContextKey).HasMaxLength(300);

                entity.Property(e => e.Model).IsRequired();

                entity.Property(e => e.ProductVersion)
                    .IsRequired()
                    .HasMaxLength(32);
            });
            */

        }
    }
}
