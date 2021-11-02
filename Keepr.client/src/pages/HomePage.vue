<template>
<div class="container-fluid ">
  <div class="row">
    <div class="col-12 masonry">

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

.masonry{
  padding: 1em;;
  columns: 4;
  div {
    width: 1500px;
    margin: 0 1rem 1rem 0 auto;
    display: inline-block;
    width: 100%;
    text-align: center;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
      overflow: hidden;
    
}
  @for $i from 1 through 36 { 
    div:nth-child(#{$i}) {
      $h: (random(400) + 100) + px;
      height: $h;
      line-height: $h;
    }
 } 
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
}
  
</style>
