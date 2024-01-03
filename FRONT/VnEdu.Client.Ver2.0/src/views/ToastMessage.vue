<template>
    <div v-show="isHideToastMessage" class="m-box-toast">
        <div class="m-toast">
            <div class="toast-icon"></div>
            <div class="toast-title">{{TitleToastMessage}}</div>
            <div class="toast-x" @click="clickBtnX()"></div>
        </div>
    </div>
</template>

<script>
import { eventBus } from'../main.js';
export default {
    data() {
        return {
            isHideToastMessage: false,
            TitleToastMessage: null,
        }
    },

    methods: {
        /**
         * click vào btn dấu x
         * CreatedBy: MinhVN(16/01/2022)
         */
        clickBtnX(){
            this.isHideToastMessage = !this.isHideToastMessage;
        }
    },

    created() {
        var m = this;

        // nhận dữ liệu từ component 
        eventBus.$on('isShowToastMessageWas', (isShowToastMessageData) =>{
            m.isHideToastMessage = isShowToastMessageData;
            setTimeout(function(){
                m.isHideToastMessage = !m.isHideToastMessage;
            }, 2000);
        })

        // nhận dữ liệu từ component TheRegister 
        eventBus.$on('TitleToastMessageWas', (TitleToastMessageData) =>{
            m.TitleToastMessage = TitleToastMessageData;
        })
    },
}
</script>