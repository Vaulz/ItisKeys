import firebase from 'firebase/app'
import 'firebase/firestore'
import axios from 'axios'

export default {
  state: {
    loadedReservations: []
  },
  mutations: {
    setLoadedReservations(state, payload) {
      state.loadedReservations = payload
    }
  },
  actions: {
    async loadReservations({ commit }) {
      commit('setLoading', true)
      try {
        let response = await axios.get('/api/Reservation/GetReservations/')
        commit('setLoadedReservations', response.data)
        commit('setLoading', false)
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
      }
    },
    editReservation({ getters }, payload) {
      firebase
        .firestore()
        .collection('reservations')
        .doc(payload.id)
        .update({
          start: payload.start,
          end: payload.end
        })
        .catch(error => console.error(error))
    },
    deleteReservation({ getters }, payload) {
      firebase
        .firestore()
        .collection('reservations')
        .doc(payload.id)
        .delete()
        .catch(error => console.error(error))
    },
    createReservation({ getters, commit }, payload) {
      return new Promise((resolve, reject) => {
        const reservation = {
          room: {
            roomNumber: payload.roomNumber
          },
          start: payload.start,
          end: payload.end,
          reservator: {
            id: getters.user.id
          }
        }
        let valid = true
        getters.loadedReservations.forEach(e => {
          let start = new Date(e.start)
          let end = new Date(e.end)
          if (
            payload.roomNumber == e.room.roomNumber &&
            ((payload.start >= start && payload.start <= end) ||
              (payload.end <= end && payload.end >= start) ||
              (payload.start <= start && payload.end >= end))
          ) {
            commit('setError', {
              message: 'Данная аудитория занята на выбратнное вами время'
            })
            valid = false
            reject('Занято')
            return
          }
        })
        if (valid) {
          let response = axios.post('/api/Reservation/CreateReservation', reservation)
          resolve('Не занято')
        }
      })
    }
  },
  getters: {
    loadedReservations(state) {
      return state.loadedReservations.sort((firstReservation, secondReservation) => firstReservation.start > secondReservation.start)
    }
  }
}
