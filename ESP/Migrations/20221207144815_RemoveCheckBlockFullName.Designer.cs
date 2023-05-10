﻿// <auto-generated />
using System;
using ESP.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ESP.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20221207144815_RemoveCheckBlockFullName")]
    partial class RemoveCheckBlockFullName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CheckBlockCheckCode", b =>
                {
                    b.Property<int>("CheckBlocksId")
                        .HasColumnType("int");

                    b.Property<int>("CheckCodesId")
                        .HasColumnType("int");

                    b.HasKey("CheckBlocksId", "CheckCodesId");

                    b.HasIndex("CheckCodesId");

                    b.ToTable("CheckBlocksAndCheckCodes", (string)null);
                });

            modelBuilder.Entity("CheckBlockSubjectType", b =>
                {
                    b.Property<int>("CheckBlocksId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectTypesId")
                        .HasColumnType("int");

                    b.HasKey("CheckBlocksId", "SubjectTypesId");

                    b.HasIndex("SubjectTypesId");

                    b.ToTable("CheckBlocksAndSubjectTypes", (string)null);
                });

            modelBuilder.Entity("ESP.Models.Block", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Blocks");
                });

            modelBuilder.Entity("ESP.Models.CheckBlock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BlockId")
                        .HasColumnType("int");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlockId");

                    b.ToTable("CheckBlocks");
                });

            modelBuilder.Entity("ESP.Models.CheckCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CheckCodes");
                });

            modelBuilder.Entity("ESP.Models.ProhibitionCode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("CheckCodeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CheckCodeId");

                    b.ToTable("ProhibitionCodes");
                });

            modelBuilder.Entity("ESP.Models.SubjectType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("SubjectTypes");
                });

            modelBuilder.Entity("CheckBlockCheckCode", b =>
                {
                    b.HasOne("ESP.Models.CheckBlock", null)
                        .WithMany()
                        .HasForeignKey("CheckBlocksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.CheckCode", null)
                        .WithMany()
                        .HasForeignKey("CheckCodesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CheckBlockSubjectType", b =>
                {
                    b.HasOne("ESP.Models.CheckBlock", null)
                        .WithMany()
                        .HasForeignKey("CheckBlocksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ESP.Models.SubjectType", null)
                        .WithMany()
                        .HasForeignKey("SubjectTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ESP.Models.CheckBlock", b =>
                {
                    b.HasOne("ESP.Models.Block", "Block")
                        .WithMany("CheckBlocks")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Block");
                });

            modelBuilder.Entity("ESP.Models.ProhibitionCode", b =>
                {
                    b.HasOne("ESP.Models.CheckCode", "CheckCode")
                        .WithMany("ProhibitionCodes")
                        .HasForeignKey("CheckCodeId");

                    b.Navigation("CheckCode");
                });

            modelBuilder.Entity("ESP.Models.Block", b =>
                {
                    b.Navigation("CheckBlocks");
                });

            modelBuilder.Entity("ESP.Models.CheckCode", b =>
                {
                    b.Navigation("ProhibitionCodes");
                });
#pragma warning restore 612, 618
        }
    }
}
