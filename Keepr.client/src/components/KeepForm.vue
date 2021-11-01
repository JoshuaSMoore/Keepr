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
      <label for="img">Img Url</label>
      <input type="text"
             class="form-control"
             name="img"
             placeholder="Img Url...."
             v-model="editable.img"
             required
      />
       </div>
    <button type="submit" class="btn btn-outline-primary mt-1">Submit</button>
  </form>
</template>


<script>
import { Modal } from 'bootstrap'
import { Keep } from '../Models/Keep.js'
import { ref, watchEffect } from '@vue/runtime-core'
import Pop from '../utils/Pop.js'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService.js'
export default {
  props: {
    keep: {
      type: Object,
      default: () => new Keep()
    }
  },
  setup(props) {
    const editable = ref({})
    watchEffect(() => {
      editable.value = { ...props.keep }
    })
    const route = useRoute()
    return {
      editable,
      async handleSubmit() {
        try {
          if (editable.value.id) {
            await keepsService.editKeep(editable.value)
            const editModal = Modal.getInstance(document.getElementById(`edit-keep-${props.keep.id}`))
            editModal.hide()
            Pop.toast('Keep has been edited')
            return
          }
          await keepsService.createKeep(editable.value)
          const modal = Modal.getInstance(document.getElementById('create-keep'))
          modal.hide()
          editable.value = {}
          Pop.toast('Keep Created!', 'success')
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