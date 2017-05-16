using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SiteSpeedManager.Master.Data;
using SiteSpeedManager.Master.Data.Models;

namespace SiteSpeedManager.Master.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.AgentCountryAssociation", b =>
                {
                    b.Property<int>("AgentId")
                        .HasColumnName("agentId");

                    b.Property<string>("CountryId")
                        .HasColumnName("countryId");

                    b.HasKey("AgentId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("agentCountries");
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.AgentDao", b =>
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

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.CountryDao", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<bool>("IsEnabled")
                        .HasColumnName("isDisabled");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("countries");
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.DataStoreDao", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int>("Type")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("datastores");

                    b.HasDiscriminator<string>("Discriminator").HasValue("DataStoreDao");
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.PageCountryAssociation", b =>
                {
                    b.Property<int>("PageId")
                        .HasColumnName("pageId");

                    b.Property<string>("CountryId")
                        .HasColumnName("countryId");

                    b.HasKey("PageId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("pageCountries");
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.PageDao", b =>
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

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.PerformanceProfileDao", b =>
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

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.SiteCountryAssociation", b =>
                {
                    b.Property<int>("SiteId")
                        .HasColumnName("siteId");

                    b.Property<string>("CountryId")
                        .HasColumnName("countryId");

                    b.HasKey("SiteId", "CountryId");

                    b.HasIndex("CountryId");

                    b.ToTable("siteCountries");
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.SiteDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Domain")
                        .HasColumnName("domain");

                    b.Property<bool>("IsEnabled")
                        .HasColumnName("isEnabled");

                    b.Property<string>("ResultStoreId")
                        .HasColumnName("resultStoreId");

                    b.Property<string>("TimingStoreId")
                        .HasColumnName("timingStoreId");

                    b.HasKey("Id");

                    b.HasIndex("ResultStoreId");

                    b.HasIndex("TimingStoreId");

                    b.ToTable("sites");
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.SiteProfileAssociation", b =>
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

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.GrafanaDbDataStoreDao", b =>
                {
                    b.HasBaseType("SiteSpeedManager.Master.Data.Models.DataStoreDao");

                    b.Property<bool>("HasCredentials")
                        .HasColumnName("db-usesCredentials");

                    b.Property<string>("Host")
                        .HasColumnName("db-host");

                    b.Property<int>("HttpPort")
                        .HasColumnName("grf-httpPort");

                    b.Property<bool>("IsEnabled")
                        .HasColumnName("db-isEnabled");

                    b.Property<string>("Namespace")
                        .HasColumnName("grf-namespace");

                    b.Property<string>("Password")
                        .HasColumnName("db-password");

                    b.Property<int>("Port")
                        .HasColumnName("db-port");

                    b.Property<string>("Username")
                        .HasColumnName("db-username");

                    b.ToTable("datastores");

                    b.HasDiscriminator().HasValue("GrafanaDbDataStoreDao");
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.InfluxDbDataStoreDao", b =>
                {
                    b.HasBaseType("SiteSpeedManager.Master.Data.Models.DataStoreDao");

                    b.Property<string>("Database")
                        .HasColumnName("inf-database");

                    b.Property<bool>("HasCredentials")
                        .HasColumnName("db-usesCredentials");

                    b.Property<string>("Host")
                        .HasColumnName("db-host");

                    b.Property<bool>("IsEnabled")
                        .HasColumnName("db-isEnabled");

                    b.Property<string>("Password")
                        .HasColumnName("db-password");

                    b.Property<int>("Port")
                        .HasColumnName("db-port");

                    b.Property<string>("Username")
                        .HasColumnName("db-username");

                    b.ToTable("datastores");

                    b.HasDiscriminator().HasValue("InfluxDbDataStoreDao");
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.S3DataStoreDao", b =>
                {
                    b.HasBaseType("SiteSpeedManager.Master.Data.Models.DataStoreDao");

                    b.Property<string>("BucketName")
                        .HasColumnName("aws-s3-bucket");

                    b.Property<string>("Key")
                        .HasColumnName("aws-accesskey");

                    b.Property<string>("Path")
                        .HasColumnName("aws-s3-path");

                    b.Property<string>("Region")
                        .HasColumnName("aws-region");

                    b.Property<string>("Secret")
                        .HasColumnName("aws-secretkey");

                    b.ToTable("datastores");

                    b.HasDiscriminator().HasValue("S3DataStoreDao");
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.AgentCountryAssociation", b =>
                {
                    b.HasOne("SiteSpeedManager.Master.Data.Models.AgentDao", "Agent")
                        .WithMany("Countries")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiteSpeedManager.Master.Data.Models.CountryDao", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.PageCountryAssociation", b =>
                {
                    b.HasOne("SiteSpeedManager.Master.Data.Models.CountryDao", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiteSpeedManager.Master.Data.Models.PageDao", "Page")
                        .WithMany("Countries")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.PageDao", b =>
                {
                    b.HasOne("SiteSpeedManager.Master.Data.Models.SiteDao", "Site")
                        .WithMany("Pages")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.SiteCountryAssociation", b =>
                {
                    b.HasOne("SiteSpeedManager.Master.Data.Models.CountryDao", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SiteSpeedManager.Master.Data.Models.SiteDao", "Site")
                        .WithMany("Countries")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.SiteDao", b =>
                {
                    b.HasOne("SiteSpeedManager.Master.Data.Models.DataStoreDao", "ResultStore")
                        .WithMany()
                        .HasForeignKey("ResultStoreId");

                    b.HasOne("SiteSpeedManager.Master.Data.Models.DataStoreDao", "TimingStore")
                        .WithMany()
                        .HasForeignKey("TimingStoreId");
                });

            modelBuilder.Entity("SiteSpeedManager.Master.Data.Models.SiteProfileAssociation", b =>
                {
                    b.HasOne("SiteSpeedManager.Master.Data.Models.PerformanceProfileDao", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId1");

                    b.HasOne("SiteSpeedManager.Master.Data.Models.SiteDao", "Site")
                        .WithMany("PerformanceProfiles")
                        .HasForeignKey("SiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
