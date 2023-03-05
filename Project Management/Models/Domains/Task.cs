using System;
namespace Project_Management.Models.Domains
{
	public class Task
	{
		public Guid Id { get; set; }
		public string Detail { get; set; }
		public DateTime Expired { get; set; }
		public Guid ProjectUserId { get; set; }
        public ProjectUser ProjectUser{ get; set; }
	}
}

