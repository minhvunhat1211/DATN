<template>
    <div class="t-schoolyear">
        <div class="header-schoolyear">
            <div class="header-name">Thông tin học kì</div>
            <div class="header-btn-insert" @click="clickBtnInsert()">Thêm học kì mới</div>
        </div>
        <div class="between-schoolyear">
            <table class="m-table" border="1">
                <thead>
                    <tr>
                        <th class="text-align-center" style="width: 100px">STT</th>
                        <th class="text-align-center">Tên học kì</th>
                        <th class="text-align-center">Ngày bắt đầu</th>
                        <th class="text-align-center">Ngày kết thúc</th>
                        <th class="text-align-center">Năm học</th>
                        <th class="text-align-center" style="width: 150px">Chức năng</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(semester,i) in semesters" :key="semester.SemesterId">
                        <th class="text-align-center">{{++i}}</th>
                        <th class="text-align-center">{{semester.SemesterName}}</th>
                        <th class="text-align-center">{{fomatDate(semester.DateStart)}}</th>
                        <th class="text-align-center">{{fomatDate(semester.DateEnd)}}</th>
                        <th class="text-align-center">{{semester.SchoolYearName}}</th>
                        <th class="text-align-center">
                            <div class="box-function">
                                <div class="btn-edit" @click="clickBtnEdit(semester.SemesterId)"></div>
                                <div class="btn-delete" @click="clickBtnDelete(semester.SemesterId, semester.SemesterName)"></div>
                            </div>
                        </th>
                    </tr>  
                </tbody>
            </table>
        </div>
        <div class="bottom-schoolyear">
            <div class="box-sum">Tổng số: {{semesters.length}} bản ghi</div>
        </div>
        <semester-infor></semester-infor>
        <delete-semester></delete-semester>
    </div>
</template>
<script>
import axios from 'axios'
import { eventBus } from '../../main'
import SemesterInfor from './SemesterInfor.vue'
import DeleteSemester from './DeleteSemester.vue'
import { mapGetters } from 'vuex'
import moment from 'moment'
export default {
    components: {
        DeleteSemester, SemesterInfor
    },
    data() {
        return {
            semesters: [],
        }
    },
    computed: {
        ...mapGetters(['user', 'URLAPI', 'token']),
    },
    methods: {
        /**
         * click vào btn thêm mới năm học
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnInsert(){
            eventBus.$emit("isShowBtnSaveAndInsertWas", true);
            eventBus.$emit("isShowSemesterInforWas", true);
            eventBus.$emit('formodWas', "add");
            eventBus.$emit('thisWas', this);
            eventBus.$emit('semesterWas', {});
        },
        /**
         * Lấy tất cả năm học
         * CreatedBy: MinhVN(28/01/2022)
         */
        loadDataSemester(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Semesters`,
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
                    m.semesters = response.data.Data;
                }
            })
            .catch(function(res){
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", `${res}`);
            });
        },
        /**
         * click btn sửa
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnEdit(semesterId){
            var m = this;

            axios
            .get(`${m.URLAPI}/api/v1/Semesters/${semesterId}`,
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
                    eventBus.$emit('semesterWas', response.data.Data);
                } 
            })
            .catch(function (res){
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", `${res}`);
            });

            eventBus.$emit("isShowSemesterInforWas", true);
            eventBus.$emit("isShowBtnSaveAndInsertWas", false);
            eventBus.$emit('formodWas', "edit");
            eventBus.$emit('thisWas', this);
        },
        /**
         * click btn xóa
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnDelete(semesterId, semesterName){
            eventBus.$emit('thisWas', this);
            eventBus.$emit("isShowDeleteSemesterWas", true);
            eventBus.$emit("semesterIdWas", semesterId);
            eventBus.$emit("titlenameWas", `bạn có thực sự muốn xóa < ${semesterName} > không?`);
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
        m.loadDataSemester();
    },
}
</script>