<template>
  <div class="container mt-5">
    <h1 class="text-center mb-4 fw-bold display-4">Gestion des vendeurs</h1>

    <router-link
      to="/vendeurcreation"
      class="btn btn-lg btn-block mb-4 w-100 bleuMarinSecondaireFond text-white"
    >
      Ajouter un vendeur
    </router-link>

    <table class="table table-striped">
      <thead class="table-dark">
        <tr>
          <th scope="col">#</th>
          <th scope="col">Nom</th>
          <th scope="col">Prénom</th>
          <th scope="col">Courriel</th>
          <th scope="col">Téléphone</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(vendeur, index) in vendeurs" :key="vendeur.id">
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ vendeur.nom }}</td>
          <td>{{ vendeur.prenom }}</td>
          <td>{{ vendeur.courriel }}</td>
          <td>{{ vendeur.telephone }}</td>
          <td>
            <router-link
              :to="{ name: 'vendeurModification', params: { id: vendeur.id } }"
              class="btn btn-primary btn-lg bleuMarinSecondaireFond"
            >
              Modifier
            </router-link>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useStore } from "vuex";

const store = useStore();
const vendeurs = ref([]);

onMounted(async () => {
  try {
    vendeurs.value = await store.dispatch("obtenirTousVendeurs");
  } catch (error) {
    console.error("Erreur lors de la récupération des vendeurs:", error);
  }
});
</script>

<style scoped>
.bleuMarinSecondaireFond {
  background-color: #1e3a8a; /* Assurez-vous que cette couleur correspond à celle de votre navbar */
  border-color: #1e3a8a;
}
.bleuMarinSecondaireFond:hover {
  background-color: #4d6dc4; /* Une teinte légèrement plus foncée pour l'effet hover */
  border-color: #1e3a8a;
}

/* Ajoutez ces styles pour le texte */
.container {
  font-family: Arial, sans-serif; /* Utilisez la même police que votre navbar */
}

h1,
.btn {
  color: #ffffff; /* Couleur du texte blanc, ajustez si nécessaire */
}

.table {
  color: #333333; /* Couleur du texte foncé pour le contenu du tableau */
}

h1 {
  color: #1e3a8a; /* Assurez-vous que cette couleur est visible sur votre fond */
}
</style>
