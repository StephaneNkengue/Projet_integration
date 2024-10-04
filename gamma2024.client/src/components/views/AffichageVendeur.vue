<template>
    <div class="container mt-5">
      <h2>Gestion des vendeurs</h2>
      <router-link to="/vendeurcreation" class="btn btn-primary mb-3">Ajouter un vendeur</router-link>
      <table class="table">
        <thead>
          <tr>
            <th>Nom</th>
            <th>Prénom</th>
            <th>Courriel</th>
            <th>Téléphone</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="vendeur in vendeurs" :key="vendeur.id">
            <td>{{ vendeur.nom }}</td>
            <td>{{ vendeur.prenom }}</td>
            <td>{{ vendeur.courriel }}</td>
            <td>{{ vendeur.telephone }}</td>
            <td>
              <router-link :to="`/vendeurmodification/${vendeur.id}`" class="btn btn-sm btn-info">Modifier</router-link>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue';
  import { useStore } from 'vuex';
  
  const store = useStore();
  const vendeurs = ref([]);
  
  onMounted(async () => {
    try {
      vendeurs.value = await store.dispatch('obtenirTousVendeurs');
    } catch (error) {
      console.error("Erreur lors de la récupération des vendeurs:", error);
    }
  });
  </script>