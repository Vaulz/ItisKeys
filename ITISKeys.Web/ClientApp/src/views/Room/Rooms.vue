<template>
  <v-container>
    <v-layout row justify-center>
      <v-flex xs12 sm4>
        <transition name="scale-transition">
          <alert @dismissed="onDismissed" v-if="error" :text="error.message"></alert>
        </transition>
        <v-card>
          <v-toolbar color="primary" dark>
            <v-btn icon @click="showSearch = !showSearch">
              <v-icon>search</v-icon>
            </v-btn>
            <transition name="slide-fade">
              <v-flex xs12 sm6 mt-3 ml-3 v-if="showSearch">
                <v-text-field
                  label="Search..."
                  single-line
                  v-model="search"
                  clearable
                  clear-icon="clear"
                  :clear-icon-cb="clearMessage"
                  color="white"
                ></v-text-field>
              </v-flex>
            </transition>
          </v-toolbar>
          <v-list two-line class="pb-0">
            <template v-for="(room, index) in filteredRooms">
              <v-list-tile :key="room.roomNumber" ripple @click="toggle(room.id)">
                <v-list-tile-content>
                  <v-list-tile-title>{{ room.roomNumber }}</v-list-tile-title>
                </v-list-tile-content>

                <v-list-tile-action>
                  <v-btn class="black--text" v-if="room.inStock" color="success">Взять</v-btn>
                  <v-btn v-else-if="canReturn(room.id)" color="accent">Вернуть</v-btn>
                  <v-btn v-else color="warning">Взят</v-btn>
                </v-list-tile-action>
              </v-list-tile>
              <v-divider v-if="index + 1 < rooms.length" :key="index"></v-divider>
            </template>
          </v-list>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Alert from './../../components/Alert.vue'
export default {
  components: {
    alert: Alert
  },
  data() {
    return {
      showSearch: false,
      search: ''
    }
  },
  computed: {
    rooms() {
      return this.$store.getters.loadedRooms
    },
    loading() {
      return this.$store.getters.loading
    },
    error() {
      return this.$store.getters.error
    },
    user() {
      return this.$store.getters.user
    },
    filteredRooms() {
      return this.rooms.filter(room => room.roomNumber.toString().match(this.search))
    }
  },
  methods: {
    toggle(id) {
      let room = this.rooms.find(room => room.id === id)
      if (this.canReturn(id)) {
        this.$store.dispatch('returnKey', {
          inStock: room.inStock,
          id: id,
          keeper: room.keeper
        })
      } else {
        this.$store.dispatch('takeKey', {
          inStock: room.inStock,
          id: id,
          keeper: room.keeper,
          roomNumber: room.roomNumber
        })
      }
    },
    canReturn(id) {
      let room = this.rooms.find(room => room.id === id)
      if (room.keeper) {
        return this.user.id === room.keeper.id
      }
      return false
    },
    clearMessage() {
      this.search = ''
    },
    onDismissed() {
      this.$store.dispatch('clearError')
    }
  },
  created() {
    this.$store.dispatch('loadRooms')
  }
}
</script>
