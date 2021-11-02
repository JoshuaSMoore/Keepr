<template>
 <div class="keepcard">
  <div
        class="float-end on-hover action"
        v-if="keep.creatorId == account.id"
      >
        <i class="mdi mdi-delete text-danger" @click="deleteKeep()"></i>
        <i
          class="mdi mdi-lead-pencil"
          data-bs-toggle="modal"
          :data-bs-target="'#edit-keep-' + keep.id"
        ></i>
      </div>
   <img :src="keep.img" class="card-img-top keepimage" alt="..." />
      <h5 class="card-text p-2">{{keep.name}}
      </h5>
 </div>
   <Modal :id="'edit-keep-' + keep.id">
    <template #modal-title>
      <h6>Edit your keep!</h6>
    </template>
    <template #modal-body>
      <KeepForm :keep="keep" />
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
      type: Object,
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
.profilepic{
  height: 3rem;
  width: 3rem;
}
</style>