using System;
using Microsoft.EntityFrameworkCore;
using Project_Management.Data;
using Project_Management.Models;
using Project_Management.Models.Domains;

namespace Project_Management.Repositories
{
	public class UserRepository : IUserRepository
	{
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
		{
            _appDbContext = appDbContext;
        }

        public async Task<Models.Dtos.Paginated<User>> GetAllAsync(
            string field,
            string sort,
            string search,
            int pageNumber,
            int pageSize
        )
        {
            IQueryable<User> query = _appDbContext.Users;

            if (!string.IsNullOrEmpty(field))
            {
                if (!string.IsNullOrEmpty(sort))
                {
                    query = query.OrderByProperty(field, sort.ToLower() == "desc");
                }

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.SearchByProperty(field, search);
                }
            }

            Models.Dtos.Paginated<User> results = await query.Paginate(pageNumber, pageSize);

            return results;
        }
    }
}

