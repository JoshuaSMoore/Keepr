<template>
<div class="container-fluid" v-if="profile">
    <div class=" pt-4 ps-3" style="max-width: 540px;">
  <div class="row g-0">
    <div class="col-md-4">
      <img :src="profile.picture" class="img-fluid rounded-start" alt="...">
    </div>
    <div class="col-md-8">
      <div class="card-body">
        <h5 class="card-title">{{ profile.name }}</h5>
        <p class="card-text">Keeps: {{keeps.length}} <br> Vaults: {{vaults.length}}</p>
        <!-- <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p> -->
      </div>
    </div>
  </div>
</div>
  </div>
  <div class="container-fluid p-3">
    <div class="row justify-content-between">  
    <h3> Vaults
       <i class="mdi mdi-plus text-primary selectable" data-bs-toggle="modal" data-bs-target="#create-vault"></i>
    </h3>
    <ProfileVaults v-for="v in vaults" :key="v.id" :vault="v"/>
    </div>
  </div>
  <div class="container-fluid p-3 masonry">

      <h3> Keeps
        <i class="mdi mdi-plus text-primary selectable" data-bs-toggle="modal" data-bs-target="#create-keep"></i>
      </h3>
      <ProfileKeeps 
  v-for="k in keeps"
  :key="k.id"
  :keep="k"
  />

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
import { useRoute, useRouter } from "vue-router"
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
    profile: computed(()=> AppState.profile)
  }
}
}
</script>


<style lang="scss" scoped>
.masonry{
  padding: 1em;;
  column-count: 4;
}
img{width: 100%;
margin-bottom: 1em;
}
@media(max-width: 800px){
  .masonry{
    column-count: 1;
  }
}
.container-fluid{
  background-color: black;
  color: ghostwhite;
}
</style>