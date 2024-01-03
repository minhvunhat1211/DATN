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
                    <div class="name-combobox">Chọn học sinh</div>
                    <div class="box-combobox">
                        <div class="box-combobox-header" @click="clickBtnDownStudent()">
                            <input class="input-data" type="text" readonly="true" :value="studentName">
                            <div class="box-icon">
                                <div class="icon-down"></div>
                            </div>
                        </div>
                        <div class="box-combobox-content type-10" v-show="isShowSelectStudent">
                            <div v-for="student in students" :key="student.StudentId"  class="item-combobox" @click="clickItemSelectStudent(student.StudentId, student.StudentCode, student.FullName)">{{student.StudentCode}}, {{student.FullName}}</div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="box-see">
                <button class="btn-see" @click="clickBtnSee()">Xem</button>
            </div>
        </div>
        <div class="bottom-studentclass" v-show="isShowStudentSubject">
            <div class="box-search box-filter">
                <div class="header-btn-insert btn-insert-box" @click="clickBtnInsert()">Thêm môn học cho học sinh</div>
            </div>
            <div class="between-schoolyear table-studet-class">
                <table class="m-table" border="1">
                    <thead>
                        <tr>
                            <th class="text-align-center" style="width: 50px">STT</th>
                            <th class="text-align-center">Tên môn học</th>
                            <th class="text-align-center" style="width: 120px">Chức năng</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(student_subject,i) in student_subjects" :key="student_subject.SubjectId">
                            <th class="text-align-center">{{++i}}</th>
                            <th class="text-align-center">{{student_subject.SubjectName}}</th>
                            <th class="text-align-center">
                                <div class="box-function">
                                    <div class="btn-delete" @click="clickBtnDelete(student_subject.SubjectId, student_subject.SubjectName)"></div>
                                </div>
                            </th>
                        </tr>  
                        
                    </tbody>
                </table>
            </div>
            <div class="bottom-schoolyear box-type-paging paging-student-class">
                <div class="box-sum">Tổng số: {{student_subjects.length}} bản ghi</div>
            </div>
        </div>
        <delete-student-subject></delete-student-subject>
        <student-subject-infor></student-subject-infor>
    </div>
</template>

<script>
import axios from 'axios';
import { mapGetters } from 'vuex';
import { eventBus } from '../../main';
import DeleteStudentSubject from './DeleteStudentSubject.vue';
import StudentSubjectInfor from './StudentSubjectInfor.vue';
export default {
    components:{
        DeleteStudentSubject,
        StudentSubjectInfor, 
    },
    data() {
        return {
            isShowStudentSubject: false,
            isShowSelectSemester: false,
            isShowSelectSchoolYear: false,
            isShowSelectStudent: false,
            isShowBoxItemSize: false,
            schoolyears: [
               
            ],
            semesters: [
               
            ],
            students:[

            ],
            semesterName: null,
            semesterId: null,
            studentName: null,
            studentId: null,
            schoolyearName: null,
            schoolyearId: null,
            student_subjects: [

            ],
        }
    },

    computed:{
        ...mapGetters(['URLAPI', 'token']),
    },
    methods: {
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
        clickItemSize(name){
            this.isShowBoxItemSize = !this.isShowBoxItemSize;
            this.pagename = name;
        },
        /**
         * click vào btn down gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnDownSemester(){
            this.isShowSelectSemester = !this.isShowSelectSemester;
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
            this.isShowStudentSubject = false;
        },

        /**
         * click vào btn down gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnDownStudent(){
            this.isShowSelectStudent = !this.isShowSelectStudent;
        },

        /**
         * click vào item gender
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickItemSelectStudent(studentId, studentCode, studentName)
        {
            this.isShowSelectStudent = !this.isShowSelectStudent;
            this.studentName = studentCode + ", " + studentName;
            this.studentId = studentId;
            this.isShowStudentSubject = false;
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
            this.isShowStudentSubject = false;

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
                else
                { 
                   m.semesters = response.data.Data;
                }
            })
            .catch(function (res){
                console.log(res);
            });

        },
        /**
         * lấy tất cả học sinh
         * CreatedBy: MinhVN(25/01/2022)
         */
        getDataStudent(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Students`,
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
                   m.students = response.data.Data;
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
            else if(m.studentId == null || m.studentId == "")
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Vui lòng chọn học sinh cần xem");
            }
            else{
                m.isShowStudentSubject = true;
                axios
                .get(`${m.URLAPI}/api/v1/Student_Subjects/GetBySubjectSemesterSchooYearClass?studentId=${m.studentId}&semesterId=${m.semesterId}&schoolYearId=${m.schoolyearId}`,
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
                        m.student_subjects = response.data.Data;
                        eventBus.$emit("schoolyearIdWas", m.schoolyearId);
                        eventBus.$emit("semesterIdWas", m.semesterId);
                        eventBus.$emit("studentIdWas", m.studentId);
                        eventBus.$emit("isShowToastMessageWas", true);
                        eventBus.$emit("TitleToastMessageWas", "Xem thành công");
                    }
                })
                .catch(function (res){
                    console.log(res);
                });
            }
        },
        loadDataStudent_Subject(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Student_Subjects/GetBySubjectSemesterSchooYearClass?studentId=${m.studentId}&semesterId=${m.semesterId}&schoolYearId=${m.schoolyearId}`,
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
                    m.student_subjects = response.data.Data;
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
            eventBus.$emit("isShowStudentSubjectInforWas", true);
            eventBus.$emit('nullWas', null);
            eventBus.$emit('thisWas', this);
        },
        /**
         * click btn xóa
         * CreatedBy: MinhVN(08/02/2022)
         */
        clickBtnDelete(subjectId, subjectName){
            eventBus.$emit("isShowDeleteStudentSubjectWas", true);
            eventBus.$emit("subjectIdWas", subjectId);
            eventBus.$emit("titlenameWas", `bạn có thực sự muốn xóa môn < ${subjectName} > không?`);
            eventBus.$emit('thisWas', this);
        },
    },

    created() {
        var m = this;
        m.getDataSchoolYear();
        m.getDataStudent();
    },
}
</script>

<style>

</style>