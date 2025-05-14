<script setup lang="ts">
import type { ISector } from '@/types/ISector'

defineProps<{
  sectors: ISector[]
  level: number
}>()
</script>

<template>
  <template v-for="sector in sectors" :key="sector.id">
    <option :value="sector.id">
      {{ '&nbsp;&nbsp;&nbsp;&nbsp;'.repeat(level) }} {{ sector.name }}
    </option>
    <template v-if="sector.childSectors && sector.childSectors.length > 0">
      <Selectbox :sectors="sector.childSectors" :level="level + 1" />
    </template>
  </template>
</template>
