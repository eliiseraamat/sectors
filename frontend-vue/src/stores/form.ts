import { ref } from 'vue'
import { defineStore } from 'pinia'

export const formStore = defineStore('form', () => {
  const name = ref('')
  const formId = ref()
  const sectors = ref<number[]>([])
  const agreedToTerms = ref<boolean>(false)
  const isEditing = ref(false)
  const isErrors = ref(false)

  return { name, formId, sectors, agreedToTerms, isEditing, isErrors }
})
