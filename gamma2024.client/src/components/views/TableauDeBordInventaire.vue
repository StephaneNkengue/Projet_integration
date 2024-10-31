<template>
  <div class="d-flex justify-content-between">
    <div v-if="messageConfirmation" class="alert alert-success">
      {{ messageConfirmation }}
    </div>
    <h2 class="d-flex-1">Liste des lots</h2>
    <div class="d-flex d-flex-1 align-items-end">
      <button
        class="bleuMoyenFond btnSurvolerBleuMoyenFond boutonPersonnalise text-white d-flex-1"
        type="button"
        id="ajouterLotButton"
        @click="redirigerVersCreationLot"
      >
        Ajouter un lot
      </button>
      <div class="dropdown d-flex-1">
        <button
          class="bleuMoyenFond btnSurvolerBleuMoyenFond boutonPersonnalise text-white dropdown-toggle"
          type="button"
          id="dropdownMenuButton"
          data-toggle="dropdown"
          data-bs-toggle="dropdown"
          aria-haspopup="true"
          aria-expanded="false"
        >
          Sélectionner les colonnes
        </button>
        <ul class="dropdown-menu">
          <li class="d-flex justify-content-start dropdown-item">
            <input
              class="checkboxTous d-flex-1"
              type="checkbox"
              id="tousSelectionnerCheckbox"
              :checked="Object.values(colonnesVisibles).every((v) => v)"
              @change="toggleToutesColonnes"
            />
            <label class="d-flex-1 labelpadding" for="tousSelectionnerCheckbox">
              Tous Sélectionner
            </label>
          </li>
          <li
            v-for="(visible, colonne) in colonnesVisibles"
            :key="colonne"
            class="d-flex justify-content-start dropdown-item"
          >
            <input
              class="checkboxSeul d-flex-1"
              type="checkbox"
              :id="`lot${
                colonne.charAt(0).toUpperCase() + colonne.slice(1)
              }Checkbox`"
              :checked="visible"
              @change="toggleColonne(colonne)"
            />
            <label
              class="d-flex-1 labelpadding"
              :for="`lot${
                colonne.charAt(0).toUpperCase() + colonne.slice(1)
              }Checkbox`"
            >
              {{ colonne.charAt(0).toUpperCase() + colonne.slice(1) }}
            </label>
          </li>
        </ul>
      </div>
      <div class="d-flex d-flex-1 me-1 gap-1 align-items-center">
        <label for="Recherche">Rechercher : </label>
        <input
          data-bs-theme="light"
          type="search"
          aria-label="Recherche"
          v-model="rechercheDansListeDeLot"
        />
        <div class="dropdown d-flex-1">
          <button
            class="bleuMoyenFond btnSurvolerBleuMoyenFond boutonPersonnalise text-white dropdown-toggle"
            type="button"
            id="dropdownMenuButton"
            data-toggle="dropdown"
            data-bs-toggle="dropdown"
            aria-haspopup="true"
            aria-expanded="false"
          >
            Sélectionner les colonnes de recherche
          </button>
          <ul class="dropdown-menu">
            <li class="d-flex justify-content-start dropdown-item">
              <input
                class="checkboxTousRecherche d-flex-1"
                type="checkbox"
                id="tousSelectionnerRechercheCheckbox"
                :checked="Object.values(colonnesRecherche).every((v) => v)"
                @change="toggleToutesColonnesRecherche"
              />
              <label
                class="d-flex-1 labelpadding"
                for="tousSelectionnerRechercheCheckbox"
              >
                Tous Sélectionner
              </label>
            </li>
            <li
              v-for="(visible, colonne) in colonnesRecherche"
              :key="colonne"
              class="d-flex justify-content-start dropdown-item"
            >
              <input
                class="checkboxSeulRecherche d-flex-1"
                type="checkbox"
                :id="`recherche${
                  colonne.charAt(0).toUpperCase() + colonne.slice(1)
                }Checkbox`"
                :checked="visible"
                @change="toggleColonneRecherche(colonne)"
              />
              <label
                class="d-flex-1 labelpadding"
                :for="`recherche${
                  colonne.charAt(0).toUpperCase() + colonne.slice(1)
                }Checkbox`"
              >
                {{ colonne.charAt(0).toUpperCase() + colonne.slice(1) }}
              </label>
            </li>
          </ul>
        </div>
      </div>
      <div class="d-flex-1">
        <div class="d-flex flex-row-reverse w-100 px-4 me-2 gap-2">
          <button
            class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond"
            type="button"
            @click="afficherTousLots"
            v-bind:disabled="lotsParPage == nbLotsRecus"
          >
            Tous
          </button>
          <button
            class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond"
            type="button"
            @click="afficherLotsParPage(20)"
            v-bind:disabled="lotsParPage == 20"
          >
            20
          </button>
          <button
            class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond"
            type="button"
            @click="afficherLotsParPage(50)"
            v-bind:disabled="lotsParPage == 50"
          >
            50
          </button>
          <button
            class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond"
            type="button"
            @click="afficherLotsParPage(100)"
            v-bind:disabled="lotsParPage == 100"
          >
            100
          </button>
        </div>
      </div>
    </div>
  </div>
  <div class="margesPourTable">
    <table class="table table-striped mt-5">
      <colgroup id="colgroup">
        <col id="colEncan" />
        <col id="colNumero" />
        <col id="colPrixOuverture" />
        <col id="colValeurMinPourVente" />
        <col id="colEstimationMin" />
        <col id="colEstimationMax" />
        <col id="colCategorie" />
        <col id="colArtiste" />
        <col id="colDimensions" />
        <col id="colDescription" />
        <col id="colMedium" />
        <col id="colVendeur" />
        <col id="colVendu" />
        <col id="colLivraison" />
        <col id="colModifier" />
        <col id="colSupprimer" />
      </colgroup>
      <thead>
        <tr>
          <th v-if="colonnesVisibles.encan">Encan</th>
          <th v-if="colonnesVisibles.numero">Lot #</th>
          <th v-if="colonnesVisibles.prixOuverture">Prix Ouverture</th>
          <th v-if="colonnesVisibles.valeurMinPourVente">
            Valeur Min Pour Vente
          </th>
          <th v-if="colonnesVisibles.estimationMin">Estimation Min</th>
          <th v-if="colonnesVisibles.estimationMax">Estimation Max</th>
          <th v-if="colonnesVisibles.categorie">Catégorie</th>
          <th v-if="colonnesVisibles.artiste">Artiste</th>
          <th v-if="colonnesVisibles.dimension">Dimension (en po)</th>
          <th v-if="colonnesVisibles.description">Description</th>
          <th v-if="colonnesVisibles.medium">Medium</th>
          <th v-if="colonnesVisibles.vendeur">Vendeur</th>
          <th v-if="colonnesVisibles.estVendu">Vendu</th>
          <th v-if="colonnesVisibles.livraison">Livraison</th>
          <th v-if="colonnesVisibles.modifier"></th>
          <th v-if="colonnesVisibles.supprimer"></th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="lot in lotsAffiches" :key="lot.id">
          <td v-if="colonnesVisibles.encan">{{ lot.numeroEncan }}</td>
          <td v-if="colonnesVisibles.numero">{{ lot.code }}</td>
          <td v-if="colonnesVisibles.prixOuverture">{{ lot.prixOuverture }}</td>
          <td v-if="colonnesVisibles.valeurMinPourVente">
            {{ lot.prixMinPourVente }}
          </td>
          <td v-if="colonnesVisibles.estimationMin">
            {{ lot.valeurEstimeMin }}
          </td>
          <td v-if="colonnesVisibles.estimationMax">
            {{ lot.valeurEstimeMax }}
          </td>
          <td v-if="colonnesVisibles.categorie">{{ lot.categorie }}</td>
          <td v-if="colonnesVisibles.artiste">{{ lot.artiste }}</td>
          <td v-if="colonnesVisibles.dimension">{{ lot.dimension }}</td>
          <td v-if="colonnesVisibles.description">{{ lot.description }}</td>
          <td v-if="colonnesVisibles.medium">{{ lot.medium }}</td>
          <td v-if="colonnesVisibles.vendeur">{{ lot.vendeur }}</td>
          <td v-if="colonnesVisibles.estVendu">
            <img
              v-if="lot.estVendu"
              src="/icons/vendu.png"
              width="40"
              height="40"
            />
            <img v-else src="/icons/nonvendu.png" width="40" height="40" />
          </td>
          <td v-if="colonnesVisibles.livraison">
            <img
              v-if="lot.estLivrable"
              src="/icons/livrable.png"
              width="40"
              height="40"
            />
            <img v-else src="/icons/nonlivrable.png" width="40" height="40" />
          </td>
          <td v-if="colonnesVisibles.modifier">
            <router-link
              :to="{ name: 'ModificationLot', params: { id: lot.id } }"
            >
              <img src="/icons/modifier.png" width="30" height="30" />
            </router-link>
          </td>
          <td v-if="colonnesVisibles.supprimer">
            <img
              @click="ouvrirBoiteModale(lot.id)"
              class="curseurpointeur"
              src="/icons/supprimer.png"
              width="30"
              height="30"
            />
          </td>
        </tr>
      </tbody>
    </table>
  </div>

  <!-- Ajout de la pagination en bas -->
  <div class="d-flex flex-row justify-content-center gap-1 flex-wrap p-3">
    <!-- Bouton Précédent -->
    <button
      type="button"
      class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
      @click="reculerPage"
      v-bind:disabled="pageCourante == 1"
    >
      Précédent
    </button>

    <!-- Boutons de pages -->
    <div v-for="item in listePagination">
      <button
        type="button"
        class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
        :pageId="item"
        @click="changerPage(item)"
        v-bind:disabled="pageCourante == item || item == '...'"
      >
        {{ item }}
      </button>
    </div>

    <!-- Bouton Suivant -->
    <button
      type="button"
      class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
      @click="avancerPage"
      v-bind:disabled="pageCourante == nbPages"
    >
      Suivant
    </button>
  </div>

  <!-- Modal de confirmation -->
  <div v-if="lotASupprimer !== null" class="modal-overlay">
    <div class="modal-content">
      <p>Êtes-vous sûr de vouloir supprimer ce lot ?</p>
      <div class="modal-buttons">
        <button @click="confirmerSuppression" class="btn btn-danger">OK</button>
        <button @click="annulerSuppression" class="btn btn-secondary">
          Annuler
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref, watch } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";

const store = useStore();
const router = useRouter();
const lots = ref([]);
const lotASupprimer = ref(null);
const messageConfirmation = ref(null);

// Nouvelles variables pour la pagination et la recherche
const lotsFiltres = ref([]);
const rechercheDansListeDeLot = ref("");
const nbLotsRecus = ref(0);
const lotsParPage = ref(20); // Valeur par défaut
const pageCourante = ref(1);
const lotsAffiches = ref([]);
const nbPages = ref(1);
const listePagination = ref([]);

const colonnesVisibles = ref({
  encan: true,
  numero: true,
  prixOuverture: true,
  valeurMinPourVente: true,
  estimationMin: true,
  estimationMax: true,
  categorie: true,
  artiste: true,
  dimension: true,
  description: true,
  medium: true,
  vendeur: true,
  estVendu: true,
  livraison: true,
  modifier: true,
  supprimer: true,
});

const colonnesRecherche = ref({
  numero: true,
  artiste: true,
  medium: true,
});

const toggleToutesColonnes = () => {
  const nouvelEtat = !Object.values(colonnesVisibles.value).every((v) => v);
  Object.keys(colonnesVisibles.value).forEach((key) => {
    colonnesVisibles.value[key] = nouvelEtat;
  });
};

const toggleColonne = (colonne) => {
  colonnesVisibles.value[colonne] = !colonnesVisibles.value[colonne];
};

const toggleToutesColonnesRecherche = () => {
  const nouvelEtat = !Object.values(colonnesRecherche.value).every((v) => v);
  Object.keys(colonnesRecherche.value).forEach((key) => {
    colonnesRecherche.value[key] = nouvelEtat;
  });
};

const toggleColonneRecherche = (colonne) => {
  colonnesRecherche.value[colonne] = !colonnesRecherche.value[colonne];
};

const changerNbLotParPage = (nouvLotsParPage) => {
  lotsParPage.value = nouvLotsParPage;
  nbPages.value = recalculerNbPages();
  pageCourante.value = 1;
  genererListePagination();
  chercherLotsAAfficher();
};

const afficherTousLots = () => {
  lotsParPage.value = nbLotsRecus.value;
  nbPages.value = recalculerNbPages();
  pageCourante.value = 1;
  genererListePagination();
  chercherLotsAAfficher();
};

const reculerPage = ref(function (event) {
  if (pageCourante.value > 1) {
    pageCourante.value--;
    chercherLotsAAfficher(event);
  }
});

const avancerPage = ref(function (event) {
  if (pageCourante.value < nbPages.value) {
    pageCourante.value++;
    chercherLotsAAfficher(event);
  }
});

const changerPage = ref(function (numeroPage) {
  if (numeroPage !== "...") {
    pageCourante.value = parseInt(numeroPage);
    chercherLotsAAfficher();
  }
});

function recalculerNbPages() {
  return Math.ceil(nbLotsRecus.value / lotsParPage.value);
}

function genererListePagination() {
  listePagination.value = [];
  for (let i = 1; i <= nbPages.value; i++) {
    if (
      i == 1 ||
      (i >= pageCourante.value - 1 && i <= pageCourante.value + 1) ||
      i == nbPages.value
    ) {
      listePagination.value.push(i);
    } else if (
      listePagination.value[listePagination.value.length - 1] != "..."
    ) {
      listePagination.value.push("...");
    }
  }
}

function chercherLotsAAfficher(event) {
  lotsAffiches.value = [];
  let positionDebut = (pageCourante.value - 1) * lotsParPage.value;
  let positionFin = pageCourante.value * lotsParPage.value;
  for (
    let i = positionDebut;
    i < positionFin && i < lotsFiltres.value.length;
    i++
  ) {
    lotsAffiches.value.push(lotsFiltres.value[i]);
  }
}

onMounted(async () => {
  try {
    await initialiseData();
    setupRechercheAvancee();
  } catch (error) {
    console.error("Erreur lors de la récupération des lots:", error);
  }
});

async function initialiseData() {
  const response = await store.dispatch("obtenirTousLots");
  lots.value = response;
  lotsFiltres.value = lots.value;
  nbLotsRecus.value = lotsFiltres.value.length;
  nbPages.value = recalculerNbPages();
  genererListePagination();
  chercherLotsAAfficher();
}

function setupRechercheAvancee() {
  const checkboxQuiSelectionneToutRecherche = document.querySelector(
    ".checkboxTousRecherche"
  );
  const listeDesCheckboxesRecherche = document.querySelectorAll(
    ".checkboxSeulRecherche"
  );

  if (checkboxQuiSelectionneToutRecherche) {
    checkboxQuiSelectionneToutRecherche.addEventListener(
      "change",
      function (e) {
        if (this.checked) {
          listeDesCheckboxesRecherche.forEach((el) => {
            el.checked = true;
            el.disabled = true;
          });
        } else {
          listeDesCheckboxesRecherche.forEach((el) => {
            el.disabled = false;
            el.checked = false;
          });
        }
        rechercherAvance(rechercheDansListeDeLot.value);
      }
    );
  }

  listeDesCheckboxesRecherche.forEach((el) => {
    el.addEventListener("click", () => {
      rechercherAvance(rechercheDansListeDeLot.value);
    });
  });
}

// Ajout du watch pour la recherche
watch(rechercheDansListeDeLot, (newValue) => {
  rechercherAvance(newValue);
});

function rechercherAvance(valeur) {
  if (!valeur) {
    lotsFiltres.value = lots.value;
  } else {
    valeur = valeur.toLowerCase();
    lotsFiltres.value = lots.value.filter((lot) => {
      return Object.entries(lot).some(([key, value]) => {
        if (typeof value === "string" || typeof value === "number") {
          return value.toString().toLowerCase().includes(valeur);
        }
        return false;
      });
    });
  }
  nbLotsRecus.value = lotsFiltres.value.length;
  pageCourante.value = 1;
  nbPages.value = recalculerNbPages();
  genererListePagination();
  chercherLotsAAfficher();
}

const ouvrirBoiteModale = (id) => {
  lotASupprimer.value = id;
};

const confirmerSuppression = async () => {
  if (lotASupprimer.value !== null) {
    try {
      await store.dispatch("supprimerLot", lotASupprimer.value);
      lots.value = lots.value.filter((lot) => lot.id !== lotASupprimer.value);
      messageConfirmation.value = "Lot supprimé avec succès.";
      setTimeout(() => {
        messageConfirmation.value = null;
      }, 2000);
    } catch (error) {
      console.error("Erreur lors de la suppression du lot:", error);
    } finally {
      lotASupprimer.value = null;
    }
  }
  await initialiseData();
};

const annulerSuppression = () => {
  lotASupprimer.value = null;
};

const redirigerVersCreationLot = () => {
  router.push({ name: "CreationLot" });
};

const afficherLotsParPage = (nbLots) => {
  changerNbLotParPage(nbLots);
};
</script>

<style>
@import "datatables.net-dt";

.boutonPersonnalise {
  margin: 5px;
  padding-left: 15px;
  padding-right: 15px;
  border: none;
  border-radius: 5px;
  font-size: 15px;
  height: 25px;
}

h2 {
  padding-left: 15px;
}

li label {
  font-size: 15px;
}

.margesPourTable {
  padding-left: 15px;
  padding-right: 15px;
}

th {
  font-weight: bold;
  font-size: 13px;
  text-align: center !important;
}

li label {
  font-size: 13px;
  padding-left: 5px;
}

.dt-column-title {
  font-weight: bold;
}

tr:nth-child(even) {
  background-color: #f8f9fa;
}

tr:nth-child(odd) {
  background-color: #e9ecef;
}

tr:hover {
  background-color: #d1e7ff;
}

.cacher {
  visibility: collapse;
}

.curseurpointeur {
  cursor: pointer;
}

.checkboxTousRecherche,
.checkboxSeulRecherche,
.checkboxTous,
.checkboxSeul {
  margin-right: 7px;
}

td {
  font-size: 13px;
  text-align: center !important;
}
</style>

<style scoped>
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
}

.modal-content {
  background-color: white;
  padding: 20px;
  border-radius: 5px;
  text-align: center;
}

.modal-buttons {
  margin-top: 15px;
}

.modal-buttons button {
  margin: 0 10px;
}

.alert {
  margin: 10px 0;
  padding: 10px;
  border-radius: 5px;
}

.alert-success {
  background-color: #d4edda;
  color: #155724;
}
</style>
