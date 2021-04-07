﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Utilities;

namespace Utilities.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20210402194100_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Utilities.Models.Transaction", b =>
                {
                    b.Property<long>("ReferenceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Account")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("CurrentBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PrevBalance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("ReferenceId");

                    b.HasIndex("UserId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Utilities.Models.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("varchar(500)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Utilities.Models.Transaction", b =>
                {
                    b.HasOne("Utilities.Models.User", null)
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Utilities.Models.User", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}