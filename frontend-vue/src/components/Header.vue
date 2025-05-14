<script setup lang="ts">
import { formStore } from '@/stores/form'
import { RouterLink, useRouter } from 'vue-router'
import { clearStore } from '@/functions/Functions'

const store = formStore()
const router = useRouter()

const storeHandler = () => {
  clearStore()
  router.push({ name: 'home' })
}
</script>

<template>
  <header>
    <nav class="navbar">
      <RouterLink class="navbar-brand" to="/" @click.prevent="storeHandler">Home</RouterLink>
    </nav>
  </header>
</template>
