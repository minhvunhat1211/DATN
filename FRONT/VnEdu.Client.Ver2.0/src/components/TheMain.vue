<template>
    <div class="t-main">
        <the-menu></the-menu>
        <the-content></the-content>
        <form-waning></form-waning>
        <toast-message></toast-message>
    </div>
</template>

<script>
import TheContent from '../components/TheContent.vue'
import TheMenu from '../components/TheMenu.vue'
import FormWaning from '../views/FormWaning.vue'
import ToastMessage from '../views/ToastMessage.vue'
export default {
    name: 'TheMain',
    components:{
        TheMenu, TheContent, FormWaning, ToastMessage
    },
}
</script>