<template>
  <div class="p-4">
    <div class="mb-4 flex gap-4">
      <input
        v-model="store.searchQuery"
        type="text"
        placeholder="Search notes..."
        class="px-4 py-2 border rounded-lg flex-grow"
      />
      <select v-model="store.sortBy" class="px-4 py-2 border rounded-lg">
        <option value="createdAt_desc">Created Date (Newest)</option>
        <option value="createdAt_asc">Created Date (Oldest)</option>
        <option value="updatedAt_desc">Updated Date (Newest)</option>
        <option value="updatedAt_asc">Updated Date (Oldest)</option>
        <option value="title">Title</option>
      </select>
    </div>

    <div v-if="store.loading" class="text-center py-4">Loading...</div>

    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
      <div
        v-for="note in store.filteredNotes"
        :key="note.id"
        @click="viewNoteDetails(note)"
        class="p-4 border rounded-lg shadow hover:shadow-md transition-shadow cursor-pointer"
      >
        <h3 class="text-xl font-semibold mb-2">{{ note.title }}</h3>
        <p class="text-gray-600 mb-4 line-clamp-3">{{ note.content }}</p>
        <div class="space-y-2 text-sm text-gray-500">
          <div class="flex justify-between">
            <span
              >Created: {{ new Date(note.createdAt).toLocaleString() }}</span
            >
          </div>
          <div class="flex justify-end space-x-2">
            <button
              @click.stop="editNote(note)"
              class="text-blue-500 hover:text-blue-700"
            >
              Edit
            </button>
            <button
              @click.stop="confirmDelete(note.id)"
              class="text-red-500 hover:text-red-700"
            >
              Delete
            </button>
          </div>
        </div>
      </div>
    </div>

    <button
      @click="showCreateModal = true"
      class="fixed bottom-4 right-4 bg-blue-500 text-white p-4 rounded-full shadow-lg hover:bg-blue-600"
    >
      +
    </button>

    <NoteModal
      v-if="showCreateModal || editingNote"
      :note="editingNote"
      @close="closeModal"
      @save="saveNote"
    />

    <ViewNoteModal
      v-if="viewingNote"
      :note="viewingNote"
      @close="viewingNote = null"
      @edit="editNote"
    />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from "vue";
import { useNoteStore } from "../stores/noteStore";
import type { Note } from "../types/Note";
import NoteModal from "./NoteModal.vue";
import ViewNoteModal from "./ViewNoteModal.vue";

const store = useNoteStore();
const showCreateModal = ref(false);
const editingNote = ref<Note | null>(null);
const viewingNote = ref<Note | null>(null);

onMounted(() => {
  store.fetchNotes();
});

const viewNoteDetails = (note: Note) => {
  viewingNote.value = note;
};

const editNote = (note: Note) => {
  editingNote.value = note;
  viewingNote.value = null;
};

const closeModal = () => {
  showCreateModal.value = false;
  editingNote.value = null;
};

const saveNote = async (note: Partial<Note>) => {
  if (editingNote.value) {
    await store.updateNote({ ...editingNote.value, ...note });
  } else {
    await store.createNote(note);
  }
  closeModal();
};

const confirmDelete = async (id: number) => {
  if (confirm("Are you sure you want to delete this note?")) {
    await store.deleteNote(id);
  }
};
</script>
