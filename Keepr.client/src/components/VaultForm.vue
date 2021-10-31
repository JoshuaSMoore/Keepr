<template>
  <form @submit.prevent="handleSubmit()">
    <div class="form-group">
      <label for="name" class="sr-only"></label>
      <textarea
        type="text"
        name="name"
        class="form-control"
        placeholder="Name here"
        required
        v-model="editable.name"
      />
    </div>
          <div class="form-group">
      <label for="description" class="sr-only"></label>
      <textarea
        type="text"
        name="description"
        class="form-control"
        placeholder="Description here"
        required
        v-model="editable.description"
      />
       </div>
     <div class="form-group">
      <label for="isPrivate">Private: </label>
      <input
        type="checkbox"
        class="ms-3 mt-3"
        name="isPrivate"
        v-model="editable.isPrivate"
      />
    </div>
    <button type="submit" class="btn btn-outline-primary mt-1">Submit</button>
  </form>
</template>


<script>
import { Modal } from 'bootstrap'
import { Vault } from '../Models/Vault.js'
import { ref, watchEffect } from '@vue/runtime-core'
import Pop from '../utils/Pop.js'
import { useRoute } from 'vue-router'
import { vaultsService } from '../services/VaultsService.js'
export default {
  props: {
    vault: {
      type: Vault,
      default: () => new Vault()
    }
  },
  setup(props) {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...props.vault }
    })
    const route = useRoute()
    return {
      editable,
      async handleSubmit() {
        try {
          if (editable.value.id) {
            await vaultsService.editVault(editable.value)
            const editModal = Modal.getInstance(document.getElementById(`edit-vault-${props.vault.id}`))
            editModal.hide()
            Pop.toast('Vault has been edited')
            return
          }
          editable.value.vaultId = route.params.id
          await vaultsService.createVault(editable.value)
          const modal = Modal.getInstance(document.getElementById('create-vault'))
          modal.hide()
          editable.value = {}
          Pop.toast('Vault Created!', 'success')
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

</style>