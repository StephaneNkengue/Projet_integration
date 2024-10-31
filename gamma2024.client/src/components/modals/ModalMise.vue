<template>
  <div class="modal fade" id="modalMise" tabindex="-1" aria-labelledby="modalMiseLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title" id="modalMiseLabel">Êtes-vous sûr de faire cette mise?</h5>
          <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
        </div>
        <div class="modal-body">
          <p>valeur : {{ lot.valeurEstimeMin }}$ - {{ lot.valeurEstimeMax }}$</p>
          <p>mise actuelle : {{ lot.mise }}$</p>
          <div class="d-flex justify-content-center align-items-center gap-2">
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
import { ref, computed, onMounted } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import { Modal } from 'bootstrap';

const props = defineProps({
  lot: {
    type: Object,
    required: true
  }
});

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
    modalInstance.hide();
    router.push('/connexion');
    return;
  }

  try {
    // TODO: Implémenter l'appel API pour placer la mise
    console.log(`Nouvelle mise de ${montantMise.value}$ pour le lot ${props.lot.id}`);
    modalInstance.hide();
    // Émettre un événement pour informer le parent que la mise a été placée
    emit('miseConfirmee', montantMise.value);
  } catch (error) {
    console.error('Erreur lors de la mise:', error);
  }
};

onMounted(() => {
  modalInstance = new Modal(document.getElementById('modalMise'));
  montantMise.value = miseMinimale.value;
});

defineExpose({
  show: () => modalInstance.show()
});
</script> 