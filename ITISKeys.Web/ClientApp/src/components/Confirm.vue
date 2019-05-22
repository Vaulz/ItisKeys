<template>
  <v-layout row justify-end>
    <v-dialog v-model="dialog" persistent width="350px">
			<v-btn slot="activator" class="error ml-0 mr-0 mt-0 mb-0">Отменить</v-btn>
      <v-card>
        <v-card-text>Вы действительно хотите отменить бронирование?</v-card-text>
        <v-card-actions>
					<v-spacer></v-spacer>
          <v-btn class="error" flat @click.native="dialog = false">Нет</v-btn>
          <v-btn class="success" flat @click="confirm">Да</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-layout>
</template>

<script>
  export default {
		props: ['reservation'],
    data () {
      return {
        dialog: false
      }
		},
		methods: {
			confirm() {
				this.dialog = false
				this.$emit('confirm', {confirmed: true})
			}
		}
  }
</script>