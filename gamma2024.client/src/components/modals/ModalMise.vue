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
            <p class="fs-5 mb-0">
              votre mise :
              {{ userHasBid ? lot.mise.toFixed(0) + "$" : "aucune mise" }}
            </p>

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
import { ref, computed, onMounted } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";

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
  const miseBase =
    props.lot.mise > 0 ? props.lot.mise : props.lot.prixOuverture;
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
    const modalElement = document.getElementById(`modalMise_${props.lot.id}`);
    const modal = bootstrap.Modal.getInstance(modalElement);
    modal.hide();

    // Créer et afficher la modal d'erreur de connexion
    const modalErreurElement = document.createElement("div");
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

    const modalErreur = new bootstrap.Modal(
      document.getElementById("modalErreurConnexion")
    );
    modalErreur.show();

    document
      .getElementById("btnRedirigerConnexion")
      .addEventListener("click", () => {
        router.push("/connexion");
      });

    document
      .getElementById("modalErreurConnexion")
      .addEventListener("hidden.bs.modal", () => {
        document.body.removeChild(modalErreurElement);
      });

    return;
  }

  const response = await store.dispatch("placerMise", {
    idLot: props.lot.id,
    montant: montantMise.value,
  });

  if (response.success) {
    // Afficher un message de confirmation à l'utilisateur
    alert("Mise confirmée avec succès !");
    const modalElement = document.getElementById(`modalMise_${props.lot.id}`);
    const modal = bootstrap.Modal.getInstance(modalElement);
    modal.hide();
  } else {
    // Afficher un message d'erreur à l'utilisateur
    alert(response.message || "Erreur lors de la mise");
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
