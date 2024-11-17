using GICEmployee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GICEmployee.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Cafe> Cafes { get; set; }

        public DbSet<EmployeeCafeRelationship> EmployeeCafeRelationships { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Configure the model relationships and validations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Employee to EmployeeCafeRelationship (1:1)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.EmployeeCafeRelationship)
                .WithOne(er => er.Employee)
                .HasForeignKey<EmployeeCafeRelationship>(er => er.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cafe to EmployeeCafeRelationship (1:M)
            modelBuilder.Entity<Cafe>()
                .HasMany(c => c.EmployeeCafeRelationships)
                .WithOne(er => er.Cafe)
                .HasForeignKey(er => er.CafeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Composite key for EmployeeCafeRelationship
            modelBuilder.Entity<EmployeeCafeRelationship>()
                .HasKey(er => new { er.EmployeeId, er.CafeId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
