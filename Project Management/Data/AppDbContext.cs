using System;
using Microsoft.EntityFrameworkCore;
using Project_Management.Models.Domains;

namespace Project_Management.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{
		}

        /*
		 * Many to many additional configuration
		 * 
		 */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			/*
			 * Many to Many (Project - User)
			 * 
			 */
			modelBuilder.Entity<ProjectUser>()
				.HasOne(p => p.Project)
				.WithMany(pu => pu.ProjectUsers)
				.HasForeignKey(pu => pu.ProjectId)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<ProjectUser>()
				.HasOne(u => u.User)
				.WithMany(pu => pu.ProjectUsers)
				.HasForeignKey(pu => pu.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<User> Users { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<ProjectUser> ProjectUsers { get; set; }
		public DbSet<Models.Domains.Task> Tasks { get; set; }
	}
}

