using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services;

namespace VnEdu.Api.Registrars
{
    /// <summary>
    /// Information of ServiceRegistrar
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public class ServiceRegistrar : IWebApplicationBuilderRegistrar
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">WebApplicationBuilder</param>
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IClassService, ClassService>();
            builder.Services.AddScoped<IDecentralizationService, DecentralizationService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IdentityService>();
            builder.Services.AddScoped<ISchoolYearService, SchoolYearService>();
            builder.Services.AddScoped<ISemesterService, SemesterService>();
            builder.Services.AddScoped<ISubjectService, SubjectService>();
            builder.Services.AddScoped<IDetailedTableScoreService, DetailedTableScoreService>();
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<IStudent_ClassService, Student_ClassService>();
            builder.Services.AddScoped<IStudent_SubjectService, Student_SubjectService>();
            builder.Services.AddScoped<ITeacher_ClassService, Teacher_ClassService>();
            builder.Services.AddScoped<ITeacher_SubjectService, Teacher_SubjectService>();
        }
    }
}