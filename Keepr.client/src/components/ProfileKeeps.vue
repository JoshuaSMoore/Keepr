<template>
<div class = "col-md-2 shadow rounded m-3">
 <div class="card keepcard p-1">
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
   <img :src="keep.img" class="card-img-top keepimage selectable" alt="..." />
      <h5 class="card-text d-flex justify-content-between p-2">{{keep.name}}
      </h5>
 </div>
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
  max-height: 13rem;
}
.profilepic{
  height: 3rem;
  width: 3rem;
}
</style>