<script setup lang="ts">
import { RouterView } from 'vue-router'
import Header from '@/components/Header.vue'
import Footer from '@/components/Footer.vue'
</script>

<template>
  <Header />
  <main class="container">
    <RouterView />
  </main>

  <Footer />
</template>

<style scoped>
.container {
  background-color: #f5f7fa;
  min-height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  padding: 2rem;
}
</style>
