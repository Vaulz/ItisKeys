import 'firebase/firestore'
import axios from 'axios'

export default {
  state: {
    loadedRooms: []
  },
  mutations: {
    setLoadedRooms(state, payload) {
      state.loadedRooms = payload
    }
  },
  actions: {
    async takeKey({ commit, getters }, payload) {
      if (payload.keeper) {
        commit('setError', { message: 'Ключ уже взят' })
        return
      } else {
        commit('setLoading', true)
        try {
          let response = await axios.put('/api/Room/TakeKey/', {
            id: payload.id,
            roomNumber: payload.roomNumber,
            inStock: !payload.inStock,
            keeper: {
              UId: getters.user.uid,
              name: getters.user.name,
              email: getters.user.email
            }
          })
          commit('setLoadedRooms', response.data)
          commit('setLoading', false)
        } catch (err) {
          console.log(err)
          commit('setLoading', false)
        }
      }
    },
    async returnKey({ commit }, payload) {
      commit('setLoading', true)
      try {
        let response = await axios.put('/api/Room/ReturnKey/', {
          id: payload.id,
          roomNumber: payload.roomNumber,
          inStock: !payload.inStock,
          keeper: null
        })
        commit('setLoadedRooms', response.data)
        commit('setLoading', false)
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
      }
    },
    async loadRooms({ commit }) {
      commit('setLoading', true)
      try {
        let response = await axios.get('/api/Room/GetRooms/')
        commit('setLoadedRooms', response.data)
        commit('setLoading', false)
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
      }
    },
    async createRoom({ commit }, payload) {
      commit('setLoading', true)
      try {
        const room = {
          roomNumber: payload.roomNumber,
          inStock: true
        }
        let response = await axios.post('/api/Room/CreateRoom/', room)
        commit('setLoadedRooms', response.data)
        commit('setLoading', false)
      } catch (err) {
        console.log(err)
        commit('setLoading', false)
      }
    }
  },
  getters: {
    loadedRooms(state) {
      return state.loadedRooms.sort((firstRoom, secondRoom) => firstRoom.roomNumber > secondRoom.roomNumber)
    }
  }
}
