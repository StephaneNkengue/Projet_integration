<template>
  <router-link
    class="text-decoration-none"
    :to="{ name: 'DetailsLot', params: { idLot: lot.id } }"
  >
    <div
      class="card"
      :class="{
        'user-bid': isUserHighestBidder,
        'user-outbid': isUserOutbid,
      }"
    >
      <div class="card-body">
        <div
          class="d-flex flex-md-row flex-column flex-wrap justify-content-between gap-1"
        >
          <div
            class="d-flex flex-column justify-content-center align-items-center col-12 col-md-4 col-lg-3"
          >
            <img :src="urlImage" class="img-fluid" alt="Image du lot" />
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
            <div class="mise-actuelle" v-if="estMontantValide(miseActuelle)">
              {{ formatMontant(miseActuelle) }}$
            </div>
            <div class="mise-actuelle" v-else>0.00$</div>
          </div>
          <div
            class="d-flex align-self-center gap-1 flex-column flex-md-row align-items-center"
          >
            <button
              type="button"
              class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond"
              @click.stop.prevent="handleMiseClick"
              v-if="!isAdmin"
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
import { onMounted, ref, computed } from "vue";
import { useStore } from "vuex";
import ModalMise from "@/components/modals/ModalMise.vue";
import { toast } from "vue3-toastify";
import { useRouter } from "vue-router";

const store = useStore();
const router = useRouter();
const props = defineProps({
  lotRecu: Object,
});
const modalMise = ref(null);
const urlApi = ref("/api");
const lot = ref({ ...props.lotRecu });

const lotPourModal = computed(() => ({ ...props.lotRecu }));

const isUserHighestBidder = computed(() => {
  const lotActuel = store.getters.getLot(props.lotRecu.id);
  return (
    store.getters.hasUserBidOnLot(props.lotRecu.id) &&
    lotActuel?.mise === miseActuelle.value
  );
});

const miseActuelle = computed(() => {
  const lot = store.getters.getLot(props.lotRecu.id);
  return lot?.mise !== undefined ? lot.mise : props.lotRecu.mise || 0;
});

const formatMontant = (montant) => {
  const valeur = Number(montant);
  return isNaN(valeur) ? "0.00" : valeur.toFixed(2);
};

const estMontantValide = (montant) => {
  return montant !== undefined && montant !== null && !isNaN(Number(montant));
};

const urlImage = computed(() => {
  const lotStore = store.getters.getLot(props.lotRecu.id);
  if (lotStore?.photos?.[0]?.lien) {
    return urlApi.value + lotStore.photos[0].lien;
  }
  return urlApi.value + props.lotRecu.photos[0].lien;
});

const isAdmin = computed(() => store.getters.isAdmin);
const isLoggedIn = computed(() => store.state.isLoggedIn);

const handleMiseClick = (event) => {
  if (!isLoggedIn.value) {
    toast.info("Veuillez vous connecter pour pouvoir miser", {
      position: "top-right",
      autoClose: 5000,
      hideProgressBar: false,
      closeOnClick: false,
      pauseOnHover: true,
      draggable: true,
      theme: "colored",
      style: {
        fontSize: "16px",
        padding: "15px",
      },
      onClose: () => {
        router.push("/connexion");
      },
    });
    return;
  }
  ouvrirModalMise(event);
};

const ouvrirModalMise = (event) => {
  event.stopPropagation();
  modalMise.value.show();
};

const onMiseConfirmee = async (montant) => {
  lot.value.mise = montant;

  try {
    const response = await store.dispatch(
      "chercherDetailsLotParId",
      lot.value.id
    );
    if (response && response.data) {
      lot.value = response.data;
    }
  } catch (error) {
    console.error("Erreur lors du rechargement des données du lot:", error);
  }
};

onMounted(async () => {
  lot.value = props.lotRecu;
  urlApi.value = await store.state.api.defaults.baseURL.replace("/api", "");

  // Vérifier si la connexion SignalR est active, sinon initialiser
  if (!store.state.connection) {
    await store.dispatch("initSignalR");
  }
});

const isUserOutbid = computed(() => {
  return (
    store.getters.hasUserBidOnLot(props.lotRecu.id) &&
    !isUserHighestBidder.value
  );
});
</script>

<style scoped>
.card {
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

.user-bid {
  border: 2px solid #4caf50 !important;
  box-shadow: 0 0 10px rgba(76, 175, 80, 0.3) !important;
}

.user-outbid {
  border: 2px solid #ff0000 !important;
  box-shadow: 0 0 10px rgba(255, 0, 0, 0.3) !important;
}
</style>
