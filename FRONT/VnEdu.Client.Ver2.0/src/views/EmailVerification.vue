<template>
    <div class="t-login">
        <div class="login-opacity">
            <div class="box-login">
                <div class="login-top">
                    <div class="logo-login">
                    </div>
                </div>
                <input @keyup.enter="clickBtnOTP()" ref="email" type="text" class="box-input" placeholder="Email" v-model="email">
                <button v-show="isShowGetOTP" class="btn-login" @click="clickBtnOTP()">Lấy mã OTP</button>
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
import FormWaning from '../views/FormWaning.vue'
import ToastMessage from '../views/ToastMessage.vue'
import { eventBus } from'../main.js'
import { mapGetters, mapMutations } from 'vuex';
import axios from 'axios';
export default {
    name: 'EmailVerification',
    components:{
        FormWaning, ToastMessage
    },
    data() {
        return {
            email: "",
            isShowGetOTP: true,
            isShowLoading: false,
        }
    },
    computed: {
        ...mapGetters(['URLAPI']),
    },
    methods: {
        ...mapMutations(['setEmail']),
        /**
         * click vào btn lấy mã OTP
         * CreatedBy: MinhVN(14/01/2023)
         */
        clickBtnOTP(){
            var m = this;

            m.isShowGetOTP = false;
            m.isShowLoading = true;

            axios
            .post(`${m.URLAPI}/api/v1/Authentications/SendOTPByEmail?email=${m.email}`)
            .then(function (response){
                if(response.data.IsError)
                { 
                    // gửi dữ liệu đến component FormWaning là 'true'
                    eventBus.$emit("isShowFormWaningWas", true);
                    eventBus.$emit("errorWas", `${response.data.ErrorData.Message}`);

                    m.isShowGetOTP = true;
                    m.isShowLoading = false;
                }
                else
                {
                    m.setEmail(m.email);
                    m.$router.push({ path: "/verification" });

                    m.isShowGetOTP = true;
                    m.isShowLoading = false;
                }
            })
            .catch(function (res){
                console.log(res);
            });
        }
    },
}
</script>