<template>
<div class="masonry">

  <Keep 
  v-for="k in keeps"
  :key="k.id"
  :keep="k"
  />
    </div>
</template>

<script>
import { computed, watchEffect } from "@vue/runtime-core"
import { AppState } from "../AppState"
import Pop from "../utils/Pop"
import { keepsService } from '../services/KeepsService.js'
import { Keep } from '../Models/Keep.js'
export default {

  name: 'Home',
  setup() {
    watchEffect(() => {
      try {
      keepsService.getAll()
    } catch (error) { 
      Pop.toast(error.message, 'error')
  }
})
return {
  keeps: computed(() => AppState.keeps),
  keep: computed(() => AppState.keep),
  account: computed(() => AppState.account),
  user: computed(() => AppState.user),
  }
}
}
</script>

<style scoped lang="scss">

.masonry {
  columns: 4 200px;
  column-gap:2rem;
  padding-top: 2rem;
  width: 100%;
  padding: 1rem;
  div {
    width: 100px;
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
