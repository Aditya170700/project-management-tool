using System;
using Project_Management.Models.Domains;

namespace Project_Management.Repositories
{
	public interface IUserRepository
	{
        Task<Models.Dtos.Paginated<User>> GetAllAsync(string field, string sort, string search, int pageNumber, int pageSize);
    }
}

