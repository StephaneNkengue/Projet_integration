<template>
  <router-link class="text-decoration-none"
               :to="{ name: 'DetailsLot', params: { idLot: lot.id } }">
      <div class="card" :class="{
    'user-bid': isUserHighestBidder,
    'user-outbid': isUserOutbid
  }">
          <div class="card-body">
              <div class="d-flex flex-md-row flex-column flex-wrap justify-content-between gap-1">
                  <div class="d-flex flex-column justify-content-center align-items-center col-12 col-md-4 col-lg-3">
                      <img :src="urlImage"
                           class="img-fluid"
                           alt="Image du lot" />
                  </div>
                  <div class="d-flex align-items-center justify-content-center gap-3 flex-md-column flex-row flex-lg-row">
                      <p class="mb-0">Lot {{ lot.numero }}</p>
                      <p class="mb-0">{{ lot.artiste }}</p>
                      <p class="mb-0">{{ lot.hauteur }} x {{ lot.largeur }} po</p>
                  </div>
                  <div class="d-flex flex-column flex-lg-row align-items-center gap-3 justify-content-center">
                      
                      <div class="mise-actuelle" v-if="estMontantValide(miseActuelle)">
                          Mise actuelle: {{ formatMontant(miseActuelle) }}$
                      </div>
                      <div class="mise-actuelle" v-else>
                          0.00$
                      </div>
                      <p class="text-center mb-0">
                          Valeur: {{ lot.valeurEstimeMin.toFixed(0) }}$ -
                          {{ lot.valeurEstimeMax.toFixed(0) }}$
                      </p>
                      <p class="text-center mb-0 text-muted">
                          {{ nombreOffres }} {{ nombreOffres <= 1 ? 'offre' : 'offres' }}
                      </p>
                  </div>
                  <div class="d-flex align-self-center gap-1 flex-column flex-md-row align-items-center">
                      <button type="button"
                              class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond"
                              @click.stop.prevent="handleMiseClick"
                              v-if="!isAdmin">
                          Miser {{ formatMontant(getMiseMinimale) }}$
                      </button>
                      <img src="/icons/IconeLivrable.png"
                           height="50"
                           width="50"
                           alt="Livrable"
                           v-if="lot.estLivrable" />
                      <img src="/icons/IconeNonLivrable.png"
                           height="50"
                           width="50"
                           alt="Non livrable"
                           v-else />
                  </div>
              </div>
          </div>
      </div>
  </router-link>

  <ModalMise ref="modalMise"
             :lot="lotPourModal"
             @miseConfirmee="onMiseConfirmee" />
</template>
<script setup>
  import { onMounted, ref, computed, watch, onUnmounted, nextTick } from "vue";
  import { useStore } from "vuex";
  import ModalMise from '@/components/modals/ModalMise.vue';
  import { toast } from 'vue3-toastify';
  import { useRouter } from 'vue-router';

  const store = useStore();
  const router = useRouter();
  const props = defineProps({
      lotRecu: Object,
  });

  const urlApi = ref("/api");
  const userLastBid = ref(0);
  const modalMise = ref(null);

  const calculerPasEnchere = (montant) => {
      if (montant <= 199.0) return 10.0;
      if (montant <= 499.0) return 25.0;
      if (montant <= 999.0) return 50.0;
      if (montant <= 1999.0) return 100.0;
      if (montant <= 4999.0) return 200.0;
      if (montant <= 9999.0) return 250.0;
      if (montant <= 19999.0) return 500.0;
      if (montant <= 49999.0) return 1000.0;
      if (montant <= 99999.0) return 2000.0;
      if (montant <= 499999.0) return 5000.0;
      return 10000.0;
  };

  const formatMontant = (montant) => {
      const valeur = Number(montant);
      return isNaN(valeur) ? '0' : Math.floor(valeur).toString();
  };

  const miseActuelle = computed(() => {
      const lot = store.getters.getLot(props.lotRecu.id);
      if (lot?.mise !== undefined) {
          return lot.mise;
      }
      return props.lotRecu.mise || 0;
  });

  const getMiseMinimale = computed(() => {
      if (!miseActuelle.value || miseActuelle.value === 0) {
          return props.lotRecu?.prixOuverture || 0;
      }
      return calculerPasEnchere(miseActuelle.value) + miseActuelle.value;
  });

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

  const isUserHighestBidder = computed(() => {
      const lotActuel = store.getters.getLot(props.lotRecu.id);
      const userId = store.state.user?.id;
      return store.getters.hasUserBidOnLot(props.lotRecu.id) && 
             lotActuel?.idClientMise === userId;
  });

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

  watch(() => store.state.lots, (newLots) => {
      const lotActuel = store.getters.getLot(props.lotRecu.id);
      if (lotActuel) {
          lot.value = {...lotActuel};
          nextTick(() => {
              isUserHighestBidder.value;
              isUserOutbid.value;
          });
      }
  }, { deep: true, immediate: true });

  const isAdmin = computed(() => store.getters.isAdmin);
  const isLoggedIn = computed(() => store.state.isLoggedIn);

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

  watch(isLoggedIn, (newValue) => {
      if (!newValue) {
          // Forcer la mise à jour de l'état du lot quand l'utilisateur se déconnecte
          const lotActuel = store.getters.getLot(props.lotRecu.id);
          if (lotActuel) {
              lot.value = lotActuel;
          }
      }
  });

  const isUserOutbid = computed(() => {
      const lotActuel = store.getters.getLot(props.lotRecu.id);
      const aFaitUneMise = store.getters.hasUserBidOnLot(props.lotRecu.id) || userLastBid.value > 0;
      const nestPasPlusHautEncherisseur = lotActuel?.idClientMise !== store.state.user?.id;
      const miseExiste = lotActuel?.mise > 0;
      const estSurenchere = lotActuel?.mise > userLastBid.value;

      return aFaitUneMise && nestPasPlusHautEncherisseur && miseExiste && estSurenchere;
  });

  const handleNewBid = async (data) => {
    console.log('SignalR Received:', {
      data,
      currentUserId: store.state.user?.id,
      userLastBid: userLastBid.value
    });

    if (data.idLot === props.lotRecu.id) {
      store.commit('updateLotMise', {
        idLot: data.idLot,
        montant: data.montant,
        userId: data.userId,
        userLastBid: data.userLastBid,
        nombreMises: data.nombreMises
      });

      // Mettre à jour immédiatement le lot local
      const lotActuel = store.getters.getLot(props.lotRecu.id);
      if (lotActuel) {
        lot.value = {...lotActuel};
      }

      if (data.userLastBid && data.userLastBid.userId === store.state.user?.id) {
        console.log('Updating userLastBid:', data.userLastBid.montant);
        userLastBid.value = data.userLastBid.montant;
      }

      nextTick(() => {
        console.log('After nextTick:', {
          isHighestBidder: isUserHighestBidder.value,
          isOutbid: isUserOutbid.value,
          userLastBid: userLastBid.value,
          nombreMises: nombreOffres.value
        });
      });
    }
  };

  onMounted(async () => {
      // Initialisation immédiate avec les données du store
      const lotActuel = store.getters.getLot(props.lotRecu.id);
      if (lotActuel) {
          lot.value = {...lotActuel};
      } else {
          lot.value = {...props.lotRecu};
      }
      
      urlApi.value = await store.state.api.defaults.baseURL.replace("\api", "");
      
      // Récupérer la dernière mise de l'utilisateur
      if (props.lotRecu.id) {
          userLastBid.value = await store.dispatch('getUserBidForLot', props.lotRecu.id);
      }
      
      // S'abonner aux événements de mise
      if (store.state.connection) {
          store.state.connection.on("ReceiveNewBid", handleNewBid);
      }

      // Forcer une réévaluation des computed properties
      nextTick(() => {
          isUserHighestBidder.value;
          isUserOutbid.value;
      });
  });

  watch(() => store.getters.getLot(props.lotRecu.id)?.mise, async (newMise) => {
      if (newMise && props.lotRecu.id) {
          userLastBid.value = await store.dispatch('getUserBidForLot', props.lotRecu.id);
      }
  });

  onUnmounted(() => {
      if (store.state.connection) {
          store.state.connection.off("ReceiveNewBid", handleNewBid);
      }
  });

  watch(() => props.lotRecu, (newLot) => {
      if (newLot) {
          lot.value = {...newLot};
          // Forcer la mise à jour des données depuis le store
          const lotActuel = store.getters.getLot(newLot.id);
          if (lotActuel) {
              lot.value = {...lotActuel};
          }
      }
  }, { immediate: true, deep: true });

  const nombreOffres = computed(() => {
      return store.getters.getUniqueOffersCount(props.lotRecu.id);
  });

</script>
<style scoped>
  .card {
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

