<template>
  <v-container>
    <v-layout row>
      <v-flex xs12 sm6 offset-sm3>
        <v-card>
          <v-container grid-list-xs>
            <v-layout row wrap>
              <v-flex xs12>
                <v-form ref="form" v-model="valid" lazy-validation>
                  <v-text-field
                    v-model="roomNumber"
                    :rules="roomNumberRules"
                    label="Номер аудитории"
                    required
                  ></v-text-field>
                  <v-btn
                    :disabled="!valid"
                    class="success ml-0 black--text"
                    @click.prevent="onCreateRoom"
                  >Создать</v-btn>
                </v-form>
              </v-flex>
            </v-layout>
          </v-container>
        </v-card>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
export default {
  data() {
    return {
      valid: true,
      roomNumberRules: [v => !!v || 'Необходимо указать номер аудитории', v => (v && v.length == 4) || 'Номер аудитории должен состоять из 4 цифр'],
      roomNumber: ''
    }
  },
  methods: {
    onCreateRoom() {
      const room = {
        roomNumber: this.roomNumber
      }
      if (this.$refs.form.validate()) {
        this.$store.dispatch('createRoom', room)
        this.$router.push('/rooms')
      }
    }
  }
}
</script>