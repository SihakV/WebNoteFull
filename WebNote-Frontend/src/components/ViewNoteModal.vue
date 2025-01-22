<template>
  <div
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4"
  >
    <div
      class="bg-white rounded-lg p-6 w-full max-w-2xl max-h-[80vh] flex flex-col"
    >
      <div class="mb-4 flex justify-between items-center">
        <h2 class="text-2xl font-semibold">{{ note.title }}</h2>
        <button
          @click="$emit('close')"
          class="text-gray-500 hover:text-gray-700"
        >
          ✕
        </button>
      </div>

      <div class="mb-6 overflow-y-auto whitespace-pre-wrap">
        {{ note.content }}
      </div>

      <div class="text-sm text-gray-500">
        Created: {{ new Date(note.createdAt).toLocaleString() }}
      </div>

      <div class="mt-4 flex justify-end space-x-2">
        <button
          @click="$emit('edit', note)"
          class="px-4 py-2 text-blue-500 hover:text-blue-700"
        >
          Edit
        </button>
        <button
          @click="$emit('close')"
          class="px-4 py-2 bg-gray-100 rounded hover:bg-gray-200"
        >
          Close
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import type { Note } from "../types/Note";

defineProps<{
  note: Note;
}>();

defineEmits<{
  close: [];
  edit: [note: Note];
}>();
</script>
