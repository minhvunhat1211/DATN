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
                    <div class="name-combobox">Chọn lớp học</div>
                    <div class="box-combobox">
                        <div class="box-combobox-header" @click="clickBtnDownClass()">
                            <input class="input-data" type="text" readonly="true" :value="className">
                            <div class="box-icon">
                                <div class="icon-down"></div>
                            </div>
                        </div>
                        <div class="box-combobox-content type-10" v-show="isShowSelectClass">
                            <div v-for="Class in Classs" :key="Class.ClassId" class="item-combobox" @click="clickItemSelectClass(Class.ClassId, Class.ClassName)">{{Class.ClassName}}</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-see">
                <button class="btn-see" @click="clickBtnSee()">Xem</button>
            </div>
        </div>
        <div class="bottom-studentclass" v-show="isShowTeacherClass">
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
                <div class="header-btn-insert btn-insert-box" @click="clickBtnInsert()">Thêm giáo viên vào lớp</div>
            </div>
            <div class="between-schoolyear table-studet-class">
                <table class="m-table" border="1">
                    <thead>
                        <tr>
                            <th class="text-align-center" style="width: 50px">STT</th>
                            <th class="text-align-left" style="width: 110px">Mã giáo viên</th>
                            <th class="text-align-left">Tên giáo viên</th>
                            <th class="text-align-left">Môn dạy</th>
                            <th class="text-align-center" style="width: 80px">Giới tính</th>
                            <th class="text-align-center" style="width: 90px">Ngày sinh</th>
                            <th class="text-align-center" style="width: 110px">Số điện thoại</th>
                            <th class="text-align-left" >Địa chỉ</th>
                            <th class="text-align-center" style="width: 120px">Chức năng</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(teacher_class,i) in teacher_classs" :key="teacher_class.SchoolYearId">
                            <th class="text-align-center">{{++i}}</th>
                            <th class="text-align-left">{{teacher_class.TeacherCode}}</th>
                            <th class="text-align-left">{{teacher_class.FullName}}</th>
                            <th class="text-align-left">{{teacher_class.SubjectName}}</th>
                            <th class="text-align-center">{{FomatGender(teacher_class.Gender)}}</th>
                            <th class="text-align-center">{{FomatDate(teacher_class.DateOfBirth)}}</th>
                            <th class="text-align-center">{{teacher_class.PhoneNumber}}</th>
                            <th class="text-align-left">{{teacher_class.Address}}</th>
                            <th class="text-align-center">
                                <div class="box-function">
                                    <div class="btn-delete" @click="clickBtnDelete(teacher_class.TeacherId, teacher_class.TeacherCode)"></div>
                                </div>
                            </th>
                        </tr>  
                        
                    </tbody>
                </table>
            </div>
            <div class="bottom-schoolyear box-type-paging paging-student-class">
                <div class="box-sum">Tổng số: {{teacher_classs.length}} bản ghi</div>
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
        <delete-teacher-class></delete-teacher-class>
        <teacher-class-infor></teacher-class-infor>
    </div>
</template>

<script>
import axios from 'axios';
import { mapGetters } from 'vuex';
import { eventBus } from '../../main';
import moment from 'moment';
import * as XLSX from 'xlsx'
import DeleteTeacherClass from './DeleteTeacherClass.vue';
import TeacherClassInfor from './TeacherClassInfor.vue';
export default {
    components:{
        DeleteTeacherClass,
        TeacherClassInfor, 
    },
    data() {
        return {
            isShowTeacherClass: false,
            isShowSelectSemester: false,
            isShowSelectSchoolYear: false,
            isShowSelectClass: false,
            isShowBoxItemSize: false,
            schoolyears: [
               
            ],
            semesters: [
               
            ],
            Classs:[

            ],
            semesterName: null,
            semesterId: null,
            className: null,
            classId: null,
            schoolyearName: null,
            schoolyearId: null,
            teacher_classs: [

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
            m.loadDataTeacher_Class();
        },
        clickPageNext(){
            var m = this;
            if(m.pageNumber < m.totalPage)
            {
                m.pageNumber++;
                m.loadDataTeacher_Class();
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
                m.loadDataTeacher_Class();
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
            this.loadDataTeacher_Class();
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
            this.loadDataTeacher_Class();
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
            this.isShowTeacherClass = false;
        },

        /**
         * click vào btn down gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnDownClass(){
            if(this.semesterId == null)
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn học kì trước");
            }
            else{
                this.isShowSelectClass = !this.isShowSelectClass;
            }
        },

        /**
         * click vào item gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickItemSelectClass(classId, className)
        {
            this.isShowSelectClass = !this.isShowSelectClass;
            this.className = className;
            this.classId = classId;
            this.isShowTeacherClass = false;
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
            this.isShowTeacherClass = false;

            this.getDataSemester(schoolyearId);
            this.getDataClass(schoolyearId);
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
            else if(m.Classs.length == 0)
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Năm học này chưa có lớp học vui lòng chọn năm học khác");
            }
            else if(m.semesterId == null || m.semesterId == "")
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn học kì cần xem");
            }
            else if(m.classId == null || m.classId == "")
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn lớp học cần xem");
            }
            else{
                m.isShowTeacherClass = true;
                m.pageNumber = 1;
                axios
                .get(`${m.URLAPI}/api/v1/Teacher_Classs/GetPagingTeacherClassByClassSemesterSchoolYear?schoolYearId=${m.schoolyearId}&semesterId=${m.semesterId}&classId=${m.classId}&valueFilter=${m.SearchValue}&pageIndex=${m.pageNumber}&pageSize=${m.pageSize}`,
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
                        m.teacher_classs = response.data.Data.Data;
                        m.totalRecord = response.data.Data.ToTalRecord;
                        m.totalPage = response.data.Data.ToTalPage;
                        eventBus.$emit("schoolyearIdWas", m.schoolyearId);
                        eventBus.$emit("semesterIdWas", m.semesterId);
                        eventBus.$emit("classIdWas", m.classId);
                        eventBus.$emit("isShowToastMessageWas", true);
                        eventBus.$emit("TitleToastMessageWas", "Xem thành công");
                    }
                })
                .catch(function (res){
                    console.log(res);
                });
            }
        },
        loadDataTeacher_Class(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Teacher_Classs/GetPagingTeacherClassByClassSemesterSchoolYear?schoolYearId=${m.schoolyearId}&semesterId=${m.semesterId}&classId=${m.classId}&valueFilter=${m.SearchValue}&pageIndex=${m.pageNumber}&pageSize=${m.pageSize}`,
                {'headers': { 'Authorization': 'Bearer ' + m.token }})
            .then(function (response){
                if(response && response.data.Data)
                {
                    m.teacher_classs = response.data.Data.Data;
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
            eventBus.$emit("isShowTeacherClassInforWas", true);
            eventBus.$emit('nullWas', null);
            eventBus.$emit('thisWas', this);
        },
        /**
         * click btn xóa
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnDelete(teacherId, teacherCode){
            eventBus.$emit("isShowDeleteTeacherClassWas", true);
            eventBus.$emit("teacherIdWas", teacherId);
            eventBus.$emit("titlenameWas", `bạn có thực sự muốn xóa giáo viên có mã < ${teacherCode} > không?`);
            eventBus.$emit('thisWas', this);
        },
        /**
         * lấy tất cả lớp học
         * CreatedBy: MinhVN(06/01/2023)
         */
        getDataClass(schoolYearId){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Classs/GetClassBySchoolYearId?schoolYearId=${schoolYearId}`,
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                { 
                   m.Classs = response.data.Data;
                }
            })
            .catch(function (res){
                console.log(res);
            });

        },

        /**
         * click btn export excel
         * CreatedBy: MinhVN(21/03/2023)
         */
        clickBtnExportExcel(){
            var m = this;
            
            axios
            .get(`${m.URLAPI}/api/v1/Teacher_Classs/GetPagingTeacherClassByClassSemesterSchoolYear?schoolYearId=${m.schoolyearId}&semesterId=${m.semesterId}&classId=${m.classId}&pageIndex=${1}&pageSize=${1000}`,
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
                    .map(item => [item.TeacherCode, item.FullName, item.SubjectName, m.FomatGender(item.Gender), m.FomatDate(item.DateOfBirth), item.PhoneNumber, item.Email, item.Address]);

                    data.unshift(['Mã giáo viên', 'Tên giáo viên', 'Môn dạy', 'Giới tính', 'Ngày sinh', 'Số điện thoại', 'Email', 'Địa chỉ']);

                    const ws = XLSX.utils.aoa_to_sheet(data);
                    ws['!cols'] = [
                        { wpx: 80 }, 
                        { wpx: 150 },
                        { wpx: 80 }, 
                        { wpx: 50 },
                        { wpx: 80 }, 
                        { wpx: 80 }, 
                        { wpx: 200 }, 
                        { wpx: 80 }, 
                    ];
                    const wb = XLSX.utils.book_new();
                    XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');
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
    },
}
</script>
