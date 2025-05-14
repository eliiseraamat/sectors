<script setup lang="ts">
import { SectorService } from '@/services/FormService'
import type { ISector } from '@/types/ISector'
import { onMounted, ref } from 'vue'
import Selectbox from '@/components/Selectbox.vue'
import { formStore } from '@/stores/form'
import { useRoute, useRouter } from 'vue-router'
import { clearStore, validateData } from '@/functions/Functions'

const service = new SectorService()
const route = useRoute()
const router = useRouter()
const store = formStore()

let data = ref<ISector[]>([])
let errorMessage = ref<string[]>([])
let message = ref<string>('')

const fetchPageData = async () => {
  try {
    const result = await service.getAllAsync()
    if (result === undefined) {
      console.log('No sectors found')
      return
    }
    data.value = result.data!
  } catch (error) {
    console.error('Error fetching sectors:', error)
  }
}

onMounted(async () => {
  await fetchPageData()

  const formId = Number(route.query.formId)
  if (formId && formId > 0) {
    try {
      const dto = await service.getFormWithSectorsAsync(formId)
      store.name = dto.data!.form.name
      store.sectors = dto.data!.sectorIds
      store.agreedToTerms = dto.data!.form.agreedToTerms
      store.formId = dto.data!.form.id
      store.isEditing = true
      message.value = 'Your form has been updated. You can edit and resubmit it if needed.'
    } catch (error) {
      console.error('Failed to load form:', error)
    }
  }
})

const handleSave = async () => {
  errorMessage.value = validateData()
  if (errorMessage.value.length > 0) {
    store.isErrors = true
    return
  }

  if (store.isEditing) {
    try {
      await service.updateFormWithSectorsAsync({
        form: { id: store.formId, name: store.name, agreedToTerms: store.agreedToTerms },
        sectorIds: store.sectors,
      })
      store.isErrors = false
      message.value = 'Your form has been updated. You can edit and resubmit it if needed.'
    } catch (error) {
      console.error('Error occurred during saving:', error)
    }
    return
  }
  try {
    const form = await service.saveFormAsync({
      name: store.name,
      agreedToTerms: store.agreedToTerms,
    })
    await service.saveSectorInFormsAsync({
      form: form.data!,
      sectorIds: store.sectors,
    })
    store.formId = form.data!.id
    store.isEditing = true
    store.isErrors = false
    message.value = 'Your form has been saved. You can now edit and resubmit it if needed.'
    router.push({ name: 'home', query: { formId: form!.data!.id } })
  } catch (error) {
    console.error('Error occurred during saving:', error)
  }
}

const handleExit = () => {
  clearStore()
  router.push({ name: 'home' })
}
</script>

<template>
  <div class="form-container">
    <h2>Form</h2>
    <div class="form-group">
      <div class="message success" v-if="store.isEditing && !store.isErrors">
        <div>{{ message }}</div>
      </div>
      <div class="message" v-else>
        <div>Please enter your name and pick the sectors you are currently involved in.</div>
      </div>
      <div class="message error" v-if="store.isErrors">
        <div v-for="error in errorMessage">{{ error }}</div>
      </div>
      <div class="form-field">
        <label for="name">Name:</label>
        <input type="text" id="firstName" v-model="store.name" />
      </div>
      <div class="form-field">
        <label for="sectors">Sectors:</label>
        <select multiple size="5" v-model="store.sectors" id="sectors">
          <Selectbox :sectors="data" :level="0" />
        </select>
      </div>
      <div class="form-field checkbox">
        <input type="checkbox" id="agreed" v-model="store.agreedToTerms" />
        <label for="agreed">I agree to the terms and conditions</label>
      </div>
    </div>
    <div class="buttons">
      <button @click="handleSave" class="save-button">
        {{ store.isEditing ? 'Update' : 'Save' }}
      </button>

      <button @click="handleExit" class="exit-button" v-if="store.isEditing">Exit</button>
    </div>
  </div>
</template>

<style scoped></style>
