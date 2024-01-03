<template>
    <div class="t-dialog" v-show="isShowDeleteSemester">
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
import { mapGetters } from 'vuex'
export default {
    data() {
        return {
            isShowDeleteSemester: false,
            semesterId: null,
            titleName: null,
            m2: null,
        }
    },
    computed: {
        ...mapGetters(['URLAPI', 'token']),
    },
    methods: {
        /**
         * click Btn không
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnNo(){
            this.isShowDeleteSemester = !this.isShowDeleteSemester;
        },
        /**
         * click Btn có
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnYes(){
            var m = this;
            axios
            .delete(`${m.URLAPI}/api/v1/Semesters/${m.semesterId}`,
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
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
                    // gửi dữ liệu sang component FormToastMessage là 'xóa học kì thành công'
                    eventBus.$emit("TitleToastMessageWas", "Xóa học kì thành công");
                    m.m2.loadDataSemester();
                    m.isShowDeleteSemester = !m.isShowDeleteSemester;
                }
            })
            .catch(function(res){
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", `${res}`);
            })
        },
    },
    created() {
        var  m = this;
        eventBus.$on("isShowDeleteSemesterWas", (isShowDeleteSemesterData) =>{
            m.isShowDeleteSemester = isShowDeleteSemesterData;
        });
        eventBus.$on("semesterIdWas", (semesterIdData) =>{
            m.semesterId = semesterIdData;
        });
        eventBus.$on("titlenameWas", (titlenameData) =>{
            m.titleName = titlenameData;
        });
        eventBus.$on("thisWas", (thisData) =>{
            m.m2 = thisData;
        });
    },
}
</script>