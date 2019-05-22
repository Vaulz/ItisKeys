<template>
  <v-container>
    <v-layout row wrap justify-center>
      <v-flex xs12 sm6>
        <v-card class="mb-4">
          <v-container>
            <v-layout row wrap>
              <v-flex xs12 sm12>
                <v-select
                  v-model="selectedRooms"
                  :items="rooms"
                  prepend-icon="vpn_key"
                  item-text="roomNumber"
                  label="Выберите аудиторию"
                  multiple
                ></v-select>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
    <transition-group name="slide" tag="v-layout" class="row wrap justify-center">
      <v-flex
        xs12
        sm2
        v-for="reservation in filteredReservations"
        :key="reservation.id"
        class="mr-4 mb-4"
        style="min-width:290px"
      >
        <v-card :key="reservation.room.roomNumber">
          <v-card-title primary-title class="accent white--text" style="min-height: 70px">
            {{reservation.room.roomNumber}}
            <v-spacer></v-spacer>
            <!-- <template v-if="isCreator(reservation)">
              <edit-reservation :reservation="reservation"></edit-reservation>
            </template>-->
          </v-card-title>
          <v-card-text>
            <v-icon class="mr-1 accent--text">event</v-icon>
            {{reservation.start | date}}
          </v-card-text>
          <v-card-text>
            <v-icon class="mr-1 accent--text">access_time</v-icon>
            <span class="accent--text">Начало:</span>
            {{reservation.start | time}}
          </v-card-text>
          <v-card-text>
            <v-icon class="mr-1 accent--text">access_time</v-icon>
            <span class="accent--text">Окончание:</span>
            {{reservation.end | time}}
          </v-card-text>
        </v-card>
      </v-flex>
    </transition-group>
  </v-container>
</template>

<script>
import moment from 'moment'
moment.locale('ru')
export default {
  data() {
    return {
      selectedRooms: null,
      menu: false
    }
  },
  computed: {
    reservations() {
      return this.$store.getters.loadedReservations
    },
    formatedDate() {
      if (this.date) return moment(this.date).format('DD MMMM, YYYY ')
    },
    user() {
      return this.$store.getters.user
    },
    loading() {
      return this.$store.getters.loading
    },
    rooms() {
      return this.$store.getters.loadedRooms
    },
    filteredReservations() {
      if (this.selectedRooms && this.selectedRooms.length != 0) {
        return this.reservations.filter(reservation => {
          let filteredByRooms = this.selectedRooms.includes(reservation.room.roomNumber)
          return filteredByRooms
        })
      }
      return this.reservations
    }
  },
  methods: {
    isCreator(reservation) {
      return reservation.reservator.id == this.user.id
    },
    clearMessage() {
      this.date = null
    }
  },
  created() {
    this.$store.dispatch('loadReservations')
    this.$store.dispatch('loadRooms')
  }
}
</script>

<style>
.slide-enter {
  transform: translateY(100px);
  opacity: 0;
}

.slide-enter-active {
  transition: all 1s ease-out;
}

.slide-leave-active {
  position: absolute;
}

.slide-leave-to {
  transform: translateY(1000px);
  opacity: 0;
}

.slide-move {
  transition: all 1s;
}
</style>
