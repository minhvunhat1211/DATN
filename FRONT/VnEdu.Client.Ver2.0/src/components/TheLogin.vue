<template>
    <div class="t-login">
        <div class="login-opacity">
            <div class="box-login">
                <div class="login-top">
                    <div class="logo-login">
                    </div>
                </div>
                <input @keyup.enter="clickBtnLogin()" ref="username" type="text" class="box-input" placeholder="Tên đăng nhập" v-model="phoneNumber">
                <div class="box-pass-user">
                    <input @keyup.enter="clickBtnLogin()" type="password" id="password" class="box-input" placeholder="Mật khẩu" v-model="passWord">
                    <div class="icon-eye" @click="clickBtnIconEye()" :class="{'hide-password': isHidePassword}"></div>
                </div>
                <div class="box-forgot-password">
                    <router-link style="text-decoration: none;" to="/emailverification">
                        <div class="bottom-name" style="color: blue;">Quên mật khẩu?</div>
                    </router-link>
                </div>
                <button v-show="isShowIconLogin" class="btn-login" @click="clickBtnLogin()">Đăng nhập</button>
                <button v-show="isShowLoading" class="btn-login">
                    <div class="spinner-border" role="status">
                        <span class="sr-only">Loading...</span>
                    </div>
                </button>
            </div>
            <div class="title-copyright">Copyright © 2023 - 2024 VNEDU</div>
        </div>
        <form-waning></form-waning>
        <toast-message></toast-message>
    </div>
</template>

<script>
import axios from "axios";
import FormWaning from '../views/FormWaning.vue'
import ToastMessage from '../views/ToastMessage.vue'
import { eventBus } from '../main.js';
import { mapGetters, mapMutations } from 'vuex';
export default {
    name: 'TheLogin',
    components:{
        FormWaning, ToastMessage
    },
    data() {
        return {
            isHidePassword: false,
            phoneNumber: null,
            passWord: null,
            isShowIconLogin: true,
            isShowLoading: false
        }
    },
    computed:{
        ...mapGetters(['user', 'URLAPI', 'token']),
    },
    methods: {
        ...mapMutations(['setUser', 'setToken']),
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
         * click vào link quên mật khẩu
         * CreatedBy: MinhVN(19/01/2022)
         */
        clickLinkForgotPassword(){
            // gửi dữ liệu đến component FormWaning là 'true'
            eventBus.$emit("isShowFormWaningWas", true);
            // gửi dữ liệu đến component FormWaning là 'Có lỗi xảy ra, liên hệ với nhà trường để được trợ giúp.'
            eventBus.$emit("errorWas", "Có lỗi xảy ra, liên hệ với nhà trường để được trợ giúp.");
        },

        /**
         * click vào btn login
         * CreatedBy: MinhVN(13/01/2022)
         */
        clickBtnLogin(){
            var m = this;

            m.isShowIconLogin = false;
            m.isShowLoading = true;

            let userLogin = {
                "Email": m.phoneNumber,
                "Password": m.passWord
            }

            if(m.phoneNumber == null || m.passWord == null)
            {
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                // gửi dữ liệu đến component FormWaning là 'Tên đăng nhập hoặc mật khẩu không được để trống'
                eventBus.$emit("errorWas", "Tên đăng nhập hoặc mật khẩu không được để trống");

                m.isShowIconLogin = true;
                m.isShowLoading = false;
            }
            else
            {
                axios
                .post(`${m.URLAPI}/api/v1/Authentications/LoginForEmail`, userLogin)
                .then(function (response){
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);

                        m.isShowIconLogin = true;
                        m.isShowLoading = false;
                    }
                    else{
                        var m1 = m;
                        m1.setToken(response.data.Data);

                        axios
                        .get(`${m1.URLAPI}/api/v1/users/GetUserById`, {'headers': { 'Authorization': 'Bearer ' + m1.token}})
                        .then(function(response){
                            if(response.data.IsError)
                            { 
                                // gửi dữ liệu đến component FormWaning là 'true'
                                eventBus.$emit("isShowFormWaningWas", true);
                                eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);

                                m.isShowIconLogin = true;
                                m.isShowLoading = false;
                            }
                            else {
                                m1.setUser(response.data.Data);
                                localStorage.setItem('jwt', response.data.Data);
                                // chuyển đến trang main
                                m1.$router.push({ path: "/main" });

                                m.isShowIconLogin = true;
                                m.isShowLoading = false;
                            }
                        })
                        .catch(function(res){
                            console.log(res);
                        });
                    }
                })
                .catch(function (res){
                    console.log(res);
                });
            }
        },

        /**
         * click vào btn login
         * CreatedBy: MinhVN(13/01/2022)
         */
        keyupBtnLogin(){
            var m = this;

            let userLogin = {
                "Email": m.phoneNumber,
                "Password": m.passWord
            }

            if(m.phoneNumber == null || m.passWord == null)
            {
                // gửi dữ liệu đến component FormWaning là 'true'
                eventBus.$emit("isShowFormWaningWas", true);
                // gửi dữ liệu đến component FormWaning là 'Tên đăng nhập hoặc mật khẩu không được để trống'
                eventBus.$emit("errorWas", "Tên đăng nhập hoặc mật khẩu không được để trống");
            }
            else
            {
                axios
                .post(`${m.URLAPI}/api/v1/Authentications/LoginForEmail`, userLogin)
                .then(function (response){
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else{
                        var m1 = m;
                        m1.setToken(response.data.Data);

                        axios
                        .get(`${m1.URLAPI}/api/v1/Users/GetUserById`, {'headers': { 'Authorization': 'Bearer ' + m1.token}})
                        .then(function(response){
                            if(response.data.IsError)
                            { 
                                // gửi dữ liệu đến component FormWaning là 'true'
                                eventBus.$emit("isShowFormWaningWas", true);
                                // gửi dữ liệu đến component FormWaning là 'Tên tài khoản hoặc mật khẩu không chính xác'
                                eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                            }
                            else {
                                m1.setUser(response.data.Data);
                                localStorage.setItem('jwt', response.data.Data)
                                // chuyển đến trang main
                                m1.$router.push({ path: "/main" });
                            }
                        })
                        .catch(function(res){
                            console.log(res);
                        });
                    }
                })
                .catch(function (res){
                    console.log(res);
                });
            }
        },
    },
    mounted() {
        // focus vào ô nhâp tên đăng nhập
        this.$refs.username.focus();
    },
}
</script>