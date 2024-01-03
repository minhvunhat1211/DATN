<template>
    <div class="t-dialog" v-show="isShowDeleteStudentClass">
        <div class="m-form">
            <div class="form-title">
                <div class="title-icon"></div>
                <div class="title-name">{{titleName}}</div>
            </div>
            <div class="form-btn">
                <div class="form-btn-cancle" @click="clickBtnNo()">Không</div>
                <div class="form-btn-delete" @click="clickBtnYes()">Có</div>
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
            isShowDeleteStudentClass: false,
            studentId: null,
            classId: null,
            schoolyearId: null,
            semesterId: null,
            titleName: null,
            m2: null,
        }
    },
    computed:{
        ...mapGetters(['URLAPI', 'token']),
    },
    methods: {
        /**
         * click Btn không
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnNo(){
            this.isShowDeleteStudentClass = !this.isShowDeleteStudentClass;
        },
        /**
         * click Btn có
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnYes(){
            var m = this;
            axios
            .delete(`${m.URLAPI}/api/v1/Student_Classs/DeleteStudent_Class?StudentId=${m.studentId}&ClassId=${m.classId}&SemesterId=${m.semesterId}&SchoolYearId=${m.schoolyearId}`,
                {'headers': { 'Authorization': 'Bearer ' + m.token }})
            .then(function(response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else{
                    console.log(response);
                    // gửi dữ liệu sang component FormToastMessage là 'true'
                    eventBus.$emit("isShowToastMessageWas", true);
                    // gửi dữ liệu sang component FormToastMessage là 'xóa học sinh thành công'
                    eventBus.$emit("TitleToastMessageWas", "Xóa học sinh thành công");
                    m.m2.loadDataStudent_Class();
                    m.isShowDeleteStudentClass = !m.isShowDeleteStudentClass;
                }   
            })
            .catch(function(res){
                console.log(res);
            })
        },
    },
    created() {
        var  m = this;
        eventBus.$on("isShowDeleteStudentClassWas", (isShowDeleteStudentClassData) =>{
            m.isShowDeleteStudentClass = isShowDeleteStudentClassData;
        });
        eventBus.$on("studentIdWas", (studentIdData) =>{
            m.studentId = studentIdData;
        });
        eventBus.$on("titlenameWas", (titlenameData) =>{
            m.titleName = titlenameData;
        });
        eventBus.$on("thisWas", (thisData) =>{
            m.m2 = thisData;
        });
         eventBus.$on("schoolyearIdWas", (schoolyearIdData) =>{
            m.schoolyearId = schoolyearIdData;
        });
        eventBus.$on("semesterIdWas", (semesterIdData) =>{
            m.semesterId = semesterIdData;
        });
        eventBus.$on("classIdWas", (classIdData) =>{
            m.classId = classIdData;
        });
    },
}
</script>