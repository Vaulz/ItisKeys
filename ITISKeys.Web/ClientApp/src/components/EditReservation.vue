<template>
  <v-dialog width="500px" v-model="editDialog">
    <v-btn small class="mt-0 mb-0 pl-0 pr-0" slot="activator">
      <v-icon>edit</v-icon>
    </v-btn>
    <v-card>
      <v-container>
        <v-layout row wrap>
          <v-flex xs12>
            <v-card-title class="pr-0 pl-0">Редактирование
              <v-spacer></v-spacer>
              <confirm :reservation="this.reservation" @confirm="deleteReservation"></confirm>
            </v-card-title>
          </v-flex>
        </v-layout>
        <v-divider></v-divider>
        <v-layout row wrap class="mt-4">
          <v-flex xs12>
            <v-form ref="form" v-model="valid" lazy-validation>
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
                  v-model="date"
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
              <v-card-actions class="pr-0">
                <v-spacer></v-spacer>
                <v-btn class="error--text" @click="editDialog = false">Закрыть</v-btn>
                <v-btn
                  class="success--text ml-4"
                  @click="onSaveChanges"
                  :disabled="!valid"
                >Сохранить</v-btn>
              </v-card-actions>
            </v-form>
          </v-flex>
        </v-layout>
      </v-container>
    </v-card>
  </v-dialog>
</template>

<script>
import moment from 'moment'
moment.locale('ru')
import Confirm from './Confirm.vue'
export default {
  components: {
    confirm: Confirm
  },
  props: ['reservation'],
  data() {
    return {
      editDialog: false,
      valid: true,
      date: moment(this.reservation.start).format('YYYY-MM-DD'),
      time: moment(this.reservation.start).format('HH:mm'),
      time2: moment(this.reservation.end).format('HH:mm'),
      menu: false,
      menu2: false,
      menu3: false,
      timeStartRules: [v => !!v || 'Укажите время начала бронирования'],
      timeEndRules: [v => !!v || 'Укажите время окончания бронирования', v => this.time2 > this.time || 'Время окончания должно быть больше']
    }
  },
  methods: {
    onSaveChanges() {
      if (this.$refs.form.validate()) {
        this.editDialog = false
        this.$store.dispatch('editReservation', {
          id: this.reservation.id,
          start: this.submittableDateTime(this.time),
          end: this.submittableDateTime(this.time2)
        })
      }
    },
    deleteReservation(data) {
      this.editDialog = false
      if (data.confirmed) {
        this.$store.dispatch('deleteReservation', {
          id: this.reservation.id
        })
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
    }
  }
}
</script>
