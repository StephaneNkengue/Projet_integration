import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import PrimeVue from 'primevue/config'
import Toast from 'vue3-toastify'
import 'vue3-toastify/dist/index.css';
import api from './services/api';

async function initApp() {
    await store.dispatch('checkAuthStatus');
    
    createApp(App).use(store).use(router).mount('#app')
}

initApp()

