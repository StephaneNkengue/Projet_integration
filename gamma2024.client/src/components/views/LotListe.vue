<template>
  <router-link
    class="text-decoration-none"
    :to="{ name: 'DetailsLot', params: { idLot: lot.id } }"
  >
    <div class="card" :class="{ 'user-bid': hasUserBidOnLot }">
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
              class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond"
              @click.stop.prevent="ouvrirModalMise"
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

  <ModalMise 
    ref="modalMise"
    :lot="lotPourModal"
    @miseConfirmee="onMiseConfirmee"
  />
</template>
<script setup>
import { onMounted, ref, computed} from "vue";
import { useStore } from "vuex";
import ModalMise from '@/components/modals/ModalMise.vue';

const store = useStore();
const props = defineProps({
  lotRecu: Object,
});

const urlApi = ref("/api");
// Créer un objet computed pour le lot à passer au modal
const lotPourModal = computed(() => ({
  id: props.lotRecu.id,
  numero: props.lotRecu.numero,
  mise: props.lotRecu.mise,
  prixOuverture: props.lotRecu.prixOuverture,
  prixMinPourVente: props.lotRecu.prixMinPourVente,
  artiste: props.lotRecu.artiste,
  hauteur: props.lotRecu.hauteur,
  largeur: props.lotRecu.largeur,
  estVendu: props.lotRecu.estVendu,
  dateFinVente: props.lotRecu.dateFinVente,
  estLivrable: props.lotRecu.estLivrable,
  valeurEstimeMin: props.lotRecu.valeurEstimeMin,
  valeurEstimeMax: props.lotRecu.valeurEstimeMax,
  photos: props.lotRecu.photos,
  description: props.lotRecu.description,
}));

// Garder lot comme ref pour le template
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

const modalMise = ref(null);

onMounted(async () => {
  lot.value = props.lotRecu;
  urlApi.value = await store.state.api.defaults.baseURL.replace("\api", "");
});

const ouvrirModalMise = (event) => {
  event.stopPropagation();
  console.log("Ouverture modal pour lot:", lotPourModal.value);
  modalMise.value.show();
};

const onMiseConfirmee = async (montant) => {
  lot.value.mise = montant;
  
  try {
    const response = await store.dispatch('chercherDetailsLotParId', lot.value.id);
    if (response && response.data) {
      lot.value = response.data;
    }
  } catch (error) {
    console.error("Erreur lors du rechargement des données du lot:", error);
  }
};

const hasUserBidOnLot = computed(() => {
  return store.getters.hasUserBidOnLot(props.lotRecu.id);
});
</script>
<style scoped>
.user-bid {
  border: 2px solid #4CAF50;
  box-shadow: 0 0 10px rgba(76, 175, 80, 0.3);
}
</style>

