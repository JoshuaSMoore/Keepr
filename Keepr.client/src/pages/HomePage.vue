<template>
<div class="container-fluid ">
  <div class="row">
    <div class="col-12">

  <Keep 
  v-for="k in keeps"
  :key="k.id"
  :keep="k"
  />
    </div>
</div>
  </div>
   <Modal id="keep-modal">
    <template #modal-title>
    </template>
    <template #modal-body>
    </template>
  </Modal>
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

.container-fluid{
  padding: 2em;
  column-count: 4;
  background-color:black;
}
img{width: 100%;
margin-bottom: 1em;
}
@media(max-width: 800px){
  .container{
    column-count: 3;
  }
}
  
</style>
