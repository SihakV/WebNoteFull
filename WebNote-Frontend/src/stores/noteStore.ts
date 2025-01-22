// src/stores/noteStore.ts
import { defineStore } from "pinia";
import axios from "axios";
import type { Note } from "../types/Note";

const API_URL = "https://localhost:7188/api"; // Update with your API port

export const useNoteStore = defineStore("notes", {
  state: () => ({
    notes: [] as Note[],
    loading: false,
    error: null as string | null,
    searchQuery: "",
    sortBy: "createdAt_desc" as string,
  }),

  getters: {
    filteredNotes: (state) => {
      let sortedNotes = state.notes.filter(
        (note) =>
          note.title.toLowerCase().includes(state.searchQuery.toLowerCase()) ||
          note.content.toLowerCase().includes(state.searchQuery.toLowerCase())
      );

      const [field, order] = state.sortBy.split("_");

      if (field === "title") {
        return sortedNotes.sort((a, b) => a.title.localeCompare(b.title));
      }

      return sortedNotes.sort((a, b) => {
        const dateA = new Date(a[field as "createdAt" | "updatedAt"]).getTime();
        const dateB = new Date(b[field as "createdAt" | "updatedAt"]).getTime();
        return order === "asc" ? dateA - dateB : dateB - dateA;
      });
    },
  },

  actions: {
    async fetchNotes() {
      try {
        this.loading = true;
        const response = await axios.get(`${API_URL}/notes`);
        this.notes = response.data;
      } catch (error) {
        this.error = "Failed to fetch notes";
        console.error("Error fetching notes:", error);
      } finally {
        this.loading = false;
      }
    },

    async createNote(note: Partial<Note>) {
      try {
        this.loading = true;
        const response = await axios.post(`${API_URL}/notes`, note);
        this.notes.unshift(response.data);
      } catch (error) {
        this.error = "Failed to create note";
        console.error("Error creating note:", error);
      } finally {
        this.loading = false;
      }
    },

    async updateNote(note: Note) {
      try {
        this.loading = true;
        await axios.put(`${API_URL}/notes/${note.id}`, note);
        const index = this.notes.findIndex((n) => n.id === note.id);
        if (index !== -1) {
          this.notes[index] = note;
        }
      } catch (error) {
        this.error = "Failed to update note";
        console.error("Error updating note:", error);
      } finally {
        this.loading = false;
      }
    },

    async deleteNote(id: number) {
      try {
        this.loading = true;
        await axios.delete(`${API_URL}/notes/${id}`);
        this.notes = this.notes.filter((note) => note.id !== id);
      } catch (error) {
        this.error = "Failed to delete note";
        console.error("Error deleting note:", error);
      } finally {
        this.loading = false;
      }
    },
  },
});
