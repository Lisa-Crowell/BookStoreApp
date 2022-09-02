﻿// <auto-generated />
using System;
using BookStoreApp.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStoreApp.API.Migrations
{
    [DbContext(typeof(BookStoreDbContext))]
    partial class BookStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BookStoreApp.API.Data.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bio")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)");

                    b.Property<string>("LastName")
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookStoreApp.API.Data.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AuthorId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)")
                        .HasColumnName("ISBN");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(10,2)");

                    b.Property<string>("Summary")
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Title")
                        .HasMaxLength(55)
                        .HasColumnType("varchar(55)");

                    b.Property<int?>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex(new[] { "Isbn" }, "UQ__Books__447D36EA09FAB742")
                        .IsUnique();

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookStoreApp.API.Data.Book", b =>
                {
                    b.HasOne("BookStoreApp.API.Data.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .HasConstraintName("FK_Books_ToTable");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("BookStoreApp.API.Data.Author", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
