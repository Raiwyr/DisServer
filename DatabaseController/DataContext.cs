using DatabaseController.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseController
{
    public class DataContext : DbContext
    {
        #region Tables
        public DbSet<User> Users { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ReleaseForm> ReleaseForms { get; set; }
        public DbSet<Indication> Indications { get; set; }
        public DbSet<Contraindication> Contraindications { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Availability> Availabilitys { get; set; }
        public DbSet<Review> Reviews { get; set; }
        #endregion

        public DataContext()
        {
            Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=disdb.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(User.Id));

                entity.Property(e => e.Login)
                    .HasColumnName(nameof(User.Login))
                    .IsRequired();

                entity.Property(e => e.Password)
                    .HasColumnName(nameof(User.Password))
                    .IsRequired();

                entity.HasOne(e => e.UserInfo)
                    .WithOne(e => e.User)
                    .HasForeignKey<UserInfo>(e => e.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Orders)
                    .WithOne(e => e.User)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.ToTable("UserInfos");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(UserInfo.Id));

                entity.Property(e => e.FullName)
                    .HasColumnName(nameof(UserInfo.FullName))
                    .IsRequired();

                entity.Property(e => e.BirthDate)
                    .HasColumnName(nameof(UserInfo.BirthDate))
                    .IsRequired();

                entity.Property(e => e.Phone)
                    .HasColumnName(nameof(UserInfo.Phone))
                    .IsRequired();

                entity.HasOne(e => e.Gender)
                    .WithMany(e => e.UserInfos)
                    .HasForeignKey(e => e.GenderId);
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.ToTable("Genders");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(Gender.Id));

                entity.Property(e => e.Name)
                    .HasColumnName(nameof(Gender.Name))
                    .IsRequired();
            });


            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(Product.Id));

                entity.Property(e => e.Name)
                    .HasColumnName(nameof(Product.Name))
                    .IsRequired();

                entity.Property(e => e.Composition)
                    .HasColumnName(nameof(Product.Composition))
                    .IsRequired();

                entity.Property(e => e.Dosage)
                    .HasColumnName(nameof(Product.Dosage))
                    .IsRequired();

                entity.Property(e => e.QuantityPackage)
                    .HasColumnName(nameof(Product.QuantityPackage))
                    .IsRequired();

                entity.Property(e => e.ExpirationDate)
                    .HasColumnName(nameof(Product.ExpirationDate))
                    .IsRequired();

                entity.HasOne(e => e.ProductType)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.ProductTypeId);

                entity.HasOne(e => e.ReleaseForm)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.ReleaseFormId);

                entity.HasMany(e => e.Indication)
                    .WithMany(e => e.Products);

                entity.HasMany(e => e.Contraindication)
                    .WithMany(e => e.Products);

                entity.HasOne(e => e.Manufacturer)
                    .WithMany(e => e.Products)
                    .HasForeignKey(e => e.ManufacturerId);

                entity.HasOne(e => e.Availability)
                    .WithOne(e => e.Product)
                    .HasForeignKey<Availability>(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Review)
                    .WithOne(e => e.Product)
                    .HasForeignKey(e => e.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductTypes");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(ProductType.Id));

                entity.Property(e => e.Name)
                    .HasColumnName(nameof(ProductType.Name))
                    .IsRequired();
            });

            modelBuilder.Entity<ReleaseForm>(entity =>
            {
                entity.ToTable("ReleaseForms");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(ReleaseForm.Id));

                entity.Property(e => e.Name)
                    .HasColumnName(nameof(ReleaseForm.Name))
                    .IsRequired();
            });

            modelBuilder.Entity<Indication>(entity =>
            {
                entity.ToTable("Indications");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(Indication.Id));

                entity.Property(e => e.Name)
                    .HasColumnName(nameof(Indication.Name))
                    .IsRequired();
            });

            modelBuilder.Entity<Contraindication>(entity =>
            {
                entity.ToTable("Contraindications");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(Contraindication.Id));

                entity.Property(e => e.Name)
                    .HasColumnName(nameof(Contraindication.Name))
                    .IsRequired();
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.ToTable("Manufacturers");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(Manufacturer.Id));

                entity.Property(e => e.Name)
                    .HasColumnName(nameof(Manufacturer.Name))
                    .IsRequired();
            });

            modelBuilder.Entity<Availability>(entity =>
            {
                entity.ToTable("Availabilitys");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(Availability.Id));

                entity.Property(e => e.Quantity)
                    .HasColumnName(nameof(Availability.Quantity))
                    .IsRequired();

                entity.Property(e => e.Price)
                    .HasColumnName(nameof(Availability.Price))
                    .IsRequired();
            });


            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(Order.Id));

                entity.Property(e => e.OrderDate)
                    .HasColumnName(nameof(Order.OrderDate))
                    .IsRequired();

                entity.Property(e => e.OrderStatus)
                    .HasColumnName(nameof(Order.OrderStatus))
                    .IsRequired();

                entity.HasOne(e => e.User)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.UserId);

                entity.HasOne(e => e.PaymentType)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.PaymentTypeId);

                entity.HasMany(e => e.Products)
                    .WithMany(e => e.Orders)
                    .UsingEntity<OrderProductInfo>(
                        e => e.HasOne(e => e.Product)
                            .WithMany(e => e.OrderProductInfos)
                            .HasForeignKey(e => e.ProductId),
                        e => e.HasOne(e => e.Order)
                            .WithMany(e => e.OrderProductInfos)
                            .HasForeignKey(e => e.OrderId),
                        e =>
                        {
                            e.Property(e => e.ProductQuantity);
                            e.HasKey(e => new { e.OrderId, e.ProductId });
                        }
                        );
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentTypes");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(PaymentType.Id));

                entity.Property(e => e.Name)
                    .HasColumnName(nameof(PaymentType.Name))
                    .IsRequired();
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("Reviews");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName(nameof(Review.Id));

                entity.Property(e => e.Assessment)
                    .HasColumnName(nameof(Review.Assessment))
                    .IsRequired();

                entity.Property(e => e.Message)
                    .HasColumnName(nameof(Review.Message))
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .HasColumnName(nameof(Review.UserId))
                    .IsRequired();

                entity.Property(e => e.UserName)
                    .HasColumnName(nameof(Review.UserName))
                    .IsRequired();

                entity.Property(e => e.DateReview)
                    .HasColumnName(nameof(Review.DateReview))
                    .IsRequired();
            });
        }
    }
}
