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
            CreateMap<ApplicationUser, StudentViewModel>();

            CreateMap<ApplicationUser, TeacherViewModel>();
            CreateMap<ApplicationUser, TeacherWithCategoriesViewModel>()
                .ForMember(dest => dest.Categories, options => options.MapFrom(src => src.Teacher!.Categories));

            CreateMap<ApplicationUser, TeacherWithCoursesViewModel>()
                .ForMember(dest => dest.Courses, options => options.MapFrom(src => src.Teacher!.Courses));

            CreateMap<Category, CategoryViewModel>()
                .ForMember(dest => dest.CategoryId, options => options.MapFrom(src => src.Id));

            CreateMap<Category, CategoryWithCoursesViewModel>()
                .ForMember(dest => dest.CategoryId, options => options.MapFrom(src => src.Id));

            CreateMap<Course, CourseViewModel>()
                .ForMember(dest => dest.CourseId, options => options.MapFrom(src => src.Id));
            CreateMap<Course, CourseWithCategoryViewModel>()
                .ForMember(dest => dest.CourseId, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category!.CategoryName));

            CreateMap<CourseStudents, StudentWithCoursesViewModel>()
               .ForMember(dest => dest.FirstName, options => options.MapFrom(src => src.Student!.ApplicationUser!.FirstName))
               .ForMember(dest => dest.LastName, options => options.MapFrom(src => src.Student!.ApplicationUser!.LastName))
               .ForMember(dest => dest.Email, options => options.MapFrom(src => src.Student!.ApplicationUser!.Email))
               .ForMember(dest => dest.CourseNo, options => options.MapFrom(src => src.Course!.CourseNo))
               .ForMember(dest => dest.Title, options => options.MapFrom(src => src.Course!.Title))
               .ForMember(dest => dest.EnrollmentDate, options => options.MapFrom(src => src.EnrollmentDate));


            CreateMap<PostCourseViewModel, Course>();
            CreateMap<PatchCourseViewModel, Course>();

            CreateMap<PostStudentViewModel, Address>();
            CreateMap<PostStudentViewModel, Student>();
            CreateMap<PostStudentViewModel, ApplicationUser>();

            CreateMap<PatchStudentViewModel, Address>();
            CreateMap<PatchStudentViewModel, ApplicationUser>();

            CreateMap<PostTeacherViewModel, Teacher>()
                .ForMember(dest => dest.Categories, dest => dest.Ignore());

            CreateMap<PostTeacherViewModel, Address>();
            CreateMap<PostTeacherViewModel, ApplicationUser>();

        }
    }
}