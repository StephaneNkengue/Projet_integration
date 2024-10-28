<template>
    <div class="container mt-5">
      <h1 class="text-center mb-4 fw-bold display-4">Gestion des lots</h1>

      <router-link to="/lots/creation" class="btn btn-lg btn-block mb-4 w-100 bleuMarinSecondaireFond text-white">
        Ajouter un lot
      </router-link>

      <table class="table table-striped table-borderless">
        <thead class="table-dark">
          <tr>
            <th scope="col">#</th>
            <th scope="col">Code</th>
            <th scope="col">Artiste</th>
            <th scope="col">Valeur estimée min</th>
            <th scope="col">Valeur estimée max</th>
            <th scope="col">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(lot, index) in lots" :key="lot.id">
            <th scope="row">{{ index + 1 }}</th>
            <td>{{ lot.code }}</td>
            <td>{{ lot.artiste }}</td>
            <td>{{ lot.valeurEstimeMin }}</td>
            <td>{{ lot.valeurEstimeMax }}</td>
            <td>
              <router-link 
                :to="{ name: 'ModificationLot', params: { id: lot.id } }" 
                class="btn btn-sm btn-primary me-2"
            >
                Modifier
              </router-link>
              <button @click="supprimerLot(lot.id)" class="btn btn-sm btn-danger">
                Supprimer
        </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
</template>

<script setup>
  import { ref, onMounted } from 'vue';
  import { useStore } from 'vuex';
  import { useRouter } from 'vue-router';

  const store = useStore();
  const router = useRouter();
  const lots = ref([]);

  onMounted(async () => {
    try {
        const response = await store.dispatch('obtenirTousLots');
        lots.value = response.data;
    } catch (error) {
        console.error("Erreur lors de la récupération des lots:", error);
    }
  });

  const supprimerLot = async (id) => {
    if (confirm('Êtes-vous sûr de vouloir supprimer ce lot ?')) {
      try {
        await store.dispatch('supprimerLot', id);
        lots.value = lots.value.filter(lot => lot.id !== id);
      } catch (error) {
        console.error("Erreur lors de la suppression du lot:", error);
        }
    }
  };
        for (let i = positionDebut; i < positionFin && i < listeLots.value.length; i++) {
            lotsAffiche.value.push(listeLots.value[i]);
        }
    
</script>

<style scoped>
  /* Styles similaires ceux de AffichageVendeur.vue */
</style>
