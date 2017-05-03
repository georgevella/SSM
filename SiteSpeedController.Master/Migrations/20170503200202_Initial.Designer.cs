using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SiteSpeedController.Master.Data;
using SiteSpeedController.Master.Data.Models;

namespace SiteSpeedController.Master.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20170503200202_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.AgentCountryAssociation", b =>
                {
                    b.Property<int>("AgentId")
                        .HasColumnName("agentId");

                    b.Property<string>("CountryId")
                        .HasColumnName("countryId");

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

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("countries");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.DataStoreDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("HasCredentials")
                        .HasColumnName("hasCredentials");

                    b.Property<string>("Host")
                        .HasColumnName("host");

                    b.Property<bool>("IsDefault")
                        .HasColumnName("isDefault");

                    b.Property<string>("Password")
                        .HasColumnName("password");

                    b.Property<int>("Port")
                        .HasColumnName("port");

                    b.Property<int>("Type")
                        .HasColumnName("type");

                    b.Property<string>("Username")
                        .HasColumnName("username");

                    b.HasKey("Id");

                    b.ToTable("datastores");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.PageCountryAssociation", b =>
                {
                    b.Property<int>("PageId")
                        .HasColumnName("pageId");

                    b.Property<string>("CountryId")
                        .HasColumnName("countryId");

                    b.HasKey("PageId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("pageCountries");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.PageDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Alias")
                        .HasColumnName("alias");

                    b.Property<bool>("IsEnabled")
                        .HasColumnName("isEnabled");

                    b.Property<bool>("OverridesSiteCountryList")
                        .HasColumnName("overridesCountryList");

                    b.Property<string>("Path")
                        .HasColumnName("path");

                    b.Property<int>("SiteId")
                        .HasColumnName("siteId");

                    b.HasKey("Id");

                    b.HasIndex("SiteId");

                    b.ToTable("pages");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.PerformanceProfileDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int>("CustomDownstream")
                        .HasColumnName("downstream");

                    b.Property<int>("CustomUpstream")
                        .HasColumnName("upstream");

                    b.Property<int>("Speed")
                        .HasColumnName("speed");

                    b.Property<bool>("SpeedIndexEnabled")
                        .HasColumnName("speedIndexEnabled");

                    b.Property<string>("UserAgent")
                        .HasColumnName("useragent");

                    b.Property<bool>("VideoEnabled")
                        .HasColumnName("videoEnabled");

                    b.Property<int>("ViewportHeight")
                        .HasColumnName("viewportHeight");

                    b.Property<int>("ViewportWidth")
                        .HasColumnName("viewportWidth");

                    b.HasKey("Id");

                    b.ToTable("performanceProfiles");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.SiteCountryAssociation", b =>
                {
                    b.Property<int>("SiteId")
                        .HasColumnName("siteId");

                    b.Property<string>("CountryId")
                        .HasColumnName("countryId");

                    b.HasKey("SiteId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("siteCountries");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.SiteDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<int?>("DataStoreId")
                        .HasColumnName("datastoreId");

                    b.Property<string>("Domain")
                        .HasColumnName("domain");

                    b.Property<bool>("IsEnabled")
                        .HasColumnName("isEnabled");

                    b.HasKey("Id");

                    b.HasIndex("DataStoreId");

                    b.ToTable("sites");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.SiteProfileAssociation", b =>
                {
                    b.Property<int>("SiteId")
                        .HasColumnName("siteId");

                    b.Property<string>("ProfileId")
                        .HasColumnName("profileId");

                    b.Property<int?>("ProfileId1");

                    b.HasKey("SiteId", "ProfileId");

                    b.HasIndex("ProfileId1");

                    b.ToTable("siteProfiles");
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

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.PageCountryAssociation", b =>
                {
                    b.HasOne("SiteSpeedController.Master.Data.Models.CountryDao", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiteSpeedController.Master.Data.Models.PageDao", "Page")
                        .WithMany("Countries")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.PageDao", b =>
                {
                    b.HasOne("SiteSpeedController.Master.Data.Models.SiteDao", "Site")
                        .WithMany("Pages")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.SiteCountryAssociation", b =>
                {
                    b.HasOne("SiteSpeedController.Master.Data.Models.CountryDao", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiteSpeedController.Master.Data.Models.SiteDao", "Site")
                        .WithMany("Countries")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.SiteDao", b =>
                {
                    b.HasOne("SiteSpeedController.Master.Data.Models.DataStoreDao", "DataStore")
                        .WithMany()
                        .HasForeignKey("DataStoreId");
                });

            modelBuilder.Entity("SiteSpeedController.Master.Data.Models.SiteProfileAssociation", b =>
                {
                    b.HasOne("SiteSpeedController.Master.Data.Models.PerformanceProfileDao", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId1");

                    b.HasOne("SiteSpeedController.Master.Data.Models.SiteDao", "Site")
                        .WithMany("PerformanceProfiles")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
