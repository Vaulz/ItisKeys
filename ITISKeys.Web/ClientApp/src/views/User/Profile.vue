<template>
  <v-container>
    <v-layout row wrap justify-center>
      <v-flex xs12 sm5 class="text-xs-center">
        <v-card class="pa-5">
          <h2>{{user.name}}</h2>
          <v-card-text>
            <div class="info--text">{{user.email}}</div>
          </v-card-text>
          <v-text-field v-model="user.age" label="Возраст"></v-text-field>
          <v-text-field v-model="user.sex" label="Пол"></v-text-field>
          <v-text-field v-model="user.role" label="Роль"></v-text-field>
          <v-btn color="accent" @click="newUser">Сохранить</v-btn>
        </v-card>
        <v-btn color="accent" :to="'/room/new'">Добавить ключ</v-btn>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import axios from 'axios'
import { mapActions } from 'vuex'
export default {
  computed: {
    user() {
      return this.$store.getters.user
    }
  },
  methods: {
    ...mapActions(['updateUser']),
    newUser() {
      this.updateUser(this.user)
    }
  }
}
</script>