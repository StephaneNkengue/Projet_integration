<template>
  <router-link
    class="text-decoration-none card d-flex align-self-stretch"
    :class="{ 'user-bid': hasUserBidOnLot }"
    :to="{ name: 'DetailsLot', params: { idLot: lot.id } }"
  >
    <div class="align-self-stretch">
      <div class="card-body d-flex flex-column justify-content-between">
        <div class="d-flex align-items-middle justify-content-between">
          <p class="fs-7 pe-2 mt-1 mb-0">
            Lot {{ lot.numero }} {{ lot.artiste }} {{ lot.hauteur }} x
            {{ lot.largeur }} po
          </p>

          <img
            src="/icons/IconeLivrable.png"
            height="30"
            alt="Livrable"
            v-if="lot.estLivrable"
          />

          <img
            src="/icons/IconeNonLivrable.png"
            height="30"
            alt="Non livrable"
            v-else
          />
        </div>

        <div class="row justify-content-around align-items-middle">
          <img
            v-bind:src="urlApi + lot.photos[0].lien"
            class="img-fluid my-2 col-8 col-sm-10 col-md-12"
            alt="Image du lot"
          />
        </div>

                <p class="text-center mb-0">Valeur: {{(lot.valeurEstimeMin).toFixed(0)}}$ - {{(lot.valeurEstimeMax).toFixed(0)}}$</p>
                <p class="text-center mb-0">Mise actuelle: {{ lot.mise.toFixed(0) }}$</p>

        <div class="d-flex justify-content-around pt-2">
          <button
            type="button"
            class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond"
            @click.prevent="ouvrirModalMise"
          >
            Miser
          </button>
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
import { onMounted, ref, computed } from "vue";
import { useStore } from "vuex";
import ModalMise from '@/components/modals/ModalMise.vue';

const store = useStore();
const props = defineProps({
  lotRecu: Object
});

const urlApi = ref("/api");

// Créer un objet computed pour le lot à passer au modal
const lotPourModal = computed(() => ({
  id: props.lotRecu.id,
  numero: props.lotRecu.numero,
  description: props.lotRecu.description,
  valeurEstimeMin: props.lotRecu.valeurEstimeMin,
  valeurEstimeMax: props.lotRecu.valeurEstimeMax,
  artiste: props.lotRecu.artiste,
  mise: props.lotRecu.mise,
  estVendu: props.lotRecu.estVendu,
  dateFinVente: props.lotRecu.dateFinVente,
  estLivrable: props.lotRecu.estLivrable,
  largeur: props.lotRecu.largeur,
  hauteur: props.lotRecu.hauteur,
  photos: props.lotRecu.photos
}));

const mise = ref(0);
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
  if (lot.value.mise != null) {
    mise.value = lot.value.mise;
  }
});

const ouvrirModalMise = (event) => {
  event.stopPropagation();
  console.log("Ouverture modal pour lot:", lotPourModal.value);
  modalMise.value.show();
};

const onMiseConfirmee = (montant) => {
  mise.value = montant;
  lot.value.mise = montant;
  // Autres actions après une mise réussie...
};

// Ajouter le computed pour vérifier si l'utilisateur a misé
const hasUserBidOnLot = computed(() => {
  return store.getters.hasUserBidOnLot(props.lotRecu.id);
});

</script>

<style scoped>
.fs-7 {
  font-size: 0.89rem;
}

.card {
  box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 3px 5px 0 rgba(0, 0, 0, 0.19);
}

.user-bid {
  border: 2px solid #4CAF50;
  box-shadow: 0 0 10px rgba(76, 175, 80, 0.3);
}
</style>
