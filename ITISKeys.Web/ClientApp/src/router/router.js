import Vue from 'vue'
import Router from 'vue-router'
import Home from '../views/Home.vue'
import auth from './auth'
import Reservations from '../views/Reservation/Reservations.vue'
import Rooms from '../views/Room/Rooms.vue'
import CreateReservation from '../views/Reservation/CreateReservation.vue'
import CreateRoom from '../views/Room/CreateRoom.vue'
import Profile from '../views/User/Profile.vue'
import SignIn from '../views/User/SignIn.vue'

Vue.use(Router)

export default new Router({
	mode: 'history',
  base: process.env.BASE_URL,
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
			beforeEnter: auth
    },
		{
      path: '/rooms',
      name: 'Rooms',
      component: Rooms,
			beforeEnter: auth
		},
		{
      path: '/reservations',
      name: 'Reservations',
			component: Reservations,
			beforeEnter: auth
		},
		{
      path: '/reservation/new',
      name: 'Create Reservation',
			component: CreateReservation,
			beforeEnter: auth
		},
		{
      path: '/room/new',
      name: 'CreateRoom',
			component: CreateRoom,
			beforeEnter: auth
		},
		{
      path: '/profile',
      name: 'Profile',
			component: Profile,
			beforeEnter: auth
		},
		{
      path: '/signin',
      name: 'SignIn',
			component: SignIn
		}
  ]
})
