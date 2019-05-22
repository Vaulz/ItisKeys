<template>
  <v-container>
    <v-layout row wrap class="mt-4">
      <v-flex xs12 class="text-xs-center">
        <p>
          <img
            src="https://upload.wikimedia.org/wikipedia/en/thumb/e/e6/KFU_logo_2015.svg/1280px-KFU_logo_2015.svg.png"
            alt
          >
        </p>
      </v-flex>
    </v-layout>
  </v-container>
</template>


<script>
export default {
  computed: {
    loading() {
      return this.$store.getters.loading
    }
  }
}
</script>

<style lang="sass" scoped>
	.title
		position: absolute
		bottom: 50px
		background-color: rgba(0,0,0,0.5)
		color: #fff
		font-size: 2em
		padding: 20px
</style>

