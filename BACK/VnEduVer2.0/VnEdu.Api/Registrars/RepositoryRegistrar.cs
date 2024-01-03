using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Infrastructure.Repositories;

namespace VnEdu.Api.Registrars
{
    /// <summary>
    /// Information of RepositoryRegistrar
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public class RepositoryRegistrar : IWebApplicationBuilderRegistrar
    {
        /// <summary>
        /// RegisterServices
        /// </summary>
        /// <param name="builder">WebApplicationBuilder</param>
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IClassRepository, ClassRepository>();
            builder.Services.AddScoped<IDecentralizationRepository, DecentralizationRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ISchoolYearRepository, SchoolYearRepository>();
            builder.Services.AddScoped<ISemesterRepository, SemesterRepository>();
            builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
            builder.Services.AddScoped<IDetailedTableScoreRepository, DetailedTableScoreRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.Services.AddScoped<IStudent_ClassRepository, Student_ClassRepository>();
            builder.Services.AddScoped<IStudent_SubjectRepository, Student_SubjectRepository>();
            builder.Services.AddScoped<ITeacher_ClassRepository, Teacher_ClassRepository>();
            builder.Services.AddScoped<ITeacher_SubjectRepository, Teacher_SubjectRepository>();
        }
    }
}
