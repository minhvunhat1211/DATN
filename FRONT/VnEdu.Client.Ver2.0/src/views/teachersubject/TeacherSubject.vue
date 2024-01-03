<template>
    <div class="t-scorelookup t-student-class">
        <div class="header-scorelookup">
            <div class="box-scorelookup">
                <div class="combobox-scorelookup">
                    <div class="name-combobox">Chọn năm học</div>
                    <div class="box-combobox">
                        <div class="box-combobox-header" @click="clickBtnDownSchoolYear()">
                            <input class="input-data" type="text" readonly="true" :value="schoolyearName">
                            <div class="box-icon">
                                <div class="icon-down"></div>
                            </div>
                        </div>
                        <div class="box-combobox-content" v-show="isShowSelectSchoolYear">
                            <div v-for="schoolyear in schoolyears" :key="schoolyear.SchoolYearId" class="item-combobox" @click="clickItemSelectSchoolYear(schoolyear.SchoolYearId, schoolyear.SchoolYearName)">{{schoolyear.SchoolYearName}}</div>
                        </div>
                    </div>
                </div>
                <div class="combobox-scorelookup">
                    <div class="name-combobox">Chọn học kì</div>
                    <div class="box-combobox">
                        <div class="box-combobox-header" @click="clickBtnDownSemester()">
                            <input class="input-data" type="text" readonly="true" :value="semesterName">
                            <div class="box-icon">
                                <div class="icon-down"></div>
                            </div>
                        </div>
                        <div class="box-combobox-content" v-show="isShowSelectSemester">
                            <div v-for="semester in semesters" :key="semester.SemesterId" class="item-combobox" @click="clickItemSelectSemester(semester.SemesterId, semester.SemesterName)">{{semester.SemesterName}}</div>
                        </div>
                    </div>
                </div>
                <div class="combobox-scorelookup">
                    <div class="name-combobox">Chọn môn học</div>
                    <div class="box-combobox">
                        <div class="box-combobox-header" @click="clickBtnDownSubject()">
                            <input class="input-data" type="text" readonly="true" :value="subjectName">
                            <div class="box-icon">
                                <div class="icon-down"></div>
                            </div>
                        </div>
                        <div class="box-combobox-content type-10" v-show="isShowSelectSubject">
                            <div v-for="subject in subjects" :key="subject.SubjectId" class="item-combobox" @click="clickItemSelectSubject(subject.SubjectId, subject.SubjectName)">{{subject.SubjectName}}</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-see">
                <button class="btn-see" @click="clickBtnSee()">Xem</button>
            </div>
        </div>
        <div class="bottom-studentclass" v-show="isShowTeacherSubject">
            <div class="box-search box-filter">
                <div class="d-flex flex-row">
                    <div class="box-input-text input-student">
                        <input type="text" class="input-text" placeholder="Tìm kiếm theo mã, tên giáo viên" v-model="SearchValue">
                        <div class="box-icon-search" @click="clickBtnSearch()">
                            <div class="icon-search"></div>
                        </div>
                    </div>
                    <div class="export-excel" @click="clickBtnExportExcel()"></div>
                </div>
                <div class="header-btn-insert btn-insert-box" @click="clickBtnInsert()">Thêm giáo viên vào môn</div>
            </div>
            <div class="between-schoolyear table-studet-class">
                <table class="m-table" border="1">
                    <thead>
                        <tr>
                            <th class="text-align-center" style="width: 50px">STT</th>
                            <th class="text-align-left" style="width: 110px">Mã giáo viên</th>
                            <th class="text-align-left">Tên giáo viên</th>
                            <th class="text-align-center" style="width: 80px">Giới tính</th>
                            <th class="text-align-center" style="width: 90px">Ngày sinh</th>
                            <th class="text-align-center" style="width: 110px">Số điện thoại</th>
                            <th class="text-align-left" >Địa chỉ</th>
                            <th class="text-align-center" style="width: 120px">Chức năng</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(teacher_subject,i) in teacher_subjects" :key="teacher_subject.StudentId">
                            <th class="text-align-center">{{++i}}</th>
                            <th class="text-align-left">{{teacher_subject.TeacherCode}}</th>
                            <th class="text-align-left">{{teacher_subject.FullName}}</th>
                            <th class="text-align-center">{{FomatGender(teacher_subject.Gender)}}</th>
                            <th class="text-align-center">{{FomatDate(teacher_subject.DateOfBirth)}}</th>
                            <th class="text-align-center">{{teacher_subject.PhoneNumber}}</th>
                            <th class="text-align-left">{{teacher_subject.Address}}</th>
                            <th class="text-align-center">
                                <div class="box-function">
                                    <div class="btn-delete" @click="clickBtnDelete(teacher_subject.TeacherId, teacher_subject.TeacherCode)"></div>
                                </div>
                            </th>
                        </tr>  
                        
                    </tbody>
                </table>
            </div>
            <div class="bottom-schoolyear box-type-paging paging-student-class">
                <div class="box-sum">Tổng số: {{teacher_subjects.length}} bản ghi</div>
                <div class="box-paging">
                    <div class="pri" @click="clickPagePri()">
                        <div class="page-pri"></div>
                    </div>
                    <div class="page-number">
                        <div v-for="i in pagings" :key="i" :class="{ active: pageNumber == i }" class="number" @click="clickPageNumber(i)" >{{i}}</div>
                    </div>
                    <div class="next" @click="clickPageNext()">
                        <div class="page-next" ></div>
                    </div>
                </div>
                <div class="box-size">
                    <input type="text" class="box-size-input" readonly :value="pagename">
                    <div class="box-select-size" @click="clickBtnSelectSize()">
                        <div class="icon-size"></div>
                    </div>
                    <div class="box-size-item" v-show="isShowBoxItemSize">
                        <div v-for="page in pages" :key="page.index" class="item-size" @click="clickItemSize(page.index, page.name)">{{page.name}}</div>
                    </div>
                </div>
            </div>
        </div>
        <delete-teacher-subject></delete-teacher-subject>
        <teacher-subject-infor></teacher-subject-infor>
    </div>
</template>

<script>
import * as XLSX from 'xlsx'
import axios from 'axios';
import { mapGetters } from 'vuex';
import { eventBus } from '../../main';
import moment from 'moment';
import DeleteTeacherSubject from './DeleteTeacherSubject.vue';
import TeacherSubjectInfor from './TeacherSubjectInfor.vue';
export default {
    components:{
        DeleteTeacherSubject,
        TeacherSubjectInfor, 
    },
    data() {
        return {
            isShowTeacherSubject: false,
            isShowSelectSemester: false,
            isShowSelectSchoolYear: false,
            isShowSelectSubject: false,
            isShowBoxItemSize: false,
            schoolyears: [
               
            ],
            semesters: [
               
            ],
            subjects:[

            ],
            semesterName: null,
            semesterId: null,
            subjectName: null,
            subjectId: null,
            schoolyearName: null,
            schoolyearId: null,
            teacher_subjects: [

            ],
            semesterSum: 0,
            isShowLoading: false,
            isShowScore: false,
            titleSemester: null,
            pages: [
                {
                    index: 10,
                    name: "10 bản ghi trên 1 trang"
                },
                {
                    index: 20,
                    name: "20 bản ghi trên 1 trang"
                },
                {
                    index: 30,
                    name: "30 bản ghi trên 1 trang"
                },
                {
                    index: 50,
                    name: "50 bản ghi trên 1 trang"
                },
                {
                    index: 100,
                    name: "100 bản ghi trên 1 trang"
                }
            ],
            SearchValue: '',
            pageSize: 10, 
            pageNumber: 1,
            totalRecord: null,
            totalPage: null,
            pagename: "10 bản ghi trên 1 trang",
        }
    },

    computed:{
        ...mapGetters(['URLAPI', 'token']),
        pagings() {
            let numShown = 5;
            numShown = Math.min(numShown, this.totalPage);
            let first = this.pageNumber - Math.floor(numShown / 2);
            first = Math.max(first, 1);
            first = Math.min(first, this.totalPage - numShown + 1);
            return [...Array(numShown)].map((k,i) => i + first);
        }
    },
    methods: {
        clickPageNumber(i){
            var m = this;
            m.pageNumber = i;
            m.loadDataTeacher_Subject();
        },
        clickPageNext(){
            var m = this;
            if(m.pageNumber < m.totalPage)
            {
                m.pageNumber++;
                m.loadDataTeacher_Subject();
                console.log(m.pageNumber);
            }
            else
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Đã đến trang cuối cùng");
            }
        },
        clickPagePri(){
            var m = this;
            if(m.pageNumber > 1)
            {
                m.pageNumber--;
                m.loadDataTeacher_Subject();
                console.log(m.pageNumber);
            }
            else
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Đã đến trang đầu tiên");
            }
        },
        /**
         * click vào btn tìm kiếm 
         * CreatedBy: MinhVN (04/01/2022)
         */
        clickBtnSearch(){
            this.pageNumber = 1;
            this.loadDataTeacher_Subject();
            eventBus.$emit("isShowToastMessageWas", true);
            eventBus.$emit("TitleToastMessageWas", "Tìm kiếm giáo viên thành công");
        },
        /**
         * Định dạng lại ngày sinh để hiển thị 
         * CreatedBy: MinhVN (09/02/2022)
         */
        FomatDate(value){
            if (value){
                return moment(String(value)).format('DD-MM-YYYY');
            }
        },
        /**
         * Định dạng lại giới tính 
         * CreatedBy: MinhVN (09/02/2022)
         */
        FomatGender(value){
            if (value != null){
                if(value == 0)
                {
                    return "Nữ";
                }
                else if(value == 1)
                {
                    return "Nam";
                }
                else if(value == 2){
                    return "Khác";
                }
            }
            else{
                return "";
            }
        },

        /**
         * click vào btn hiện box size
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnSelectSize(){
            this.isShowBoxItemSize = !this.isShowBoxItemSize;
        },
        /**
         * click vào chọn số bản ghi trên 1 trang
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickItemSize(index, name){
            this.pageSize = index;
            this.pagename = name;
            this.loadDataTeacher_Subject();
            this.isShowBoxItemSize = false;
        },
        /**
         * click vào btn down gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnDownSemester(){
            if(this.schoolyearId == null)
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn năm học trước");
            }
            else{
                this.isShowSelectSemester = !this.isShowSelectSemester;
            }
            
        },

        /**
         * click vào item gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickItemSelectSemester(semesterId, semesterName)
        {
            this.isShowSelectSemester = !this.isShowSelectSemester;
            this.semesterName = semesterName;
            this.semesterId = semesterId;
            this.isShowTeacherSubject = false;
        },

        /**
         * click vào btn down gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnDownSubject(){
            if(this.semesterId == null)
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn học kì trước");
            }
            else{
                this.isShowSelectSubject = !this.isShowSelectSubject;
            }
        },

        /**
         * click vào item gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickItemSelectSubject(subjectId, subjectName)
        {
            this.isShowSelectSubject = !this.isShowSelectSubject;
            this.subjectName = subjectName;
            this.subjectId = subjectId;
            this.isShowTeacherSubject = false;
        },
        /**
         * click vào btn down gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnDownSchoolYear(){
            this.isShowSelectSchoolYear = !this.isShowSelectSchoolYear;
        },

        /**
         * click vào item gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickItemSelectSchoolYear(schoolyearId, schoolyearName)
        {
            this.isShowSelectSchoolYear = !this.isShowSelectSchoolYear;
            this.schoolyearName = schoolyearName;
            this.schoolyearId = schoolyearId;
            this.isShowTeacherSubject = false;

            this.getDataSemester(schoolyearId);
        },

        /**
         * lấy tất cả năm học
         * CreatedBy: MinhVN(25/01/2022)
         */
        getDataSchoolYear(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/SchoolYears`,
                {'headers': { 'Authorization': 'Bearer ' + m.token }})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                { 
                   m.schoolyears = response.data.Data;
                }
            })
            .catch(function (res){
                console.log(res);
            });
        },

        /**
         * lấy tất cả học kì
         * CreatedBy: MinhVN(25/01/2022)
         */
        getDataSemester(schoolYearId){
            var m = this;
            axios
             .get(`${m.URLAPI}/api/v1/Semesters/GetSemesterBySchoolYearId?schoolYearId=${schoolYearId}`,
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                if(response && response.data)
                { 
                   m.semesters = response.data.Data;
                }
            })
            .catch(function (res){
                console.log(res);
            });

        },
        /**
         * click vào btn xem
         * CreatedBy: MinhVN(25/01/2022)
         */
        clickBtnSee(){
            var m = this;
            if(m.schoolyearId == null || m.schoolyearId == "")
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn năm học cần xem");
            }
            else if(m.semesterId == null || m.semesterId == "")
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn học kì cần xem");
            }
            else if(m.subjectId == null || m.subjectId == "")
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn môn học cần xem");
            }
            else{
                m.isShowTeacherSubject = true;
                m.pageNumber = 1;
                axios
                .get(`${m.URLAPI}/api/v1/Teacher_Subjects/GetPagingTeacherSubjectByClassSemesterSchoolYear?schoolYearId=${m.schoolyearId}&semesterId=${m.semesterId}&subjectId=${m.subjectId}&valueFilter=${m.SearchValue}&pageIndex=${m.pageNumber}&pageSize=${m.pageSize}`,
                    {'headers': { 'Authorization': 'Bearer ' + m.token }})
                .then(function (response){
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else
                    {
                        m.teacher_subjects = response.data.Data.Data;
                        m.totalRecord = response.data.Data.ToTalRecord;
                        m.totalPage = response.data.Data.ToTalPage;
                        eventBus.$emit("schoolyearIdWas", m.schoolyearId);
                        eventBus.$emit("semesterIdWas", m.semesterId);
                        eventBus.$emit("subjectIdWas", m.subjectId);
                        eventBus.$emit("isShowToastMessageWas", true);
                        eventBus.$emit("TitleToastMessageWas", "Xem thành công");
                    }
                })
                .catch(function (res){
                    console.log(res);
                });
            }
        },
        loadDataTeacher_Subject(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Teacher_Subjects/GetPagingTeacherSubjectByClassSemesterSchoolYear?schoolYearId=${m.schoolyearId}&semesterId=${m.semesterId}&subjectId=${m.subjectId}&valueFilter=${m.SearchValue}&pageIndex=${m.pageNumber}&pageSize=${m.pageSize}`,
                {'headers': { 'Authorization': 'Bearer ' + m.token }})
            .then(function (response){
                if(response && response.data.Data)
                {
                    m.teacher_subjects = response.data.Data.Data;
                    m.totalRecord = response.data.Data.ToTalRecord;
                    m.totalPage = response.data.Data.ToTalPage;
                }
            })
            .catch(function (res){
                console.log(res);
            });
        },
        /**
         * click vào btn thêm mới người dùng
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnInsert(){
            eventBus.$emit("isShowBtnSaveAndInsertWas", true);
            eventBus.$emit("isShowTeacherSubjectInforWas", true);
            eventBus.$emit('nullWas', null);
            eventBus.$emit('thisWas', this);
        },
        /**
         * click btn xóa
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnDelete(teacherId, teacherCode){
            eventBus.$emit("isShowDeleteTeacherSubjectWas", true);
            eventBus.$emit("teacherIdWas", teacherId);
            eventBus.$emit("titlenameWas", `bạn có thực sự muốn xóa giáo viên có mã < ${teacherCode} > không?`);
            eventBus.$emit('thisWas', this);
        },
        /**
         * lấy tất cả lớp học
         * CreatedBy: MinhVN(06/01/2023)
         */
        getDataSubject(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Subjects`, 
                {'headers': { 'Authorization': 'Bearer ' + m.token }})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                { 
                   m.subjects = response.data.Data;
                }
            })
            .catch(function (res){
                console.log(res);
            });
        },
        /**
         * click btn export excel
         * CreatedBy: MinhVN(24/03/2023)
         */
        clickBtnExportExcel(){
            var m = this;
            
            axios
           .get(`${m.URLAPI}/api/v1/Teacher_Subjects/GetPagingTeacherSubjectByClassSemesterSchoolYear?schoolYearId=${m.schoolyearId}&semesterId=${m.semesterId}&subjectId=${m.subjectId}&pageIndex=${1}&pageSize=${1000}`,
                {'headers': { 'Authorization': 'Bearer ' + m.token }})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                {
                    const data = response.data.Data.Data
                    .map(item => [item.TeacherCode, item.FullName, m.FomatGender(item.Gender), m.FomatDate(item.DateOfBirth), item.PhoneNumber, item.Email, item.Address]);

                    data.unshift(['Mã giáo viên', 'Tên giáo viên', 'Giới tính', 'Ngày sinh', 'Số điện thoại', 'Email', 'Địa chỉ']);
                    const ws = XLSX.utils.aoa_to_sheet(data);
                    ws['!cols'] = [
                        { wpx: 80 }, 
                        { wpx: 150 }, 
                        { wpx: 50 },
                        { wpx: 100 }, 
                        { wpx: 100 }, 
                        { wpx: 200 },
                        { wpx: 80 },  
                    ];
                    const wb = XLSX.utils.book_new();
                    XLSX.utils.book_append_sheet(wb, ws, 'Danh sách giáo viên');
                    XLSX.writeFile(wb, 'Danhsachgiaovien.xlsx');
                }
            })
            .catch(function (res){
                console.log(res);
            });
        }
    },

    created() {
        var m = this;
        m.getDataSchoolYear();
        m.getDataSubject();
    },
}
</script>

<style>

</style>