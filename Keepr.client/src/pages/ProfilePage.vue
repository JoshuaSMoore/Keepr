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
    <h3> Vaults
       <i class="mdi mdi-plus text-primary selectable" data-bs-toggle="modal" data-bs-target="#create-vault"></i>
    </h3>
    <ProfileVaults v-for="v in vaults" :key="v.id" :vault="v" class="col-md-2 shadow rounded m-3" />
    </div>
  </div>
  <div class="container-fluid">
    <div class="row justify-content-between">
      <h3> Keeps
        <i class="mdi mdi-plus text-primary selectable" data-bs-toggle="modal" data-bs-target="#create-keep"></i>
      </h3>
      <ProfileKeeps 
  v-for="k in keeps"
  :key="k.id"
  :keep="k"
  class=""
  />
    </div>
  </div>
   <Modal id="create-keep">
    <template #modal-title>
      <h6>Create a Keep</h6>
    </template>
    <template #modal-body>
      <KeepForm />
    </template>
  </Modal>
     <Modal id="create-vault">
    <template #modal-title>
      <h6>Create a Vault</h6>
    </template>
    <template #modal-body>
      <VaultForm />
    </template>
  </Modal>
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