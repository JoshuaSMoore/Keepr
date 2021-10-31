<template>
  <div class="col-md-3 p-2 shadow rounded">
 <div class="card keepcard p-1">
  <div
        class="float-end on-hover action"
        v-if="keep.creatorId == account.id"
      >
        <i class="mdi mdi-delete text-danger" @click="deleteKeep()"></i>
        <!-- <i
          class="mdi mdi-lead-pencil"
          data-bs-toggle="modal"
          :data-bs-target="'#edit-keep-' + keep.id"
        ></i> -->
      </div>
   <img :src="keep.img" class="card-img keepimage selectable" alt="..."  :data-bs-target="'#keep-modal-' + keep.id" data-bs-toggle="modal" />
   </div>
      <h5 class="card-text d-flex justify-content-bottom p-2">{{keep.name}}
        <router-link :to= "{ name: 'ProfilePage', params: { id: keep.creatorId } }">
      <img :src="keep.creator.picture" class="rounded-circle profilepic" alt="" />
        </router-link>
      </h5>
 </div>
 <Modal :id="'keep-modal-' + keep.id">
    <template #modal-body>
      <div class="row">
        <div class="" v-for="k in keep.img" :key="k.id">
          <img class="img-fluid modalkeep float-left" :src="keep.img" alt="">
          <h3> 
          {{ keep.name }}
           {{ keep.description }}
          </h3>
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
  height: 3rem;
  width: 3rem;
}
.modalkeep{
  background-repeat: no-repeat;
}
</style>