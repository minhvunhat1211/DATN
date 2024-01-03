using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of Teacher_ClassService
    /// CreatedBy: MinhVN(03/01/2023)
    /// </summary>
    public class Teacher_ClassService : ITeacher_ClassService
    {
        private readonly ITeacher_ClassRepository _teacher_ClassRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IClassRepository _classRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ISchoolYearRepository _schoolYearRepository;

        public Teacher_ClassService(ITeacher_ClassRepository teacher_ClassRepository, ITeacherRepository teacherRepository,
            IClassRepository classRepository, ISemesterRepository semesterRepository, ISchoolYearRepository schoolYearRepository)
        {
            _teacher_ClassRepository = teacher_ClassRepository;
            _teacherRepository = teacherRepository;
            _classRepository = classRepository;
            _semesterRepository = semesterRepository;
            _schoolYearRepository = schoolYearRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="teacherId">TeacherId</param>
        /// <param name="classId">ClassId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(03/01/2023)
        public async Task<OperationResult<bool>> Delete(int teacherId, int classId, int semesterId, int schoolYearId)
        {
            return await _teacher_ClassRepository.Delete(teacherId, classId, semesterId, schoolYearId);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="teacher_Class">Teacher_Class</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(03/01/2023)
        public async Task<OperationResult<bool>> Insert(Teacher_Class teacher_Class)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // Get teacherById
            var teacherById = await _teacherRepository.GetById(teacher_Class.TeacherId);

            // 1. teacherById is null
            if (teacherById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Teacher_ClassByTeacherIdNotFound, teacher_Class.TeacherId));

                return result;
            }

            // Get classById
            var classById = await _classRepository.GetById(teacher_Class.ClassId);

            // 2. classById is null
            if (classById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Teacher_ClassByClassIdNotFound, teacher_Class.ClassId));

                return result;
            }

            // Get semesterById
            var semesterById = await _semesterRepository.GetById(teacher_Class.SemesterId);

            // 3. semesterById is null
            if (semesterById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Teacher_ClassBySemesterIdNotFound, teacher_Class.SemesterId));

                return result;
            }

            // Get schoolYearById
            var schoolYearById = await _schoolYearRepository.GetById(teacher_Class.SchoolYearId);

            // 3. schoolYearById is null
            if (schoolYearById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Teacher_ClassBySchoolYearIdNotFound, teacher_Class.SchoolYearId));

                return result;
            }

            // Get teacher_ClassById
            var teacher_ClassById = await _teacher_ClassRepository.GetTeacherClassById(teacher_Class.SchoolYearId,
                teacher_Class.SemesterId, teacher_Class.ClassId, teacher_Class.TeacherId);

            // 4. teacher_ClassById is not null
            if (teacher_ClassById.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.Teacher_ClassByIdAlreadyExist);

                return result;
            }

            result = await _teacher_ClassRepository.Insert(teacher_Class);

            return result;
        }
    }
}