<template>
  <router-link
    class="text-decoration-none"
    :to="{ name: 'DetailsLot', params: { idLot: lot.id } }"
  >
    <div class="card">
      <div class="card-body">
        <div
          class="d-flex flex-md-row flex-column flex-wrap justify-content-between gap-1"
        >
          <div
            class="d-flex flex-column justify-content-center align-items-center"
          >
            <img
              v-bind:src="urlApi + lot.photos[0].lien"
              height="150"
              alt="Image du lot"
            />
          </div>

          <div
            class="d-flex align-items-center justify-content-center gap-3 flex-md-column flex-row flex-lg-row"
          >
            <p class="mb-0">Lot {{ lot.numero }}</p>
            <p class="mb-0">{{ lot.artiste }}</p>
            <p class="mb-0">{{ lot.hauteur }} x {{ lot.largeur }} po</p>
          </div>

          <div
            class="d-flex flex-column flex-lg-row align-items-center gap-3 justify-content-center"
          >
            <p class="text-center mb-0">
              Valeur: {{ lot.valeurEstimeMin.toFixed(0) }}$ -
              {{ lot.valeurEstimeMax.toFixed(0) }}$
            </p>
            <p class="text-center mb-0">
              Mise actuelle: {{ lot.mise.toFixed(0) }}$
            </p>
          </div>

          <div
            class="d-flex align-self-center gap-1 flex-column flex-md-row align-items-center"
          >
            <button
              type="button"
              class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond justify-self-end m-1"
            >
              Miser
            </button>
            <img
              src="/icons/IconeLivrable.png"
              height="50"
              width="50"
              alt="Livrable"
              v-if="lot.estLivrable"
            />

            <img
              src="/icons/IconeNonLivrable.png"
              height="50"
              width="50"
              alt="Non livrable"
              v-else
            />
          </div>
        </div>
      </div>
    </div>
  </router-link>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useStore } from "vuex";

const store = useStore();

const props = defineProps({
  lotRecu: Object,
});

const urlApi = ref("/api");
const lot = ref({
  id: 0,
  numero: "",
  description: "",
  valeurEstimeMin: 0,
  valeurEstimeMax: 0,
  artiste: " ",
  mise: 0,
  estVendu: true,
  dateFinVente: "",
  estLivrable: true,
  largeur: 0,
  hauteur: 0,
  photos: [
    {
      id: 0,
      lien: "",
      idLot: 0,
      lot: null,
    },
  ],
});

onMounted(async () => {
  lot.value = props.lotRecu;
  urlApi.value = await store.state.api.defaults.baseURL.replace("\api", "");
});
</script>

<style scoped></style>
