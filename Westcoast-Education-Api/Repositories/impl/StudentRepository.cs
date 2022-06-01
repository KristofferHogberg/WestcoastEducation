using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Westcoast_Education_Api.Data;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.Repositories.Interfaces;
using Westcoast_Education_Api.ViewModels.Student;

namespace Westcoast_Education_Api.Repositories.impl
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public StudentRepository(ApplicationContext context, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        public async Task<List<StudentViewModel>> GetAllStudentsAsync()
        {
            return await _context.ApplicationUsers
                .Include(u => u.Student)
                .ThenInclude(u => u!.ApplicationUser!.Address)
                .Where(u => u.StudentId != null)
                .ProjectTo<StudentViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task<List<StudentWithCoursesViewModel>> GetCourseStudentsRegistriesAsync()
        {
            return await _context.CourseStudents
                .Include(s => s.Course)
                .ThenInclude(u => u!.Students)
                .ThenInclude(s => s!.ApplicationUser)
                .ProjectTo<StudentWithCoursesViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }
        public async Task<IdentityResult> CreateStudentAsync(PostStudentViewModel model)
        {
            var student = _mapper.Map<Student>(model);
            var address = _mapper.Map<Address>(model);
            var appUser = _mapper.Map<ApplicationUser>(model);

            appUser.Student = student;
            appUser.Address = address;

            if (appUser is null)
            {
                throw new Exception($"Could not create student {model.Email}");
            }

            return await _userManager.CreateAsync(appUser);
        }
        public async Task UpdateStudentAsync(int id, PatchStudentViewModel model)
        {
            var appUser = await _context.ApplicationUsers
                .Include(u => u.Address)
                .Where(s => s.StudentId == id)
                .SingleOrDefaultAsync();

            if (appUser is null)
            {
                throw new Exception($"Could not create student {model.Email}");
            }

            _mapper.Map<PatchStudentViewModel, ApplicationUser>(model, appUser);
            _mapper.Map<PatchStudentViewModel, Address>(model, appUser.Address!);
            _context.ApplicationUsers.Update(appUser);
        }
        public async Task DeleteStudentAsync(int id)
        {
            var response = await _context.Students.Include(u => u.ApplicationUser)
            .ThenInclude(u => u!.Address)

            .Where(s => s.ApplicationUser!.StudentId == id).SingleOrDefaultAsync();

            if (response is null)
            {
                throw new Exception($"We could not find a student with id: {id}");
            }

            // TODO: Fix real cascade with address
            var address = response.ApplicationUser!.Address;

            if (address is null)
            {
                throw new Exception($"We could not find the address: {response.ApplicationUser.AddressId}");
            }

            _context.Students.Remove(response);
            _context.Addresses.Remove(address!);
        }
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}