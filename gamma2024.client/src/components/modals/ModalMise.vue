<template>
  <div
    class="modal fade"
    :id="`modalMise_${lot?.id}`"
    tabindex="-1"
    :aria-labelledby="`modalMiseLabel_${lot?.id}`"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" :id="`modalMiseLabel_${lot?.id}`">
            Êtes-vous sûr de faire cette mise?
          </h5>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
            aria-label="Close"
          ></button>
        </div>
        <div class="modal-body">
          <div class="d-flex flex-column gap-3">
            <p class="fs-5 mb-0">lot {{ lot?.numero }}</p>
            <p class="fs-5 mb-0">votre mise : {{ affichageMiseActuelle }}</p>

            <div class="d-flex flex-column gap-2">
              <p class="mb-0">Ma mise maximale à ne pas dépasser :</p>
              <div class="d-flex align-items-center gap-2">
                <button
                  class="btn btn-outline-secondary"
                  @click="decrementerMise"
                  :disabled="montantMise <= getMiseMinimale"
                >
                  -{{ pasEnchere }}$
                </button>
                <span class="fs-5">{{ montantMise }}$</span>
                <button
                  class="btn btn-outline-secondary"
                  @click="incrementerMise"
                >
                  +{{ pasEnchere }}$
                </button>
              </div>
              <p class="text-muted small">
                Ajouter le montant maximum souhaité
              </p>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary"
            data-bs-dismiss="modal"
          >
            Annuler
          </button>
          <button
            type="button"
            class="btn btn-primary"
            @click="confirmerMise"
            :disabled="!isMiseValide"
          >
            Miser
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, watch, onMounted, computed, nextTick } from 'vue';
import { Modal } from 'bootstrap';
import { useStore } from 'vuex';
import { toast } from 'vue3-toastify';

const store = useStore();
const modalInstance = ref(null);
const montantMise = ref(0);
const montantInitial = ref(0);

const props = defineProps({
  lot: {
    type: Object,
    required: true
  }
});

// Fonction pour calculer le pas d'enchère selon le montant
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

// Computed pour le pas d'enchère actuel
const pasEnchere = computed(() => {
  return calculerPasEnchere(montantMise.value);
});

// Computed pour la mise minimale
const getMiseMinimale = computed(() => {
  const miseActuelle = props.lot?.mise || 0;
  const prixOuverture = props.lot?.prixOuverture || 0;
  const montantBase = miseActuelle || prixOuverture;
  return montantBase + calculerPasEnchere(montantBase);
});

// Computed pour valider la mise
const isMiseValide = computed(() => {
  return montantMise.value >= getMiseMinimale.value;
});

// Calcul du montant initial basé sur le lot
const calculerMontantInitial = (lot) => {
  if (!lot) return 0;
  return lot.mise || lot.prixOuverture || 0;
};

// Confirmer la mise
const confirmerMise = async () => {
  try {
    const response = await store.dispatch("placerMise", {
      idLot: props.lot.id,
      montant: montantMise.value,
    });

    if (response.success) {
      modalInstance.value.hide();
      emit("miseConfirmee", montantMise.value);
      
      toast.success("Votre mise a été placée avec succès", {
        position: toast.POSITION.TOP_RIGHT,
        autoClose: 3000
      });
    }
  } catch (error) {
    toast.error(error.message || "Erreur lors de la mise", {
      position: toast.POSITION.TOP_RIGHT,
      autoClose: 5000
    });
  }
};

// Initialisation du montant de la mise
watch(() => props.lot, (newLot) => {
  if (newLot) {
    montantInitial.value = calculerMontantInitial(newLot);
    const pasInitial = calculerPasEnchere(montantInitial.value);
    montantMise.value = montantInitial.value + pasInitial;
  }
}, { immediate: true, deep: true });

// Gestion du modal
onMounted(() => {
  nextTick(() => {
    const modalElement = document.getElementById(`modalMise_${props.lot?.id}`);
    if (modalElement) {
      modalInstance.value = new Modal(modalElement, {
        backdrop: 'static',
        keyboard: false
      });
    }
  });
});

// Méthodes pour la mise modifiées
const incrementerMise = () => {
  const nouveauMontant = montantMise.value + pasEnchere.value;
  montantMise.value = nouveauMontant;
};

const decrementerMise = () => {
  const pasActuel = calculerPasEnchere(montantMise.value);
  const nouveauMontant = montantMise.value - pasActuel;
  if (nouveauMontant >= getMiseMinimale.value) {
    montantMise.value = nouveauMontant;
  }
};

// Méthodes du modal
const show = () => {
  if (modalInstance.value) {
    const montantBase = calculerMontantInitial(props.lot);
    const pasInitial = calculerPasEnchere(montantBase);
    montantMise.value = montantBase + pasInitial;
    modalInstance.value.show();
  }
};

const hide = () => {
  if (modalInstance.value) {
    modalInstance.value.hide();
  }
};

// Computed properties
const affichageMiseActuelle = computed(() => {
  const lot = store.getters.getLot(props.lot?.id);
  return lot?.mise ? `${lot.mise}$` : 'aucune mise';
});

const hasUserBidOnLot = computed(() => {
  return store.getters.hasUserBidOnLot(props.lot?.id);
});

// Expose les méthodes nécessaires
defineExpose({
  show,
  hide
});

// Définir les émissions
const emit = defineEmits(['miseConfirmee']);
</script>

<style scoped>
.modal-body {
  padding: 1.5rem;
}

.gap-3 {
  gap: 1rem !important;
}

.gap-2 {
  gap: 0.75rem !important;
}
</style>
