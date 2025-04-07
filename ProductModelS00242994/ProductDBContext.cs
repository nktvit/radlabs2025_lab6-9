using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using ProductModel.GRN;

namespace ProductModel
{
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<GRN.GRN> GRNs { get; set; }
        public DbSet<GRNLine> GRNLines { get; set; }

        static public bool inProduction;

        public ProductDBContext()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite connection string for macOS
            var connectionString = "Data Source=Week8ProductCoreDB-2025-S00242994.db";
            optionsBuilder.UseSqlite(connectionString)
                .LogTo(Console.WriteLine,
                    new[] { DbLoggerCategory.Database.Command.Name },
                    LogLevel.Information);
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // NOTE: this line is activated from the bin folder whihc is a sub
            // folder of the class library project
            // You must build the project before calling Add-migration
            if (!inProduction)
            {
                Product[] products = DBHelper.Get<Product>(@"../ProductModelS00242994/Products.csv").ToArray();
                modelBuilder.Entity<Product>().HasData(products);

                // Set up relationship between GRN and GRNLine
                modelBuilder.Entity<GRNLine>()
                    .HasOne(gl => gl.parentGRN)
                    .WithMany(g => g.GRNLines)
                    .HasForeignKey(gl => gl.GrnID);

                // Set up relationship between GRNLine and Product
                modelBuilder.Entity<GRNLine>()
                    .HasOne(gl => gl.associatedProduct)
                    .WithMany()
                    .HasForeignKey(gl => gl.StockID);
            }

            base.OnModelCreating(modelBuilder);
        }


    }
}


