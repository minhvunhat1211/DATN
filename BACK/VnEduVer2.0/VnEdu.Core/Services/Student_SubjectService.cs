using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of Student_SubjectService
    /// CreatedBy: MinhVN(02/01/2023)
    /// </summary>
    public class Student_SubjectService : IStudent_SubjectService
    {
        private readonly IStudent_SubjectRepository _student_SubjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISemesterRepository _semesterRepository;
        private readonly ISchoolYearRepository _schoolYearRepository;
        private readonly IDetailedTableScoreRepository _detailedTableScoreRepository;

        public Student_SubjectService(IStudent_SubjectRepository student_SubjectRepository, IStudentRepository studentRepository, 
            ISubjectRepository subjectRepository, ISemesterRepository semesterRepository, ISchoolYearRepository schoolYearRepository,
            IDetailedTableScoreRepository detailedTableScoreRepository)
        {
            _student_SubjectRepository = student_SubjectRepository;
            _studentRepository = studentRepository;
            _subjectRepository = subjectRepository;
            _semesterRepository = semesterRepository;
            _schoolYearRepository = schoolYearRepository;
            _detailedTableScoreRepository = detailedTableScoreRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="studentId">StudentId</param>
        /// <param name="subjectId">SubjectId</param>
        /// <param name="semesterId">SemesterId</param>
        /// <param name="schoolYearId">SchoolYearId</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public async Task<OperationResult<bool>> Delete(int studentId, int subjectId, int semesterId, int schoolYearId)
        {
            return await _student_SubjectRepository.Delete(studentId, subjectId, semesterId, schoolYearId);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="student_Subject">Student_Subject</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(02/01/2023)
        public async Task<OperationResult<bool>> Insert(Student_Subject student_Subject)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // Get studentById
            var studentById = await _studentRepository.GetById(student_Subject.StudentId);

            // 1. studentById is null
            if (studentById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Student_SubjectByStudentIdNotFound, student_Subject.StudentId));

                return result;
            }

            // Get subjectById
            var subjectById = await _subjectRepository.GetById(student_Subject.SubjectId);

            // 2. subjectById is null
            if (subjectById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Student_SubjectBySubjectIdNotFound, student_Subject.SubjectId));

                return result;
            }

            // Get semesterById
            var semesterById = await _semesterRepository.GetById(student_Subject.SemesterId);

            // 3. semesterById is null
            if (semesterById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Student_SubjectBySemesterIdNotFound, student_Subject.SemesterId));

                return result;
            }

            // Get schoolYearById
            var schoolYearById = await _schoolYearRepository.GetById(student_Subject.SchoolYearId);

            // 3. schoolYearById is null
            if (schoolYearById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.Student_SubjectBySchoolYearIdNotFound, student_Subject.SchoolYearId));

                return result;
            }

            // Get student_SubjectById
            var student_SubjectById = await _student_SubjectRepository.GetStudent_SubjectById(student_Subject.SubjectId,
                student_Subject.SemesterId, student_Subject.SchoolYearId, student_Subject.StudentId);

            // 4. student_SubjectById is not null
            if (student_SubjectById.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.Student_SubjectByIdAlreadyExist);

                return result;
            }

            result = await _student_SubjectRepository.Insert(student_Subject);

            // Get student_SubjectById
            var student_SubjectByIdCheck = await _student_SubjectRepository.GetStudent_SubjectById(student_Subject.SubjectId,
                student_Subject.SemesterId, student_Subject.SchoolYearId, student_Subject.StudentId);

            // 4. student_SubjectByIdCheck is null
            if (student_SubjectByIdCheck.Data is null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.Student_SubjectByIdNotFound);

                return result;
            }

            var detailedTableScoreNew = new DetailedTableScore()
            {
                DetailedTableScoreId = student_SubjectByIdCheck.Data.DetailedTableScoreId,
                CreatedDate = DateTime.UtcNow,
                ModifiedDate = DateTime.UtcNow,
                CreatedBy = student_Subject.CreatedBy
            };

            await _detailedTableScoreRepository.Insert(detailedTableScoreNew);

            return result;
        }
    }
}