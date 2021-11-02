<template>
<div class="container-fluid">
    <div class="mb-3 pt-4 ps-3" style="max-width: 540px;">
  <div class="row g-0">
    <div class="col-12">
    <Keep 
  v-for="k in keeps"
  :key="k.id"
  :keep="k"
  />
  </div>
  </div>
  </div>
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

  setup(props){
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

</style>