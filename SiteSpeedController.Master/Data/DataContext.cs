using Microsoft.EntityFrameworkCore;
using SiteSpeedController.Master.Data.Models;

namespace SiteSpeedController.Master.Data
{
    public class DataContext : DbContext
    {
        public DbSet<AgentDao> Agents { get; set; }        
        public DbSet<CountryDao> Countries { get; set; }        

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
                .WithMany( a => a.Countries )
                .HasForeignKey(x => x.AgentId);

            modelBuilder.Entity<AgentDao>()
                .HasIndex(x => x.HostIdentifier)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}