<template>
    <div class="t-dialog" v-show="isShowSchoolYearInfor">
         <div class="t-schoolyearinfor" >
            <div class="box-x" @click="clickBtnX()">
                <div class="icon-x"></div>
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập tên năm học</div>
                <input ref="schoolyearname" type="text" class="schoolyearinfor-input" placeholder="Nhập tên năm học" v-model="schoolyear.SchoolYearName">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập ngày bắt đầu</div>
                <input type="date" class="schoolyearinfor-input" :value="fomatDate(schoolyear.DateStart)" @input="schoolyear.DateStart = $event.target.value">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập ngày kết thúc</div>
                <input type="date" class="schoolyearinfor-input" :value="fomatDate(schoolyear.DateEnd)" @input="schoolyear.DateEnd = $event.target.value">
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
import axios from 'axios'
import { eventBus } from '../../main'
import { mapGetters } from 'vuex'
import moment from 'moment'
export default {
    data() {
        return {
            isShowSchoolYearInfor: false,
            isShowBtnSaveAndInsert: true,
            schoolyear: {},
            m2: null,
            formod: null,
        }
    },
    computed:{
        ...mapGetters(['user', 'URLAPI', 'token']),
    },
    methods: {
       /**
        * click Btn hủy
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnCancel(){
           this.isShowSchoolYearInfor = !this.isShowSchoolYearInfor;
       },
       /**
        * click Btn X
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnX(){
           this.isShowSchoolYearInfor = !this.isShowSchoolYearInfor;
       },
       /**
        * click Btn lưu
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnSave()
       {
            var m = this;

            if (m.schoolyear.DateStart == "")
            {
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Ngày bắt đầu không được để trống");
                return;
            }

            if (m.schoolyear.DateEnd == "")
            {
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Ngày kết thúc không được để trống");
                return;
            }

            if(m.formod == "add")
            {
                m.schoolyear.CreatedBy = m.user.UserName;
                axios({
                    method: "post",
                    url: `${m.URLAPI}/api/v1/SchoolYears`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.schoolyear,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        m.schoolyear = {};
                        m.m2.loadDataSchoolYear();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Thêm năm học thành công'
                        eventBus.$emit("TitleToastMessageWas", "Thêm năm học thành công");
                        m.isShowSchoolYearInfor = !m.isShowSchoolYearInfor;
                        console.log(response);
                    }
                })
                .catch(function (error) {
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${error}`);
                });
            }
            else if(m.formod == "edit")
            {
                m.schoolyear.ModifiedBy = m.user.UserName;
                axios({
                    method: "put",
                    url: `${m.URLAPI}/api/v1/SchoolYears/${m.schoolyear.SchoolYearId}`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.schoolyear,
                })
                .then(function (response) {
                    if (response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else {
                        m.schoolyear = {};
                        m.m2.loadDataSchoolYear();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Sửa năm học thành công'
                        eventBus.$emit("TitleToastMessageWas", "Sửa năm học thành công");
                        m.isShowSchoolYearInfor = !m.isShowSchoolYearInfor;
                        console.log(response);
                    }
                })
                .catch(function (error) {
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${error}`);
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
           m.schoolyear.CreatedBy = m.user.UserName;
           axios({
                method: "post",
                url: `${m.URLAPI}/api/v1/SchoolYears`,
                headers: { 'Authorization': 'Bearer ' + m.token},
                data: m.schoolyear,
            })
            .then(function (response) {
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else{
                    m.schoolyear = {};
                    // gửi dữ liệu sang component FormToastMessage là 'true'
                    eventBus.$emit("isShowToastMessageWas", true);
                    // gửi dữ liệu sang component FormToastMessage là 'Thêm năm học thành công'
                    eventBus.$emit("TitleToastMessageWas", "Thêm năm học thành công");
                    m.m2.loadDataSchoolYear();
                    m.$refs.schoolyearname.focus();
                    console.log(response);
                }
            })
            .catch(function (error) {
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", `${error}`);
            });
       },
       /**
         * Định dạng lại ngày sinh để hiển thị 
         * CreatedBy: MinhVN (10/02/2022)
         */
        fomatDate(value){
            if (value){
                return moment(String(value)).format('YYYY-MM-DD');
            }
        }
    },
    created() {
        var  m = this;
        eventBus.$on("isShowSchoolYearInforWas", (isShowSchoolYearInforData) =>{
            m.isShowSchoolYearInfor = isShowSchoolYearInforData;
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
        eventBus.$on("schoolyearWas", (schoolyearData) =>{
            m.schoolyear = schoolyearData;
        });
    },
}
</script>