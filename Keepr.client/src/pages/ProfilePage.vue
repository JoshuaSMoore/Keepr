<template>
<div class="container-fluid ">
  <div class="row justify-content-around">
PROFILE PAGE
  </div>
  </div>
</template>


<script>
import {  computed, onMounted } from '@vue/runtime-core'
import { profilesService } from '../services/ProfilesService.js'
import { AppState } from '../AppState.js'
import Pop from "../utils/Pop"
import { useRoute } from "vue-router"
export default {
  setup(){
    const route = useRoute()
    onMounted(() => {
      // try {
      //   profilesService.getUsersProfile(route.params.id)
      // } catch (error) {
      //   Pop.toast(error.message, 'error')
        try {
          profilesService.getUsersKeeps()
        } catch (error) {
          Pop.toast(error.message, 'error')
        }
        try {
          profilesService.getUsersVaults()
        } catch (error) {
          Pop.toast(error.message, 'error')
        }
      
    })
    return {
      route,
    keeps: computed(() => AppState.keeps),
    account: computed(() => AppState.account),
    user: computed(() => AppState.user),
    vaults: computed(() => AppState.vaults),
  }
}
}
</script>


<style lang="scss" scoped>

</style>