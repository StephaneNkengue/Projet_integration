<template>
    <div class="container mt-5">
      <h2>Modification du vendeur</h2>
      <form @submit.prevent="submitForm">
        <div class="mb-3">
          <label for="nom" class="form-label">Nom</label>
          <input v-model="vendeur.nom" type="text" class="form-control" id="nom" required>
        </div>
        <div class="mb-3">
          <label for="prenom" class="form-label">Prénom</label>
          <input v-model="vendeur.prenom" type="text" class="form-control" id="prenom" required>
        </div>
        <div class="mb-3">
          <label for="courriel" class="form-label">Courriel</label>
          <input v-model="vendeur.courriel" type="email" class="form-control" id="courriel" required>
        </div>
        <div class="mb-3">
          <label for="telephone" class="form-label">Téléphone</label>
          <input v-model="vendeur.telephone" type="tel" class="form-control" id="telephone" required>
        </div>
        <div class="mb-3">
          <label for="numeroCivique" class="form-label">Numéro civique</label>
          <input v-model="vendeur.adresse.numeroCivique" type="text" class="form-control" id="numeroCivique" required>
        </div>
        <div class="mb-3">
          <label for="rue" class="form-label">Rue</label>
          <input v-model="vendeur.adresse.rue" type="text" class="form-control" id="rue" required>
        </div>
        <div class="mb-3">
          <label for="ville" class="form-label">Ville</label>
          <input v-model="vendeur.adresse.ville" type="text" class="form-control" id="ville" required>
        </div>
        <div class="mb-3">
          <label for="province" class="form-label">Province</label>
          <input v-model="vendeur.adresse.province" type="text" class="form-control" id="province" required>
        </div>
        <div class="mb-3">
          <label for="codePostal" class="form-label">Code postal</label>
          <input v-model="vendeur.adresse.codePostal" type="text" class="form-control" id="codePostal" required>
        </div>
        <button type="submit" class="btn btn-primary">Mettre à jour le vendeur</button>
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
  
  const vendeur = ref({
    id: '',
    nom: '',
    prenom: '',
    courriel: '',
    telephone: '',
    adresse: {
      numeroCivique: '',
      rue: '',
      ville: '',
      province: '',
      codePostal: ''
    }
  });
  
  onMounted(async () => {
    const vendeurId = route.params.id;
    try {
      const vendeurData = await store.dispatch('obtenirVendeur', vendeurId);
      // Mappage des données reçues vers le modèle du formulaire
      vendeur.value = {
        id: vendeurData.id,
        nom: vendeurData.nom,
        prenom: vendeurData.prenom,
        courriel: vendeurData.courriel,
        telephone: vendeurData.telephone,
        adresse: {
          numeroCivique: vendeurData.adresse.numeroCivique,
          rue: vendeurData.adresse.rue,
          ville: vendeurData.adresse.ville,
          province: vendeurData.adresse.province,
          codePostal: vendeurData.adresse.codePostal
        }
      };
    } catch (error) {
      console.error("Erreur lors de la récupération des données du vendeur:", error);
    }
  });
  
  const submitForm = async () => {
    try {
      const result = await store.dispatch('modifierVendeur', vendeur.value);
      if (result.success) {
        alert(result.message); // Afficher un message de succès
        router.push('/affichagevendeurs'); // Rediriger vers la liste des vendeurs après modification
      } else {
        alert(result.error); // Afficher un message d'erreur
      }
    } catch (error) {
      console.error("Erreur lors de la modification du vendeur:", error);
      alert("Une erreur est survenue lors de la modification du vendeur.");
    }
  };
  </script>