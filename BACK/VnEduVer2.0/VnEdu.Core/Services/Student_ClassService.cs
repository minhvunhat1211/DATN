using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of Student_ClassService
    /// CreatedBy: MinhVN(25/12/2022)
    /// </summary>
    public class Student_ClassService : IStudent_ClassService
    {
        private readonly IStudent_ClassRepository _student_ClassRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ISchoolYearRepository _schoolYearRepository;

        public Student_ClassService(IStudent_ClassRepository student_ClassRepository, IStudentRepository studentRepository,
            IClassRepository classRepository, ISemesterRepository semesterRepository, ISchoolYearRepository schoolYearRepository)
        {
            _student_ClassRepository = student_ClassRepository;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _semesterRepository = semesterRepository;
            _schoolYearRepository = schoolYearRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(25/12/2022)
        public async Task<OperationResult<bool>> Delete(int studentId, int classId, int semesterId, int schoolYearId)
        {
            return await _student_ClassRepository.Delete(studentId, classId, semesterId, schoolYearId);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="student_Class">Student_Class</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(25/12/2022)
        public async Task<OperationResult<bool>> Insert(Student_Class student_Class)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // Get studentById
            var studentById = await _studentRepository.GetById(student_Class.StudentId);

            // 1. studentById is null
            if (studentById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Student_ClassByStudentIdNotFound, student_Class.StudentId));

                return result;
            }

            // Get classById
            var classById = await _classRepository.GetById(student_Class.ClassId);

            // 2. classById is null
            if (classById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Student_ClassByClassIdNotFound, student_Class.ClassId));

                return result;
            }

            // Get semesterById
            var semesterById = await _semesterRepository.GetById(student_Class.SemesterId);

            // 3. semesterById is null
            if (semesterById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Student_ClassBySemesterIdNotFound, student_Class.SemesterId));

                return result;
            }

            // Get schoolYearById
            var schoolYearById = await _schoolYearRepository.GetById(student_Class.SchoolYearId);

            // 3. schoolYearById is null
            if (schoolYearById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Student_ClassBySchoolYearIdNotFound, student_Class.SchoolYearId));

                return result;
            }

            // Get student_ClassById
            var student_ClassById = await _student_ClassRepository.GetStudent_ClassById(student_Class.StudentId,
                student_Class.SchoolYearId, student_Class.SemesterId, student_Class.ClassId);

            // 4. student_ClassById is not null
            if (student_ClassById.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.Student_ClassByIdAlreadyExist);

                return result;
            }

            // Get student_ClassByStudentSchoolYearSemester
            var student_ClassByStudentSchoolYearSemester = await _student_ClassRepository.GetStudent_ClassByStudentSchoolYearSemester(
                student_Class.StudentId, student_Class.SchoolYearId, student_Class.SemesterId);

            // 5. student_ClassByStudentSchoolYearSemester is not null
            if (student_ClassByStudentSchoolYearSemester.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.Student_ClassBySchoolYearSemesterAlreadyExist);

                return result;
            }

            result = await _student_ClassRepository.Insert(student_Class);

            return result;
        }
    }
}