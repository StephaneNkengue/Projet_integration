import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import store from './store'
import router from './router'
import PrimeVue from 'primevue/config'
import Toast from 'vue3-toastify'
import 'vue3-toastify/dist/index.css';

import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'

const app = createApp(App)

app.component('VueDatePicker', VueDatePicker);

app.use(store)
    .use(Toast)
    .use(router)
    .use(PrimeVue)
    .mount('#app');


