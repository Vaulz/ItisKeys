import firebase from 'firebase/app'
import 'firebase/auth'
import axios from 'axios'

export default {
  state: {
    user: null
  },
  mutations: {
    setUser(state, payload) {
      state.user = payload
    }
  },
  actions: {
    authCheck({ state, commit }) {
      return new Promise((resolve, reject) => {
        firebase.auth().onAuthStateChanged(
          user => {
            if (user) {
              let promise = axios
                .post('/api/User/AddUser/', {
                  name: user.displayName,
                  Uid: user.uid,
                  email: user.email
                })
                .then(response => {
                  commit('setUser', response.data)
                })
            }
            resolve(state.user)
          },
          error => reject(error)
        )
      })
    },
    updateUser({ commit }, payload) {
      let promise = axios.put('/api/User/UpdateUser/', payload).then(response => {
        commit('setUser', response.data)
      })
    },
    logout({ commit }) {
      firebase.auth().signOut()
      commit('setUser', null)
    }
  },
  getters: {
    user(state) {
      return state.user
    }
  }
}
