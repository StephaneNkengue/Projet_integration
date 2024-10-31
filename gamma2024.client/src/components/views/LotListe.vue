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
import { onMounted, ref, computed } from "vue";
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

// Fonction pour calculer le pas d'enchère en fonction du montant
const calculerPasEnchere = (montant) => {
  if (montant <= 199.00) return 10.00;
  if (montant <= 499.00) return 25.00;
  if (montant <= 999.00) return 50.00;
  if (montant <= 1999.00) return 100.00;
  if (montant <= 4999.00) return 200.00;
  if (montant <= 9999.00) return 250.00;
  if (montant <= 19999.00) return 500.00;
  if (montant <= 49999.00) return 1000.00;
  if (montant <= 99999.00) return 2000.00;
  if (montant <= 499999.00) return 5000.00;
  return 10000.00;
};

// Calcul du pas d'enchère actuel
const pasEnchere = computed(() => {
  return calculerPasEnchere(montantMise.value);
});

// Mise minimale = mise actuelle + pas d'enchère initial
const miseMinimale = computed(() => {
  return lot.value.mise + calculerPasEnchere(lot.value.mise);
});

// Fonctions d'incrémentation/décrémentation
const incrementerMise = () => {
  montantMise.value += calculerPasEnchere(montantMise.value);
};

const decrementerMise = () => {
  const nouveauMontant = montantMise.value - calculerPasEnchere(montantMise.value - 0.01);
  if (nouveauMontant >= miseMinimale.value) {
    montantMise.value = nouveauMontant;
  }
};

onMounted(async () => {
  lot.value = props.lotRecu;
  urlApi.value = await store.state.api.defaults.baseURL.replace("\api", "");
  // Initialiser le montant de mise au minimum requis
  montantMise.value = miseMinimale.value;
});
</script>

<style scoped>
/* ... styles existants ... */

.input-group input[type="number"] {
  text-align: center;
}

/* Désactiver les flèches du input number */
.input-group input[type="number"]::-webkit-inner-spin-button,
.input-group input[type="number"]::-webkit-outer-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

.input-group input[type="number"] {
  -moz-appearance: textfield;
}
</style>
