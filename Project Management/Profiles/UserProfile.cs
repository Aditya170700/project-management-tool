
using System;
using AutoMapper;

namespace Project_Management.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<Models.Domains.User, Models.Dtos.User>()
				.ReverseMap();
		}
	}
}

