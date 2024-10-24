<template>
  <div class="d-flex flex-column align-items-center pb-3">
    <h1>Encan {{ numeroEncan }}</h1>

    <div class="d-flex gap-2" v-if="chargement">
      <div class="spinner-border" role="status">
        <span class="visually-hidden">Chargement des encans...</span>
      </div>
      <p>Chargement de l'encan en cours...</p>
    </div>

    <h5 class="text-center" v-if="encan == '' && !chargement">
      Il n'y a présentement aucun encan ayant ce numéro
    </h5>

    <div v-else>
      <p class="text-center">
        Date de debut de la soirée de cloture: {{ soireeDate }}
      </p>
      <AffichageLots />
    </div>
  </div>
</template>

<script setup>
import AffichageLots from "@/components/views/AffichageLots.vue";
import { onMounted, ref } from "vue";
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
const dateDebut = ref();
const dateFin = ref();
const soireeDate = ref();

const props = defineProps({
  numeroEncan: String,
});

onMounted(async () => {
  const numero = props.numeroEncan;
  const response = await store.dispatch("chercherEncanParNumero", numero);
  chargement.value = false;

  encan.value = response.data;

  let affiche2Annees = function () {
    let anneeFin = encan.dateFin.split("-")[0];
    let anneeDebut = encan.dateDebut.split("-")[0];

    if (anneeDebut == anneeFin) {
      return false;
    }
    return true;
  };
  dateDebut.value = formatageDate(encan.dateDebut, affiche2Annees, false);
  dateFin.value = formatageDate(encan.dateFin, true, false);
  soireeDate.value = formatageDate(
    encan.dateDebutSoireeCloture,
    affiche2Annees,
    true
  );
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
