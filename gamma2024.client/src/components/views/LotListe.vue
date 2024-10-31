<template>
  <div class="card">
    <!-- Zone cliquable pour les détails -->
    <router-link
      class="text-decoration-none"
      :to="{ name: 'DetailsLot', params: { idLot: lot.id } }"
    >
      <div class="card-body">
        <div class="d-flex flex-md-row flex-column flex-wrap justify-content-between gap-1">
          <!-- Image -->
          <div class="d-flex flex-column justify-content-center align-items-center">
            <img
              v-bind:src="urlApi + lot.photos[0].lien"
              height="150"
              alt="Image du lot"
            />
          </div>

          <!-- Informations -->
          <div class="d-flex align-items-center justify-content-center gap-3 flex-md-column flex-row flex-lg-row">
            <p class="mb-0">Lot {{ lot.numero }}</p>
            <p class="mb-0">{{ lot.artiste }}</p>
            <p class="mb-0">{{ lot.hauteur }} x {{ lot.largeur }} po</p>
          </div>

          <!-- Prix -->
          <div class="d-flex flex-column flex-lg-row align-items-center gap-3 justify-content-center">
            <p class="text-center mb-0">
              Valeur: {{ lot.valeurEstimeMin.toFixed(0) }}$ -
              {{ lot.valeurEstimeMax.toFixed(0) }}$
            </p>
            <p class="text-center mb-0" :class="{ 'mise-recente': isMiseRecente }">
              Mise actuelle: {{ lot.mise.toFixed(0) }}$
            </p>
          </div>
        </div>
      </div>
    </router-link>

    <!-- Zone de mise -->
    <div class="card-footer bg-transparent" @click.stop>
      <!-- Alerte pour les messages -->
      <div v-if="alertMessage" 
           :class="`alert alert-${alertType} alert-dismissible fade show`" 
           role="alert">
        {{ alertMessage }}
        <button type="button" 
                class="btn-close" 
                @click="alertMessage = ''"
                aria-label="Close">
        </button>
      </div>

      <div class="d-flex flex-column align-items-center gap-2">
        <div class="input-group">
          <button 
            class="btn btn-outline-secondary" 
            type="button"
            @click="decrementerMise"
            :disabled="montantMise <= miseMinimale"
          >
            -
          </button>
          <input
            type="number"
            class="form-control text-center"
            v-model="montantMise"
            :min="miseMinimale"
            :step="pasEnchere"
            readonly
          />
          <button 
            class="btn btn-outline-secondary" 
            type="button"
            @click="incrementerMise"
          >
            +
          </button>
          <span class="input-group-text">$</span>
        </div>
        <small class="text-muted">Pas d'enchère: {{ pasEnchere }}$</small>
        <button
          type="button"
          class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond w-100"
          @click="handleMiseClick"
          :disabled="!isMiseValide"
        >
          Miser
        </button>

        <div class="d-flex align-items-center justify-content-center">
          <img
            :src="lot.estLivrable ? '/icons/IconeLivrable.png' : '/icons/IconeNonLivrable.png'"
            height="50"
            width="50"
            :alt="lot.estLivrable ? 'Livrable' : 'Non livrable'"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref, computed, watch } from "vue";
import { useStore } from "vuex";
import { useRouter } from 'vue-router';

const store = useStore();
const router = useRouter();

const props = defineProps({
  lotRecu: Object,
});

const urlApi = ref("/api");
const montantMise = ref(0);
const isMiseRecente = ref(false);
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

// Ajout des refs pour les alertes
const alertMessage = ref('');
const alertType = ref(''); // 'success' ou 'danger'

// Fonction pour afficher une alerte
const showAlert = (message, type = 'success') => {
  alertMessage.value = message;
  alertType.value = type;
  // Optionnel : faire disparaître l'alerte après 3 secondes
  setTimeout(() => {
    alertMessage.value = '';
  }, 3000);
};

// Fonction pour calculer le pas d'enchère en fonction de la valeur estimée
const calculerPasEnchere = (valeur) => {
  if (valeur <= 199.00) return 10.00;
  if (valeur <= 499.00) return 25.00;
  if (valeur <= 999.00) return 50.00;
  if (valeur <= 1999.00) return 100.00;
  if (valeur <= 4999.00) return 200.00;
  if (valeur <= 9999.00) return 250.00;
  if (valeur <= 19999.00) return 500.00;
  if (valeur <= 49999.00) return 1000.00;
  if (valeur <= 99999.00) return 2000.00;
  if (valeur <= 499999.00) return 5000.00;
  return 10000.00;
};

// Le pas d'enchère reste constant car il est basé sur la valeur estimée
const pasEnchere = computed(() => {
  return calculerPasEnchere(lot.value.valeurEstimeMin);
});

// Mise minimale = mise actuelle + pas d'enchère
const miseMinimale = computed(() => {
  return (lot.value.mise || 0) + pasEnchere.value;
});

// Validation de la mise
const isMiseValide = computed(() => {
  return montantMise.value >= miseMinimale.value;
});

const incrementerMise = () => {
  montantMise.value += pasEnchere.value;
};

const decrementerMise = () => {
  const nouveauMontant = montantMise.value - pasEnchere.value;
  if (nouveauMontant >= miseMinimale.value) {
    montantMise.value = nouveauMontant;
  }
};

const handleMiseClick = async () => {
  if (!store.state.isLoggedIn) {
    router.push('/connexion');
    return;
  }

  if (!isMiseValide.value) {
    showAlert(`La mise minimum est de ${miseMinimale.value}$`, 'danger');
    return;
  }

  try {
    // TODO: Implémenter l'appel API pour placer la mise
    console.log(`Nouvelle mise de ${montantMise.value}$ pour le lot ${lot.value.id}`);
    
    // Simuler une mise réussie pour le moment
    lot.value.mise = montantMise.value;
    isMiseRecente.value = true;
    setTimeout(() => {
      isMiseRecente.value = false;
    }, 5000);
    
    showAlert('Mise placée avec succès!', 'success');
  } catch (error) {
    showAlert('Erreur lors de la mise', 'danger');
  }
};

onMounted(async () => {
  console.log("Props reçues:", props.lotRecu); // Debug
  lot.value = props.lotRecu;
  urlApi.value = await store.state.api.defaults.baseURL.replace("/api", "");
  
  // S'assurer que la mise initiale est correctement calculée
  console.log("Valeur estimée min:", lot.value.valeurEstimeMin); // Debug
  console.log("Pas d'enchère:", pasEnchere.value); // Debug
  console.log("Mise minimale:", miseMinimale.value); // Debug
  
  montantMise.value = miseMinimale.value;
});

// Watch pour déboguer les changements
watch(() => props.lotRecu, (newVal) => {
  console.log("Props lotRecu changées:", newVal);
  lot.value = newVal;
  montantMise.value = miseMinimale.value;
}, { deep: true });
</script>

<style scoped>
.mise-recente {
  border: 2px solid #28a745;
  padding: 5px;
  border-radius: 5px;
  animation: highlight 2s ease-in-out;
}

@keyframes highlight {
  0% { background-color: #28a74533; }
  100% { background-color: transparent; }
}

.input-group input[type="number"] {
  text-align: center;
}

.input-group input[type="number"]::-webkit-inner-spin-button,
.input-group input[type="number"]::-webkit-outer-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

.input-group input[type="number"] {
  -moz-appearance: textfield;
}

.alert {
  margin-bottom: 1rem;
  font-size: 0.9rem;
}

/* Animation pour l'apparition/disparition de l'alerte */
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.3s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
