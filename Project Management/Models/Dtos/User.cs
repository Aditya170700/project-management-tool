using System;
namespace Project_Management.Models.Dtos
{
	public class User
	{
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Photo { get; set; }
    }
}

