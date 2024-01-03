<template>
    <div class="box-infor">
        <div class="header-infor">Thông tin cá nhân</div>
        <div class="box-input-changepassword">
            <div class="name-changepassword">Mã</div>
            <input v-if="user.DecentralizationName == 'Học sinh'" class="input-changepassword" type="text" readonly :value="infor.StudentCode">
            <input v-else class="input-changepassword" type="text" readonly :value="infor.TeacherCode">
        </div>
        <div class="box-input-changepassword">
            <div class="name-changepassword">Họ và tên</div>
            <input ref="fullname" id="fullname" class="input-changepassword" type="text" readonly :value="infor.FullName">
        </div>
        <div class="box-input-changepassword">
            <div class="name-changepassword">Giới tính</div>
            <input id="phonenumber" class="input-changepassword" type="text" readonly :value="fomatGender(infor.Gender)">
        </div>
        <div class="box-input-changepassword">
            <div class="name-changepassword">Ngày sinh</div>
            <input id="phonenumber" class="input-changepassword" type="text" readonly :value="FomatDate(infor.DateOfBirth)">
        </div>
        <div class="box-input-changepassword">
            <div class="name-changepassword">Số điện thoại</div>
            <input id="phonenumber" class="input-changepassword" type="text" readonly :value="infor.PhoneNumber">
        </div>
        <div class="box-input-changepassword">
            <div class="name-changepassword">Email</div>
            <input id="phonenumber" class="input-changepassword" type="text" readonly :value="infor.Email">
        </div>
        <div class="box-input-changepassword">
            <div class="name-changepassword">Địa chỉ</div>
            <input id="phonenumber" class="input-changepassword" type="text" readonly :value="infor.Address">
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import { mapGetters } from 'vuex';
import moment from 'moment';
import { eventBus } from '../main.js';
export default {
    data() {
        return {
            infor: {}
        }
    },
    computed:{
        ...mapGetters(['user', 'URLAPI', 'token']),
    },
    methods: {
         /**
         * Định dạng lại giới tính 
         * CreatedBy: MinhVN (25/01/2022)
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
         * CreatedBy: MinhVN (25/01/2022)
         */
        FomatDate(value){
            if (value){
                return moment(String(value)).format('DD-MM-YYYY');
            }
        },

        /**
         * Lấy thông tin của một student
         * CreatedBy: MinhVN (07/03/2022)
         */
        LoadDataInformationStudent()
        {
            var m = this;
            
            // Kiểm tra quyền đăng nhập là học sinh hay giáo viên
            if (m.user.DecentralizationName == "Học sinh")
            {
                axios
                .get(`${m.URLAPI}/api/v1/Students/GetInformationByEmail?email=${m.user.Email}`,
                    {'headers': { 'Authorization': 'Bearer ' + m.token }})
                .then(function (response){
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else
                    { 
                        m.infor = response.data.Data;
                    }
                })
                .catch(function (res){
                    console.log(res);
                });
            }
            else{
                axios
                .get(`${m.URLAPI}/api/v1/Teachers/GetInformationByEmail?email=${m.user.Email}`,
                    {'headers': { 'Authorization': 'Bearer ' + m.token }})
                .then(function (response){
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else
                    { 
                        m.infor = response.data.Data;
                    }
                })
                .catch(function (res){
                    console.log(res);
                });
            }
        }
    },
    created() {
        var m = this;
        m.LoadDataInformationStudent();
    }
}
</script>
