<template>
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
   <img :src="keep.img" class="card-img-top keepimage selectable" alt="..." data-bs-toggle="modal" data-bs-target="#keep-modal" />
      <h6 class="card-text">{{keep.name}}</h6>
      <p class="card-text">{{keep.description}}</p>
      <img :src="keep.creator.picture" class="rounded-circle" alt="" />
 </div>
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
</style>