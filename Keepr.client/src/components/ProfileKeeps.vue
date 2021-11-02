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
      <h5 class="card-text text-light action" :data-bs-target="'#keep-modal-' + keep.id" data-bs-toggle="modal">{{keep.name}}
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
   <Modal :id="'keep-modal-' + keep.id">
    <template #modal-body>
  <div class="row g-0">
    <div class="col-md-8">
      <img :src="keep.img" style="max-width: 40rem" alt="...">
    </div>
    <div class="col-md-3">
      <div class="card-body border-bottom border-secondary">
        <h5 class="card-title">{{ keep.name }}</h5>
        <p class="card-text"><small class="text-muted">Shares: {{keep.shares}}<br> Views: {{keep.views}} </small></p>
        <p class="card-text">Keeps: {{keep.description}} </p>
      </div>
      <div class="row justify-content-around">   
      <div class="modaltext">
   <div class="dropdown">
          <button
            class="btn btn-secondary dropdown-toggle"
            type="button"
            id="dropdownMenuButton1"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            Add to Vault
          </button>
          <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton1">
            <li v-for="vault in vault" :key="vault.id" @click="addToVault(vault, keep)" class="selectable">{{vault.name}}</li>
          </ul>
        </div>
       <img :src="keep.creator.picture" class="rounded-circle profilepic ms-5" alt="" />
       <small> {{keep.creator.name}}</small>
      </div>
       </div>
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