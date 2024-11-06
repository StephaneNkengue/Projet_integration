<template>
  <div
    class="modal fade"
    :id="`modalMise_${lot.id}`"
    tabindex="-1"
    :aria-labelledby="`modalMiseLabel_${lot.id}`"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" :id="`modalMiseLabel_${lot.id}`">
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
            <p class="fs-5 mb-0">lot {{ lot.numero }}</p>
            <p class="fs-5 mb-0">votre mise : {{ userHasBid ? lot.mise.toFixed(0) + '$' : 'aucune mise' }}</p>
            
            <div class="d-flex flex-column gap-2">
              <p class="mb-0">Ma mise maximale à ne pas dépasser :</p>
              <div class="d-flex align-items-center gap-2">
                <button class="btn btn-outline-secondary" @click="decrementerMise" :disabled="montantMise <= getMiseMinimale">
                  -{{ pasEnchere }}$
                </button>
                <span class="fs-5">{{ montantMise }}$</span>
                <button class="btn btn-outline-secondary" @click="incrementerMise">
                  +{{ pasEnchere }}$
                </button>
              </div>
              <p class="text-muted small">Ajouter le montant maximum souhaité</p>
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
import { ref, computed, onMounted, h } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import { toast } from 'vue3-toastify';
import ToastContent from '../Toast/toastConfirm.vue';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';

const props = defineProps({
  lot: {
    type: Object,
    required: true,
  },
});

const store = useStore();
const router = useRouter();
const montantMise = ref(0);
let modalInstance = null;

const userHasBid = computed(() => {
  return store.getters.hasUserBidOnLot(props.lot.id);
});

const getMiseInitiale = computed(() => {
  return props.lot.mise ? props.lot.mise : props.lot.prixOuverture;
});

// Fonction pour calculer le pas d'enchère
const calculerPasEnchere = (valeur) => {
  if (valeur <= 199.0) return 10.0;
  if (valeur <= 499.0) return 25.0;
  if (valeur <= 999.0) return 50.0;
  if (valeur <= 1999.0) return 100.0;
  if (valeur <= 4999.0) return 200.0;
  if (valeur <= 9999.0) return 250.0;
  if (valeur <= 19999.0) return 500.0;
  if (valeur <= 49999.0) return 1000.0;
  if (valeur <= 99999.0) return 2000.0;
  if (valeur <= 499999.0) return 5000.0;
  return 10000.0;
};

const pasEnchere = computed(() => {
  return calculerPasEnchere(props.lot.valeurEstimeMin);
});

const getMiseMinimale = computed(() => {
  // Si la mise actuelle est 0, utiliser le prix d'ouverture
  const miseBase = props.lot.mise > 0 ? props.lot.mise : props.lot.prixOuverture;
  return miseBase + pasEnchere.value;
});

const isMiseValide = computed(() => {
  return montantMise.value >= getMiseMinimale.value;
});

const incrementerMise = () => {
  montantMise.value += pasEnchere.value;
};

const decrementerMise = () => {
  const nouveauMontant = montantMise.value - pasEnchere.value;
  if (nouveauMontant >= getMiseMinimale.value) {
    montantMise.value = nouveauMontant;
  }
};

const confirmerMise = async () => {
  if (!store.state.isLoggedIn) {
    router.push('/connexion');
    return;
  }

  const response = await store.dispatch('placerMise', {
    idLot: props.lot.id,
    montant: montantMise.value
  });

  if (response.success) {
    // Remplacer l'alert par un toast
    toast.success(
      h(ToastContent, {
        title: "Mise placée avec succès !",
        description: `Votre mise de ${montantMise.value}$ a été enregistrée pour le lot ${props.lot.numero}`,
      }),
      {
        position: toast.POSITION.TOP_CENTER,
        autoClose: 3000,
        pauseOnFocusLoss: false,
        theme: "dark",
        toastStyle: {
          backgroundColor: "#052445",
          color: "#fff",
        },
      }
    );

    const modalElement = document.getElementById(`modalMise_${props.lot.id}`);
    const modal = bootstrap.Modal.getInstance(modalElement);
    modal.hide();
    
    // Émettre l'événement pour mettre à jour le parent
    emit('miseConfirmee', montantMise.value);
  } else {
    // Gérer l'erreur avec un toast d'erreur
    toast.error(
      h(ToastContent, {
        title: "Erreur",
        description: response.message || 'Erreur lors de la mise',
      }),
      {
        position: toast.POSITION.TOP_CENTER,
        autoClose: 3000,
        pauseOnFocusLoss: false,
        theme: "dark",
        toastStyle: {
          backgroundColor: "#dc3545",
          color: "#fff",
        },
      }
    );
  }
};

onMounted(() => {
  const modalElement = document.getElementById(`modalMise_${props.lot.id}`);
  modalInstance = new bootstrap.Modal(modalElement);
  // Initialiser avec la mise minimale calculée
  montantMise.value = getMiseMinimale.value;
});

defineExpose({
  show: () => {
    montantMise.value = getMiseMinimale.value;
    modalInstance.show();
  },
  hide: () => modalInstance.hide(),
});
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
