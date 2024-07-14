using Entites;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class EmployeeDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //optionsBuilder.UseSqlServer(@"Server=localhost;Database=IK;Trusted_Connection=True;");
        }

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            


            modelBuilder.Entity<tblEmployees>().HasKey(u => u.ID);



        }
        public DbSet<tblEmployees> tblEmployees { get; set; }

    }
}
