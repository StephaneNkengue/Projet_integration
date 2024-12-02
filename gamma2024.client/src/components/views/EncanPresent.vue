<template>
  <div class="d-flex flex-column align-items-center pb-3">
    <div class="d-flex gap-2" v-if="chargement">
      <div class="spinner-border" role="status">
        <span class="visually-hidden">Chargement des encans...</span>
      </div>
      <p>Chargement de l'encan en cours...</p>
    </div>

    <div v-if="!chargement" class="w-100">
      <div v-if="type === 'aucun'">
        <h5 class="text-center">
          Il n'y a présentement aucun encan en cours
        </h5>
      </div>

      <div v-else-if="type === 'courant'">
        <h1>
          Encan en cours <span v-if="encan != ''">({{ encan.numeroEncan }})</span>
        </h1>
        <p class="text-center">
          Date de début de la soirée de clotûre: {{ soireeDate }}
        </p>
        <AffichageLots :idEncan="encan.id" />
      </div>

      <div v-else-if="type === 'soireeCloture'">
        <SoireeCloture :encan="encan" />
      </div>
    </div>
  </div>
</template>

<script setup>
import AffichageLots from "@/components/views/AffichageLots.vue";
import SoireeCloture from "@/components/views/SoireeCloture.vue";
import { onMounted, ref, onUnmounted } from "vue";
import { useStore } from "vuex";

let mois = [
  "janvier",
  "février",
  "mars",
  "avril",
  "mai",
  "juin",
  "juillet",
  "août",
  "septembre",
  "octobre",
  "novembre",
  "décembre",
];

const store = useStore();
const chargement = ref(true);
const encan = ref("");
const soireeDate = ref();
const type = ref(null);

// Vérification périodique de l'état
const interval = ref(null);

const verifierEtat = async () => {
  const etatType = await store.dispatch('verifierEtatEncan')
  type.value = etatType
  encan.value = store.state.encanCourant
  if (encan.value != "") {
    soireeDate.value = formatageDate(
      encan.value.dateDebutSoireeCloture,
      true,
      true
    );
  }
}

onMounted(async () => {
  await verifierEtat();
  // Démarrer la surveillance des transitions
  await store.dispatch('surveillerTransitionEncan')
  interval.value = setInterval(verifierEtat, 30000);
  chargement.value = false;
});

onUnmounted(() => {
  if (interval.value) {
    clearInterval(interval.value);
  }
});

function formatageDate(dateTexte, siAnnee, siHeure) {
  let sep = dateTexte.split("T");
  let dates = sep[0].split("-");
  let temps = sep[1].split(":");

  let resultat = dates[2] + " " + mois[dates[1] - 1];

  if (siAnnee) {
    resultat += " " + dates[0];
  }
  if (siHeure) {
    resultat += " à " + parseInt(temps[0]) + "h" + temps[1];
  }
  return resultat;
}
</script>

<style scoped></style>
