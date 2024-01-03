<template>
    <div class="t-dialog" v-show="isShowDeleteTeacherClass">
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
            isShowDeleteTeacherClass: false,
            teacherId: null,
            classId: null,
            schoolyearId: null,
            semesterId: null,
            titleName: null,
            m2: null,
        }
    },
    computed:{
        ...mapGetters(['URLAPI', 'token', 'token']),
    },
    methods: {
        /**
         * click Btn không
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnNo(){
            this.isShowDeleteTeacherClass = !this.isShowDeleteTeacherClass;
        },
        /**
         * click Btn có
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnYes(){
            var m = this;
            axios
            .delete(`${m.URLAPI}/api/v1/Teacher_Classs/DeleteTeacher_Class?TeacherId=${m.teacherId}&ClassId=${m.classId}&SemesterId=${m.semesterId}&SchoolYearId=${m.schoolyearId}`,
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
                    // gửi dữ liệu sang component FormToastMessage là 'xóa giáo viên thành công'
                    eventBus.$emit("TitleToastMessageWas", "Xóa giáo viên thành công");
                    m.m2.loadDataTeacher_Class();
                    m.isShowDeleteTeacherClass = !m.isShowDeleteTeacherClass;
                }   
            })
            .catch(function(res){
                console.log(res);
            })
        },
    },
    created() {
        var  m = this;
        eventBus.$on("isShowDeleteTeacherClassWas", (isShowDeleteTeacherClassData) =>{
            m.isShowDeleteTeacherClass = isShowDeleteTeacherClassData;
        });
        eventBus.$on("teacherIdWas", (teacherIdData) =>{
            m.teacherId = teacherIdData;
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