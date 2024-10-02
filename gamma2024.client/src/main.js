import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import router from './router'
import PrimeVue from 'primevue/config'
import Toast from 'vue3-toastify'
import 'vue3-toastify/dist/index.css';

const app = createApp(App)

app.use(store)
    .use(Toast)
    .use(router)
    .use(PrimeVue)
    .mount('#app');


