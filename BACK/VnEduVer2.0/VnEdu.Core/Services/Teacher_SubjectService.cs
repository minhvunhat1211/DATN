using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of Teacher_SubjectService
    /// CreatedBy: MinhVN(04/01/2023)
    /// </summary>
    public class Teacher_SubjectService : ITeacher_SubjectService
    {
        private readonly ITeacher_SubjectRepository _teacher_SubjectRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ISchoolYearRepository _schoolYearRepository;

        public Teacher_SubjectService(ITeacher_SubjectRepository teacher_SubjectRepository, ITeacherRepository teacherRepository,
            ISubjectRepository subjectRepository, ISemesterRepository semesterRepository, ISchoolYearRepository schoolYearRepository)
        {
            _teacher_SubjectRepository = teacher_SubjectRepository;
            _teacherRepository = teacherRepository;
            _subjectRepository = subjectRepository;
            _semesterRepository = semesterRepository;
            _schoolYearRepository = schoolYearRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="teacherId">TeacherId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">ScoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        public async Task<OperationResult<bool>> Delete(int teacherId, int subjectId, int semesterId, int schoolYearId)
        {
            return await _teacher_SubjectRepository.Delete(teacherId, subjectId, semesterId, schoolYearId);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="teacher_Subject">Teacher_Subject</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(04/01/2023)
        public async Task<OperationResult<bool>> Insert(Teacher_Subject teacher_Subject)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // Get teacherById
            var teacherById = await _teacherRepository.GetById(teacher_Subject.TeacherId);

            // 1. teacherById is null
            if (teacherById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Teacher_SubjectByTeacherIdNotFound, teacher_Subject.TeacherId));

                return result;
            }

            // Get subjectById
            var subjectById = await _subjectRepository.GetById(teacher_Subject.SubjectId);

            // 2. subjectById is null
            if (subjectById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Teacher_SubjectBySubjectIdNotFound, teacher_Subject.SubjectId));

                return result;
            }

            // Get semesterById
            var semesterById = await _semesterRepository.GetById(teacher_Subject.SemesterId);

            // 3. semesterById is null
            if (semesterById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Teacher_SubjectBySemesterIdNotFound, teacher_Subject.SemesterId));

                return result;
            }

            // Get schoolYearById
            var schoolYearById = await _schoolYearRepository.GetById(teacher_Subject.SchoolYearId);

            // 3. schoolYearById is null
            if (schoolYearById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Teacher_SubjectBySchoolYearIdNotFound, teacher_Subject.SchoolYearId));

                return result;
            }

            // Get teacher_SubjectById
            var teacher_SubjectById = await _teacher_SubjectRepository.GetTeacherSubjectById(teacher_Subject.SchoolYearId,
                teacher_Subject.SemesterId, teacher_Subject.SubjectId, teacher_Subject.TeacherId);

            // 4. teacher_SubjectById is not null
            if (teacher_SubjectById.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.Teacher_SubjectByIdAlreadyExist);

                return result;
            }

            result = await _teacher_SubjectRepository.Insert(teacher_Subject);

            return result;
        }
    }
}