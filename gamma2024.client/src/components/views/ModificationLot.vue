<template>
  <div class="container mt-5">
    <h1 class="text-center mb-4">Modification du lot</h1>
    <form @submit.prevent="modifierLot" v-if="lot">
      <div class="mb-3">
        <label for="code" class="form-label">Code</label>
        <input v-model="lot.code" type="text" class="form-control" id="code" required>
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
        <label for="idMedium" class="form-label">Medium</label>
        <select v-model.number="lot.idMedium" class="form-select" id="idMedium" required>
          <!-- Ajoutez les options des mediums ici -->
        </select>
      </div>
      <div class="mb-3">
        <label for="largeur" class="form-label">Largeur</label>
        <input v-model.number="lot.largeur" type="number" step="0.01" class="form-control" id="largeur" required>
      </div>
      <div class="mb-3">
        <label for="hauteur" class="form-label">Hauteur</label>
        <input v-model.number="lot.hauteur" type="number" step="0.01" class="form-control" id="hauteur" required>
      </div>
      <div class="mb-3">
        <label for="nouvellesPhotos" class="form-label">Ajouter de nouvelles photos</label>
        <input type="file" id="nouvellesPhotos" ref="photoInput" @change="handleNewPhotoUpload" multiple accept="image/*" class="form-control">
      </div>
      <div v-if="lot.photos && lot.photos.length > 0" class="mb-3">
        <h4>Photos existantes</h4>
        <div v-for="photo in lot.photos" :key="photo.id" class="mb-2">
          <img :src="photo.url" alt="Photo du lot" style="width: 100px; height: 100px; object-fit: cover;">
          <button @click.prevent="marquerPhotoASupprimer(photo.id)" class="btn btn-danger btn-sm ms-2">Supprimer</button>
        </div>
      </div>
      <button type="submit" class="btn btn-primary">Modifier le lot</button>
    </form>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useStore } from 'vuex';
import { useRouter, useRoute } from 'vue-router';

const store = useStore();
const router = useRouter();
const route = useRoute();

const lot = ref(null);
const photosASupprimer = ref([]);
const nouvellesPhotos = ref([]);

onMounted(async () => {
  try {
    lot.value = await store.dispatch('obtenirLot', route.params.id);
  } catch (error) {
    console.error("Erreur lors de la récupération du lot:", error);
  }
});

const handleNewPhotoUpload = (event) => {
  nouvellesPhotos.value = Array.from(event.target.files);
};

const marquerPhotoASupprimer = (photoId) => {
  photosASupprimer.value.push(photoId);
};

const modifierLot = async () => {
  try {
    const formData = new FormData();
    
    // Ajouter tous les champs du lot au FormData
    for (const [key, value] of Object.entries(lot.value)) {
      if (key === 'photos') continue; // On gère les photos séparément
      if (value instanceof Date) {
        formData.append(key, value.toISOString());
      } else if (typeof value === 'object' && value !== null) {
        formData.append(key, JSON.stringify(value));
      } else {
        formData.append(key, value);
      }
    }
    
    // Ajouter les nouvelles photos
    nouvellesPhotos.value.forEach((photo, index) => {
      formData.append(`NouvellesPhotos`, photo);
    });
    
    // Ajouter les IDs des photos à supprimer
    photosASupprimer.value.forEach((photoId, index) => {
      formData.append(`PhotosASupprimer`, photoId);
    });
    
    await store.dispatch('modifierLot', { id: route.params.id, lotData: formData });
    router.push({ name: 'AffichageLots' });
  } catch (error) {
    console.error("Erreur lors de la modification du lot:", error);
  }
};
</script>
