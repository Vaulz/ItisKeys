<template>
	
		<v-dialog
		v-model="dialog"
		max-width="350"	
		persistent 
		>
			<v-alert
				:value="true"
				type="error"
				dismissible
				@input="onClose"
			>
      	{{text}}
    	</v-alert>		
		</v-dialog>
	
	
</template>

<script>
export default {
	props: ['text'],
	data () {
		return {
			dialog: this.text
		}
	},
	methods: {
		onClose() {
			this.dialog = false
			this.$emit('dismissed')
		}
	}
}
</script>