<template>
    <div class="t-dialog" v-show="isShowStudentClassInfor">
         <div class="t-schoolyearinfor" >
            <div class="box-x" @click="clickBtnX()">
                <div class="icon-x"></div>
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Chọn học sinh</div>
                <div class="box-size box-student-combobox box-student-class-combobox">
                    <input id="value-student" type="text" class="box-size-input box-student-input" :value="fullName">
                    <div class="box-select-size box-student-select" @click="clickBtnSelectStudent()">
                        <div class="icon-down"></div>
                    </div>
                    <div class="box-size-item box-item-student type-2 type-10" v-show="isShowBoxItemStudent">
                        <div v-for="student in students" :key="student.StudentId" class="item-size item-student" @click="clickItemStudent(student.StudentId, student.StudentCode, student.FullName)">{{student.StudentCode}} , {{student.FullName}}</div>
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
            isShowStudentClassInfor: false,
            isShowBtnSaveAndInsert: true,
            isShowBoxItemStudent: false,
            studentId: null,
            m2: null,
            fullName: null,
            formod: null,
            students: [],
            student_class: {},
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
        clickBtnSelectStudent(){
            this.isShowBoxItemStudent = !this.isShowBoxItemStudent;
        },
        /**
        * click vào 1 giới tính 
        * CreatedBy: MinhVN(10/02/2022)
        */
        clickItemStudent(studentId, studentCode, fullName){
            this.student_class.StudentId = studentId;
            this.fullName = studentCode + " , " + fullName;
            this.isShowBoxItemStudent = !this.isShowBoxItemStudent;
        },
    
       /**
        * click Btn hủy
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnCancel(){
           this.isShowStudentClassInfor = !this.isShowStudentClassInfor;
       },
       /**
        * click Btn X
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnX(){
           this.isShowStudentClassInfor = !this.isShowStudentClassInfor;
       },
       /**
        * click Btn lưu
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnSave()
       {
            var m = this;
            m.student_class.CreatedBy = m.user.UserName;  
            console.log(m.student_class);
            axios({
                method: "post",
                url: `${m.URLAPI}/api/v1/Student_Classs?studentId=${m.student_class.StudentId}&classId=${m.student_class.ClassId}&semesterId=${m.student_class.SemesterId}&schoolYearId=${m.student_class.SchoolYearId}`,
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
                    // gửi dữ liệu sang component FormToastMessage là 'Thêm học sinh thành công'
                    eventBus.$emit("TitleToastMessageWas", "Thêm học sinh thành công");
                    m.m2.loadDataStudent_Class();
                    m.isShowStudentClassInfor = !m.isShowStudentClassInfor;
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
           m.student_class.CreatedBy = m.user.UserName;
           axios({
                method: "post",
                url: `${m.URLAPI}/api/v1/Student_Classs?studentId=${m.student_class.StudentId}&classId=${m.student_class.ClassId}&semesterId=${m.student_class.SemesterId}&schoolYearId=${m.student_class.SchoolYearId}`,
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
                    // gửi dữ liệu sang component FormToastMessage là 'Thêm học sinh thành công'
                    eventBus.$emit("TitleToastMessageWas", "Thêm học sinh thành công");
                    m.m2.loadDataStudent_Class();
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
       loadDataStudents(){
           var m = this;
           axios
           .get(`${m.URLAPI}/api/v1/Students`, 
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
                    m.students = response.data.Data;
                }
           })
           .catch(function(response){
               console.log(response)
           })
       },
    //    handleKeyDown(){
    //         this.isShowBoxItemStudent = true;
    //         let valueSearch = document.getElementById("value-student").value;
    //         let list = [];
    //         this.students.forEach(student => {
    //             if (student.StudentCode.includes(valueSearch) || student.FullName.includes(valueSearch))
    //             {
    //                 list.push(student);
    //             }
    //         });

    //         this.students = list;
    //    }
    },
    created() {
        var  m = this;
        eventBus.$on("isShowStudentClassInforWas", (isShowStudentInforData) =>{
            m.isShowStudentClassInfor = isShowStudentInforData;
        });
        eventBus.$on("isShowBtnSaveAndInsertWas", (isShowBtnSaveAndInsertData) =>{
            m.isShowBtnSaveAndInsert = isShowBtnSaveAndInsertData;
        });
        eventBus.$on("thisWas", (thisData) =>{
            m.m2 = thisData;
        });
        eventBus.$on("schoolyearIdWas", (schoolyearIdData) =>{
            m.student_class.SchoolYearId = schoolyearIdData;
        });
        eventBus.$on("semesterIdWas", (semesterIdData) =>{
            m.student_class.SemesterId = semesterIdData;
        });
        eventBus.$on("classIdWas", (classIdData) =>{
            m.student_class.ClassId = classIdData;
        });
        eventBus.$on("formodWas", (formodData) =>{
            m.formod = formodData;
        });
        eventBus.$on("nullWas", (nullData) =>{
            m.fullName = nullData;
        });
        m.loadDataStudents();
    },
}
</script>