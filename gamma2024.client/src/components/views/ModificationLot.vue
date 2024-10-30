<template>
  <div class="container mt-5">
    <h1 class="text-center mb-4">Modification du lot</h1>
    <form @submit.prevent="modifierLot" v-if="lot">
      <!-- Bloc des photos existantes -->
      <div v-if="lot.photosModifie && lot.photosModifie.length > 0" class="mb-3">
        <h4>Photos existantes</h4>
        <div class="d-flex flex-wrap">
          <div v-for="photo in lot.photosModifie" :key="photo.id" class="me-2 mb-2 position-relative">
            <img :src="getImageUrl(photo.url)" alt="Photo du lot" 
                 style="width: 100px; height: 100px; object-fit: cover; border: 1px solid #ddd;">
            <button @click.prevent="marquerPhotoASupprimer(photo.id)" class="btn btn-danger btn-sm position-absolute top-0 end-0">X</button>
          </div>
        </div>
      </div>
      <div v-if="photosASupprimer.length > 0" class="alert alert-warning">
        {{ photosASupprimer.length }} photo(s) marquée(s) pour suppression
      </div>
      <!-- Champ pour ajouter de nouvelles photos -->
      <div class="mb-3">
        <label for="nouvellesPhotos" class="form-label">Ajouter des photos</label>
        <input type="file" class="form-control" id="nouvellesPhotos" multiple @change="ajouterNouvellesPhotos">
      </div>
      <!-- Prévisualisation des nouvelles photos -->
      <div v-if="nouvellesPhotos.length > 0" class="mb-3">
        <h4>Nouvelles photos</h4>
        <div class="d-flex flex-wrap">
          <div v-for="(photo, index) in nouvellesPhotos" :key="index" class="me-2 mb-2">
            <img :src="photo.preview" alt="Nouvelle photo" style="width: 100px; height: 100px; object-fit: cover;">
          </div>
        </div>
      </div>
      <!-- Le reste des champs du formulaire -->
      <div class="mb-3">
        <label for="code" class="form-label">Numéro</label>
        <input v-model="lot.numero" type="text" class="form-control" id="code" required>
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
        <label for="prixOuverture" class="form-label">Prix d'ouverture</label>
        <input v-model.number="lot.prixOuverture" type="number" class="form-control" id="prixOuverture" required>
      </div>
      <div class="mb-3">
        <label for="prixMinPourVente" class="form-label">Prix minimum pour vente</label>
        <input v-model.number="lot.prixMinPourVente" type="number" class="form-control" id="prixMinPourVente" required>
      </div>
      <div class="mb-3">
        <label for="artiste" class="form-label">Artiste</label>
        <input v-model="lot.artiste" type="text" class="form-control" id="artiste" required>
      </div>
      <div class="mb-3">
        <label for="idCategorie" class="form-label">Catégorie</label>
        <select v-model="lot.idCategorie" class="form-select" id="idCategorie" required>
          <option v-for="categorie in categories" :key="categorie.id" :value="categorie.id">
            {{ categorie.nom }}
          </option>
        </select>
      </div>
      <div class="mb-3">
        <label for="idVendeur" class="form-label">Vendeur</label>
        <select v-model="lot.idVendeur" class="form-select" id="idVendeur" required>
          <option v-for="vendeur in vendeurs" :key="vendeur.id" :value="vendeur.id">
             {{ vendeur.prenom }} {{ vendeur.nom }}
          </option>
        </select>
      </div>
      <div class="mb-3 form-check">
        <input v-model="lot.estLivrable" type="checkbox" class="form-check-input" id="estLivrable">
        <label class="form-check-label" for="estLivrable">Est livrable</label>
      </div>
      <div class="mb-3">
        <label for="idMedium" class="form-label">Medium</label>
        <select v-model="lot.idMedium" class="form-select" id="idMedium" required>
          <option v-for="medium in mediums" :key="medium.id" :value="medium.id">
            {{ medium.type }}
          </option>
        </select>
      </div>
      <div class="mb-3">
        <label for="idEncan" class="form-label">Encan</label>
        <select v-model="lot.idEncanModifie" class="form-select" id="idEncan" required>
          <option v-for="encan in encans" :key="encan.id" :value="encan.id">
            {{ encan.numeroEncan }}
          </option>
        </select>
      </div>
      <div class="mb-3">
        <label for="hauteur" class="form-label">Hauteur</label>
        <input v-model.number="lot.hauteur" type="number" class="form-control" id="hauteur" required>
      </div>
      <div class="mb-3">
        <label for="largeur" class="form-label">Largeur</label>
        <input v-model.number="lot.largeur" type="number" class="form-control" id="largeur" required>
      </div>
      <button type="submit" class="btn btn-primary">Modifier le lot</button>
    </form>
    <br>
    <div v-if="message" class="alert alert-success mt-3">
      {{ message }}
    </div>
    <div v-if="erreur" class="alert alert-danger mt-3">
      {{ erreur }}
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onBeforeUnmount, computed } from 'vue';
import { useStore } from 'vuex';
import { useRouter, useRoute } from 'vue-router';

const store = useStore();
const router = useRouter();
const route = useRoute();

const lot = ref(null);
const categories = ref([]);
const vendeurs = ref([]);
const mediums = ref([]);
const encans = ref([]);
const nouvellesPhotos = ref([]);
const photosASupprimer = ref([]);
const message = ref('');
const erreur = ref('');

onMounted(async () => {
  try {
    lot.value = await store.dispatch('obtenirLot', route.params.id);
    console.log('Photos du lot:', lot.value.photos);
    categories.value = await store.dispatch('obtenirCategories');
    vendeurs.value = await store.dispatch('obtenirVendeurs');
    mediums.value = await store.dispatch('obtenirMediums');
    encans.value = await store.dispatch('obtenirEncans');
  } catch (error) {
    console.error("Erreur lors de la récupération des données:", error);
  }
});

onBeforeUnmount(() => {
  nouvellesPhotos.value.forEach(photo => {
    URL.revokeObjectURL(photo.preview);
  });
});

const marquerPhotoASupprimer = (photoId) => {
  if (!photosASupprimer.value.includes(photoId)) {
    photosASupprimer.value.push(photoId);
    lot.value.photosModifie = lot.value.photosModifie.filter(photo => photo.id !== photoId);
  }
};

const modifierLot = async () => {
  try {
    const formData = new FormData();
    
    // Ajoutez chaque champ au FormData
    formData.append('Id', lot.value.id);
    formData.append('Numero', lot.value.numero || '');
    formData.append('Description', lot.value.description || '');
    formData.append('ValeurEstimeMin', lot.value.valeurEstimeMin || 0);
    formData.append('ValeurEstimeMax', lot.value.valeurEstimeMax || 0);
    formData.append('PrixOuverture', lot.value.prixOuverture || 0);
    formData.append('PrixMinPourVente', lot.value.prixMinPourVente || 0);
    formData.append('Artiste', lot.value.artiste || '');
    formData.append('IdCategorie', lot.value.idCategorie || 0);
    formData.append('IdVendeur', lot.value.idVendeur || 0);
    formData.append('EstLivrable', lot.value.estLivrable || false);
    formData.append('IdMedium', lot.value.idMedium || 0);
    formData.append('IdEncanModifie', lot.value.idEncanModifie || 0);
    formData.append('Hauteur', lot.value.hauteur || 0);
    formData.append('Largeur', lot.value.largeur || 0);
    
    // Ajoutez les nouvelles photos
    nouvellesPhotos.value.forEach(({ file }) => {
      formData.append('NouvellesPhotos', file);
    });
    
    // Ajoutez les IDs des photos à supprimer
    photosASupprimer.value.forEach((photoId) => {
      formData.append('PhotosASupprimer[]', photoId);
    });
    
    const response = await store.dispatch('modifierLot', { id: route.params.id, lotData: formData });
    if (response.success) {
      message.value = "Le lot a été modifié avec succès.";
      erreur.value = '';
      setTimeout(() => {
        router.push({ name: 'TableauDeBordInventaire' });
      }, 2000); 
    } else {
      erreur.value = "Erreur lors de la modification du lot: " + response.data;
      message.value = '';
    }
  } catch (error) {
    if (error.response && error.response.data) {
      erreur.value = "Erreur lors de la modification du lot: " + error.response.data;
    } else {
      erreur.value = "Erreur lors de la modification du lot: " + error.message;
    }
    message.value = '';
  }
};

const getImageUrl = computed(() => (url) => {
  if (!url) return '';
  const baseUrl = store.state.api.defaults.baseURL.replace('/api', '');
  return new URL(url, baseUrl).href;
});

const ajouterNouvellesPhotos = (event) => {
  const files = event.target.files;
  for (let i = 0; i < files.length; i++) {
    const file = files[i];
    if (file.type.startsWith('image/')) {
      const preview = URL.createObjectURL(file);
      nouvellesPhotos.value.push({ file, preview });
    }
  }
};
</script>

