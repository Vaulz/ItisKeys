import moment from 'moment'

export default value => {
  moment.locale('ru')
  return moment(value).format('HH:mm')
}
