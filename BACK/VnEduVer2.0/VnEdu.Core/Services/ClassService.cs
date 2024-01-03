using VnEdu.Core.Entities.Models;
using VnEdu.Core.Entities.OperationResult;
using VnEdu.Core.Entities.Results;
using VnEdu.Core.Interfaces.IRepositories;
using VnEdu.Core.Interfaces.IServices;
using VnEdu.Core.Services.Commom;

namespace VnEdu.Core.Services
{
    /// <summary>
    /// Information of ClassService
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly ISchoolYearRepository _schoolYearRepository;
        private readonly ITeacherRepository _teacherRepository;

        public ClassService(IClassRepository classRepository, ISchoolYearRepository schoolYearRepository, ITeacherRepository teacherRepository)
        {
            _classRepository = classRepository;
            _schoolYearRepository = schoolYearRepository;
            _teacherRepository = teacherRepository;
        }

        /// <summary>
        /// Delete a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Number record delete success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public async Task<OperationResult<bool>> Delete(int id)
        {
            return await _classRepository.Delete(id);
        }

        /// <summary>
        /// Insert a record
        /// </summary>
        /// <param name="classCreate">Class</param>
        /// <returns>Number record insert success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public async Task<OperationResult<bool>> Insert(Class classCreate)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. ClassName is null
            if (string.IsNullOrWhiteSpace(classCreate.ClassName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByNameNotEmpty);

                return result;
            }

            // Get classByName
            var classByName = await _classRepository.GetClassByName(classCreate.ClassName, classCreate.SchoolYearId);

            // 2. ClassName is already exist
            if (classByName.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByNameAlreadyExist);

                return result;
            }

            // 3. TeacherId is null
            if (classCreate.TeacherId == default(int))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByNotEmptyTeacher);

                return result;
            }

            // Get teacherById
            var teacherById = await _teacherRepository.GetById(classCreate.TeacherId);

            // 4. teacherById is null
            if (teacherById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.TeacherByIdNotFound, classCreate.TeacherId));

                return result;
            }


            // Get classBySchoolYearTeacher
            var classBySchoolYearTeacher = await _classRepository.GetBySchoolYearTeacher(classCreate.SchoolYearId, classCreate.TeacherId);

            // 5. classBySchoolYearTeacher is not null
            if (classBySchoolYearTeacher.Data is not null)
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassBySchoolYearTeacherAlreadyExist);

                return result;
            }

            // 6. Grade is null
            if (string.IsNullOrWhiteSpace(classCreate.Grade))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByGradeNotEmpty);

                return result;
            }

            // 7. Room is null
            if (string.IsNullOrWhiteSpace(classCreate.Room))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByRoomNotEmpty);

                return result;
            }

            // 8. SchoolYearIs is null
            if (classCreate.SchoolYearId == default(int))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByNameNotEmptySchoolYear);

                return result;
            }

            // Get schoolYearById
            var schoolYearById = await _schoolYearRepository.GetById(classCreate.SchoolYearId);

            // 9. schoolYearById is null
            if (schoolYearById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.SchoolYearByIdNotFound, classCreate.SchoolYearId));

                return result;
            }

            result = await _classRepository.Insert(classCreate);

            return result;
        }

        /// <summary>
        /// Update a record
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="classUpdate">Class</param>
        /// <returns>Number record update success</returns>
        /// CreatedBy: MinhVN(21/12/2022)
        public async Task<OperationResult<bool>> Update(int id, Class classUpdate)
        {
            var result = new OperationResult<bool>();

            // Validate data

            // 1. ClassName is null
            if (string.IsNullOrWhiteSpace(classUpdate.ClassName))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByNameNotEmpty);

                return result;
            }

            // Get classByName
            var classByName = await _classRepository.GetClassByName(classUpdate.ClassName, classUpdate.SchoolYearId);

            // classByName is not null
            if (classByName.Data is not null)
            {
                // 2.1 ClassName is already exist
                if (classByName.Data.ClassName.ToLower().Trim().Equals(classUpdate.ClassName.ToLower().Trim())
                    && classByName.Data.ClassId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByNameAlreadyExist);

                    return result;
                }
            }

            // 3. TeacherId is null
            if (classUpdate.TeacherId == default(int))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByNotEmptyTeacher);

                return result;
            }

            // Get teacherById
            var teacherById = await _teacherRepository.GetById(classUpdate.TeacherId);

            // 4. teacherById is null
            if (teacherById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.TeacherByIdNotFound, classUpdate.TeacherId));

                return result;
            }

            // Get classBySchoolYearTeacher
            var classBySchoolYearTeacher = await _classRepository.GetBySchoolYearTeacher(classUpdate.SchoolYearId, classUpdate.TeacherId);

            // 5. classBySchoolYearTeacher is not null
            if (classBySchoolYearTeacher.Data is not null)
            {
                if (classBySchoolYearTeacher.Data.ClassId != id)
                {
                    result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassBySchoolYearTeacherAlreadyExist);

                    return result;
                }
            }

            // 6. Grade is null
            if (string.IsNullOrWhiteSpace(classUpdate.Grade))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByGradeNotEmpty);

                return result;
            }

            // 7. Room is null
            if (string.IsNullOrWhiteSpace(classUpdate.Room))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByRoomNotEmpty);

                return result;
            }

            // 8. SchoolYearIs is null
            if (classUpdate.SchoolYearId == default(int))
            {
                result.AddError(ErrorCode.NotFound, ConfigErrorMessageService.ClassByNameNotEmptySchoolYear);

                return result;
            }

            // Get schoolYearById
            var schoolYearById = await _schoolYearRepository.GetById(classUpdate.SchoolYearId);

            // 9. schoolYearById is null
            if (schoolYearById.Data is null)
            {
                result.AddError(ErrorCode.NotFound,
                    string.Format(ConfigErrorMessageService.SchoolYearByIdNotFound, classUpdate.SchoolYearId));

                return result;
            }

            result = await _classRepository.Update(id, classUpdate);

            return result;
        }
    }
}