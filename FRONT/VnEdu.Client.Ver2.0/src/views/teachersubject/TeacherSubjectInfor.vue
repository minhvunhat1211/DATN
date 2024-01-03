<template>
    <div class="t-dialog" v-show="isShowTeacherSubjectInfor">
         <div class="t-schoolyearinfor" >
            <div class="box-x" @click="clickBtnX()">
                <div class="icon-x"></div>
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Chọn giáo viên</div>
                <div class="box-size box-student-combobox box-student-class-combobox">
                    <input type="text" class="box-size-input box-student-input" readonly :value="fullName">
                    <div class="box-select-size box-student-select" @click="clickBtnSelectTeacher()">
                        <div class="icon-down"></div>
                    </div>
                    <div class="box-size-item box-item-student type-2 type-10" v-show="isShowBoxItemTeacher">
                        <div v-for="teacher in teachers" :key="teacher.TeacherId + `1`" class="item-size item-student" @click="clickItemTeacher(teacher.TeacherId, teacher.TeacherCode, teacher.FullName)">{{teacher.TeacherCode}} , {{teacher.FullName}}</div>
                    </div>
                </div>
            </div>
            <div class="box-schoolyearinfor-bottom">
                <div class="box-schoolyearinfor-cancle" @click="clickBtnCancel()">Hủy</div>
                <div class="box-btn-schoolyearinfor">
                    <div class="box-schoolyearinfor-save" @click="clickBtnSave()">Lưu</div>
                    <div class="box-schoolyearinfor-saveandinsert" v-show="isShowBtnSaveAndInsert" @click="clickBtnSaveAndInsert()">Lưu và thêm</div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import axios from 'axios';
import { eventBus } from '../../main'
import { mapGetters } from 'vuex';
export default {
    data() {
        return {
            isShowTeacherSubjectInfor: false,
            isShowBtnSaveAndInsert: true,
            isShowBoxItemTeacher: false,
            teacherId: null,
            m2: null,
            fullName: null,
            formod: null,
            teachers: [],
            teacher_subject: {},
        }
    },
    computed:{
        ...mapGetters(['user', 'URLAPI', 'token']),
    },
    methods: {
        /**
        * click Btn lựa chọn giới tính
        * CreatedBy: MinhVN(10/02/2022)
        */
        clickBtnSelectTeacher(){
            this.isShowBoxItemTeacher = !this.isShowBoxItemTeacher;
        },
        /**
        * click vào 1 giới tính 
        * CreatedBy: MinhVN(10/02/2022)
        */
        clickItemTeacher(teacherId, teacherCode, fullName){
            this.teacher_subject.TeacherId = teacherId;
            this.fullName = teacherCode + " , " + fullName;
            this.isShowBoxItemTeacher = !this.isShowBoxItemTeacher;
        },
    
       /**
        * click Btn hủy
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnCancel(){
           this.isShowTeacherSubjectInfor = !this.isShowTeacherSubjectInfor;
       },
       /**
        * click Btn X
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnX(){
           this.isShowTeacherSubjectInfor = !this.isShowTeacherSubjectInfor;
       },
       /**
        * click Btn lưu
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnSave()
       {
            var m = this;
            m.teacher_subject.CreatedBy = m.user.UserName;  
            axios({
                method: "post",
                url: `${m.URLAPI}/api/v1/Teacher_Subjects?teacherId=${m.teacher_subject.TeacherId}&subjectId=${m.teacher_subject.SubjectId}&semesterId=${m.teacher_subject.SemesterId}&schoolYearId=${m.teacher_subject.SchoolYearId}`,
                headers: { 'Authorization': 'Bearer ' + m.token }
            })
            .then(function (response) {
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else{
                    m.fullName = null;
                    // gửi dữ liệu sang component FormToastMessage là 'true'
                    eventBus.$emit("isShowToastMessageWas", true);
                    // gửi dữ liệu sang component FormToastMessage là 'Thêm giáo viên thành công'
                    eventBus.$emit("TitleToastMessageWas", "Thêm giáo viên thành công");
                    m.m2.loadDataTeacher_Subject();
                    m.isShowTeacherSubjectInfor = !m.isShowTeacherSubjectInfor;
                    console.log(response);
                }
            })
            .catch(function (error) {
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                // gửi dữ liệu đến component FormWaning 
                eventBus.$emit("errorWas", error);
            });
       },
       /**
        * click Btn lưu và thêm
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnSaveAndInsert()
       {
           var m = this;
           m.teacher_subject.CreatedBy = m.user.UserName;
           axios({
                method: "post",
                url: `${m.URLAPI}/api/v1/Teacher_Subjects?teacherId=${m.teacher_subject.TeacherId}&subjectId=${m.teacher_subject.SubjectId}&semesterId=${m.teacher_subject.SemesterId}&schoolYearId=${m.teacher_subject.SchoolYearId}`,
                headers: { 'Authorization': 'Bearer ' + m.token }
            })
            .then(function (response) {
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else{
                    m.fullName = null;
                    // gửi dữ liệu sang component FormToastMessage là 'true'
                    eventBus.$emit("isShowToastMessageWas", true);
                    // gửi dữ liệu sang component FormToastMessage là 'Thêm giáo viên thành công'
                    eventBus.$emit("TitleToastMessageWas", "Thêm giáo viên thành công");
                    m.m2.loadDataTeacher_Subject();
                    console.log(response);
                }
            })
            .catch(function (error) {
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                // gửi dữ liệu đến component FormWaning 
                eventBus.$emit("errorWas", error);
            });
       },
       loadDataTeachers(){
           var m = this;
           axios
           .get(`${m.URLAPI}/api/v1/Teachers`, 
            {'headers': { 'Authorization': 'Bearer ' + m.token }})
           .then(function(response)
           {
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                {
                    m.teachers = response.data.Data;
                }
           })
           .catch(function(response){
               console.log(response)
           })
       }
    },
    created() {
        var  m = this;
        eventBus.$on("isShowTeacherSubjectInforWas", (isShowStudentInforData) =>{
            m.isShowTeacherSubjectInfor = isShowStudentInforData;
        });
        eventBus.$on("isShowBtnSaveAndInsertWas", (isShowBtnSaveAndInsertData) =>{
            m.isShowBtnSaveAndInsert = isShowBtnSaveAndInsertData;
        });
        eventBus.$on("thisWas", (thisData) =>{
            m.m2 = thisData;
        });
        eventBus.$on("schoolyearIdWas", (schoolyearIdData) =>{
            m.teacher_subject.SchoolYearId = schoolyearIdData;
        });
        eventBus.$on("semesterIdWas", (semesterIdData) =>{
            m.teacher_subject.SemesterId = semesterIdData;
        });
        eventBus.$on("subjectIdWas", (subjectIdData) =>{
            m.teacher_subject.SubjectId = subjectIdData;
        });
        eventBus.$on("formodWas", (formodData) =>{
            m.formod = formodData;
        });
        eventBus.$on("nullWas", (nullData) =>{
            m.fullName = nullData;
        });
        m.loadDataTeachers();
    },
}
</script>