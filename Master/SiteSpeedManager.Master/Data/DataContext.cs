using Microsoft.EntityFrameworkCore;
using SiteSpeedManager.Master.Data.Models;

namespace SiteSpeedManager.Master.Data
{
    public class DataContext : DbContext
    {
        public DbSet<AgentDao> Agents { get; set; }

        public DbSet<CountryDao> Countries { get; set; }

        public DbSet<SiteDao> Sites { get; set; }

        public DbSet<PageDao> Pages { get; set; }

        public DbSet<DataStoreDao> DataStores { get; set; }

        public DbSet<PerformanceProfileDao> PerformanceProfiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=data.db");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgentCountryAssociation>()
                .HasKey(association => new
                {
                    association.AgentId,
                    association.CountryId
                });

            modelBuilder.Entity<AgentCountryAssociation>()
                .HasOne(pt => pt.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId);

            modelBuilder.Entity<AgentCountryAssociation>()
                .HasOne(pt => pt.Agent)
                .WithMany(a => a.Countries)
                .HasForeignKey(x => x.AgentId);

            modelBuilder.Entity<PageCountryAssociation>()
                .HasKey(association => new
                {
                    association.PageId,
                    association.CountryId
                });

            modelBuilder.Entity<PageCountryAssociation>()
                .HasOne(pt => pt.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId);

            modelBuilder.Entity<PageCountryAssociation>()
                .HasOne(pt => pt.Page)
                .WithMany(a => a.Countries)
                .HasForeignKey(x => x.PageId);

            modelBuilder.Entity<SiteCountryAssociation>()
                .HasKey(association => new
                {
                    association.SiteId,
                    association.CountryId
                });

            modelBuilder.Entity<SiteCountryAssociation>()
                .HasOne(pt => pt.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId);

            modelBuilder.Entity<SiteCountryAssociation>()
                .HasOne(pt => pt.Site)
                .WithMany(a => a.Countries)
                .HasForeignKey(x => x.SiteId);

            modelBuilder.Entity<SiteProfileAssociation>()
                .HasKey(association => new
                {
                    association.SiteId,
                    association.ProfileId
                });

            modelBuilder.Entity<SiteProfileAssociation>()
                .HasOne(pt => pt.Site)
                .WithMany()
                .HasForeignKey(x => x.SiteId);

            modelBuilder.Entity<SiteProfileAssociation>()
                .HasOne(pt => pt.Site)
                .WithMany(a => a.PerformanceProfiles)
                .HasForeignKey(x => x.SiteId);

            modelBuilder.Entity<AgentDao>()
                .HasIndex(x => x.HostIdentifier)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}