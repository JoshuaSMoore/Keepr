<template>
<div class="container-fluid">
  <div class="row">
    <div class="col-md-4">
 <div class=" card about text-start p-2">
   <h1>
    <img class="rounded" :src="account.picture" alt="" />
    {{ account.name }}</h1>
    <p>Keeps: {{keeps.length}} <br> Vaults: {{vaults.length}} </p>
    </div>
  </div>
  </div>
  </div>
  <div class="container-fluid">
    <div class="row justify-content-between">  
    <h3> Vaults</h3>
    <ProfileVaults v-for="v in vaults" :key="v.id" :vault="v" class="col-md-2 shadow rounded m-3" />
    </div>
  </div>
  <div class="container-fluid">
    <div class="row justify-content-between">
      <h3> Keeps</h3>
      <ProfileKeeps 
  v-for="k in keeps"
  :key="k.id"
  :keep="k"
  class="col-md-2 shadow rounded m-3"
  />
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
        try {
          profilesService.getUsersProfile(route.params.id)
          profilesService.getUsersKeeps(route.params.id)
          profilesService.getUsersVaults(route.params.id)
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