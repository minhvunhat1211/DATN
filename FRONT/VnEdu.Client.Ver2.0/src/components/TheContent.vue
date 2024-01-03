<template>
    <div class="t-content">
        <the-header></the-header>
        <the-center></the-center>
    </div>
</template>

<script>
import TheHeader from './TheHeader.vue'
import TheCenter from './TheCenter.vue'
export default {
    name: 'Content',
    components:{
        TheHeader, TheCenter
    }
}
</script>