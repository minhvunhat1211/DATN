<template>
    <div class="t-dialog" v-show="isShowFormWaning">
        <div class="m-form">
            <div class="form-title">
                <div class="title-icon"></div>
                <div class="title-name" style="font-size: 13px;">{{error}}</div>
            </div>
            <div class="form-btn">
                <div class="form-btn-agree" @click="clickBtnAgree()">Đồng ý</div>
            </div>
        </div>
    </div>
</template>

<script>
import { eventBus } from '../main';
export default {
    data() {
        return {
            isShowFormWaning: false,
            error: null,
        }
    },

    methods: {
        /**
         * click vào btn đồng ý
         * CreatedBy: MinhVN(16/01/2022)
         */
        clickBtnAgree()
        {
            this.isShowFormWaning = !this.isShowFormWaning;
        }
    },

    created() {
        // nhận dữ liệu từ component TheLogin
        eventBus.$on("isShowFormWaningWas", (isShowFormWaningData) =>{
            this.isShowFormWaning = isShowFormWaningData;
        });

        // nhận dữ liệu từ component TheLogin
        eventBus.$on("errorWas", (errorData) =>{
            this.error = errorData;
        });
    },
}
</script>