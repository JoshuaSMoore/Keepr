<template>
<div class="container-fluid">
  <div class="row g-0">
    <div class="col-md-8">
      <div class="card-body">
  <h1 class="card-title" v-for="v in vault" :vault="v" :key="v.id">{{vault.name}}
  </h1>
        <p class="card-text">Keeps: {{keeps.length}}</p>
        <!-- <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p> -->
      </div>
    </div>
  </div>
</div>
<div class="container-fluid masonry">
    <VaultKeeps 
  v-for="k in keeps"
  :key="k.id"
  :keep="k"
  />
  </div>

</template>


<script>
import {  computed, onMounted } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService.js'
import { AppState } from '../AppState.js'
import Pop from "../utils/Pop"
import { useRoute } from "vue-router"
import { Keep } from '../Models/Keep.js'
import { Vault } from '../Models/Vault.js'
export default {

  setup(){
    const route = useRoute()
    onMounted(() => {
        try {
         vaultsService.getVault(route.params.id)
         vaultsService.getKeepByVault(route.params.id)
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
    vault: computed(() => AppState.vault),
  }
}
}
</script>
<style lang="scss" scoped>
body {
  margin: 0;
  padding: 1rem;
}

.masonry{
  padding: 1em;
  columns: 4;
  break-inside: avoid;
  display: inline-block;
img{width: 100%;
margin-bottom: 1em;
}
}
@media(max-width: 800px){
  .masonry{
    column-count: 1;
  }
}
.container-fluid{
  background-color: black;
}
</style>