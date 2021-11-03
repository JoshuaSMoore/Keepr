<template>
<div class="container-fluid">
  <div class="row g-0">
    <div class="col-md-8">
      <div class="card-body text-light">
  <h1 class="card-title">{{vault.name}}
  </h1>
        <p class="card-text">Keeps: {{keeps.length}}</p>
        <!-- <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p> -->
      </div>
    </div>
  </div>
</div>
<div class="masonry">
    <VaultKeeps 
  v-for="k in keeps"
  :key="k.id"
  :keep="k"
  />
  </div>

</template>


<script>
import {  computed, onMounted, watchEffect } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService.js'
import { AppState } from '../AppState.js'
import Pop from "../utils/Pop"
import { useRoute, useRouter} from "vue-router"
import { Keep } from '../Models/Keep.js'
import { Vault } from '../Models/Vault.js'
import { keepsService } from '../services/KeepsService.js'
import { router } from '../router.js'
export default {
 props: {
    keep: {
      type: Object,
      default: () => new Keep()
    }
  },
  setup(props){
    const route = useRoute()
    const router = useRouter()
    watchEffect(() => {
        try {
         vaultsService.getVault(route.params.id)
         vaultsService.getKeepByVault(route.params.id)
        } catch (error) {
          router.push({ name: 'Home'})
          Pop.toast(error.message, 'error')
        }
    })
    return {
      route,
      router,
    keeps: computed(() => AppState.keeps),
    account: computed(() => AppState.account),
    user: computed(() => AppState.user),
    vaults: computed(() => AppState.vaults),
    vault: computed(() => AppState.vault),
    vaultkeeps: computed(() => AppState.vaultKeepId),
    vaultkeep: computed(() => AppState.vaultkeep)
  }
}
}
</script>
<style lang="scss" scoped>
.masonry {
  columns: 3 200px;
  column-gap: 2rem;
  padding-top: 2rem;
  width: 100%;
  padding: 1rem;
  div {
    width: 150px;
    background: black;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
  }
  }
.container-fluid{
  background-color: black;
}
</style>