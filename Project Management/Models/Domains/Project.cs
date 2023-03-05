using System;
namespace Project_Management.Models.Domains
{
	public class Project
	{
		public Guid Id { get; set; }
		public string Title { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }

		public IEnumerable<ProjectUser> ProjectUsers { get; set; }
	}
}

