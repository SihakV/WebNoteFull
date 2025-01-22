<template>
  <div class="min-h-screen bg-gray-50">
    <header class="bg-white shadow">
      <div class="max-w-7xl mx-auto py-6 px-4">
        <h1 class="text-3xl font-bold text-gray-900">
          Notes App for <span class="text-pink-500">Techbodia</span>
        </h1>
      </div>
    </header>
    <main class="max-w-7xl mx-auto py-6 px-4">
      <NoteList />
    </main>
  </div>
</template>

<script setup lang="ts">
import NoteList from "./components/NoteList.vue";
</script>
