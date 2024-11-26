<script setup>
import BarreDeNavigation from "./components/views/BarreDeNavigation.vue";
import PiedDePage from "./components/views/PiedDePage.vue";
import { onMounted } from "vue";
import { useStore } from "vuex";

const store = useStore();

onMounted(async () => {
  if (store.state.isLoggedIn) {
    await store.dispatch("fetchUserBids");
  }
  // Initialiser SignalR une seule fois au montage de l'application
  if (store.state.isLoggedIn && !store.state.connection) {
    await store.dispatch("initializeSignalR");
  }
});
</script>

<template>
  <BarreDeNavigation />

  <main class="mh-100 flex-grow-1">
    <router-view></router-view>
  </main>

  <footer>
    <PiedDePage />
  </footer>

  <Toaster position="top-right" />
</template>

<script>
export default {
  name: "App",
};
</script>

<style scoped></style>
