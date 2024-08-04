﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TravelExperts.DataAccess.Models;
using TravelExperts.Models.ViewModel;


namespace TravelExperts.DataAccess.Data;

public partial class TravelExpertsContext : DbContext
{
    public TravelExpertsContext(DbContextOptions<TravelExpertsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Package> Packages { get; set; }

    public virtual DbSet<PackagesProductsSupplier> PackagesProductsSuppliers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductsSupplier> ProductsSuppliers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId)
                .HasName("aaaaaCustomers_PK")
                .IsClustered(false);

            entity.HasIndex(e => e.AgentId, "EmployeesCustomers");

            entity.Property(e => e.CustAddress)
                .IsRequired()
                .HasMaxLength(75);
            entity.Property(e => e.CustBusPhone)
                .IsRequired()
                .HasMaxLength(20);
            entity.Property(e => e.CustCity)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CustCountry).HasMaxLength(25);
            entity.Property(e => e.CustEmail)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.CustFirstName)
                .IsRequired()
                .HasMaxLength(25);
            entity.Property(e => e.CustHomePhone).HasMaxLength(20);
            entity.Property(e => e.CustLastName)
                .IsRequired()
                .HasMaxLength(25);
            entity.Property(e => e.CustPostal)
                .IsRequired()
                .HasMaxLength(7);
            entity.Property(e => e.CustProv)
                .IsRequired()
                .HasMaxLength(2);
        });

        modelBuilder.Entity<Package>(entity =>
        {
            entity.HasKey(e => e.PackageId)
                .HasName("aaaaaPackages_PK")
                .IsClustered(false);

            entity.Property(e => e.PkgAgencyCommission)
                .HasDefaultValue(0m)
                .HasColumnType("money");
            entity.Property(e => e.PkgBasePrice).HasColumnType("money");
            entity.Property(e => e.PkgDesc).HasMaxLength(50);
            entity.Property(e => e.PkgEndDate).HasColumnType("datetime");
            entity.Property(e => e.PkgName)
                .IsRequired()
                .HasMaxLength(50);
            entity.Property(e => e.PkgStartDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PackagesProductsSupplier>(entity =>
        {
            entity.HasKey(e => e.PackageProductSupplierId).HasName("PK__Packages__53E8ED99B65B5F2C");

            entity.ToTable("Packages_Products_Suppliers");

            entity.HasIndex(e => e.PackageId, "PackagesPackages_Products_Suppliers");

            entity.HasIndex(e => e.ProductSupplierId, "ProductSupplierId");

            entity.HasIndex(e => e.ProductSupplierId, "Products_SuppliersPackages_Products_Suppliers");

            entity.HasIndex(e => new { e.PackageId, e.ProductSupplierId }, "UQ__Packages__29CA8E9589C9A9DF").IsUnique();

            entity.HasOne(d => d.Package).WithMany(p => p.PackagesProductsSuppliers)
                .HasForeignKey(d => d.PackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Packages_Products_Supplie_FK00");

            entity.HasOne(d => d.ProductSupplier).WithMany(p => p.PackagesProductsSuppliers)
                .HasForeignKey(d => d.ProductSupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Packages_Products_Supplie_FK01");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId)
                .HasName("aaaaaProducts_PK")
                .IsClustered(false);

            entity.HasIndex(e => e.ProductId, "ProductId");

            entity.Property(e => e.ProdName)
                .IsRequired()
                .HasMaxLength(50);
        });

        modelBuilder.Entity<ProductsSupplier>(entity =>
        {
            entity.HasKey(e => e.ProductSupplierId)
                .HasName("aaaaaProducts_Suppliers_PK")
                .IsClustered(false);

            entity.ToTable("Products_Suppliers");

            entity.HasIndex(e => e.SupplierId, "Product Supplier ID");

            entity.HasIndex(e => e.ProductId, "ProductId");

            entity.HasIndex(e => e.ProductSupplierId, "ProductSupplierId");

            entity.HasIndex(e => e.ProductId, "ProductsProducts_Suppliers1");

            entity.HasIndex(e => e.SupplierId, "SuppliersProducts_Suppliers1");

            entity.Property(e => e.ProductId).HasDefaultValue(0);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductsSuppliers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("Products_Suppliers_FK00");

            entity.HasOne(d => d.Supplier).WithMany(p => p.ProductsSuppliers)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("Products_Suppliers_FK01");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId)
                .HasName("aaaaaSuppliers_PK")
                .IsClustered(false);

            entity.HasIndex(e => e.SupplierId, "SupplierId");

            entity.Property(e => e.SupName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}