import { store } from '../store'

export default (to, from, next) => {
  store
    .dispatch('authCheck')
    .then(user => {
      if (user) {
        next()
      } else {
        next('/signin')
      }
    })
    .catch(error => console.log(error))
}
