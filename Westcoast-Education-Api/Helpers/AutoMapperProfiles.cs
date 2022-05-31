using AutoMapper;
using Westcoast_Education_Api.Models;
using Westcoast_Education_Api.ViewModels.Address;
using Westcoast_Education_Api.ViewModels.Category;
using Westcoast_Education_Api.ViewModels.Course;
using Westcoast_Education_Api.ViewModels.Student;
using Westcoast_Education_Api.ViewModels.Teacher;

namespace Westcoast_Education_Api.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Address, AddressViewModel>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<ApplicationUser, TeacherViewModel>();
            CreateMap<ApplicationUser, TeacherWithCategoriesViewModel>()
                .ForMember(dest => dest.Categories, options => options.MapFrom(src => src.Teacher!.Categories));

            CreateMap<Course, CourseViewModel>();
            CreateMap<Course, CourseWithCategoryViewModel>();

            CreateMap<CourseStudents, StudentWithCoursesViewModel>()
                .ForMember(dest => dest.CourseNo, options => options.MapFrom(src => src.Course!.CourseNo));

        }
    }
}