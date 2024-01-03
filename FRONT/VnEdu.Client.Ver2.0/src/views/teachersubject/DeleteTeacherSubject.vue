<template>
    <div class="t-dialog" v-show="isShowDeleteTeacherSubject">
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
            isShowDeleteTeacherSubject: false,
            teacherId: null,
            subjectId: null,
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
            this.isShowDeleteTeacherSubject = !this.isShowDeleteTeacherSubject;
        },
        /**
         * click Btn có
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnYes(){
            var m = this;
            axios
            .delete(`${m.URLAPI}/api/v1/Teacher_Subjects/DeleteTeacher_Subject?teacherId=${m.teacherId}&subjectId=${m.subjectId}&semesterId=${m.semesterId}&schoolYearId=${m.schoolyearId}`,
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
                    m.m2.loadDataTeacher_Subject();
                    m.isShowDeleteTeacherSubject = !m.isShowDeleteTeacherSubject;
                }   
            })
            .catch(function(res){
                console.log(res);
            })
        },
    },
    created() {
        var  m = this;
        eventBus.$on("isShowDeleteTeacherSubjectWas", (isShowDeleteTeacherSubjectData) =>{
            m.isShowDeleteTeacherSubject = isShowDeleteTeacherSubjectData;
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
        eventBus.$on("subjectIdWas", (subjectIdData) =>{
            m.subjectId = subjectIdData;
        });
    },
}
</script>