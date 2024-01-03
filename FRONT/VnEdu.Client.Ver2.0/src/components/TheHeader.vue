<template>
    <div class="t-header">
        <div class="box-header">
            <div class="box-header-name">{{user.UserName}}</div>
            <div class="box-name-permission">{{user.DecentralizationName}}</div>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex'

export default {
    data() {
        return {
            
        }
    },
    methods: {
        
    },
    computed: {
        ...mapGetters(['user']),
    },
   
}
</script>