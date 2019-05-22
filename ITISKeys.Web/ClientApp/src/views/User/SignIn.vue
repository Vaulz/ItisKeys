<template>
  <v-container>
    <v-layout row wrap justify-center>
      <v-flex xs12 sm4 lg3>
        <div id="firebaseui-auth-container"></div>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
import firebase from 'firebase/app'
import 'firebase/auth'
import firebaseui from 'firebaseui'

export default {
  mounted() {
    let uiConfig = {
      signInSuccessUrl: '/rooms',
      signInOptions: [firebase.auth.GoogleAuthProvider.PROVIDER_ID, firebase.auth.EmailAuthProvider.PROVIDER_ID]
    }
    let ui = firebaseui.auth.AuthUI.getInstance()
    if (!ui) {
      ui = new firebaseui.auth.AuthUI(firebase.auth())
    }
    ui.start('#firebaseui-auth-container', uiConfig)
  }
}
</script>


