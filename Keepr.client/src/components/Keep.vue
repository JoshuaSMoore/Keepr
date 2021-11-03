<template>
 <div class=" card rounded keepcard m-4">
  <div
        class="on-hover action delete"
        v-if="keep.creatorId == account.id"
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
   </div>
 <Modal :id="'keep-modal-' + keep.id">
    <template #modal-body>
  <div class="row pe-5 me-1">
    <div class="col-md-8">
      <img :src="keep.img" style="max-width: 28rem" alt="...">
    </div>
    <div class="col-md-3 pe-5">
      <div class="card-body border-bottom border-secondary text-center modaltop">
        <h2 class="card-title">{{ keep.name }}</h2>
        <p class="card-text"><small class="text-muted">Keeps: {{keep.keeps}}<br> Views: {{keep.views}} </small></p>
      </div>
        <h5 class="card-text pt-3"><small> <b> Description: </b> </small> <br> {{keep.description}} </h5>
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
        <h5>
       <img :src="keep.creator.picture" class="rounded-circle profilepic ms-5" alt="" />
       {{keep.creator.name}}</h5>
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
    vault: computed(() => AppState.vault),
    vaults: computed(() => AppState.vaults),
    usersvaults: computed(() => AppState.usersvaults),
    vaultkeeps: computed(() => AppState.vaultkeeps),
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
    },
       async addToVault(vaultId){
        try {
          let vaultkeeps = {
            keepId: props.keep.id,
            vaultId: vaultId
          };
          await keepsService.addVaultKeep(vaultkeeps)
          props.keep.shares++
          Pop.toast('Keep has been added to vault')
        } catch (error) {
          Pop.toast(error.message, 'error')
          logger.log(error)
        }
        },
         async viewCount(){
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

.keepcard{
  background-color: black;
}
.textbot{
  position: absolute;
  bottom: 3.5rem;
  left: 0;
  right: 0;
  padding: 0.5rem;
}
.delete{
  transform: translateY(0rem);
}
.modaltext{
 position: absolute;
  bottom: 0;
  left: 40rem;
  right: 0rem;
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