<template>
    <div class="t-schoolyear">
        <div class="header-schoolyear">
            <div class="header-name">Thông tin môn học</div>
            <div class="header-btn-insert" @click="clickBtnInsert()">Thêm môn học mới</div>
        </div>
        <div class="between-schoolyear">
            <table class="m-table" border="1">
                <thead>
                    <tr>
                        <th class="text-align-center" style="width: 100px">STT</th>
                        <th class="text-align-center">Tên môn học</th>
                        <th class="text-align-center">Số tiết/năm/lớp</th>
                        <th class="text-align-center">Số đơn vị học phần</th>
                        <th class="text-align-center" style="width: 150px">Chức năng</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(subject,i) in subjects" :key="subject.SubjectId">
                        <th class="text-align-center">{{++i}}</th>
                        <th class="text-align-center">{{subject.SubjectName}}</th>
                        <th class="text-align-center">{{subject.PeriodsPerYear}}</th>
                        <th class="text-align-center">{{subject.Credit}}</th>
                        <th class="text-align-center">
                            <div class="box-function">
                                <div class="btn-edit" @click="clickBtnEdit(subject.SubjectId)"></div>
                                <div class="btn-delete" @click="clickBtnDelete(subject.SubjectId, subject.SubjectName)"></div>
                            </div>
                        </th>
                    </tr>  
                </tbody>
            </table>
        </div>
        <div class="bottom-schoolyear">
            <div class="box-sum">Tổng số: {{subjects.length}} bản ghi</div>
        </div>
        <subject-infor></subject-infor>
        <delete-subject></delete-subject>
    </div>
</template>
<script>
import axios from 'axios'
import { eventBus } from '../../main'
import SubjectInfor from './SubjectInfor.vue'
import DeleteSubject from './DeleteSubject.vue'
import { mapGetters } from 'vuex'
export default {
  components: { SubjectInfor, DeleteSubject },

    data() {
        return {
            subjects: [],
        }
    },
    computed: {
        ...mapGetters(['URLAPI' , 'token']),
    },
    methods: {
        /**
         * click vào btn thêm mới năm học
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnInsert(){
            eventBus.$emit("isShowBtnSaveAndInsertWas", true);
            eventBus.$emit("isShowSubjectInforWas", true);
            eventBus.$emit('formodWas', "add");
            eventBus.$emit('thisWas', this);
            eventBus.$emit('subjectWas', {});
        },
        /**
         * Lấy tất cả năm học
         * CreatedBy: MinhVN(28/01/2022)
         */
        loadDataSubject(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Subjects`, 
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
                    m.subjects = response.data.Data;
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
        clickBtnEdit(subjectId){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Subjects/${subjectId}`,
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
                    eventBus.$emit('subjectWas', response.data.Data);
                } 
            })
            .catch(function (res){
                console.log("api lỗi" + res);
            });
            eventBus.$emit("isShowSubjectInforWas", true);
            eventBus.$emit("isShowBtnSaveAndInsertWas", false);
            eventBus.$emit('formodWas', "edit");
            eventBus.$emit('thisWas', this);
        },
        /**
         * click btn xóa
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnDelete(subjectId, subjectName){
            eventBus.$emit('thisWas', this);
            eventBus.$emit("isShowDeleteSubjectWas", true);
            eventBus.$emit("subjectIdWas", subjectId);
            eventBus.$emit("titlenameWas", `bạn có thực sự muốn xóa < ${subjectName} > không?`);
        },
    },
    created() {
        var m = this;
        m.loadDataSubject();
    },
    
}
</script>