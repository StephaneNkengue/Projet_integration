<template>
  <router-link
    class="text-decoration-none card d-flex align-self-stretch"
    :class="{
      'user-bid': isUserHighestBidder,
      'user-outbid': isUserOutbid
    }"
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
            :src="urlImage"
            class="img-fluid my-2 col-8 col-sm-10 col-md-12"
            alt="Image du lot"
          />
        </div>

                <p class="text-center mb-0">Valeur: {{(lot.valeurEstimeMin).toFixed(0)}}$ - {{(lot.valeurEstimeMax).toFixed(0)}}$</p>
                <p class="text-center mb-0">Mise actuelle: {{ miseActuelle.toFixed(0) }}$</p>

        <div class="d-flex justify-content-around pt-2">
          <button
            type="button"
            class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond"
            @click.prevent="handleMiseClick"
            v-if="!isAdmin"
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
import { ref, computed, watch, onMounted, onUnmounted } from 'vue';
import { useStore } from 'vuex';
import { toast } from 'vue3-toastify';
import { useRouter } from 'vue-router';
import ModalMise from '@/components/modals/ModalMise.vue';

const store = useStore();
const router = useRouter();
const urlApi = ref("");
const modalMise = ref(null);

// Définir les props en premier
const props = defineProps({
    lotRecu: {
        type: Object,
        required: true
    }
});

// Initialiser lot après props
const lot = ref(props.lotRecu || {});
const mise = ref(0);

// Computed properties
const isLoggedIn = computed(() => store.state.isLoggedIn);
const isAdmin = computed(() => store.getters.isAdmin);
const isUserHighestBidder = computed(() => {
    const lotActuel = store.getters.getLot(props.lotRecu.id);
    return store.getters.hasUserBidOnLot(props.lotRecu.id) && lotActuel?.mise === miseActuelle.value;
});

const miseActuelle = computed(() => {
    const lot = store.getters.getLot(props.lotRecu.id);
    if (lot?.mise !== undefined) {
        return lot.mise;
    }
    return props.lotRecu.mise || 0;
});

const formatMontant = (montant) => {
    const valeur = Number(montant);
    return isNaN(valeur) ? '0.00' : valeur.toFixed(2);
};

const urlImage = computed(() => {
    const lotStore = store.getters.getLot(props.lotRecu.id);
    if (lotStore?.photos?.[0]?.lien) {
        return urlApi.value + lotStore.photos[0].lien;
    }
    return urlApi.value + props.lotRecu.photos[0].lien;
});

// Surveiller les changements dans le store
watch(() => store.state.lots, (newLots) => {
    const lotActuel = store.getters.getLot(props.lotRecu.id);
    if (lotActuel) {
        lot.value = lotActuel;
    }
}, { deep: true });

// Ajouter une méthode pour vérifier si le montant est valide
const estMontantValide = (montant) => {
    return montant !== undefined && montant !== null && !isNaN(Number(montant));
};

// Ajouter un watch pour mettre à jour immédiatement
watch(() => store.state.lots, () => {
    // Force la réévaluation du computed
    isUserHighestBidder.value;
}, { immediate: true, deep: true });

// Déplacer le watch de isLoggedIn après sa définition
watch(isLoggedIn, (newValue) => {
    if (!newValue) {
        const lotActuel = store.getters.getLot(props.lotRecu.id);
        if (lotActuel) {
            lot.value = lotActuel;
        }
    }
});

const handleNewBid = (data) => {
  if (data.idLot === props.lotRecu.id) {
    store.commit('updateLotMise', {
      idLot: data.idLot,
      montant: data.montant,
      userId: data.userId
    });
  }
};

onMounted(async () => {
    urlApi.value = await store.state.api.defaults.baseURL.replace("\api", "");
    if (lot.value.mise != null) {
        mise.value = lot.value.mise;
    }
    
    // S'abonner aux événements de mise
    if (store.state.connection) {
        store.state.connection.on("ReceiveNewBid", handleNewBid);
    }
});

onUnmounted(() => {
    // Se désabonner des événements
    if (store.state.connection) {
        store.state.connection.off("ReceiveNewBid", handleNewBid);
    }
});

const ouvrirModalMise = (event) => {
    event.stopPropagation();
    modalMise.value.show();
};

const onMiseConfirmee = async (montant) => {
    lot.value.mise = montant;
    mise.value = montant;
    
    try {
        const response = await store.dispatch('chercherDetailsLotParId', lot.value.id);
        if (response && response.data) {
            lot.value = response.data;
            mise.value = response.data.mise;
        }
    } catch (error) {
        console.error("Erreur lors du rechargement des données du lot:", error);
    }
};

// Créer un objet computed pour le lot à passer au modal
const lotPourModal = computed(() => ({
    id: props.lotRecu.id,
    numero: props.lotRecu.numero,
    mise: props.lotRecu.mise,
    prixOuverture: props.lotRecu.prixOuverture,
    prixMinPourVente: props.lotRecu.prixMinPourVente,
    description: props.lotRecu.description,
    valeurEstimeMin: props.lotRecu.valeurEstimeMin,
    valeurEstimeMax: props.lotRecu.valeurEstimeMax,
    artiste: props.lotRecu.artiste,
    estVendu: props.lotRecu.estVendu,
    dateFinVente: props.lotRecu.dateFinVente,
    estLivrable: props.lotRecu.estLivrable,
    largeur: props.lotRecu.largeur,
    hauteur: props.lotRecu.hauteur,
    photos: props.lotRecu.photos
}));

const handleMiseClick = (event) => {
    if (!isLoggedIn.value) {
        toast.info("Veuillez vous connecter pour pouvoir miser", {
            position: "top-right",
            autoClose: 5000,
            hideProgressBar: false,
            closeOnClick: false, // Changé à false
            pauseOnHover: true,
            draggable: true,
            theme: "colored",
            style: {
                fontSize: '16px',
                padding: '15px'
            },
            onClose: () => {
                // Redirection après la fermeture du toast
                router.push('/connexion');
            }
        });
        return;
    }
    ouvrirModalMise(event);
};

// Ajouter ce computed
const isUserOutbid = computed(() => {
    const lotActuel = store.getters.getLot(props.lotRecu.id);
    return store.getters.hasUserBidOnLot(props.lotRecu.id) && !isUserHighestBidder.value;
});

</script>

<style scoped>
.fs-7 {
    font-size: 0.89rem;
}

.card {
    box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 3px 5px 0 rgba(0, 0, 0, 0.19);
    transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

.user-bid {
    border: 2px solid #4CAF50 !important;
    box-shadow: 0 0 10px rgba(76, 175, 80, 0.3) !important;
}

.user-outbid {
    border: 2px solid #FF0000 !important;
    box-shadow: 0 0 10px rgba(255, 0, 0, 0.3) !important;
}
</style>
