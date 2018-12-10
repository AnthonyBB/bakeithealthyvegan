import Vue from 'vue'
import axios from 'axios'
import VueAxios from 'vue-axios'
import './plugins/vuetify'
import App from './App.vue'
import router from './router'
import { store } from './store'
import VeeValidate from "vee-validate";

Vue.config.productionTip = false;

Vue.use(VeeValidate);

new Vue({
  router,
  store,
  VueAxios,
  axios,
  render: h => h(App),
}).$mount('#app');