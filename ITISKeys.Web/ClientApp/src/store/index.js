import Vue from 'vue'
import Vuex from 'vuex'

import user from './user'
import shared from './shared'
import room from './room'
import reservation from './reservation'

import firebase from 'firebase/app'
import 'firebase/app'
import config from '../config'

Vue.use(Vuex)

firebase.initializeApp(config).firestore().settings({timestampsInSnapshots: true})

export const store = new Vuex.Store({
	modules: {
		user: user,
		shared: shared,
		room: room,
		reservation: reservation
	}
})
