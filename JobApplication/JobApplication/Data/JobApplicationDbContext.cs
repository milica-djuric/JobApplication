using JobApplication.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobApplication.Data
{
    public class JobApplicationDbContext : DbContext
    {
        public JobApplicationDbContext(DbContextOptions<JobApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Position
            modelBuilder.Entity<Position>().HasKey(p => new { p.PositionID, p.CompanyID });
            modelBuilder.Entity<Position>().HasOne(c => c.Company).WithMany(p => p.Positions).HasForeignKey(
                fk => fk.CompanyID).OnDelete(DeleteBehavior.Cascade);
            //Application
            modelBuilder.Entity<Application>().HasKey(a => new { a.CandidateID, a.PositionID, a.CompanyID });
            modelBuilder.Entity<Application>().HasOne(c => c.Candidate).WithMany(a => a.Applications).HasForeignKey(
                fk => fk.CandidateID).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Application>().HasOne(p => p.Position).WithMany(a => a.Applications).HasForeignKey(
                fk => new { fk.PositionID, fk.CompanyID }).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
