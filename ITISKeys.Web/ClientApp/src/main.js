import '@babel/polyfill'
import Vue from 'vue'
import './plugins/vuetify'
import App from './App.vue'
import router from './router/router'
import {store} from './store'
import './registerServiceWorker'
import DateFilter from './filters/date'
import TimeFilter from './filters/time'
import EditReservation from './components/EditReservation.vue'


Vue.config.productionTip = false
Vue.filter('date', DateFilter)
Vue.filter('time', TimeFilter)
Vue.component('edit-reservation', EditReservation)

new Vue({
  router,
  store,
	render: h => h(App)
}).$mount('#app')
