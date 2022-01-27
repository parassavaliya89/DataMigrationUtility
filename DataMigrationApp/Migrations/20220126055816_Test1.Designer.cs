﻿// <auto-generated />
using DataMigrationApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataMigration.Migrations
{
    [DbContext(typeof(CommonContext))]
    [Migration("20220126055816_Test1")]
    partial class Test1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("DataMigrationApp.Models.DestinationModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("SourceModelId")
                        .HasColumnType("int");

                    b.Property<int>("Sum")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SourceModelId")
                        .IsUnique();

                    b.ToTable("Destination");
                });

            modelBuilder.Entity("DataMigrationApp.Models.MigrationStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("From")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasMaxLength(100)
                        .HasColumnType("int");

                    b.Property<int>("To")
                        .HasMaxLength(50)
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MigrationStatuses");
                });

            modelBuilder.Entity("DataMigrationApp.Models.SourceModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("FirstNumber")
                        .HasColumnType("int");

                    b.Property<int>("SecondNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Source");
                });

            modelBuilder.Entity("DataMigrationApp.Models.DestinationModel", b =>
                {
                    b.HasOne("DataMigrationApp.Models.SourceModel", "Source")
                        .WithOne("Destination")
                        .HasForeignKey("DataMigrationApp.Models.DestinationModel", "SourceModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");
                });

            modelBuilder.Entity("DataMigrationApp.Models.SourceModel", b =>
                {
                    b.Navigation("Destination");
                });
#pragma warning restore 612, 618
        }
    }
}
