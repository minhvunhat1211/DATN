namespace VnEdu.Core.Services.Commom
{
    /// <summary>
    /// Information of ConfigErrorMessageService
    /// CreatedBy: MinhVN(21/12/2022)
    /// </summary>
    public static class ConfigErrorMessageService
    {
        public const string ClassByNameNotEmpty = "Tên lớp học không được phép để trống";
        public const string ClassByNameAlreadyExist = "Tên lớp học đã tồn tại";
        public const string ClassByNameNotEmptySchoolYear = "Năm học không được phép để trống";
        public const string ClassByNotEmptyTeacher = "giáo viên chủ nhiệm không được phép để trống";
        public const string ClassByNameOldName = "Tên lớp học giống tên cũ";
        public const string ClassByGradeNotEmpty = "Khối học không được phép để trống";
        public const string ClassByRoomNotEmpty = "Phòng học không được phép để trống";
        public const string ClassBySchoolYearTeacherAlreadyExist = "Giáo viên đã chủ nhiệm 1 lớp trong năm học này";

        public const string DecentralizationByIdNotFound = "Không tìm thấy quyền có Id là <{0}>";
        public const string DecentralizationByNameNotEmpty = "Tên quyền không được phép để trống";
        public const string DecentralizationByDescriptionNotEmpty = "Mô tả quyền không được phép để trống";
        public const string DecentralizationByNameAlreadyExist = "Tên quyền đã tồn tại";
        public const string DecentralizationByNameOldName = "Tên quyền giống tên cũ";

        public const string UserByNameNotEmpty = "Tên người dùng không được phép để trống";
        public const string UserByPhoneNumberNotEmpty = "Số điện thoại không được phép để trống";
        public const string UserByPhoneNumberAlreadyExist = "Số điện thoại đã tồn tại";
        public const string UserByEmailNotEmpty = "Email không được phép để trống";
        public const string UserByEmailAlreadyExist = "Email đã tồn tại";
        public const string UserByEmailNotFound = "Email không tồn tại";
        public const string UserByPasswordNotEmpty = "Mật khẩu không được phép để trống";
        public const string UserByPhoneNumberOldName = "Số điện thoại giống số điện thoại cũ";
        public const string UserByEmailOldName = "Email giống Email cũ";
        public const string UserByCodeOTPIncorrect = "Mã OTP không chính xác";
        public const string UserByCodeOTPExpired = "Mã OTP hết hiệu lực";

        public const string SchoolYearByNameNotEmpty = "Tên năm học không được phép để trống";
        public const string SchoolYearByNameAlreadyExist = "Tên năm học đã tồn tại";
        public const string SchoolYearByDateStart = "Ngày bắt đầu không được để trống";
        public const string SchoolYearByDateEnd = "Ngày kết thúc không được để trống";
        public const string SchoolYearByNameOldName = "Tên năm học giống tên cũ";
        public const string SchoolYearByIdNotFound = "Không tìm thấy năm học có Id là <{0}>";

        public const string SemesterByNameNotEmpty = "Tên học kì không được phép để trống";
        public const string SemesterByNameAlreadyExist = "Tên học kì đã tồn tại";
        public const string SemesterByNameOldName = "Tên học kì giống tên cũ";
        public const string SemesterByDateStart = "Ngày bắt đầu không được để trống";
        public const string SemesterByDateEnd = "Ngày kết thúc không được để trống";

        public const string SubjectByNameNotEmpty = "Tên môn học không được phép để trống";
        public const string SubjectByPeriodsPerYearNotEmpty = "Số tiết học trong một tuần không được phép để trống";
        public const string SubjectByCreditNotEmpty = "Số đơn vị học phần không được phép để trống";
        public const string SubjectByNameAlreadyExist = "Tên môn học đã tồn tại";
        public const string SubjectByNameOldName = "Tên môn học giống tên cũ";

        public const string StudentByCodeNotEmpty = "Mã học sinh không được phép để trống";
        public const string StudentByCodeAlreadyExist = "Mã học sinh đã tồn tại";
        public const string StudentByNameNotEmpty = "Tên học sinh không được phép để trống";
        public const string StudentByGenderNotEmpty = "Giới tính không được phép để trống";
        public const string StudentByDateOfBirthNotEmpty = "Ngày sinh không được phép để trống";
        public const string StudentByDateUtcNowOfBirthNotEmpty = "Ngày sinh không được phép lớn hơn ngày hiện tại";
        public const string StudentByPhoneNumberNotEmpty = "Số điện thoại không được phép để trống";
        public const string StudentByPhoneNumberAlreadyExist = "Số điện thoại đã tồn tại";
        public const string StudentByEmailNotEmpty = "Email không được phép để trống";
        public const string StudentByEmailAlreadyExist = "Email đã tồn tại";
        public const string StudentByAddressNotEmpty = "Địa chỉ không được phép để trống";
        public const string StudentByCodeOldName = "Mã học sinh giống mã học sinh cũ";
        public const string StudentByPhoneNumberOldName = "Số điện thoại giống số điện thoại cũ";
        public const string StudentByEmailOldName = "Email giống Email cũ";

        public const string TeacherByCodeNotEmpty = "Mã giáo viên không được phép để trống";
        public const string TeacherByCodeAlreadyExist = "Mã giáo viên đã tồn tại";
        public const string TeacherByNameNotEmpty = "Tên giáo viên không được phép để trống";
        public const string TeacherByGenderNotEmpty = "Giới tính không được phép để trống";
        public const string TeacherByDateOfBirthNotEmpty = "Ngày sinh không được phép để trống";
        public const string TeacherByDateUtcNowOfBirthNotEmpty = "Ngày sinh không được phép lớn hơn ngày hiện tại";
        public const string TeacherByPhoneNumberNotEmpty = "Số điện thoại không được phép để trống";
        public const string TeacherByPhoneNumberAlreadyExist = "Số điện thoại đã tồn tại";
        public const string TeacherByEmailNotEmpty = "Email không được phép để trống";
        public const string TeacherByEmailAlreadyExist = "Email đã tồn tại";
        public const string TeacherByNumberCardAlreadyExist = "Số CMND/CCCD đã tồn tại";
        public const string TeacherByAddressCurentNotEmpty = "Địa chỉ hiện tại không được phép để trống";
        public const string TeacherByAddressNotEmpty = "Quê quán không được phép để trống";
        public const string TeacherByNumberCardNotEmpty = "Số CMND/CCCD không được phép để trống";
        public const string TeacherByCodeOldName = "Mã giáo viên giống mã giáo viên cũ";
        public const string TeacherByPhoneNumberOldName = "Số điện thoại giống số điện thoại cũ";
        public const string TeacherByEmailOldName = "Email giống Email cũ";
        public const string TeacherByIdNotFound = "Không tìm thấy giáo viên có Id là <{0}>";

        public const string Student_ClassByStudentIdNotFound = "Không tìm thấy học sinh có Id là <{0}>";
        public const string Student_ClassByClassIdNotFound = "Không tìm thấy lớp học có Id là <{0}>";
        public const string Student_ClassBySemesterIdNotFound = "Không tìm thấy học kì có Id là <{0}>";
        public const string Student_ClassBySchoolYearIdNotFound = "Không tìm thấy năm học có Id là <{0}>";
        public const string Student_ClassByIdAlreadyExist = "Thêm không thành công vì học sinh đang trong lớp học này";
        public const string Student_ClassBySchoolYearSemesterAlreadyExist = "Học sinh này đã học 1 lớp trong năm học và học kì này";

        public const string Student_SubjectByStudentIdNotFound = "Không tìm thấy học sinh có Id là <{0}>";
        public const string Student_SubjectBySubjectIdNotFound = "Không tìm thấy môn học có Id là <{0}>";
        public const string Student_SubjectBySemesterIdNotFound = "Không tìm thấy học kì có Id là <{0}>";
        public const string Student_SubjectBySchoolYearIdNotFound = "Không tìm thấy năm học có Id là <{0}>";
        public const string Student_SubjectByIdAlreadyExist = "Thêm không thành công vì học sinh đang học môn này";
        public const string Student_SubjectByIdNotFound = "Không tìm thấy.";

        public const string Teacher_ClassByTeacherIdNotFound = "Không tìm thấy giáo viên có Id là <{0}>";
        public const string Teacher_ClassByClassIdNotFound = "Không tìm thấy lớp học có Id là <{0}>";
        public const string Teacher_ClassBySemesterIdNotFound = "Không tìm thấy học kì có Id là <{0}>";
        public const string Teacher_ClassBySchoolYearIdNotFound = "Không tìm thấy năm học có Id là <{0}>";
        public const string Teacher_ClassByIdAlreadyExist = "Thêm không thành công vì giáo viên đang trong lớp học này";

        public const string Teacher_SubjectByTeacherIdNotFound = "Không tìm thấy giáo viên có Id là <{0}>";
        public const string Teacher_SubjectBySubjectIdNotFound = "Không tìm thấy môn học có Id là <{0}>";
        public const string Teacher_SubjectBySemesterIdNotFound = "Không tìm thấy học kì có Id là <{0}>";
        public const string Teacher_SubjectBySchoolYearIdNotFound = "Không tìm thấy năm học có Id là <{0}>";
        public const string Teacher_SubjectByIdAlreadyExist = "Thêm không thành công vì giáo viên đang dạy môn học này";
    }
}