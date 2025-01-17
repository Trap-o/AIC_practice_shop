﻿using Microsoft.EntityFrameworkCore;

namespace AIC_shop
{
    internal class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = UserData.db");
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Receipt>()
                .HasMany(c => c.Products)
                .WithMany(s => s.Receipts)
                .UsingEntity<ReceiptProduct>(
                    j => j
                    .HasOne(pt => pt.Product)
                    .WithMany(t => t.ReceiptProduct)
                    .HasForeignKey(pt => pt.ProductId),
                j => j
                .HasOne(pt => pt.Receipt)
                .WithMany(t => t.ReceiptProduct)
                .HasForeignKey(pt => pt.ReceiptId)
                );
        }
    }
}
