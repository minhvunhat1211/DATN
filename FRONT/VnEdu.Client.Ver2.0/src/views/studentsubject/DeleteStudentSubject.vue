<template>
    <div class="t-dialog" v-show="isShowDeleteStudentSubject">
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
            isShowDeleteStudentSubject: false,
            studentId: null,
            subjectId: null,
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
            this.isShowDeleteStudentSubject = !this.isShowDeleteStudentSubject;
        },
        /**
         * click Btn có
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnYes(){
            var m = this;
            axios
            .delete(`${m.URLAPI}/api/v1/Student_Subjects/DeleteStudent_Subject?studentId=${m.studentId}&subjectId=${m.subjectId}&semesterId=${m.semesterId}&schoolYearId=${m.schoolyearId}`,
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
                    // gửi dữ liệu sang component FormToastMessage là 'xóa môn học thành công'
                    eventBus.$emit("TitleToastMessageWas", "Xóa môn học thành công");
                    m.m2.loadDataStudent_Subject();
                    m.isShowDeleteStudentSubject = !m.isShowDeleteStudentSubject;
                }
            })
            .catch(function(res){
                console.log(res);
            })
        },
    },
    created() {
        var  m = this;
        eventBus.$on("isShowDeleteStudentSubjectWas", (isShowDeleteStudentSubjectData) =>{
            m.isShowDeleteStudentSubject = isShowDeleteStudentSubjectData;
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
        eventBus.$on("subjectIdWas", (subjectIdData) =>{
            m.subjectId = subjectIdData;
        });
    },
}
</script>