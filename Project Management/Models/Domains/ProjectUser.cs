using System;
namespace Project_Management.Models.Domains
{
	public class ProjectUser
	{
		public Guid Id { get; set; }
		public Guid ProjectId { get; set; }
		public Guid UserId { get; set; }
		public Project Project { get; set; }
		public User User { get; set; }

		public IEnumerable<Models.Domains.Task> Tasks { get; set; }
	}
}

