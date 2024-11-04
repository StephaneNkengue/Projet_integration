<template>
  <div class="modal fade" :id="`modalMise_${lot.id}`" tabindex="-1" :aria-labelledby="`modalMiseLabel_${lot.id}`" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" :id="`modalMiseLabel_${lot.id}`">Êtes-vous sûr de faire cette mise?</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <div class="d-flex flex-column gap-2">
            <p class="mb-0">valeur : {{ lot.valeurEstimeMin }}$ - {{ lot.valeurEstimeMax }}$</p>
            <p class="mb-0">mise actuelle : {{ lot.mise }}$</p>
            <div class="d-flex align-items-center gap-2">
              <span>votre mise :</span>
              <button class="btn btn-outline-secondary" @click="decrementerMise" :disabled="montantMise <= miseMinimale">
                -{{ pasEnchere }}$
              </button>
              <span class="fs-5">{{ montantMise }}$</span>
              <button class="btn btn-outline-secondary" @click="incrementerMise">
                +{{ pasEnchere }}$
              </button>
            </div>
          </div>
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Annuler</button>
          <button type="button" class="btn btn-primary" @click="confirmerMise" :disabled="!isMiseValide">
            Miser
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';

const props = defineProps({
  lot: {
    type: Object,
    required: true
  }
});

const emit = defineEmits(['miseConfirmee']);

const store = useStore();
const router = useRouter();
const montantMise = ref(0);
let modalInstance = null;

// Fonction pour calculer le pas d'enchère
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

const pasEnchere = computed(() => {
  return calculerPasEnchere(props.lot.valeurEstimeMin);
});

const miseMinimale = computed(() => {
  return (props.lot.mise || 0) + pasEnchere.value;
});

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

const confirmerMise = async () => {
  if (!store.state.isLoggedIn) {
    const modalElement = document.getElementById(`modalMise_${props.lot.id}`);
    const modal = bootstrap.Modal.getInstance(modalElement);
    modal.hide();
    
    // Créer et afficher la modal d'erreur de connexion
    const modalErreurElement = document.createElement('div');
    modalErreurElement.innerHTML = `
      <div class="modal fade" id="modalErreurConnexion" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title">Connexion requise</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <p>Veuillez vous connecter pour miser !</p>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-primary" data-bs-dismiss="modal" id="btnRedirigerConnexion">
                Se connecter
              </button>
            </div>
          </div>
        </div>
      </div>
    `;
    document.body.appendChild(modalErreurElement);

    const modalErreur = new bootstrap.Modal(document.getElementById('modalErreurConnexion'));
    modalErreur.show();

    document.getElementById('btnRedirigerConnexion').addEventListener('click', () => {
      router.push('/connexion');
    });

    document.getElementById('modalErreurConnexion').addEventListener('hidden.bs.modal', () => {
      document.body.removeChild(modalErreurElement);
    });
    
    return;
  }

  const response = await store.dispatch('placerMise', {
    idLot: props.lot.id,
    montant: montantMise.value
  });

  if (response.success) {
    emit('miseConfirmee', montantMise.value);
    const modalElement = document.getElementById(`modalMise_${props.lot.id}`);
    const modal = bootstrap.Modal.getInstance(modalElement);
    modal.hide();
  } else {
    // Afficher un message d'erreur à l'utilisateur
    alert(response.message || 'Erreur lors de la mise');
  }
};

onMounted(() => {
  const modalElement = document.getElementById(`modalMise_${props.lot.id}`);
  modalInstance = new bootstrap.Modal(modalElement);

  modalElement.addEventListener('hidden.bs.modal', () => {
    montantMise.value = miseMinimale.value;
  });

  montantMise.value = miseMinimale.value;
});

// Ajouter un watch sur les props
watch(() => props.lot, (newLot) => {
  console.log("Lot mis à jour:", newLot);
  // Réinitialiser le montant de mise quand le lot change
  if (modalInstance) {
    montantMise.value = miseMinimale.value;
  }
}, { deep: true });

defineExpose({
  show: () => {
    montantMise.value = miseMinimale.value; // Réinitialiser la mise à l'ouverture
    modalInstance.show();
  },
  hide: () => modalInstance.hide()
});
</script>

<style scoped>
.modal-body {
  padding: 1.5rem;
}

.modal-body p {
  margin-bottom: 0.5rem;
}

/* Alignement des éléments */
.modal-body .d-flex {
  align-items: center;
}

/* Espacement cohérent */
.gap-2 {
  gap: 0.75rem !important;
}
</style> 