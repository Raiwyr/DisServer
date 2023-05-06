﻿// <auto-generated />
using System;
using DatabaseController;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DatabaseController.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230430130120_AddWorker")]
    partial class AddWorker
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("ContraindicationProduct", b =>
                {
                    b.Property<int>("ContraindicationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ContraindicationId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("ContraindicationProduct");
                });

            modelBuilder.Entity("DatabaseController.Models.Availability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Price");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Availabilitys", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.Contraindication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Contraindications", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Genders", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.Indication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Indications", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<int>("GrandTotal")
                        .HasColumnType("INTEGER")
                        .HasColumnName("GrandTotal");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("OrderDate");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("OrderStatus");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.OrderProductInfo", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Price");

                    b.Property<int>("ProductQuantity")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ProductQuantity");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProductInfo");
                });

            modelBuilder.Entity("DatabaseController.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Composition")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Composition");

                    b.Property<string>("Dosage")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Dosage");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("ExpirationDate");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("QuantityPackage")
                        .HasColumnType("INTEGER")
                        .HasColumnName("QuantityPackage");

                    b.Property<int>("ReleaseFormId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("ReleaseFormId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.ReleaseForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("ReleaseForms", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<int>("Assessment")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Assessment");

                    b.Property<DateTime>("DateReview")
                        .HasColumnType("TEXT")
                        .HasColumnName("DateReview");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Message");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserId");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("UserName");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Reviews", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.ShoppingCart", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ProductId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCart");
                });

            modelBuilder.Entity("DatabaseController.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Password");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.UserInfo", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT")
                        .HasColumnName("BirthDate");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("FullName");

                    b.Property<int>("GenderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Phone");

                    b.HasKey("Id");

                    b.HasIndex("GenderId");

                    b.ToTable("UserInfos", (string)null);
                });

            modelBuilder.Entity("DatabaseController.Models.Worker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("Id");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("FullName");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("Password");

                    b.HasKey("Id");

                    b.ToTable("Workers", (string)null);
                });

            modelBuilder.Entity("IndicationProduct", b =>
                {
                    b.Property<int>("IndicationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("IndicationId", "ProductsId");

                    b.HasIndex("ProductsId");

                    b.ToTable("IndicationProduct");
                });

            modelBuilder.Entity("ContraindicationProduct", b =>
                {
                    b.HasOne("DatabaseController.Models.Contraindication", null)
                        .WithMany()
                        .HasForeignKey("ContraindicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseController.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DatabaseController.Models.Availability", b =>
                {
                    b.HasOne("DatabaseController.Models.Product", "Product")
                        .WithOne("Availability")
                        .HasForeignKey("DatabaseController.Models.Availability", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DatabaseController.Models.Order", b =>
                {
                    b.HasOne("DatabaseController.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DatabaseController.Models.OrderProductInfo", b =>
                {
                    b.HasOne("DatabaseController.Models.Order", "Order")
                        .WithMany("OrderProductInfos")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseController.Models.Product", "Product")
                        .WithMany("OrderProductInfos")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DatabaseController.Models.Product", b =>
                {
                    b.HasOne("DatabaseController.Models.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseController.Models.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseController.Models.ReleaseForm", "ReleaseForm")
                        .WithMany("Products")
                        .HasForeignKey("ReleaseFormId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");

                    b.Navigation("ProductType");

                    b.Navigation("ReleaseForm");
                });

            modelBuilder.Entity("DatabaseController.Models.Review", b =>
                {
                    b.HasOne("DatabaseController.Models.Product", "Product")
                        .WithMany("Review")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DatabaseController.Models.ShoppingCart", b =>
                {
                    b.HasOne("DatabaseController.Models.Product", "Product")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseController.Models.User", "User")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DatabaseController.Models.UserInfo", b =>
                {
                    b.HasOne("DatabaseController.Models.Gender", "Gender")
                        .WithMany("UserInfos")
                        .HasForeignKey("GenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseController.Models.User", "User")
                        .WithOne("UserInfo")
                        .HasForeignKey("DatabaseController.Models.UserInfo", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gender");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IndicationProduct", b =>
                {
                    b.HasOne("DatabaseController.Models.Indication", null)
                        .WithMany()
                        .HasForeignKey("IndicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DatabaseController.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DatabaseController.Models.Gender", b =>
                {
                    b.Navigation("UserInfos");
                });

            modelBuilder.Entity("DatabaseController.Models.Manufacturer", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("DatabaseController.Models.Order", b =>
                {
                    b.Navigation("OrderProductInfos");
                });

            modelBuilder.Entity("DatabaseController.Models.Product", b =>
                {
                    b.Navigation("Availability")
                        .IsRequired();

                    b.Navigation("OrderProductInfos");

                    b.Navigation("Review");

                    b.Navigation("ShoppingCarts");
                });

            modelBuilder.Entity("DatabaseController.Models.ProductType", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("DatabaseController.Models.ReleaseForm", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("DatabaseController.Models.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ShoppingCarts");

                    b.Navigation("UserInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
