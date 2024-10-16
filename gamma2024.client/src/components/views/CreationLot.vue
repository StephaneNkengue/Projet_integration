<template>
    <div class="container mt-5">
      <h1 class="text-center mb-4">Création d'un nouveau lot</h1>
      <form @submit.prevent="creerLot">
        <div class="mb-3">
          <label for="code" class="form-label">Code</label>
          <input v-model="lot.code" type="text" class="form-control" id="code" required>
        </div>
        <div class="mb-3">
          <label for="nom" class="form-label">Nom</label>
          <input v-model="lot.nom" type="text" class="form-control" id="nom" required>
        </div>
        <div class="mb-3">
          <label for="description" class="form-label">Description</label>
          <textarea v-model="lot.description" class="form-control" id="description" required></textarea>
        </div>
        <div class="mb-3">
          <label for="valeurEstimeMin" class="form-label">Valeur estimée minimale</label>
          <input v-model.number="lot.valeurEstimeMin" type="number" class="form-control" id="valeurEstimeMin" required>
        </div>
        <div class="mb-3">
          <label for="valeurEstimeMax" class="form-label">Valeur estimée maximale</label>
          <input v-model.number="lot.valeurEstimeMax" type="number" class="form-control" id="valeurEstimeMax" required>
        </div>
        <div class="mb-3">
          <label for="artiste" class="form-label">Artiste</label>
          <input v-model="lot.artiste" type="text" class="form-control" id="artiste" required>
        </div>
        <div class="mb-3">
          <label for="idCategorie" class="form-label">Catégorie</label>
          <select v-model.number="lot.idCategorie" class="form-select" id="idCategorie" required>
            <!-- Ajoutez les options des catégories ici -->
          </select>
        </div>
        <div class="mb-3">
          <label for="idVendeur" class="form-label">Vendeur</label>
          <select v-model.number="lot.idVendeur" class="form-select" id="idVendeur" required>
            <!-- Ajoutez les options des vendeurs ici -->
          </select>
        </div>
        <div class="mb-3 form-check">
          <input v-model="lot.estLivrable" type="checkbox" class="form-check-input" id="estLivrable">
          <label class="form-check-label" for="estLivrable">Est livrable</label>
        </div>
        <div class="mb-3">
          <label for="dimensions" class="form-label">Dimensions</label>
          <input v-model="lot.dimensions" type="text" class="form-control" id="dimensions">
        </div>
        <div class="mb-3">
          <label for="idMedium" class="form-label">Medium</label>
          <select v-model.number="lot.idMedium" class="form-select" id="idMedium" required>
            <!-- Ajoutez les options des mediums ici -->
          </select>
        </div>
        <button type="submit" class="btn btn-primary">Créer le lot</button>
      </form>
    </div>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  import { useStore } from 'vuex';
  import { useRouter } from 'vue-router';
  
  const store = useStore();
  const router = useRouter();
  
  const lot = ref({
    code: '',
    nom: '',
    description: '',
    valeurEstimeMin: 0,
    valeurEstimeMax: 0,
    artiste: '',
    idCategorie: null,
    idVendeur: null,
    estLivrable: false,
    dimensions: '',
    idMedium: null
  });
  
  const creerLot = async () => {
    try {
      await store.dispatch('creerLot', lot.value);
      router.push({ name: 'AffichageLots' });
    } catch (error) {
      console.error("Erreur lors de la création du lot:", error);
    }
  };
  </script>