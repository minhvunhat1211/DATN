<template>
    <div class="t-dialog" v-show="isShowTeacherInfor">
         <div class="t-schoolyearinfor" >
            <div class="box-x" @click="clickBtnX()">
                <div class="icon-x"></div>
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập mã giáo viên</div>
                <input ref="teachercode" type="text" class="schoolyearinfor-input" placeholder="Nhập mã giáo viên" v-model="teacher.TeacherCode">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập tên giáo viên</div>
                <input type="text" class="schoolyearinfor-input" placeholder="Nhập tên giáo viên" v-model="teacher.FullName">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Chọn giới tính</div>
               <div class="box-size box-student-combobox">
                    <input type="text" class="box-size-input box-student-input" readonly :value="fomatGender(teacher.Gender)">
                    <div class="box-select-size box-student-select" @click="clickBtnSelectGender()">
                        <div class="icon-down"></div>
                    </div>
                    <div class="box-size-item box-item-student" v-show="isShowBoxItemGender">
                        <div v-for="gender in genders" :key="gender.GenderId" class="item-size item-student" @click="clickItemGender(gender.GenderId)">{{gender.GenderName}}</div>
                    </div>
                </div>
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập ngày sinh</div>
                <input :max="getDateToday()" ref="administrationcode" type="date" class="schoolyearinfor-input" :value="fomatDate(teacher.DateOfBirth)" @input="teacher.DateOfBirth = $event.target.value">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập số điện thoại</div>
                <input  type="text" class="schoolyearinfor-input" placeholder="Nhập số điện thoại" v-model="teacher.PhoneNumber">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập email</div>
                <input  type="text" class="schoolyearinfor-input" placeholder="Nhập email" v-model="teacher.Email">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập số CMND/CCCD</div>
                <input  type="text" class="schoolyearinfor-input" placeholder="Nhập số CMND/CCCD" v-model="teacher.NumberCard">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập địa chỉ hiện tại</div>
                <input  type="text" class="schoolyearinfor-input" placeholder="Nhập địa chỉ hiện tại" v-model="teacher.AddressCurent">
            </div>
            <div class="box-schoolyearinfor-top">
                <div class="schoolyearinfor-name">Nhập quê quán</div>
                <input  type="text" class="schoolyearinfor-input" placeholder="Nhập quê quán" v-model="teacher.Address">
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
import moment from 'moment';
import { eventBus } from '../../main'
import { mapGetters } from 'vuex';
export default {
    data() {
        return {
            isShowTeacherInfor: false,
            isShowBtnSaveAndInsert: true,
            isShowBoxItemGender: false,
            teacher: {},
            m2: null,
            formod: null,
            genders: [
                {
                    GenderId: 0,
                    GenderName: "Nữ"
                },
                {
                    GenderId: 1,
                    GenderName: "Nam"
                },
                {
                    GenderId: 2,
                    GenderName: "Khác"
                }
            ],
        }
    },
    computed:{
        ...mapGetters(['user', 'URLAPI', 'token']),
    },
    methods: {
        /**
         * Định dạng lại giới tính 
         * CreatedBy: MinhVN (09/02/2022)
         */
        fomatGender(value){
            if (value != null){
                if(value == 0)
                {
                    return "Nữ";
                }
                else if(value == 1)
                {
                    return "Nam";
                }
                else if(value == 2){
                    return "Khác";
                }
            }
            else{
                return "";
            }
        },
        /**
         * Định dạng lại ngày sinh để hiển thị 
         * CreatedBy: MinhVN (10/02/2022)
         */
        fomatDate(value){
            if (value){
                return moment(String(value)).format('YYYY-MM-DD');
            }
        },
        /**
         * Lấy ngày hôm nay 
         * CreatedBy: MinhVN (10/02/2022)
         */
        getDateToday(){
            let today = new Date();
            let dd = today.getDate();
            let mm = today.getMonth() + 1;
            let yyyy = today.getFullYear();
            if (dd < 10) {
            dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }
            today = yyyy + '-' + mm + '-' + dd;
            return today;
        },
        /**
        * click Btn lựa chọn giới tính
        * CreatedBy: MinhVN(10/02/2022)
        */
        clickBtnSelectGender(){
            this.isShowBoxItemGender = !this.isShowBoxItemGender;
        },
        /**
        * click vào 1 giới tính 
        * CreatedBy: MinhVN(10/02/2022)
        */
        clickItemGender(genderId){
            this.teacher.Gender = genderId;
            this.isShowBoxItemGender = !this.isShowBoxItemGender;
        },
    
       /**
        * click Btn hủy
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnCancel(){
           this.isShowTeacherInfor = !this.isShowTeacherInfor;
       },
       /**
        * click Btn X
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnX(){
           this.isShowTeacherInfor = !this.isShowTeacherInfor;
       },
       /**
        * click Btn lưu
        * CreatedBy: MinhVN(28/01/2022)
        */
       clickBtnSave()
       {
            var m = this;
            m.teacher.CreatedBy = m.user.UserName;
            if(m.formod == "add")
            {
                axios({
                    method: "post",
                    url: `${m.URLAPI}/api/v1/Teachers`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.teacher,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        m.teacher = {};
                        m.m2.loadDataTeacher();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Thêm giáo viên thành công'
                        eventBus.$emit("TitleToastMessageWas", "Thêm giáo viên thành công");
                        m.isShowTeacherInfor = !m.isShowTeacherInfor;
                        console.log(response);
                    }
                })
                .catch(function (error) {
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning 
                    eventBus.$emit("errorWas", error);
                });
            }
            else if(m.formod == "edit")
            {
                m.teacher.ModifiedBy = m.user.UserName;
                axios({
                    method: "put",
                    url: `${m.URLAPI}/api/v1/Teachers/${m.teacher.TeacherId}`,
                    headers: { 'Authorization': 'Bearer ' + m.token},
                    data: m.teacher,
                })
                .then(function (response) {
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        m.teacher = {};
                        m.m2.loadDataTeacher();
                        // gửi dữ liệu sang component FormToastMessage là 'true'
                        eventBus.$emit("isShowToastMessageWas", true);
                        // gửi dữ liệu sang component FormToastMessage là 'Sửa giáo viên thành công'
                        eventBus.$emit("TitleToastMessageWas", "Sửa giáo viên thành công");
                        m.isShowTeacherInfor = !m.isShowTeacherInfor;
                        console.log(response);
                    }
                })
                .catch(function (error) {
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    // gửi dữ liệu đến component FormWaning 
                    eventBus.$emit("errorWas", error);
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
           m.teacher.CreatedBy = m.user.UserName;
           axios({
                method: "post",
                url: `${m.URLAPI}/api/v1/Teachers`,
                headers: { 'Authorization': 'Bearer ' + m.token},
                data: m.teacher,
            })
            .then(function (response) {
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else{
                    m.teacher = {};
                    // gửi dữ liệu sang component FormToastMessage là 'true'
                    eventBus.$emit("isShowToastMessageWas", true);
                    // gửi dữ liệu sang component FormToastMessage là 'Thêm giáo viên thành công'
                    eventBus.$emit("TitleToastMessageWas", "Thêm giáo viên thành công");
                    m.m2.loadDataTeacher();
                    m.$refs.teachercode.focus();
                    console.log(response);
                }
            })
            .catch(function (error) {
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                // gửi dữ liệu đến component FormWaning 
                eventBus.$emit("errorWas", error);
            });
       }
    },
    created() {
        var  m = this;
        eventBus.$on("isShowTeacherInforWas", (isShowTeacherInforData) =>{
            m.isShowTeacherInfor = isShowTeacherInforData;
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
        eventBus.$on("teacherWas", (teacherData) =>{
            m.teacher = teacherData;
        });
    },
    updated(){
        var  m = this;
        eventBus.$on("nullWas", (nullData) =>{
            m.teacher.GenderName = nullData;
        });
    }
}
</script>