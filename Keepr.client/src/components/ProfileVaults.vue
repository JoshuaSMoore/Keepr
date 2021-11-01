<template>
<div class = "col-md-2 shadow rounded m-1">
 <div class="keepcard p-2 m-1">
  <div
        class="float-end on-hover action"
        v-if="vault.creatorId == account.id"
      >
        <i class="mdi mdi-delete text-danger" @click="deleteVault()"></i>
        <i
          class="mdi mdi-lead-pencil"
          data-bs-toggle="modal"
          :data-bs-target="'#edit-vault-' + vault.id"
        ></i>
      </div>
      <h5 class="card-text p-1">{{vault.name}}
      </h5>
 </div>
</div>
   <Modal :id="'edit-vault-' + vault.id">
    <template #modal-title>
      <h6>Edit your vault!</h6>
    </template>
    <template #modal-body>
      <VaultForm :vault="vault" />
    </template>
  </Modal>
</template>


<script>
import { computed } from '@vue/reactivity'
import { Vault } from '../Models/Vault.js'
import { AppState } from '../AppState.js'
import { vaultsService } from '../services/VaultsService.js'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'

export default {
  props: {
    vault: {
      type: Vault,
      default: () => new Vault()
    }
  },
  setup(props){
    return {
    account: computed(() => AppState.account),
    async deleteVault(){
      try {
        if(await Pop.confirm()){
          await vaultsService.deleteVault(props.vault.id)
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