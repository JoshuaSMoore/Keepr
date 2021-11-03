<template>
 <div class="card rounded">
  <div
        class="on-hover action delete"
        v-if="vault.creatorId == account.id"
      >
        <i class="mdi mdi-delete text-danger" @click="deleteKeep()"></i>
      </div>
   <img :src="keep.img" class="card-img " :data-bs-target="'#keep-modal-' + keep.id" data-bs-toggle="modal" @click="viewCount()" style="" alt="..."/>
   <div class="card-img-overlay selectable">
      <div class="text-end textbot">
      <h5 class="text-light card-text"> {{keep.name}}
      </h5>
        <router-link :to= "{ name: 'ProfilePage', params: { id: keep.creatorId } }">
      <img :src="keep.creator.picture" class="rounded-circle profilepic" alt="" />
        </router-link>
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
   <Modal :id="'keep-modal-' + keep.id">
    <template #modal-body>
  <div class="row g-0">
    <div class="col-md-8">
      <img :src="keep.img" style="max-width: 40rem" alt="...">
    </div>
    <div class="col-md-3">
      <div class="card-body border-bottom border-secondary">
        <h5 class="card-title">{{ keep.name }}</h5>
        <p class="card-text"><small class="text-muted">Shares: {{keep.keeps}}<br> Views: {{keep.views}} </small></p>
        <p class="card-text">Keeps: {{keep.description}} </p>
      </div>
      <div class="row justify-content-around">   
      <div class="modaltext d-flex">
        
   <div class="dropdown">
          <button
            class="btn btn-secondary dropdown-toggle"
            type="button"
            id="vaultDropDown"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            Add to Vault
          </button>
          <ul class="dropdown-menu" aria-labelledby="vaultDropDown">
            <li v-for="v in usersvaults" :vault="v" :key="v.id" class="selectable" @click="addToVault(v.id)">{{v.name}}
            </li>
          </ul>
        </div>
       <img :src="keep.creator.picture" class="rounded-circle profilepic ms-5" alt="" />
       <h5>{{keep.creator.name}}</h5>
      </div>
       </div>
    </div>
</div>
    </template>
  </Modal>
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
      type: Object,
      // default: () => new Keep()
      required: true
    }
  },
  setup(props){
    return {
    account: computed(() => AppState.account),
    vault: computed(()=> AppState.vault),

    async deleteKeep(){
  
      try {
        if(await Pop.confirm()){
          await keepsService.deleteVaultKeep(props.keep.vaultKeepId)
          Pop.toast('Keep has been deleted')
        }
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.log(error)
      }
    },
       async viewCount() {
        props.keep.views++
        await keepsService.getById(props.keep.id)
      }
    }
    }
  }
</script>


<style lang="scss" scoped>

.profilepic{
  height: 2.5rem;
  width: 2.5rem;
}
.modaltext{
 position: absolute;
  bottom: 0;
  // left: 0;
  right: -45rem;
  padding: 0.5rem;
}
@media (max-width: 800px) {
  .modaltext{
    position: relative;
  }
}
.card-img{
  position: relative;
}



</style>