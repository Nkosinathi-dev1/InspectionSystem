using AssetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace AssetService.Persistence.Db
{
    public class AssetDbContext : DbContext
    {
        public AssetDbContext(DbContextOptions<AssetDbContext> options) : base(options) { }

        public DbSet<Asset> Assets => Set<Asset>();
        public DbSet<InspectionRecord> InspectionRecords => Set<InspectionRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Asset>(b =>
            {
                b.HasKey(a => a.Id);
                b.Property(a => a.AssetType).IsRequired().HasMaxLength(100);
                b.Property(a => a.Identifier).IsRequired().HasMaxLength(200);
                b.Property(a => a.Description).HasMaxLength(1000);
                b.HasMany(a => a.InspectionHistory)
                 .WithOne(i => i.Asset)
                 .HasForeignKey(i => i.AssetId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<InspectionRecord>(b =>
            {
                b.HasKey(i => i.Id);
                b.Property(i => i.InspectorName).IsRequired().HasMaxLength(200);
                b.Property(i => i.Status).IsRequired().HasMaxLength(50);
                b.Property(i => i.Notes).HasMaxLength(2000);
            });
        }
    }
}
