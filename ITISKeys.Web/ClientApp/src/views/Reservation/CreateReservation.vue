<template>
  <v-container>
    <v-layout row justify-center>
      <v-flex xs12 sm4>
        <alert @dismissed="onDismissed" v-if="error" :text="error.message"></alert>
        <v-card>
          <v-container>
            <h2>Бронирование аудитории</h2>
            <v-form ref="form" v-model="valid" lazy-validation>
              <v-autocomplete
                v-model="room"
                prepend-icon="vpn_key"
                :items="rooms"
                :label="`Выберите аудиторию`"
                item-text="roomNumber"
                return-object
                :rules="[v => !!v || 'Выберите аудиторию']"
                required
              ></v-autocomplete>
              <v-menu
                ref="menu2"
                :close-on-content-click="false"
                v-model="menu2"
                :nudge-right="40"
                :return-value.sync="date"
                lazy
                transition="scale-transition"
                offset-y
                full-width
                min-width="290px"
                required
              >
                <v-text-field
                  slot="activator"
                  v-model="formatedDate"
                  label="Выберите дату"
                  prepend-icon="event"
                  readonly
                  :rules="[v => !!v || 'Выберите дату']"
                ></v-text-field>
                <v-date-picker
                  reactive
                  v-model="date"
                  @input="$refs.menu2.save(date)"
                  :allowed-dates="allowedDates"
                ></v-date-picker>
              </v-menu>
              <v-menu
                ref="menu"
                :close-on-content-click="false"
                v-model="menu"
                :nudge-right="40"
                :return-value.sync="time"
                lazy
                transition="scale-transition"
                offset-y
                full-width
                max-width="290px"
                min-width="290px"
                required
              >
                <v-text-field
                  slot="activator"
                  v-model="time"
                  label="Начало бронирования"
                  prepend-icon="access_time"
                  readonly
                  :rules="timeStartRules"
                ></v-text-field>
                <v-time-picker
                  v-if="menu"
                  v-model="time"
                  @change="$refs.menu.save(time)"
                  format="24hr"
                ></v-time-picker>
              </v-menu>
              <v-menu
                ref="menu3"
                :close-on-content-click="false"
                v-model="menu3"
                :nudge-right="40"
                :return-value.sync="time2"
                lazy
                transition="scale-transition"
                offset-y
                full-width
                max-width="290px"
                min-width="290px"
                required
              >
                <v-text-field
                  slot="activator"
                  v-model="time2"
                  label="Окончание бронирования"
                  prepend-icon="access_time"
                  readonly
                  :rules="timeEndRules"
                ></v-text-field>
                <v-time-picker
                  v-if="menu3"
                  v-model="time2"
                  @change="$refs.menu3.save(time2)"
                  format="24hr"
                ></v-time-picker>
              </v-menu>
              <v-btn
                :disabled="!valid"
                class="success ml-0 mt-4 black--text"
                @click.prevent="onCreateReservation"
              >Забронировать</v-btn>
            </v-form>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import Alert from './../../components/Alert.vue'
import moment from 'moment'
moment.locale('ru')
export default {
  components: {
    alert: Alert
  },
  data() {
    return {
      valid: true,
      room: null,
      date: null,
      time: null,
      time2: null,
      menu: false,
      menu2: false,
      menu3: false,
      timeStartRules: [v => !!v || 'Укажите время начала бронирования'],
      timeEndRules: [v => !!v || 'Укажите время окончания бронирования', v => this.time2 > this.time || 'Время окончания должно быть больше']
    }
  },
  computed: {
    rooms() {
      return this.$store.getters.loadedRooms
    },
    formatedDate() {
      if (this.date) return moment(this.date).format('DD MMMM, YYYY ')
    },
    reservations() {
      return this.$store.getters.loadedReservations
    },
    error() {
      return this.$store.getters.error
    }
  },
  methods: {
    onCreateReservation() {
      const reservation = {
        roomNumber: this.room.roomNumber,
        start: this.submittableDateTime(this.time),
        end: this.submittableDateTime(this.time2)
      }
      if (this.$refs.form.validate()) {
        this.$store
          .dispatch('createReservation', reservation)
          .then(promise => this.$router.push('/reservations'))
          .catch(error => console.log(error))
      }
    },
    submittableDateTime(time) {
      const date = new Date(this.date)
      if (typeof time === 'string') {
        const hours = time.match(/^(\d+)/)[1]
        const minutes = time.match(/:(\d+)/)[1]
        date.setHours(hours)
        date.setMinutes(minutes)
      }
      return date
    },
    allowedDates: val => {
      let day = parseInt(val.split('-')[2], 10)
      let month = parseInt(val.split('-')[1], 10)
      let currentDate = new Date()
      return month == currentDate.getMonth() + 2 || (month == currentDate.getMonth() + 1 && day >= currentDate.getDate())
    },
    onDismissed() {
      this.$store.dispatch('clearError')
    }
  },
  created() {
    this.$store.dispatch('loadReservations')
    this.$store.dispatch('loadRooms')
  }
}
</script>

<style>
</style>