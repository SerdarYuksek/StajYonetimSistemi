﻿// <auto-generated />
using System;
using InternService.Api.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternService.Api.Migrations
{
    [DbContext(typeof(InternDbContext))]
    [Migration("20240106122651_mig2")]
    partial class mig2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternService.Api.Model.InternInfo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Education")
                        .HasColumnType("bit");

                    b.Property<string>("FaxNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Holliday")
                        .HasColumnType("bit");

                    b.Property<int>("InternStatusId")
                        .HasColumnType("int");

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("SaturdayInc")
                        .HasColumnType("bit");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("StudentNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("area")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("İnternNumber")
                        .HasColumnType("int");

                    b.Property<string>("İnternType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("InternStatusId");

                    b.ToTable("internInfos");
                });

            modelBuilder.Entity("InternService.Api.Model.InternStatus", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("AcceptDay")
                        .HasColumnType("int");

                    b.Property<bool>("ContributConfirm")
                        .HasColumnType("bit");

                    b.Property<bool>("InternAccept")
                        .HasColumnType("bit");

                    b.Property<bool>("InternConfirm")
                        .HasColumnType("bit");

                    b.Property<string>("RejectReason")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("internStatuses");
                });

            modelBuilder.Entity("InternService.Api.Model.InternInfo", b =>
                {
                    b.HasOne("InternService.Api.Model.InternStatus", "InternStatus")
                        .WithMany()
                        .HasForeignKey("InternStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InternStatus");
                });
#pragma warning restore 612, 618
        }
    }
}
