using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PraktikaAPI.Models;

public partial class PraktikaDbContext : DbContext
{
    public PraktikaDbContext()
    {
    }

    public PraktikaDbContext(DbContextOptions<PraktikaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Buyer> Buyers { get; set; }

    public virtual DbSet<Color> Colors { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Individual> Individuals { get; set; }

    public virtual DbSet<LegalEntity> LegalEntities { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<PriceCategory> PriceCategories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductGroup> ProductGroups { get; set; }

    public virtual DbSet<ProductPriceCategory> ProductPriceCategorys { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supply> Supplies { get; set; }

    public virtual DbSet<SupplyProduct> SupplyProducts { get; set; }

    public virtual DbSet<Textile> Textiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=PraktikaDb;Username=postgres;Password=Qazqaz");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.HasKey(e => e.BuyerId).HasName("Buyer_pkey");

            entity.ToTable("Buyer");

            entity.HasIndex(e => e.IndividualId, "Buyer_individualid_key").IsUnique();

            entity.HasIndex(e => e.LegalEntityId, "Buyer_legalentityid_key").IsUnique();

            entity.Property(e => e.BuyerId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Address).HasMaxLength(100);

            entity.HasOne(d => d.Individual).WithOne(p => p.Buyer)
                .HasForeignKey<Buyer>(d => d.IndividualId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Buyer_individualid_fkey");

            entity.HasOne(d => d.LegalEntity).WithOne(p => p.Buyer)
                .HasForeignKey<Buyer>(d => d.LegalEntityId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Buyer_legalentityid_fkey");
        });

        modelBuilder.Entity<Color>(entity =>
        {
            entity.HasKey(e => e.ColorId).HasName("Color_pkey");

            entity.ToTable("Color");

            entity.Property(e => e.ColorId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("Employee_pkey");

            entity.ToTable("Employee");

            entity.Property(e => e.EmployeeId).UseIdentityAlwaysColumn();
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(300);

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("Employee_RoleId_fkey");
        });

        modelBuilder.Entity<Individual>(entity =>
        {
            entity.HasKey(e => e.IndividualId).HasName("Individual_pkey");

            entity.ToTable("Individual");

            entity.HasIndex(e => e.SeriesPassportNumber, "Individual_SeriesPassportNumber_key").IsUnique();

            entity.Property(e => e.IndividualId).UseIdentityAlwaysColumn();
            entity.Property(e => e.FullName).HasMaxLength(300);
            entity.Property(e => e.Phone).HasMaxLength(300);
            entity.Property(e => e.SeriesPassportNumber).HasMaxLength(300);
        });

        modelBuilder.Entity<LegalEntity>(entity =>
        {
            entity.HasKey(e => e.LegalEntityId).HasName("LegalEntity_pkey");

            entity.ToTable("LegalEntity");

            entity.Property(e => e.LegalEntityId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Bank).HasMaxLength(300);
            entity.Property(e => e.Bic)
                .HasMaxLength(300)
                .HasColumnName("BIC");
            entity.Property(e => e.CheckingAccount).HasMaxLength(300);
            entity.Property(e => e.CorrespondentAccount).HasMaxLength(300);
            entity.Property(e => e.Organization).HasMaxLength(300);
            entity.Property(e => e.Phone).HasMaxLength(300);
            entity.Property(e => e.Rrc)
                .HasMaxLength(300)
                .HasColumnName("RRC");
            entity.Property(e => e.Tin)
                .HasMaxLength(300)
                .HasColumnName("TIN");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("Order_pkey");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Assembly).HasColumnType("money");
            entity.Property(e => e.Delivery).HasColumnType("money");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Buyer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BuyerId)
                .HasConstraintName("Order_BuyerId_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("Order_EmployeeId_fkey");

            entity.HasMany(d => d.SupplyProducts).WithMany(p => p.Orders)
                .UsingEntity<Dictionary<string, object>>(
                    "OrderProduct",
                    r => r.HasOne<SupplyProduct>().WithMany()
                        .HasForeignKey("SupplyProductsId")
                        .HasConstraintName("OrderProducts_SupplyProductsId_fkey"),
                    l => l.HasOne<Order>().WithMany()
                        .HasForeignKey("OrderId")
                        .HasConstraintName("OrderProducts_OrderId_fkey"),
                    j =>
                    {
                        j.HasKey("OrderId", "SupplyProductsId").HasName("OrderProducts_pkey");
                        j.ToTable("OrderProducts");
                    });
        });

        modelBuilder.Entity<PriceCategory>(entity =>
        {
            entity.HasKey(e => e.PriceCategoryId).HasName("PriceCategory_pkey");

            entity.ToTable("PriceCategory");

            entity.Property(e => e.PriceCategoryId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Category).HasMaxLength(2);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("Products_pkey");

            entity.Property(e => e.ProductId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Dimensions).HasMaxLength(20);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Color).WithMany(p => p.Products)
                .HasForeignKey(d => d.ColorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Products_ColorId_fkey");

            entity.HasOne(d => d.ProductType).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductTypeId)
                .HasConstraintName("Products_ProductTypeId_fkey");
        });

        modelBuilder.Entity<ProductGroup>(entity =>
        {
            entity.HasKey(e => e.ProductGroupId).HasName("ProductGroup_pkey");

            entity.ToTable("ProductGroup");

            entity.Property(e => e.ProductGroupId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ProductPriceCategory>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.PriceCategoryId }).HasName("ProductPriceCategorys_pkey");

            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.PriceCategory).WithMany(p => p.ProductPriceCategories)
                .HasForeignKey(d => d.PriceCategoryId)
                .HasConstraintName("ProductPriceCategorys_PriceCategoryId_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPriceCategories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("ProductPriceCategorys_ProductId_fkey");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.ProductTypeId).HasName("ProductType_pkey");

            entity.ToTable("ProductType");

            entity.Property(e => e.ProductTypeId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.ProductGroup).WithMany(p => p.ProductTypes)
                .HasForeignKey(d => d.ProductGroupId)
                .HasConstraintName("ProductType_ProductGroupId_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Supply>(entity =>
        {
            entity.HasKey(e => e.SupplyId).HasName("Supply_pkey");

            entity.ToTable("Supply");

            entity.Property(e => e.SupplyId).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<SupplyProduct>(entity =>
        {
            entity.HasKey(e => e.SupplyProductsId).HasName("SupplyProducts_pkey");

            entity.Property(e => e.SupplyProductsId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Status).HasMaxLength(30);

            entity.HasOne(d => d.Product).WithMany(p => p.SupplyProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("SupplyProducts_ProductId_fkey");

            entity.HasOne(d => d.Supply).WithMany(p => p.SupplyProducts)
                .HasForeignKey(d => d.SupplyId)
                .HasConstraintName("SupplyProducts_SupplyId_fkey");

            entity.HasOne(d => d.Textile).WithMany(p => p.SupplyProducts)
                .HasForeignKey(d => d.TextileId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("SupplyProducts_TextileId_fkey");
        });

        modelBuilder.Entity<Textile>(entity =>
        {
            entity.HasKey(e => e.TextileId).HasName("Textile_pkey");

            entity.ToTable("Textile");

            entity.Property(e => e.TextileId).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.PriceCategory).WithMany(p => p.Textiles)
                .HasForeignKey(d => d.PriceCategoryId)
                .HasConstraintName("Textile_PriceCategoryId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
