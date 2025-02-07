using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using VideoShopRentalV3.Models;

namespace VideoShopRentalV3.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }
        public DbSet<RentalHeader> RentalHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // RentalHeader -> Customer (One-to-Many)
            modelBuilder.Entity<RentalHeader>()
                        .HasOne(r => r.Customer)
                        .WithMany(c => c.RentalHeaders)
                        .HasForeignKey(r => r.CustomerId)
                        .OnDelete(DeleteBehavior.Restrict); // Better to avoid cascading deletes for Customers

            // RentalDetail -> RentalHeader (One-to-Many)
            modelBuilder.Entity<RentalDetail>()
                .HasOne(d => d.RentalHeader)
                .WithMany(r => r.RentalDetails)
                .HasForeignKey(d => d.RentalHeaderId)
                .OnDelete(DeleteBehavior.Cascade); // Ensure that deleting a RentalHeader removes RentalDetails

            // RentalDetail -> Movie (Many-to-One)
            modelBuilder.Entity<RentalDetail>()
                .HasOne(d => d.Movie)
                .WithMany(m => m.RentalDetails)
                .HasForeignKey(d => d.MovieId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascading deletions for movies

            // RentalPrice Precision
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(r => r.RentalPrice)
                      .HasPrecision(10, 2);
            });
        }

    }
}

