<template>
    <div class="d-flex flex-column justify-content-between">
        <div class="d-flex justify-content-between">
            <h2 class="d-flex-1">Liste des encans</h2>
            <router-link to="TableauDeBordEncansAjout" class="text-decoration-none">
                <button class="btn bleuMarinSecondaireFond btnSurvolerBleuMoyenFond btnClick text-white d-flex-1"
                        type="button"
                        id="ajouterEncanButton">
                    Ajouter un encan
                </button>
            </router-link>
        </div>
        <transition name="fade">
            <div v-if="errorMessage" class="alert alert-danger">
                {{ errorMessage }}
            </div>
            <div v-else>
                <div v-if="successMessage" class="alert alert-success">
                    {{ successMessage }}
                </div>
            </div>
        </transition>

        <div class="d-flex justify-content-between">
            <div class="d-flex collapse dropdown dropdown-center">
                <button class="btn dropdown-toggle bleuMarinSecondaireFond rounded text-white contenuListeDropdown" type="button" data-bs-toggle="dropdown" aria-expanded="false">
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
                <label for="Recherche">Rechercher: </label>
                <input data-bs-theme="light" type="search" aria-label="RechercheNum" v-model="encanRechercheNumEncan" placeholder="Numéro encan">

                <input data-bs-theme="light" type="search" aria-label="RechercheDate" v-model="encanRechercheDate" placeholder="Date AAAA-MM-JJ">
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
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tr v-for="(encan) in listeEncansFiltree" :key="encan.id">
                <td>{{encan.numeroEncan}}</td>
                <td class="d-flex justify-content-center">
                    <div class="d-flex collapse dropdown dropdown-center">
                        <button :encanId="encan.id" class="btn dropdown-toggle bleuMarinSecondaireFond rounded text-white contenuListeDropdown fs-7" type="button" data-bs-toggle="dropdown" aria-expanded="false">
                            <span v-if="encan.estPublie==true">Publié</span>
                            <span v-else>Non publié</span>
                        </button>

                        <ul class="dropdown-menu dropdown-menu-dark bleuMarinFond text-center">
                            <li>
                                <a class="dropdown-item" @click="encanPublieMAJ(true)" :encanId="encan.id">Publié</a>
                            </li>
                            <li>
                                <a class="dropdown-item" @click="encanPublieMAJ(false)" :encanId="encan.id">Non publié</a>
                            </li>
                        </ul>
                    </div>
                </td>
                <td>{{ dateDebut[0]}}-{{dateDebut[1]}}-{{dateDebutJour}}</td>
                <td>{{ dateFin[0]}}-{{dateFin[1]}}-{{dateFinJour}}</td>
                <td>{{ dateDebutSoireeCloture[0]}}-{{dateDebutSoireeCloture[1]}}-{{dateDebutSoireeClotureJour}} {{ dateDebutSoireeClotureHeure }} à {{ dateFinSoireeClotureHeure }}</td>
                <td>{{ encan.nbLots }}</td>
                <td>modifier</td>
                <td>supprimer</td>
            </tr>
        </table>
    </div>
</template>
<script setup>
    import { onMounted, ref, watch, reactive } from "vue";
    import { useStore } from "vuex";
    import TableauDeBordEncansAjout from '@/components/views/TableauDeBordEncansAjout.vue'

    const store = useStore();
    let listeEncans = ref([]);
    let listeEncansFiltree = ref([]);
    let dateDebut = ref("");
    let dateDebutJour = ref("");
    let dateFin = ref("");
    let dateFinJour = ref("");
    let dateDebutSoireeCloture = ref("");
    let dateDebutSoireeClotureJour = ref("");
    let dateDebutSoireeClotureHeure = ref("");
    let dateFinSoireeCloture = ref("");
    let dateFinSoireeClotureHeure = ref("");

    let encanPublieMAJ;
    const encanRechercheNumEncan = ref();
    const encanRechercheDate = ref();

    const errorMessage = ref('');
    const successMessage = ref('');

    const encan = ref("");

    onMounted(async () => {

        try {
            listeEncans.value = await store.dispatch("fetchEncanInfo");

            listeEncansFiltree.value = listeEncans.value;

            listeEncansFiltree.value.forEach(element => {
                dateDebut.value = element.dateDebut.toString().split("-");
                dateFin.value = element.dateFin.toString().split("-");
                dateDebutSoireeCloture.value = element.dateDebutSoireeCloture.toString().split("-");
                dateFinSoireeCloture.value = element.dateFinSoireeCloture.toString().split("-");

                dateDebutJour.value = dateDebut.value[2].substring(0, 2);
                dateFinJour.value = dateFin.value[2].substring(0, 2);
                dateDebutSoireeClotureJour.value = dateDebutSoireeCloture.value[2].substring(0, 2);

                dateDebutSoireeClotureHeure.value = dateDebutSoireeCloture.value[2].substring(3, 8);
                dateFinSoireeClotureHeure.value = dateFinSoireeCloture.value[2].substring(3, 8);
            });

            encanPublieMAJ = async function (statutPublie) {
                let encanId = event.srcElement.getAttribute("encanId")
                encan.value = listeEncans.value.find(e => e.id == encanId);

                if (encan.value.estPublie != statutPublie) {
                    encan.value.estPublie = !encan.value.estPublie;

                    let formData = reactive({
                        numeroEncan: encan.value.numeroEncan,
                        estPublie: encan.value.estPublie,
                    });

                    const response = await store.dispatch('mettreAJourEncanPublie', formData);
                    if (response.success) {
                        successMessage.value = "Statut encan modifié!";
                        errorMessage.value = "";
                        setTimeout(() => {
                            successMessage.value = "";
                        }, 5000);
                    }
                    else {
                        errorMessage.value = response.error;
                        successMessage.value = "";
                        setTimeout(() => {
                            errorMessage.value = "";
                        }, 5000);
                    }
                }
            }
        }
        catch (erreur) {
            console.log("Erreur encans" + erreur);
        }
    });

    watch(encanRechercheNumEncan, () => {
        listeEncansFiltree.value = listeEncans.value;

        listeEncansFiltree.value = listeEncansFiltree.value.filter(({ numeroEncan }) =>
            numeroEncan.toString().startsWith(encanRechercheNumEncan.value)
        );
    });

    watch(encanRechercheDate, () => {
        listeEncansFiltree.value = listeEncans.value;

        listeEncansFiltree.value = listeEncansFiltree.value.filter(({ dateDebut, dateFin, dateDebutSoireeCloture, dateFinSoireeCloture }) =>
            dateDebut.toString().startsWith(encanRechercheDate.value) ||
            dateFin.toString().startsWith(encanRechercheDate.value) ||
            dateDebutSoireeCloture.toString().startsWith(encanRechercheDate.value) ||
            dateFinSoireeCloture.toString().startsWith(encanRechercheDate.value)
        );
    });
</script>

<style scoped>
    .dropdown-toggle[aria-expanded="true"] {
        background-color: #5A708A;
    }

    .dropdown-item:active {
        background-color: #5A708A;
    }

    .fade-enter-active,
    .fade-leave-active {
        transition: opacity 1s ease;
    }

    .fade-enter,
    .fade-leave-to {
        opacity: 0;
    }
</style>
