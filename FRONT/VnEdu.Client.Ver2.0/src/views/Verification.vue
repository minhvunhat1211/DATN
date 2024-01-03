<template>
    <div class="t-login">
        <div class="login-opacity">
            <div class="box-login">
                <div class="login-top">
                    <div class="logo-login">
                    </div>
                </div>
                <input @keyup.enter="clickBtnConfirm()" ref="codeOTP" type="text" class="box-input" placeholder="Nhập mã OTP" v-model="codeOTP">
                <div class="box-forgot-password" @click="clickBtnOTP()">
                    <a class="link-forgot-password">Lấy lại mã OTP</a>
                </div>
                <button v-show="isShowVerification" class="btn-login" @click="clickBtnConfirm()">Xác nhận</button>
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
import FormWaning from './FormWaning.vue'
import ToastMessage from './ToastMessage.vue'
import { eventBus } from'../main.js'
import axios from 'axios';
import { mapGetters } from 'vuex';
export default {
    name: 'Verification',
    components:{
        FormWaning, ToastMessage
    },
    data() {
        return {
            codeOTP: "",
            isShowVerification: true,
            isShowLoading: false,
        }
    },
    computed:{
        ...mapGetters(['email', 'URLAPI']),
    },
    methods: {
        /**
         * click vào btn xác nhận
         * CreatedBy: MinhVN(14/01/2023)
         */
        clickBtnConfirm(){
            var m = this;

            m.isShowVerification = false;
            m.isShowLoading = true;

            if (m.codeOTP == null || m.codeOTP == "")
            {
                eventBus.$emit("isShowFormWaningWas", true);
                eventBus.$emit("errorWas", "Mã OTP không được phép để trống");

                m.isShowVerification = true;
                m.isShowLoading = false;
            }
            else{
                axios
                .post(`${m.URLAPI}/api/v1/Authentications/VerifyOTP?email=${m.email}&codeOTP=${m.codeOTP}`)
                .then(function (response){
                    if(response.data.IsError)
                    { 
                        // gửi dữ liệu đến component FormWaning là 'true'
                        eventBus.$emit("isShowFormWaningWas", true);
                        eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                    }
                    else
                    {
                        m.$router.push({ path: "/changepasswordnew" });
                    }

                    m.isShowVerification = true;
                    m.isShowLoading = false;
                })
                .catch(function (res){
                    console.log(res);
                });
            }
        },
         /**
         * click vào btn lấy mã OTP
         * CreatedBy: MinhVN(14/01/2023)
         */
        clickBtnOTP(){
            var m = this;

            axios
            .post(`${m.URLAPI}/api/v1/Authentications/SendOTPByEmail?email=${m.email}`)
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);
                }
                else
                {
                    // gửi dữ liệu sang component FormToastMessage là 'true'
                    eventBus.$emit("isShowToastMessageWas", true);
                    eventBus.$emit("TitleToastMessageWas", "Lấy lại mã OTP thành công");
                }
            })
            .catch(function (res){
                console.log(res);
            });
        }
    },
}
</script>