<template>
    <div class="t-dialog" v-show="isShowDecentralizationInfor">
         <div class="t-schoolyearinfor" >
            <div class="box-x" @click="clickBtnX()">
                <div class="icon-x"></div>
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập tên quyền</div>
                <input ref="decentralizationname" type="text" class="schoolyearinfor-input" placeholder="Nhập tên quyền" v-model="decentralization.DecentralizationName">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập mô tả quyền</div>
                <input ref="subjectname" type="text" class="schoolyearinfor-input" placeholder="Nhập mô tả quyền" v-model="decentralization.Description">
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
import { mapGetters } from 'vuex'
export default {
    data() {
        return {
            isShowDecentralizationInfor: false,
            isShowBtnSaveAndInsert: true,
            decentralization: {},
            m2: null,
            formod: null,
        }
    },
    computed: {
        ...mapGetters(['URLAPI', 'token']),
    },
    methods: {
       /**
        * click Btn hủy
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnCancel(){
           this.isShowDecentralizationInfor = !this.isShowDecentralizationInfor;
       },
       /**
        * click Btn X
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnX(){
           this.isShowDecentralizationInfor = !this.isShowDecentralizationInfor;
       },
       /**
        * click Btn lưu
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnSave()
       {
            var m = this;
            if(m.formod == "add")
            {
                axios({
                    method: "post",
                    url: `${m.URLAPI}/api/v1/Decentralizations`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.decentralization,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        m.decentralization = {};
                        m.m2.loadDataDecentralization();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Thêm quyền thành công'
                        eventBus.$emit("TitleToastMessageWas", "Thêm quyền thành công");
                        m.isShowDecentralizationInfor = !m.isShowDecentralizationInfor;
                        console.log(response);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
            }
            else if(m.formod == "edit")
            {
                axios({
                    method: "put",
                    url: `${m.URLAPI}/api/v1/Decentralizations/${m.decentralization.DecentralizationId}`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.decentralization,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else {
                        m.decentralization = {};
                        m.m2.loadDataDecentralization();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Sửa quyền thành công'
                        eventBus.$emit("TitleToastMessageWas", "Sửa quyền thành công");
                        m.isShowDecentralizationInfor = !m.isShowDecentralizationInfor;
                        console.log(response);
                    }
                })
                .catch(function (error) {
                    console.log(error);
                });
            }
       },
       /**
        * click Btn lưu và thêm
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnSaveAndInsert()
       {
           var m = this;
           axios({
                method: "post",
                url: `${m.URLAPI}/api/v1/Decentralizations`,
                headers: { 'Authorization': 'Bearer ' + m.token},
                data: m.decentralization,
            })
            .then(function (response) {
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else{
                    m.decentralization = {};
                    m.m2.loadDataDecentralization();
                    // gửi dữ liệu sang component FormToastMessage là 'true'
                    eventBus.$emit("isShowToastMessageWas", true);
                    // gửi dữ liệu sang component FormToastMessage là 'Thêm quyền thành công'
                    eventBus.$emit("TitleToastMessageWas", "Thêm quyền thành công");
                    m.$refs.decentralizationname.focus();
                    console.log(response);
                }
            })
            .catch(function (error) {
                console.log(error);
            });
       }
    },
    created() {
        var  m = this;
        eventBus.$on("isShowDecentralizationInforWas", (isShowDecentralizationInforData) =>{
            m.isShowDecentralizationInfor = isShowDecentralizationInforData;
        });
        eventBus.$on("isShowBtnSaveAndInsertWas", (isShowBtnSaveAndInsertData) =>{
            m.isShowBtnSaveAndInsert = isShowBtnSaveAndInsertData;
        });
        eventBus.$on("thisWas", (thisData) =>{
            m.m2 = thisData;
        });
        eventBus.$on("formodWas", (formodData) =>{
            m.formod = formodData;
        });
        eventBus.$on("decentralizationWas", (decentralizationData) =>{
            m.decentralization = decentralizationData;
        });
    },
}
</script>