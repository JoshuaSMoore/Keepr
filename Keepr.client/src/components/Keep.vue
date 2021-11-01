<template>

 <div class="keepcard p-3">
  <div
        class="float-end on-hover action"
        v-if="keep.creatorId == account.id"
      >
        <i class="mdi mdi-delete text-danger" @click="deleteKeep()"></i>
      </div>
   <img :src="keep.img" class="card-img keepimage selectable" alt="..."  :data-bs-target="'#keep-modal-' + keep.id" data-bs-toggle="modal" />
   </div>
   <div class="div">

      <h5 class="card-text text-light d-flex justify-content-bottom p-1">{{keep.name}}
      </h5>
        <router-link :to= "{ name: 'ProfilePage', params: { id: keep.creatorId } }">
      <img :src="keep.creator.picture" class="rounded-circle profilepic" alt="" />
       <small> {{keep.creator.name}}</small>
        </router-link>
   </div>
 <Modal :id="'keep-modal-' + keep.id">
    <template #modal-body>
      <!-- <div class="row">
          <img class="img-fluid modalkeep" :src="keep.img" alt="">
          <h3> 
          {{ keep.name }}
           {{ keep.description }}
          </h3>
        </div> -->
  <div class="row g-0">
    <div class="col-md-8">
      <img :src="keep.img" style="max-width: 40rem;" alt="...">
    </div>
    <div class="col-md-3">
      <div class="card-body border-bottom border-secondary">
        <h5 class="card-title">{{ keep.name }}</h5>
        <p class="card-text"><small class="text-muted">Shares: {{keep.shares}}<br> Views: {{keep.views}} </small></p>
        <p class="card-text">Keeps: {{keep.description}} </p>
      </div>
      <div class="btn btn-outline-secondary mt-3 m-3">Add to Vault</div>
       <img :src="keep.creator.picture" class="rounded-circle profilepic" alt="" />
       <small> {{keep.creator.name}}</small>
    </div>
</div>
    </template>
  </Modal>
</template>


<script>
import { computed } from '@vue/reactivity'
import { Keep } from '../Models/Keep.js'
import { AppState } from '../AppState.js'
import { keepsService } from '../services/KeepsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'

export default {
  props: {
    keep: {
      type: Keep,
      default: () => new Keep()
    }
  },
  setup(props){
    return {
    account: computed(() => AppState.account),
    async deleteKeep(){
      try {
        if(await Pop.confirm()){
          await keepsService.deleteKeep(props.keep.id)
          Pop.toast('Keep has been deleted')
        }
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.log(error)
      }
    }
    }
  }
}
</script>


<style lang="scss" scoped>
.keepimage{
  max-height: 40rem;
}
.profilepic{
  height: 2rem;
  width: 2rem;
}
.modalkeep{
  background-repeat: no-repeat;
}
</style>