﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SaluScanner.Repository.DbContexts;

#nullable disable

namespace SaluScanner.Repository.Migrations
{
    [DbContext(typeof(SqlServerDbContext))]
    [Migration("20221124104129_Entity_Refactor2")]
    partial class EntityRefactor2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContentProduct", b =>
                {
                    b.Property<int>("ContentsId")
                        .HasColumnType("int");

                    b.Property<int>("ProductsId")
                        .HasColumnType("int");

                    b.HasKey("ContentsId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ContentProduct");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Allergen", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AllergenDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AllergenName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Allergens");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Certificate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CertificateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Content", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AllergenId")
                        .HasColumnType("int");

                    b.Property<string>("ComponentDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ComponentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAnimalProduct")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AllergenId");

                    b.ToTable("Contents");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Nutrition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Calori")
                        .HasColumnType("real");

                    b.Property<float>("Carbonhydrate")
                        .HasColumnType("real");

                    b.Property<float>("Fat")
                        .HasColumnType("real");

                    b.Property<float>("Protein")
                        .HasColumnType("real");

                    b.Property<float>("VitamineA")
                        .HasColumnType("real");

                    b.Property<float>("VitamineB")
                        .HasColumnType("real");

                    b.Property<float>("VitamineC")
                        .HasColumnType("real");

                    b.Property<float>("VitamineD")
                        .HasColumnType("real");

                    b.Property<float>("VitamineE")
                        .HasColumnType("real");

                    b.Property<float>("VitamineK")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Nutritions");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Barcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("NutritionId")
                        .HasColumnType("int");

                    b.Property<int>("ProductDetailId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("NutritionId");

                    b.HasIndex("ProductDetailId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.ProductDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductOrigin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductDetails");
                });

            modelBuilder.Entity("ContentProduct", b =>
                {
                    b.HasOne("SaluScanner.Core.Entities.Content", null)
                        .WithMany()
                        .HasForeignKey("ContentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SaluScanner.Core.Entities.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Certificate", b =>
                {
                    b.HasOne("SaluScanner.Core.Entities.Product", null)
                        .WithMany("Certificates")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Content", b =>
                {
                    b.HasOne("SaluScanner.Core.Entities.Allergen", "Allergen")
                        .WithMany()
                        .HasForeignKey("AllergenId");

                    b.Navigation("Allergen");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Product", b =>
                {
                    b.HasOne("SaluScanner.Core.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SaluScanner.Core.Entities.Nutrition", "Nutrition")
                        .WithMany()
                        .HasForeignKey("NutritionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SaluScanner.Core.Entities.ProductDetail", "ProductDetail")
                        .WithMany()
                        .HasForeignKey("ProductDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Nutrition");

                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("SaluScanner.Core.Entities.Product", b =>
                {
                    b.Navigation("Certificates");
                });
#pragma warning restore 612, 618
        }
    }
}
