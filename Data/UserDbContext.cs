using Entites;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class UserDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            //optionsBuilder.UseSqlServer(@"Server=localhost;Database=IK;Trusted_Connection=True;");
        }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)

        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<tblUsers>().HasKey(u => u.ID);
            modelBuilder.Entity<tblUsersRoles>().HasKey(r => r.ID);
            modelBuilder.Entity<tblUsers>()
            .HasOne(u => u.tblUsersRoles)
            .WithMany(r => r.tblUsers)
            .HasForeignKey(u => u.Role);


            modelBuilder.Entity<ViewUsersAndRoleDetails>().HasKey(u => u.ID);



        }
        public DbSet<tblUsers> tblUsers { get; set; }
        public DbSet<tblUsersRoles> tblUsersRoles { get; set; }

        public DbSet<ViewUsersAndRoleDetails> ViewUsersAndRoleDetails { get; set; }
    }
}
