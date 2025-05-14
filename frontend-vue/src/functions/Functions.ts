import { formStore } from '@/stores/form'

export function clearStore(): void {
  const store = formStore()
  store.name = ''
  store.formId = 0
  store.sectors = []
  store.agreedToTerms = false
  store.isEditing = false
  store.isErrors = false
}

export function validateData(): string[] {
  const store = formStore()
  let errorMessage: string[] = []

  if (!store.name) {
    errorMessage.push('Please enter your name.')
  }
  if (store.sectors.length === 0) {
    errorMessage.push('Please select at least one sector.')
  }
  if (!store.agreedToTerms) {
    errorMessage.push('You must agree to the terms.')
  }

  return errorMessage
}
