namespace VnEdu.Infrastructure.Commom
{
    /// <summary>
    /// Information of ConfigErrorMessageRepository
    /// CreatedBy: MinhVN(05/01/2023)
    /// </summary>
    public static class ConfigErrorMessageRepository
    {
        public const string ClassByIdNotFound = "Không tìm thấy lớp học có Id là <{0}>";
        public const string ClassByNameNotFound = "Không tìm thấy lớp học có tên là <{0}>";
        public const string ClassBySchoolYearTeacherNotFound = "Giáo viên chưa chủ nhiệm lớp nào trong năm học này";

        public const string DecentralizationByIdNotFound = "Không tìm thấy quyền có Id là <{0}>";
        public const string DecentralizationByNameNotFound = "Không tìm thấy quyền có tên là <{0}>";

        public const string UserByIdNotFound = "Không tìm thấy người dùng có Id là <{0}>";
        public const string UserByNotLogin = "Tên tài khoản hoặc mật khẩu không chính xác";
        public const string UserByEmailNotFound = "Không tìm thấy người dùng có email là <{0}>";
        public const string UserByPhoneNumberNotFound = "Không tìm thấy người dùng có số điện thoại là <{0}>";
        public const string UserByIdPasswodNotFound = "Mật khẩu cũ của bạn chưa chính xác";

        public const string SchoolYearByIdNotFound = "Không tìm thấy năm học có Id là <{0}>";
        public const string SchoolYearByNameNotFound = "Không tìm thấy năm học có tên là <{0}>";

        public const string SemesterByIdNotFound = "Không tìm thấy học kì có Id là <{0}>";
        public const string SemesterByNameNotFound = "Không tìm thấy học kì có tên là <{0}>";

        public const string SubjectByIdNotFound = "Không tìm thấy môn học có Id là <{0}>";
        public const string SubjectByNameNotFound = "Không tìm thấy môn học có tên là <{0}>";

        public const string DetailedTableScoreByIdNotFound = "Không tìm thấy bảng điểm có Id là <{0}>";

        public const string StudentByIdNotFound = "Không tìm thấy học sinh có Id là <{0}>";
        public const string StudentByEmailNotFound = "Không tìm thấy học sinh có email là <{0}>";
        public const string StudentByPhoneNumberNotFound = "Không tìm thấy học sinh có số điện thoại là <{0}>";
        public const string StudentByCodeNotFound = "Không tìm thấy học sinh có mã học sinh là <{0}>";
        public const string StudentByNumberCardNotFound = "Không tìm thấy học sinh có CMND/CCCD là <{0}>";

        public const string TeacherByIdNotFound = "Không tìm thấy giáo viên có Id là <{0}>";
        public const string TeacherByEmailNotFound = "Không tìm thấy giáo viên có email là <{0}>";
        public const string TeacherByNumberCardNotFound = "Không tìm thấy giáo viên có CMND/CCCD là <{0}>";
        public const string TeacherByPhoneNumberNotFound = "Không tìm thấy giáo viên có số điện thoại là <{0}>";
        public const string TeacherByCodeNotFound = "Không tìm thấy giáo viên có mã học sinh là <{0}>";

        public const string Student_ClassByNotFound = "Xóa không thành công vì học sinh không còn trong lớp học này";

        public const string Student_SubjectByNotFound = "Xóa không thành công vì học sinh không còn học môn này";
        public const string Student_SubjectByIdNotFound = "Không tìm thấy.";

        public const string Teacher_ClassByNotFound = "Không tìm thấy giáo viên trong lớp này";

        public const string Teacher_SubjectByNotFound = "Không tìm thấy giáo viên dạy môn này";

        public const string PageIndex = "Chỉ mục trang phải lớn hơn 0";
        public const string PageSize = "Số bản ghi trên một trang phải lớn hơn 0";
    }
}