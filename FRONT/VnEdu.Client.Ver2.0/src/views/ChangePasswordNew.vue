<template>
    <div class="t-login">
        <div class="login-opacity">
            <div class="box-login">
                <div class="login-top">
                    <div class="logo-login">
                    </div>
                </div>
                <div class="box-pass-user">
                    <input @keyup.enter="clickBtnSavePassword()" type="password" id="password" class="box-input" placeholder="Nhập mật khẩu mới" v-model="newpassword">
                    <div class="icon-eye" @click="clickBtnIconEye()" :class="{'hide-password': isHidePassword}"></div>
                </div>
                <div class="box-pass-user">
                    <input @keyup.enter="clickBtnSavePassword()" type="password" id="password1" class="box-input" placeholder="Nhập lại mật khẩu mới" v-model="retypenewpassword">
                    <div class="icon-eye" @click="clickBtnIconEye1()" :class="{'hide-password': isHidePassword1}"></div>
                </div>
                <button v-show="isShowChangePasswordNew" class="btn-login" @click="clickBtnSavePassword()">Cập nhật mật khẩu mới</button>
                <button v-show="isShowLoading" class="btn-login">
                    <div class="spinner-border" role="status">
                        <span class="sr-only">Loading...</span>
                    </div>
                </button>
            </div>
        </div>
        <form-waning></form-waning>
        <toast-message></toast-message>
    </div>
</template>

<script>
import axios from 'axios';
import { mapGetters } from 'vuex';
import { eventBus } from'../main.js'
import FormWaning from '../views/FormWaning.vue'
import ToastMessage from '../views/ToastMessage.vue'
export default {
    name: 'ChangePasswordNew',
    components:{
        FormWaning, ToastMessage
    },
    data() {
        return {
            isHidePassword: false,
            isHidePassword1: false,
            phoneNumber: null,
            newpassword: null,
            retypenewpassword: null,
            isShowChangePasswordNew: true,
            isShowLoading: false,
        }
    },
    computed: {
        ...mapGetters(['URLAPI', 'email']),
    },

    methods: {
        /**
         * click vào btn lưu mật khẩu
         * CreatedBy: MinhVN(16/01/2022)
         */
        clickBtnSavePassword(){
            var m = this;

            
            
            if(m.newpassword == null || m.newpassword == "" || m.retypenewpassword == null || m.retypenewpassword == "")
            {
                eventBus.$emit("errorWas", "Các trường dữ liệu không được để trống");
                eventBus.$emit("isShowFormWaningWas", true);
            }
            else
            {   
                if(m.retypenewpassword != m.newpassword)
                {
                    eventBus.$emit("errorWas", "Mật khẩu nhập lại không giống với mật khẩu đã nhập trước đó.");
                    eventBus.$emit("isShowFormWaningWas", true);
                }
                else
                {

                    m.isShowChangePasswordNew = false;
                    m.isShowLoading = true;
                    
                    let objectNewPassword = {
                        Password: m.newpassword
                    }
                    /**
                     * gọi APi thay đổi mật khẩu
                     * CreatedBy: MinhVN(26/01/2022)
                     */
                    axios
                    .put(`${m.URLAPI}/api/v1/Authentications/ChangePasswordNew?email=${m.email}`, objectNewPassword,)
                    .then(function (response){
                        if(response.data.IsError)
                        { 
                            // gửi dữ liệu đến component FormWaning là 'true'
                            eventBus.$emit("isShowFormWaningWas", true);
                            eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                        }
                        else
                        {
                            eventBus.$emit("isShowToastMessageWas", true);
                            eventBus.$emit("TitleToastMessageWas", "Đổi mật khẩu thành công");
                            setTimeout(function(){
                                // chuyển đến trang main
                                m.$router.push({ path: "/login" });
                            }, 1000);
                        }

                        m.isShowChangePasswordNew = true;
                        m.isShowLoading = false;
                    })
                    .catch(function (res){
                        console.log(res);
                    });
                }
            }
        },

        /**
         * click vào btn con mắt
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnIconEye(){
            this.isHidePassword = !this.isHidePassword;
            let passwordField = document.querySelector("#password");
            if(this.isHidePassword) 
            {
                passwordField.setAttribute("type", "text");
            } 
            else
            {
                passwordField.setAttribute("type", "password");
            }
        },
        /**
         * click vào btn con mắt
         * CreatedBy: MinhVN(11/01/2022)
         */
        clickBtnIconEye1(){
            this.isHidePassword1 = !this.isHidePassword1;
            let passwordField = document.querySelector("#password1");
            if(this.isHidePassword1) 
            {
                passwordField.setAttribute("type", "text");
            } 
            else
            {
                passwordField.setAttribute("type", "password");
            }
        },
    },
}
</script>