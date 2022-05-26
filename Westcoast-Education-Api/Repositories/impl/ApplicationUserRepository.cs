using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.ApplicationUser;

namespace Westcoast_Education_Api.Repositories.impl
{
    public class ApplicationUserRepository : IApplicationUserRepository
    {
        private readonly ApplicationContext _context;
        private readonly IMapper _mapper;

        public ApplicationUserRepository(ApplicationContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task CreateUserAsync(PostApplicationUserViewModel model)
        {
            // TODO implement AutoMapper


            var userToAdd = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            if (userToAdd is null)
            {
                throw new Exception($"Could not add user: {model.Email} to the system");
            }

            await _context.ApplicationUsers.AddAsync(userToAdd);
        }

        public Task DeleteUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ApplicationUserViewModel>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUserViewModel> GetUserAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ApplicationUserWithCoursesViewModel>> GetUsersWithCoursesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}