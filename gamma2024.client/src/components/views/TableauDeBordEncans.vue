<template>
    <div class="d-flex flex-column justify-content-between">
        <div class="d-flex justify-content-between">
            <h2 class="d-flex-1">Liste des encans</h2>
            <button class="btn bleuMarinSecondaireFond btnSurvolerBleuMoyenFond text-white d-flex-1"
                    type="button"
                    id="ajouterEncanButton">
                Ajouter un encan
            </button>
        </div>

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
                <label for="Recherche">Rechercher : </label>
                <input data-bs-theme="light" type="search" aria-label="Recherche" v-model="encanRechercher">
            </div>
        </div>

        <table class="table table-striped mt-3">
            <thead>
                <tr>
                    <th data-field="numeroEncan">Encan</th>
                    <th data-field="status">Status</th>
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
                <td></td>
                <td>{{dateDebutJour}}-{{dateDebut[1]}}-{{ dateDebut[0]}}</td>
                <td>{{dateFinJour}}-{{dateFin[1]}}-{{ dateFin[0]}}</td>
                <td>{{dateDebutSoireeClotureJour}}-{{dateDebutSoireeCloture[1]}}-{{ dateDebutSoireeCloture[0]}} {{ dateDebutSoireeClotureHeure }} à {{dateFinSoireeClotureJour}}-{{dateFinSoireeCloture[1]}}-{{ dateFinSoireeCloture[0]}} {{ dateFinSoireeClotureHeure }}</td>
                <td>vfffd</td>
                <td>modifier</td>
                <td>supprimer</td>
            </tr>
        </table>
    </div>
</template>
<script setup>
    import { onMounted, ref, watch } from "vue";
    import { useStore } from "vuex";

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
    const dateFinSoireeClotureJour = ref("");
    const dateFinSoireeClotureHeure = ref("");

    
    onMounted(async () => {

        try {
            listeEncans.value = await store.dispatch("fetchEncanInfo");
            listeEncansFiltree.value = listeEncans.value;
            
            listeEncansFiltree.value.forEach(element => {
                dateDebut.value = element.dateDebut.toString().split("-");
                dateFin.value = element.dateFin.toString().split("-");
                dateDebutSoireeCloture.value = element.dateDebutSoireeCloture.toString().split("-");
                dateFinSoireeCloture.value = element.dateFinSoireeCloture.toString().split("-");

                dateDebutJour.value = dateDebut.value[2].substring(0,2);
                dateFinJour.value = dateFin.value[2].substring(0,2);
                dateDebutSoireeClotureJour.value = dateDebutSoireeCloture.value[2].substring(0,2);
                dateFinSoireeClotureJour.value = dateFinSoireeCloture.value[2].substring(0,2);

                dateDebutSoireeClotureHeure.value = dateDebutSoireeCloture.value[2].substring(3, 8);
                dateFinSoireeClotureHeure.value = dateFinSoireeCloture.value[2].substring(3, 8);
            });

        }
        catch (erreur) {
            console.log("Erreur encans" + erreur);
        }
    });

    const encanRechercher = ref();
    
    watch(encanRechercher, () => {
        listeEncansFiltree.value = listeEncans.value;

        listeEncansFiltree.value = listeEncansFiltree.value.filter(({ numeroEncan, dateDebut, dateFin, dateDebutSoireeCloture, dateFinSoireeCloture }) =>
            numeroEncan.toString().startsWith(encanRechercher.value) ||
            dateDebut.toString().startsWith(encanRechercher.value) ||
            dateFin.toString().startsWith(encanRechercher.value) ||
            dateDebutSoireeCloture.toString().startsWith(encanRechercher) ||
            dateFinSoireeCloture.toString().startsWith(encanRechercher.value)
        );
    })
    
    

    console.log(dateDebut)
</script>

<style scoped>
    .dropdown-toggle[aria-expanded="true"] {
        background-color: #5A708A;
    }

    .dropdown-item:active {
        background-color: #5A708A;
    }
</style>
