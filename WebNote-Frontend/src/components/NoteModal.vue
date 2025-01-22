<template>
  <div
    class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center"
  >
    <div class="bg-white rounded-lg p-6 w-full max-w-md">
      <h2 class="text-xl font-semibold mb-4">
        {{ props.note ? "Edit Note" : "Create Note" }}
      </h2>

      <form @submit.prevent="handleSubmit">
        <div class="mb-4">
          <label class="block text-sm font-medium mb-1">Title</label>
          <input
            v-model="title"
            type="text"
            required
            class="w-full px-4 py-2 border rounded-lg"
          />
        </div>

        <div class="mb-4">
          <label class="block text-sm font-medium mb-1">Content</label>
          <textarea
            v-model="content"
            rows="4"
            class="w-full px-4 py-2 border rounded-lg"
          ></textarea>
        </div>

        <div class="flex justify-end space-x-2">
          <button
            type="button"
            @click="$emit('close')"
            class="px-4 py-2 border rounded-lg hover:bg-gray-50"
          >
            Cancel
          </button>
          <button
            type="submit"
            class="px-4 py-2 bg-blue-500 text-white rounded-lg hover:bg-blue-600"
          >
            {{ props.note ? "Update" : "Create" }}
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import type { Note } from "@/types/Note";

const props = defineProps<{
  note?: Note;
}>();

const emit = defineEmits<{
  close: [];
  save: [note: Partial<Note>];
}>();

const title = ref("");
const content = ref("");

onMounted(() => {
  if (props.note) {
    title.value = props.note.title;
    content.value = props.note.content;
  }
});

const handleSubmit = () => {
  emit("save", {
    title: title.value,
    content: content.value,
  });
};
</script>
