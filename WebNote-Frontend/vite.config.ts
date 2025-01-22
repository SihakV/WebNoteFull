import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

export default defineConfig({
  plugins: [vue()],
  base: "/WebNoteFull/",
  build: {
    outDir: "../docs", // Build to docs in root folder
  },
});
