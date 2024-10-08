import "./assets/main.css";

import { createApp } from "vue";
import App from "./App.vue";
import store from "./store";
import router from "./router";
import PrimeVue from "primevue/config";
import Toast from "vue3-toastify";
import "vue3-toastify/dist/index.css";
import DataTable from "datatables.net-vue3";
import DataTablesCore from "datatables.net-dt";

const app = createApp(App);

app
  .use(store)
  .use(DataTable)
  .use(Toast)
  .use(router)
  .use(PrimeVue)
  .mount("#app");
