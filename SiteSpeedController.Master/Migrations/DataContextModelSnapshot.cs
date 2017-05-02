using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SiteSpeedController.Master.Data;

namespace SiteSpeedController.Master.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.AgentCountryAssociation", b =>
                {
                    b.Property<int>("AgentId");

                    b.Property<string>("CountryId");

                    b.HasKey("AgentId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("agentCountries");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.AgentDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("Created")
                        .HasColumnName("created");

                    b.Property<Guid>("HostIdentifier")
                        .HasColumnName("hostIdentifier");

                    b.Property<string>("Hostname")
                        .IsRequired()
                        .HasColumnName("hostname");

                    b.Property<bool>("IsApproved")
                        .HasColumnName("isApproved");

                    b.Property<bool>("IsDisabled")
                        .HasColumnName("isDisabled");

                    b.Property<int>("JobsExecuted")
                        .HasColumnName("jobsRun");

                    b.Property<DateTime>("LastUpdated")
                        .HasColumnName("lastUpdated");

                    b.Property<int>("Port")
                        .HasColumnName("port");

                    b.HasKey("Id");

                    b.HasIndex("HostIdentifier")
                        .IsUnique();

                    b.ToTable("agents");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.CountryDao", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("countries");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.AgentCountryAssociation", b =>
                {
                    b.HasOne("SiteSpeedController.Master.Data.Models.AgentDao", "Agent")
                        .WithMany("Countries")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiteSpeedController.Master.Data.Models.CountryDao", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
