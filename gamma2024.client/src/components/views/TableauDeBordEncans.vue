<template>
  <div class="d-flex flex-column justify-content-between">
    <div class="d-flex justify-content-between">
      <h2 class="d-flex-1">Liste des encans</h2>
      <router-link to="TableauDeBordEncansAjout" class="text-decoration-none">
        <button
          class="btn bleuMarinSecondaireFond btnSurvolerBleuMoyenFond btnClick text-white d-flex-1"
          type="button"
          id="ajouterEncanButton"
        >
          Ajouter un encan
        </button>
      </router-link>
    </div>

    <div class="d-flex justify-content-between">
      <div class="d-flex collapse dropdown dropdown-center">
        <button
          class="btn dropdown-toggle bleuMarinSecondaireFond rounded text-white contenuListeDropdown"
          type="button"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          Données par page
        </button>

        <ul class="dropdown-menu dropdown-menu-dark bleuMarinFond text-center">
          <li>
            <a class="dropdown-item">10</a>
          </li>
          <li>
            <a class="dropdown-item">25</a>
          </li>
          <li>
            <a class="dropdown-item">Tous</a>
          </li>
        </ul>
      </div>

      <div class="d-flex me-1 gap-1 align-items-center">
        <label for="Recherche">Rechercher : </label>
        <input
          data-bs-theme="light"
          type="search"
          aria-label="Recherche"
          v-model="encanRecherche"
        />
      </div>
    </div>

    <table class="table table-striped mt-3">
      <thead>
        <tr>
          <th data-field="numeroEncan">Encan</th>
          <th data-field="statut">Statut</th>
          <th data-field="dateDebut">Date de début</th>
          <th data-field="dateFin">Date de fin</th>
          <th data-field="soireeCloture">Soirée de clôture</th>
          <th data-field="nbLot">Nombre de lots</th>
          <th>Actions</th>
        </tr>
      </thead>
      <tr v-for="encan in listeEncansFiltree" :key="encan.id">
        <td>{{ encan.numeroEncan }}</td>
        <td class="d-flex justify-content-center">
          <div class="d-flex collapse dropdown dropdown-center">
            <button
              :encanId="encan.id"
              class="btn dropdown-toggle bleuMarinSecondaireFond rounded text-white contenuListeDropdown fs-7"
              type="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              <span v-if="encan.estPublie == true">Publié</span>
              <span v-else>Non publié</span>
            </button>

            <ul
              class="dropdown-menu dropdown-menu-dark bleuMarinFond text-center"
            >
              <li>
                <a
                  class="dropdown-item"
                  @click="encanPublieMAJ(true)"
                  :encanId="encan.id"
                  >Publié</a
                >
              </li>
              <li>
                <a
                  class="dropdown-item"
                  @click="encanPublieMAJ(false)"
                  :encanId="encan.id"
                  >Non publié</a
                >
              </li>
            </ul>
          </div>
        </td>
        <td>{{ dateDebutJour }}-{{ dateDebut[1] }}-{{ dateDebut[0] }}</td>
        <td>{{ dateFinJour }}-{{ dateFin[1] }}-{{ dateFin[0] }}</td>
        <td>
          {{ dateDebutSoireeClotureJour }}-{{ dateDebutSoireeCloture[1] }}-{{
            dateDebutSoireeCloture[0]
          }}
          {{ dateDebutSoireeClotureHeure }} à {{ dateFinSoireeClotureHeure }}
        </td>
        <td>vfffd</td>
        <td>
          <span>
            <button
              class="btn btn-info"
              @click="editerEncan(encan.numeroEncan)"
            >
              <img
                src="/public/icons/edit.png"
                class="img-fluid"
                alt="..."
              /></button
          ></span>
          <span>
            <button
              class="btn btn-danger"
              data-bs-toggle="modal"
              :data-bs-target="'#' + encan.numeroEncan"
            >
              <img
                src="/public/icons/delete.png"
                class="img-fluid"
                alt="..."
              /></button
          ></span>
        </td>
        <ConfirmDelete :h="encan" @supprimerEncan="supprimerMonEncan" />
      </tr>
    </table>
  </div>
</template>
<script setup>
import { onMounted, ref, watch } from "vue";
import { useStore } from "vuex";
import TableauDeBordEncansAjout from "@/components/views/TableauDeBordEncansAjout.vue";
import ConfirmDelete from "./BoiteModale/ConfirmDelete.vue";

const store = useStore();
const listeEncans = ref([]);
const listeEncansFiltree = ref([]);
const dateDebut = ref("");
const dateDebutJour = ref("");
const dateFin = ref("");
const dateFinJour = ref("");
const dateDebutSoireeCloture = ref("");
const dateDebutSoireeClotureJour = ref("");
const dateDebutSoireeClotureHeure = ref("");
const dateFinSoireeCloture = ref("");
const dateFinSoireeClotureHeure = ref("");

const encanPublieMAJ = ref();
const encanRecherche = ref();

onMounted(async () => {
  initializeData();
});

watch(encanRecherche, () => {
  listeEncansFiltree.value = listeEncans.value;

  listeEncansFiltree.value = listeEncansFiltree.value.filter(
    ({
      numeroEncan,
      dateDebut,
      dateFin,
      dateDebutSoireeCloture,
      dateFinSoireeCloture,
    }) =>
      numeroEncan.toString().startsWith(encanRecherche.value) ||
      dateDebut.toString().startsWith(encanRecherche.value) ||
      dateFin.toString().startsWith(encanRecherche.value) ||
      dateDebutSoireeCloture.toString().startsWith(encanRecherche) ||
      dateFinSoireeCloture.toString().startsWith(encanRecherche.value)
  );
});

const supprimerMonEncan = async (idEncan) => {
  await store.dispatch("supprimerUnEncan", idEncan);
  initializeData();
};

async function initializeData() {
  try {
    listeEncans.value = await store.dispatch("fetchEncanInfo");

    listeEncansFiltree.value = listeEncans.value;

    listeEncansFiltree.value.forEach((element) => {
      dateDebut.value = element.dateDebut.toString().split("-");
      dateFin.value = element.dateFin.toString().split("-");
      dateDebutSoireeCloture.value = element.dateDebutSoireeCloture
        .toString()
        .split("-");
      dateFinSoireeCloture.value = element.dateFinSoireeCloture
        .toString()
        .split("-");

      dateDebutJour.value = dateDebut.value[2].substring(0, 2);
      dateFinJour.value = dateFin.value[2].substring(0, 2);
      dateDebutSoireeClotureJour.value =
        dateDebutSoireeCloture.value[2].substring(0, 2);

      dateDebutSoireeClotureHeure.value =
        dateDebutSoireeCloture.value[2].substring(3, 8);
      dateFinSoireeClotureHeure.value = dateFinSoireeCloture.value[2].substring(
        3,
        8
      );
    });

    encanPublieMAJ.value = function (statutPublie) {
      let encanId = event.srcElement.getAttribute("encanId");
      let encan = listeEncans.value.find((e) => e.id == encanId);
      if (encan.estPublie != statutPublie) {
        encan.estPublie = !encan.estPublie;
      }
    };
  } catch (erreur) {
    console.log("Erreur encans" + erreur);
  }
}
</script>

<style scoped>
.dropdown-toggle[aria-expanded="true"] {
  background-color: #5a708a;
}

.dropdown-item:active {
  background-color: #5a708a;
}

img {
  width: 25px;
  height: 30px;
}
</style>
