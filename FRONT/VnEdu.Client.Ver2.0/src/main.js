import Vue from 'vue'
import App from './App.vue'
import Vuex from 'vuex'

Vue.use(Vuex)

const store = new Vuex.Store({
    state: {
        user: {},
        //URLAPI: 'https://www.vnedu.somee.com',
        URLAPI: 'https://localhost:7153',
        token: "",
        email: "",
    },
    getters: {
        user: state => state.user,
        URLAPI: state => state.URLAPI,
        token: state => state.token,
        email: state => state.email
    },
    mutations: {
        setUser(state, user) {
            state.user = user;
        },
        setToken(state, token) {
            state.token = token;
        },
        setEmail(state, email) {
            state.email = email;
        }
    },
    actions: {

    },
})

//Router
import VueRouter from 'vue-router'
import router from './router.js'

Vue.config.productionTip = false

export const eventBus = new Vue();

Vue.use(VueRouter)

new Vue({
    router,
    store,
    render: h => h(App),
}).$mount('#app')