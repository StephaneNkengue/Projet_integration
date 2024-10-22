<template>
  <div class="container mt-5">
    <h1 class="text-center mb-4">Création d'un nouveau lot</h1>
    <form @submit.prevent="creerLot">
      <div class="mb-3">
        <label for="numero" class="form-label">Numéro</label>
        <input v-model="lot.numero" type="text" class="form-control" id="numero" required>
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
        <input v-model.number="lot.prixMinPourVente" type="number" class="form-control" id="prixMinPourVente">
      </div>
      <div class="mb-3">
        <label for="artiste" class="form-label">Artiste</label>
        <input v-model="lot.artiste" type="text" class="form-control" id="artiste" required>
      </div>
      <div class="mb-3">
        <label for="idCategorie" class="form-label">Catégorie</label>
        <select v-model.number="lot.idCategorie" class="form-control" id="idCategorie" required>
          <!-- Options de catégories à ajouter dynamiquement -->
        </select>
      </div>
      <div class="mb-3">
        <label for="idVendeur" class="form-label">Vendeur</label>
        <select v-model.number="lot.idVendeur" class="form-control" id="idVendeur" required>
          <!-- Options de vendeurs à ajouter dynamiquement -->
        </select>
      </div>
      <div class="mb-3 form-check">
        <input v-model="lot.estLivrable" type="checkbox" class="form-check-input" id="estLivrable">
        <label class="form-check-label" for="estLivrable">Est livrable</label>
      </div>
      <div class="mb-3">
        <label for="hauteur" class="form-label">Hauteur</label>
        <input v-model.number="lot.hauteur" type="number" class="form-control" id="hauteur" required>
      </div>
      <div class="mb-3">
        <label for="largeur" class="form-label">Largeur</label>
        <input v-model.number="lot.largeur" type="number" class="form-control" id="largeur" required>
      </div>
      <div class="mb-3">
        <label for="idMedium" class="form-label">Medium</label>
        <select v-model.number="lot.idMedium" class="form-control" id="idMedium" required>
          <option v-for="medium in mediums" :key="medium.id" :value="medium.id">
            {{ medium.type }}
          </option>
        </select>
      </div>
      <div class="mb-3">
        <label for="idEncan" class="form-label">Encan</label>
        <select v-model.number="lot.idEncan" class="form-control" id="idEncan" required>
          <option v-for="encan in encans" :key="encan.id" :value="encan.id">
            {{ encan.numeroEncan }} - {{ encan.dateDebut | formatDate }}
          </option>
        </select>
      </div>
      <div class="mb-3">
        <label for="photos" class="form-label">Photos</label>
        <input type="file" id="photos" ref="photoInput" @change="handlePhotoUpload" multiple accept="image/*" class="form-control">
      </div>
      <div v-if="previewImages.length > 0" class="mb-3">
        <h4>Aperçu des images</h4>
        <div class="d-flex flex-wrap">
          <div v-for="(image, index) in previewImages" :key="index" class="me-2 mb-2 position-relative">
            <img :src="image" alt="Aperçu" style="width: 100px; height: 100px; object-fit: cover;">
            <button @click.prevent="removeImage(index)" class="btn btn-danger btn-sm position-absolute top-0 end-0">X</button>
          </div>
        </div>
      </div>
      <button type="submit" class="btn btn-primary">Créer le lot</button>
    </form>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';

const store = useStore();
const router = useRouter();

const lot = ref({
  numero: '',
  description: '',
  valeurEstimeMin: 0,
  valeurEstimeMax: 0,
  prixOuverture: 0,
  prixMinPourVente: null,
  artiste: '',
  dateCreation: new Date(),
  idCategorie: null,
  idVendeur: null,
  estLivrable: false,
  hauteur: 0,
  largeur: 0,
  idMedium: null,
  idEncan: null,
  photoUrls: []
});

const mediums = ref([]);
const encans = ref([]);
const previewImages = ref([]);

onMounted(async () => {
  try {
    mediums.value = await store.dispatch('obtenirMediums');
    encans.value = await store.dispatch('obtenirEncans');
  } catch (error) {
    console.error("Erreur lors du chargement des données:", error);
  }
});

const handlePhotoUpload = (event) => {
  const files = Array.from(event.target.files);
  files.forEach(file => {
    const reader = new FileReader();
    reader.onload = (e) => {
      previewImages.value.push(e.target.result);
    };
    reader.readAsDataURL(file);
    lot.value.photos.push(file);
  });
};

const removeImage = (index) => {
  previewImages.value.splice(index, 1);
  lot.value.photos.splice(index, 1);
};

const creerLot = async () => {
  try {
    const formData = new FormData();
    for (const [key, value] of Object.entries(lot.value)) {
      if (key === 'photos') {
        value.forEach((photo, index) => {
          formData.append(`photos[${index}]`, photo);
        });
      } else if (key === 'dateCreation') {
        formData.append(key, value.toISOString());
      } else {
        formData.append(key, value);
      }
    }
    await store.dispatch('creerLot', formData);
    router.push({ name: 'AffichageLots' });
  } catch (error) {
    console.error("Erreur lors de la création du lot:", error);
  }
};
</script>

