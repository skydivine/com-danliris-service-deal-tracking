﻿// <auto-generated />
using System;
using Com.DanLiris.Service.DealTracking.Lib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Com.DanLiris.Service.DealTracking.Lib.Migrations
{
    [DbContext(typeof(DealTrackingDbContext))]
    partial class DealTrackingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.Activity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("AssignedTo")
                        .HasMaxLength(255);

                    b.Property<string>("Code")
                        .HasMaxLength(255);

                    b.Property<string>("CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DealCode")
                        .HasMaxLength(255);

                    b.Property<long>("DealId");

                    b.Property<string>("DealName")
                        .HasMaxLength(4000);

                    b.Property<string>("DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("DeletedUtc");

                    b.Property<DateTimeOffset?>("DueDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastModifiedUtc");

                    b.Property<string>("Notes");

                    b.Property<long?>("StageFromId");

                    b.Property<string>("StageFromName")
                        .HasMaxLength(4000);

                    b.Property<long?>("StageToId");

                    b.Property<string>("StageToName")
                        .HasMaxLength(4000);

                    b.Property<bool>("Status");

                    b.Property<string>("TaskTitle")
                        .HasMaxLength(4000);

                    b.Property<string>("Type")
                        .HasMaxLength(255);

                    b.Property<string>("UId");

                    b.HasKey("Id");

                    b.HasIndex("DealId");

                    b.ToTable("DealTrackingActivities");
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.ActivityAttachment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<long>("ActivityId");

                    b.Property<string>("CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("DeletedUtc");

                    b.Property<string>("FileName");

                    b.Property<string>("FilePath");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastModifiedUtc");

                    b.Property<string>("UId");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("DealTrackingActivityAttachments");
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.Board", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Code")
                        .HasMaxLength(255);

                    b.Property<string>("CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("CurrencyCode")
                        .HasMaxLength(255);

                    b.Property<int>("CurrencyId");

                    b.Property<string>("CurrencySymbol")
                        .HasMaxLength(255);

                    b.Property<string>("DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("DeletedUtc");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastModifiedUtc");

                    b.Property<string>("Title")
                        .HasMaxLength(4000);

                    b.Property<string>("UId");

                    b.HasKey("Id");

                    b.ToTable("DealTrackingBoards");
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.Company", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("City")
                        .HasMaxLength(255);

                    b.Property<string>("Code")
                        .HasMaxLength(255);

                    b.Property<string>("CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("DeletedUtc");

                    b.Property<string>("Industry")
                        .HasMaxLength(1000);

                    b.Property<string>("Information");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastModifiedUtc");

                    b.Property<string>("Name")
                        .HasMaxLength(4000);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(255);

                    b.Property<string>("UId");

                    b.Property<string>("Website")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.Contact", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Code")
                        .HasMaxLength(255);

                    b.Property<long>("CompanyId");

                    b.Property<string>("CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("DeletedUtc");

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<string>("Information");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("JobTitle")
                        .HasMaxLength(8000);

                    b.Property<string>("LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastModifiedUtc");

                    b.Property<string>("Name")
                        .HasMaxLength(1000);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(255);

                    b.Property<string>("UId");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.Deal", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<double>("Amount");

                    b.Property<DateTimeOffset>("CloseDate");

                    b.Property<string>("Code")
                        .HasMaxLength(255);

                    b.Property<string>("CompanyCode")
                        .HasMaxLength(255);

                    b.Property<long>("CompanyId");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(4000);

                    b.Property<string>("ContactCode")
                        .HasMaxLength(255);

                    b.Property<long>("ContactId");

                    b.Property<string>("ContactName")
                        .HasMaxLength(4000);

                    b.Property<string>("CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("DeletedUtc");

                    b.Property<string>("Description");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastModifiedUtc");

                    b.Property<string>("Name")
                        .HasMaxLength(4000);

                    b.Property<double>("Quantity");

                    b.Property<string>("Reason");

                    b.Property<long>("StageId");

                    b.Property<string>("UId");

                    b.Property<string>("UomUnit");

                    b.HasKey("Id");

                    b.ToTable("DealTrackingDeals");
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.Reason", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<string>("Code")
                        .HasMaxLength(255);

                    b.Property<string>("CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("DeletedUtc");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastModifiedUtc");

                    b.Property<string>("LoseReason")
                        .HasMaxLength(255);

                    b.Property<string>("UId");

                    b.HasKey("Id");

                    b.ToTable("DealTrackingReasons");
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.Stage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<long>("BoardId");

                    b.Property<string>("Code")
                        .HasMaxLength(255);

                    b.Property<string>("CreatedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("CreatedUtc");

                    b.Property<string>("DealsOrder");

                    b.Property<string>("DeletedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("DeletedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("DeletedUtc");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastModifiedAgent")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<DateTime>("LastModifiedUtc");

                    b.Property<string>("Name")
                        .HasMaxLength(4000);

                    b.Property<string>("UId");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.ToTable("DealTrackingStages");
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.Activity", b =>
                {
                    b.HasOne("Com.DanLiris.Service.DealTracking.Lib.Models.Deal", "Deal")
                        .WithMany()
                        .HasForeignKey("DealId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.ActivityAttachment", b =>
                {
                    b.HasOne("Com.DanLiris.Service.DealTracking.Lib.Models.Activity", "Activity")
                        .WithMany("Attachments")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.Contact", b =>
                {
                    b.HasOne("Com.DanLiris.Service.DealTracking.Lib.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Com.DanLiris.Service.DealTracking.Lib.Models.Stage", b =>
                {
                    b.HasOne("Com.DanLiris.Service.DealTracking.Lib.Models.Board", "Board")
                        .WithMany("Stages")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
