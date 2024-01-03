<template>
    <div class="t-schoolyear">
        <div class="header-schoolyear">
            <div class="header-name">Thông tin năm học</div>
            <div class="header-btn-insert" @click="clickBtnInsert()">Thêm năm học mới</div>
        </div>
        <div class="between-schoolyear">
            <table class="m-table" border="1">
                <thead>
                    <tr>
                        <th class="text-align-center" style="width: 100px">STT</th>
                        <th class="text-align-center">Tên năm học</th>
                        <th class="text-align-center">Ngày bắt đầu</th>
                        <th class="text-align-center">Ngày kết thúc</th>
                        <th class="text-align-center" style="width: 150px">Chức năng</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(schoolyear,i) in schoolyears" :key="schoolyear.SchoolYearId">
                        <th class="text-align-center">{{++i}}</th>
                        <th class="text-align-center">{{schoolyear.SchoolYearName}}</th>
                        <th class="text-align-center">{{fomatDate(schoolyear.DateStart)}}</th>
                        <th class="text-align-center">{{fomatDate(schoolyear.DateEnd)}}</th>
                        <th class="text-align-center">
                            <div class="box-function">
                                <div class="btn-edit" @click="clickBtnEdit(schoolyear.SchoolYearId)"></div>
                                <div class="btn-delete" @click="clickBtnDelete(schoolyear.SchoolYearId, schoolyear.SchoolYearName)"></div>
                            </div>
                        </th>
                    </tr>  
                </tbody>
            </table>
        </div>
        <div class="bottom-schoolyear">
            <div class="box-sum">Tổng số: {{schoolyears.length}} bản ghi</div>
        </div>
       <school-year-infor></school-year-infor>
       <delete-school-year></delete-school-year>
    </div>
</template>
<script>
import axios from 'axios'
import { eventBus } from '../../main'
import moment from 'moment'
import SchoolYearInfor from './SchoolYearInfor.vue'
import DeleteSchoolYear from './DeleteSchoolYear.vue'
import { mapGetters } from 'vuex'
export default {
    components: {
        SchoolYearInfor, DeleteSchoolYear
    },
    data() {
        return {
            schoolyears: [],
        }
    },
    computed:{
        ...mapGetters(['URLAPI' , 'token']),
    },
    methods: {
        /**
         * click vào btn thêm mới năm học
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnInsert(){
            eventBus.$emit("isShowBtnSaveAndInsertWas", true);
            eventBus.$emit("isShowSchoolYearInforWas", true);
            eventBus.$emit('formodWas', "add");
            eventBus.$emit('thisWas', this);
            eventBus.$emit('schoolyearWas', {});
        },
        /**
         * Lấy tất cả năm học
         * CreatedBy: MinhVN(28/01/2022)
         */
        loadDataSchoolYear(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/SchoolYears`,
                {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function(response){
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
            .catch(function(res){
                console.log(res);
            });
        },
        /**
         * click btn sửa
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnEdit(schoolyearId){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/SchoolYears/${schoolyearId}`,
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
                    eventBus.$emit('schoolyearWas', response.data.Data);
                } 
            })
            .catch(function (error){
                console.log(error);
            });
            eventBus.$emit("isShowSchoolYearInforWas", true);
            eventBus.$emit("isShowBtnSaveAndInsertWas", false);
            eventBus.$emit('formodWas', "edit");
            eventBus.$emit('thisWas', this);
        },
        /**
         * click btn xóa
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnDelete(schoolyearId, schoolyearName){
            eventBus.$emit('thisWas', this);
            eventBus.$emit("isShowDeleteSchoolYearWas", true);
            eventBus.$emit("schoolyearIdWas", schoolyearId);
            eventBus.$emit("titlenameWas", `bạn có thực sự muốn xóa < ${schoolyearName} > không?`);
        },

        /**
         * Định dạng lại ngày sinh để hiển thị 
         * CreatedBy: MinhVN (09/02/2022)
         */
        fomatDate(value){
            if (value){
                return moment(String(value)).format('DD-MM-YYYY');
            }
        }
    },
    created() {
        var m = this;
        m.loadDataSchoolYear();
    },
}
</script>