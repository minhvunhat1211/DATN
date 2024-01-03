<template>
    <div class="t-schoolyear">
        <div class="header-schoolyear">
            <div class="header-name">Thông tin quyền</div>
            <div class="header-btn-insert" @click="clickBtnInsert()">Thêm quyền mới</div>
        </div>
        <div class="between-schoolyear">
            <table class="m-table" border="1">
                <thead>
                    <tr>
                        <th class="text-align-center" style="width: 100px">STT</th>
                        <th class="text-align-center">Tên quyền</th>
                        <th class="text-align-center">Mô tả quyền</th>
                        <th class="text-align-center" style="width: 150px">Chức năng</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(decentralization,i) in decentralizations" :key="decentralization.DecentralizationId">
                        <th class="text-align-center">{{++i}}</th>
                        <th class="text-align-center">{{decentralization.DecentralizationName}}</th>
                        <th class="text-align-center">{{decentralization.Description}}</th>
                        <th class="text-align-center">
                            <div class="box-function">
                                <div class="btn-edit" @click="clickBtnEdit(decentralization.DecentralizationId)"></div>
                                <div class="btn-delete" @click="clickBtnDelete(decentralization.DecentralizationId, decentralization.DecentralizationName)"></div>
                            </div>
                        </th>
                    </tr>  
                </tbody>
            </table>
        </div>
        <div class="bottom-schoolyear">
            <div class="box-sum">Tổng số: {{decentralizations.length}} bản ghi</div>
        </div>
        <decentralization-infor></decentralization-infor>
        <delete-decentralization></delete-decentralization>
    </div>
</template>
<script>
import axios from 'axios'
import { eventBus } from '../../main'
import DecentralizationInfor from './DecentralizationInfor.vue'
import DeleteDecentralization from './DeleteDecentralization.vue'
import { mapGetters } from 'vuex'
export default {
  components: { DecentralizationInfor, DeleteDecentralization },
    data() {
        return {
            decentralizations: [],
        }
    },
    computed: {
        ...mapGetters(['URLAPI', 'token']),
    },
    methods: {
        /**
         * click vào btn thêm mới năm học
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnInsert(){
            eventBus.$emit("isShowBtnSaveAndInsertWas", true);
            eventBus.$emit("isShowDecentralizationInforWas", true);
            eventBus.$emit('formodWas', "add");
            eventBus.$emit('thisWas', this);
            eventBus.$emit('decentralizationWas', {});
        },
        /**
         * Lấy tất cả năm học
         * CreatedBy: MinhVN(28/01/2022)
         */
        loadDataDecentralization(){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Decentralizations`, {'headers': { 'Authorization': 'Bearer ' + m.token}})
            .then(function(response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                {
                    m.decentralizations = response.data.Data;
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
        clickBtnEdit(decentralizationId){
            var m = this;
            axios
            .get(`${m.URLAPI}/api/v1/Decentralizations/${decentralizationId}`,
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
                    eventBus.$emit('decentralizationWas', response.data.Data);
                } 
            })
            .catch(function (res){
                console.log("api lỗi" + res);
            });
            eventBus.$emit("isShowDecentralizationInforWas", true);
            eventBus.$emit("isShowBtnSaveAndInsertWas", false);
            eventBus.$emit('formodWas', "edit");
            eventBus.$emit('thisWas', this);
        },
        /**
         * click btn xóa
         * CreatedBy: MinhVN(28/01/2022)
         */
        clickBtnDelete(decentralizationId, decentralizationName){
            eventBus.$emit('thisWas', this);
            eventBus.$emit("isShowDeleteDecentralizationWas", true);
            eventBus.$emit("decentralizationIdWas", decentralizationId);
            eventBus.$emit("titlenameWas", `bạn có thực sự muốn xóa quyền < ${decentralizationName} > không?`);
        },
    },
    created() {
        var m = this;
        m.loadDataDecentralization();
    },
    
}
</script>