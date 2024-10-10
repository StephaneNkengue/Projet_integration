import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import PrimeVue from 'primevue/config'
import Toast from 'vue3-toastify'
import 'vue3-toastify/dist/index.css';
import api from './services/api';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';

async function initApp() {
    await store.dispatch('initializeStore');
    
    const app = createApp(App);
    app.use(store).use(router);
    await router.isReady();
    app.mount('#app');
}

initApp().catch(error => console.error('Erreur lors de l\'initialisation de l\'app:', error));

