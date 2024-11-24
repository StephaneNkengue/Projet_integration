<template>
    <header class="sticky-top">
        <div>
            <nav class="navbar navbar-expand-lg bleuMarinSecondaireFond py-0"
                 data-bs-theme="dark">
                <div class="container-fluid justify-content-between">
                    <router-link :to="{ name: 'Accueil' }" class="text-decoration-none">
                        <a class="navbar-brand d-flex align-items-center fs-6">
                            <img src="/images/Logo.png"
                                 alt="Les Encans de Nantes"
                                 height="40" />
                            Les Encans de Nantes <br />au Québec
                        </a>
                    </router-link>

                    <button class="navbar-toggler"
                            data-bs-theme="dark"
                            type="button"
                            data-bs-toggle="collapse"
                            data-bs-target="#navbarSupportedContent"
                            aria-controls="navbarSupportedContent"
                            aria-expanded="false"
                            aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>

                    <div class="collapse navbar-collapse justify-content-between"
                         id="navbarSupportedContent">
                        <ul class="navbar-nav text-center">
                            <li class="nav-item align-self-center d-flex jalign-items-center align-items-center">
                                <router-link :to="{ name: 'Accueil' }"
                                             class="text-decoration-none">
                                    <a class="nav-link active"> Accueil </a>
                                </router-link>
                            </li>

                            <li class="nav-item">
                                <router-link :to="{ name: 'EncanPresent' }"
                                             class="text-decoration-none">
                                    <a class="nav-link"> Encan courant </a>
                                </router-link>
                            </li>

                            <li class="nav-item">
                                <router-link :to="{ name: 'EncansPasses' }"
                                             class="text-decoration-none">
                                    <a class="nav-link"> Encans passés </a>
                                </router-link>
                            </li>

                            <li class="nav-item">
                                <router-link :to="{ name: 'EncansFuturs' }"
                                             class="text-decoration-none">
                                    <a class="nav-link"> Encans futurs </a>
                                </router-link>
                            </li>

                            <li class="nav-item" v-if="estConnecte && estClient">
                                <router-link :to="{ name: 'HistoriqueAchatsParMembre' }"
                                             class="text-decoration-none">
                                    <a class="nav-link"> Historique des achats </a>
                                </router-link>
                            </li>
                        </ul>

                        <div class="d-flex justify-content-center gap-3 mb-2 mb-lg-0 flex-row align-items-center justify-content-center">
                            <router-link :to="{ name: 'Inscription' }" v-if="!estConnecte">
                                <button class="btn btn-outline bleuMoyenFond text-white py-0 butttonNavBar btnSurvolerBleuMoyenFond"
                                        type="button">
                                    Inscription
                                </button>
                            </router-link>
                            <router-link :to="{ name: 'Connexion' }" v-if="!estConnecte">
                                <button class="btn btn-outline bleuMoyenFond text-white py-0 butttonNavBar btnSurvolerBleuMoyenFond"
                                        type="button">
                                    Connexion
                                </button>
                            </router-link>

                            <div class="dropdown text-white align-self-stretch justify-content-center align-items-center d-flex"
                                 v-if="estAdmin">
                                <a class="nav-link dropdown-toggle"
                                   role="button"
                                   data-bs-toggle="dropdown"
                                   aria-expanded="false">
                                    Tableau de bord
                                </a>
                                <ul class="dropdown-menu bleuMarinFond text-center">
                                    <li>
                                        <router-link :to="{ name: 'TableauDeBordInventaire' }"
                                                     class="text-decoration-none">
                                            <a class="dropdown-item contenuListeDropdown">Inventaire</a>
                                        </router-link>
                                    </li>
                                    <li>
                                        <router-link :to="{ name: 'TableauDeBordEncans' }"
                                                     class="text-decoration-none">
                                            <a class="dropdown-item contenuListeDropdown">Encans</a>
                                        </router-link>
                                    </li>
                                    <li>
                                        <router-link :to="{ name: 'AffichageVendeurs' }"
                                                     class="text-decoration-none">
                                            <a class="dropdown-item contenuListeDropdown">Vendeurs</a>
                                        </router-link>
                                    </li>
                                    <li>
                                        <router-link :to="{ name: 'TableauDeBordVentes' }"
                                                     class="text-decoration-none">
                                            <a class="dropdown-item contenuListeDropdown">Ventes</a>
                                        </router-link>
                                    </li>
                                    <li>
                                        <router-link :to="{ name: 'GestionMembre' }"
                                                     class="text-decoration-none">
                                            <a class="dropdown-item contenuListeDropdown">Profils de membre</a>
                                        </router-link>
                                    </li>
                                </ul>
                            </div>

                            <div class="dropdown text-white align-self-stretch justify-content-center align-items-center d-flex"
                                 v-if="estConnecte && !estAdmin">
                                <a class="nav-link d-flex align-items-center justify-content-center"
                                   role="button"
                                   data-bs-toggle="dropdown"
                                   aria-expanded="false"
                                   @click="notification = !notification">
                                    <img src="/icons/IconeCloche.png"
                                         alt="Icon cloche"
                                         height="25" />
                                </a>
                                <ul class="dropdown-menu bleuMarinFond text-center">
                                    <li v-for="index in 5" :key="index">
                                        <router-link :to="{ name: 'Accueil' }"
                                                     class="text-decoration-none text-white d-flex align-items-center gap-3">
                                            <a class="dropdown-item text-white btnSurvolerBleuMoyenFond"
                                               @click="notification = false">
                                                test
                                            </a>
                                        </router-link>
                                    </li>
                                </ul>
                            </div>
                            <div class="dropdown text-white" v-if="estConnecte">
                                <a class="nav-link d-flex align-items-center"
                                   role="button"
                                   data-bs-toggle="dropdown"
                                   aria-expanded="false">
                                    <p class="m-0 me-1">{{ username }}</p>
                                    <img :src="avatarUrl"
                                         alt="Avatar"
                                         class="imgProfile rounded-circle" />
                                </a>
                                <ul class="dropdown-menu dropdown-menu-end bleuMarinFond text-center end-0 top-100">
                                    <li>
                                        <router-link v-if="estClient"
                                                     :to="{ name: 'ModificationProfilUtilisateur' }"
                                                     class="text-decoration-none text-white d-flex align-items-center gap-3">
                                            <a class="dropdown-item text-white btnSurvolerBleuMoyenFond"
                                               @click="activationDropdownProfil = false">
                                                Profil
                                            </a>
                                        </router-link>
                                    </li>
                                    <li>
                                        <a class="dropdown-item text-danger btnSurvolerBleuMoyenFond fw-bold"
                                           href="#"
                                           @click.prevent="deconnecter">
                                            Déconnexion
                                        </a>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </nav>
            <div v-if="
          $route.name == 'EncanPresent' ||
          $route.name == 'Encan' ||
          $route.name == 'ResultatRechercheLots'">
                <nav class="navbar bg-white navbarRechercheAvancee">
                    <div class="container-fluid d-flex justify-content-end">
                        <button class="navbar-toggler loupetoggler"
                                type="button"
                                data-bs-toggle="collapse"
                                data-bs-target="#navbarToggleRechercheAvancee"
                                aria-controls="navbarToggleRechercheAvancee"
                                aria-expanded="false"
                                aria-label="Toggle navigation">
                            <span class="navbar-toggler-icon iconeloupe"></span>
                        </button>
                    </div>
                </nav>
                <div class="bg-white">
                    <div class="container collapse card mb-5 bg-white aucunPaddingPourCarteLots"
                         id="navbarToggleRechercheAvancee">
                        <div class="card-header d-flex justify-content-center">
                            <h2>Recherche avancée de lots</h2>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-6">
                                    <div class="col mt-2">
                                        <label class="text-nowrap recherchelabel fw-bold"
                                               for="rechercheLotsNumeroEncan">
                                            Numéro d'encan
                                        </label>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <select class="form-select py-0 selectPourListe"
                                                        id="selectNumeroEncan"
                                                        @change="affichageInputNumeroEncan"
                                                        aria-label="Default select example">
                                                    <option class="py-0" value="0" selected>Égal à</option>
                                                    <option class="py-0"
                                                            id="selectNumeroEncanEntre"
                                                            value="1">
                                                        Entre
                                                    </option>
                                                </select>
                                            </div>

                                            <div class="col-sm">
                                                <input type="number"
                                                       maxlength="10"
                                                       class="form-control rechercheinput align-self-end"
                                                       id="rechercheLotsNumeroEncan" />
                                            </div>
                                            <div class="col-sm-auto inputAAfficher align-items-center"
                                                 id="inputAAfficherNumeroEncan">
                                                <label class="fs-6">et</label>
                                            </div>
                                            <div class="col-sm inputAAfficher"
                                                 id="inputAAfficherNumeroEncan">
                                                <input type="number"
                                                       maxlength="10"
                                                       class="form-control rechercheinput align-self-end"
                                                       id="rechercheLotsNumeroEncan2" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="col mt-2">
                                        <label class="text-nowrap recherchelabel fw-bold"
                                               for="rechercheLotsValeurEstimee">
                                            Prix estimé
                                        </label>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <select class="form-select py-0 selectPourListe"
                                                        id="selectValeurEstimee"
                                                        @change="affichageInputValeurEstimee"
                                                        aria-label="Default select example">
                                                    <option class="py-0" value="0" selected>Égale à</option>
                                                    <option class="py-0" value="1">Inférieure à</option>
                                                    <option class="py-0" value="2">Supérieure à</option>
                                                    <option class="py-0"
                                                            value="3"
                                                            id="selectValeurEstimeeEntre">
                                                        Entre
                                                    </option>
                                                </select>
                                            </div>

                                            <div class="col-sm">
                                                <input type="number"
                                                       maxlength="10"
                                                       class="form-control rechercheinput align-self-end"
                                                       id="rechercheLotsValeurEstimee" />
                                            </div>
                                            <div class="col-sm-auto inputAAfficher align-items-center"
                                                 id="inputAAfficherValeurEstimee">
                                                <label class="fs-6">et</label>
                                            </div>
                                            <div class="col-sm inputAAfficher"
                                                 id="inputAAfficherValeurEstimee">
                                                <input type="number"
                                                       maxlength="10"
                                                       class="form-control rechercheinput align-self-end"
                                                       id="rechercheLotsValeurEstimee2" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-2">
                                <div class="col-md-4">
                                    <label class="text-nowrap recherchelabel fw-bold"
                                           for="rechercheLotsArtiste">
                                        Artiste
                                    </label>
                                    <select class="form-select py-0 align-self-center selectPourListeQuiOntBesoinDeInvalide"
                                            id="selectArtiste"
                                            aria-label="Default select example"
                                            required>
                                        <option class="py-0 pasDeChoix" value="" selected>
                                            Pas de choix
                                        </option>
                                        <option v-for="artiste in listeDesArtistes" :key="artiste.nomArtiste" class="py-0" :value="artiste.nomArtiste">{{artiste.nomArtiste}}</option>
                                    </select>
                                </div>
                                <div class="col-md-4">
                                    <label class="text-nowrap recherchelabel fw-bold"
                                           for="rechercheLotsCategorie">
                                        Catégorie
                                    </label>
                                    <select class="form-select py-0 align-self-center selectPourListeQuiOntBesoinDeInvalide"
                                            id="selectCategorie"
                                            aria-label="Default select example"
                                            required>
                                        <option class="py-0 pasDeChoix" value="" selected>
                                            Pas de choix
                                        </option>
                                        <option v-for="categorie in listeDesCategories" :key="categorie.id" class="py-0" :value="categorie.id">{{categorie.nom}}</option>
                                    </select>
                                </div>
                                <div class="col-md-4">
                                    <label class="text-nowrap recherchelabel fw-bold"
                                           for="rechercheLotsMedium">
                                        Medium
                                    </label>
                                    <select class="form-select py-0 align-self-center selectPourListeQuiOntBesoinDeInvalide"
                                            id="selectMedium"
                                            aria-label="Default select example"
                                            required>
                                        <option class="py-0 pasDeChoix" value="" selected>
                                            Pas de choix
                                        </option>
                                        <option v-for="medium in listeDesMediums" :key="medium.id" class="py-0" :value="medium.id">{{medium.type}}</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    <div class="col mt-2">
                                        <label class="text-nowrap recherchelabel fw-bold"
                                               for="rechercheLotsHauteur">
                                            Hauteur
                                        </label>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <select class="form-select py-0 selectPourListe"
                                                        id="selectHauteur"
                                                        @change="affichageInputHauteur"
                                                        aria-label="Default select example">
                                                    <option class="py-0" value="0" selected>Égale à</option>
                                                    <option class="py-0" value="1">Inférieure à</option>
                                                    <option class="py-0" value="2">Supérieure à</option>
                                                    <option class="py-0" value="3" id="selectHauteurEntre">
                                                        Entre
                                                    </option>
                                                </select>
                                            </div>
                                            <div class="col-sm">
                                                <input type="number"
                                                       maxlength="10"
                                                       class="form-control rechercheinput align-self-end"
                                                       id="rechercheLotsHauteur" />
                                            </div>
                                            <div class="col-sm-auto inputAAfficher align-items-center"
                                                 id="inputAAfficherHauteur">
                                                <label class="fs-6">et</label>
                                            </div>
                                            <div class="col-sm inputAAfficher" id="inputAAfficherHauteur">
                                                <input type="number"
                                                       maxlength="10"
                                                       class="form-control rechercheinput align-self-end"
                                                       id="rechercheLotsHauteur2" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-6">
                                    <div class="col mt-2">
                                        <label class="text-nowrap recherchelabel fw-bold"
                                               for="rechercheLotsLargeur">
                                            Largeur
                                        </label>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <select class="form-select py-0 selectPourListe"
                                                        id="selectLargeur"
                                                        @change="affichageInputLargeur"
                                                        aria-label="Default select example">
                                                    <option class="py-0" value="0" selected>Égale à</option>
                                                    <option class="py-0" value="1">Inférieure à</option>
                                                    <option class="py-0" value="2">Supérieure à</option>
                                                    <option class="py-0" value="3" id="selectLargeurEntre">
                                                        Entre
                                                    </option>
                                                </select>
                                            </div>

                                            <div class="col-sm">
                                                <input type="number"
                                                       maxlength="10"
                                                       class="form-control rechercheinput align-self-end"
                                                       id="rechercheLotsLargeur" />
                                            </div>
                                            <div class="col-sm-auto inputAAfficher align-items-center"
                                                 id="inputAAfficherLargeur">
                                                <label class="fs-6">et</label>
                                            </div>
                                            <div class="col-sm inputAAfficher" id="inputAAfficherLargeur">
                                                <input type="number"
                                                       maxlength="10"
                                                       class="form-control rechercheinput align-self-end"
                                                       id="rechercheLotsLargeur2" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col mt-2 mb-2 d-flex justify-content-center">
                            <button class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
                                    type="button"
                                    @click="rechercheAvanceeLots">
                                Lancer la rechercher
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div v-else-if="
          $route.name == 'EncansPasses' ||
          $route.name == 'EncansFuturs' ||
          $route.name == 'TousLesEncans' ||
          $route.name == 'ResultatRechercheEncans'">
                <nav class="navbar bg-white navbarRechercheAvancee">
                    <div class="container-fluid d-flex justify-content-end">
                        <button class="navbar-toggler loupetoggler"
                                type="button"
                                data-bs-toggle="collapse"
                                data-bs-target="#navbarToggleRechercheAvancee"
                                aria-controls="navbarToggleRechercheAvancee"
                                aria-expanded="false"
                                aria-label="Toggle navigation">
                            <span class="navbar-toggler-icon iconeloupe"></span>
                        </button>
                    </div>
                </nav>
                <div class="bg-white">
                    <div class="container collapse card mb-5 bg-white aucunPaddingPourCarteEncans"
                         id="navbarToggleRechercheAvancee">
                        <div class="card-header d-flex justify-content-center">
                            <h2>Recherche avancée d'encans</h2>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-5">
                                    <div class="col mt-2">
                                        <label class="text-nowrap recherchelabel fw-bold"
                                               for="rechercheEncansNumeroEncan">
                                            Numéro d'encan
                                        </label>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <select class="form-select py-0 selectPourListe"
                                                        id="selectNumeroEncan"
                                                        @change="affichageInputNumeroEncan"
                                                        aria-label="Default select example">
                                                    <option class="py-0" value="0" selected>Égal à</option>
                                                    <option class="py-0"
                                                            id="selectNumeroEncanEntre"
                                                            value="1">
                                                        Entre
                                                    </option>
                                                </select>
                                            </div>

                                            <div class="col-sm">
                                                <input type="number"
                                                       maxlength="10"
                                                       class="form-control rechercheinput align-self-end"
                                                       id="rechercheEncansNumeroEncan" />
                                            </div>
                                            <div class="col-sm-auto inputAAfficher align-items-center"
                                                 id="inputAAfficherNumeroEncan">
                                                <label class="fs-6">et</label>
                                            </div>
                                            <div class="col-sm inputAAfficher"
                                                 id="inputAAfficherNumeroEncan">
                                                <input type="number"
                                                       maxlength="10"
                                                       class="form-control rechercheinput align-self-end"
                                                       id="rechercheEncansNumeroEncan2" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-7">
                                    <div class="col mt-2">
                                        <label class="text-nowrap recherchelabel fw-bold"
                                               for="rechercheEncansDate">
                                            Date
                                        </label>
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <select class="form-select py-0 selectPourListe"
                                                        id="selectDate"
                                                        @change="affichageInputDate"
                                                        aria-label="Default select example">
                                                    <option class="py-0" value="0" selected>Égale à</option>
                                                    <option class="py-0" value="1">Inférieure à</option>
                                                    <option class="py-0" value="2">Supérieure à</option>
                                                    <option class="py-0"
                                                            value="3"
                                                            id="selectDateEntre">
                                                        Entre
                                                    </option>
                                                </select>
                                            </div>
                                            <VueDatePicker type="date"
                                                           v-model="rechercheEncansDate1"
                                                           id="rechercheEncansDate1"
                                                           class="col-sm"
                                                           :max-date="desacDateDebutEntre"
                                                           :enable-time-picker="false"
                                                           select-text="Choisir"
                                                           cancel-text="Annuler"
                                                           now-button-label="Aujourd'hui"
                                                           :clearable="true"
                                                           :action-row="{ showNow: true }"
                                                           :format-locale="fr"
                                                           :year-range="[2000, new Date().getFullYear() + 10]" />
                                            <div class="col-sm-auto inputAAfficher inputAAfficherDate align-items-center"
                                                 id="inputAAfficherDate">
                                                <label class="fs-6">et</label>
                                            </div>
                                            <VueDatePicker type="date"
                                                           v-model="rechercheEncansDate2"
                                                           id="rechercheEncansDate2"
                                                           class="col-sm inputAAfficher inputAAfficherDate"
                                                           :min-date="desacDateFinEntre"
                                                           :enable-time-picker="false"
                                                           select-text="Choisir"
                                                           cancel-text="Annuler"
                                                           now-button-label="Aujourd'hui"
                                                           :clearable="true"
                                                           :action-row="{ showNow: true }"
                                                           :format-locale="fr"
                                                           :year-range="[2000, new Date().getFullYear() + 10]" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col mt-2 mb-2 d-flex justify-content-center">
                            <button class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
                                    type="button"
                                    @click="rechercheAvanceeEncans">
                                Lancer la rechercher
                            </button>
                        </div>
                    </div>
                </div>
            </div>
            <div v-else class="bg-white aucuneBarreDeRechercheAnvancee">

            </div>
        </div>
    </header>
</template>
<script setup>
    import { computed, watch, reactive, ref, onMounted } from "vue";
    import { useStore } from "vuex";
    import { useRouter, useRoute } from "vue-router";
    import VueDatePicker from '@vuepic/vue-datepicker';
    import '@vuepic/vue-datepicker/dist/main.css'
    import { fr } from "date-fns/locale";
    import moment from "moment";

    const store = useStore();
    const router = useRouter();
    const route = useRoute();

    const estConnecte = computed(() => store.state.isLoggedIn);
    const estAdmin = computed(() => store.getters.isAdmin);
    const estClient = computed(() => store.getters.isClient);
    const username = computed(() => store.getters.username);
    const avatarUrl = computed(() => store.getters.avatarUrl);

    const currentUser = ref(null);

    const listeDesArtistes = ref([]);
    const listeDesCategories = ref([]);
    const listeDesMediums = ref([]);

    const rechercheEncansDate1 = ref();
    const rechercheEncansDate2 = ref();

    watch(
        () => store.state.user,
        (newUser) => {
            currentUser.value = newUser;
        },
        { deep: true, immediate: true }
    );

    const refreshUserInfo = async () => {
        if (store.state.isLoggedIn) {
            try {
                await store.dispatch("fetchClientInfo");
            } catch (error) {
                console.error(
                    "Erreur lors de la récupération des informations client:",
                    error
                );
            }
        }
    };

    // Appelez refreshUserInfo lorsque l'utilisateur se connecte
    watch(
        () => store.state.isLoggedIn,
        (newValue) => {
            if (newValue) {
                refreshUserInfo();
            }
        }
    );

    async function rechercheAvanceeLots() {
        var stringquery = {};
        var selectNumeroEncan = document.querySelector("#selectNumeroEncan").value;
        var stringNumeroEncan = document.querySelector(
            "#rechercheLotsNumeroEncan"
        ).value;
        var stringNumeroEncan2 = document.querySelector(
            "#rechercheLotsNumeroEncan2"
        ).value;
        var selectValeurEstimee = document.querySelector(
            "#selectValeurEstimee"
        ).value;
        var stringValeurEstimee = document.querySelector(
            "#rechercheLotsValeurEstimee"
        ).value;
        var stringValeurEstimee2 = document.querySelector(
            "#rechercheLotsValeurEstimee2"
        ).value;
        var selectArtiste = document.querySelector("#selectArtiste").value;
        var selectCategorie = document.querySelector("#selectCategorie").value;
        var selectMedium = document.querySelector("#selectMedium").value;
        var selectHauteur = document.querySelector("#selectHauteur").value;
        var stringHauteur = document.querySelector("#rechercheLotsHauteur").value;
        var stringHauteur2 = document.querySelector("#rechercheLotsHauteur2").value;
        var selectLargeur = document.querySelector("#selectLargeur").value;
        var stringLargeur = document.querySelector("#rechercheLotsLargeur").value;
        var stringLargeur2 = document.querySelector("#rechercheLotsLargeur2").value;

        if (stringNumeroEncan) {
            stringquery['selectNumeroEncan'] = selectNumeroEncan;
            stringquery['stringNumeroEncan'] = stringNumeroEncan;
            if (selectNumeroEncan == 1) {
                stringquery['stringNumeroEncan2'] = stringNumeroEncan2;
            }
        }
        if (stringValeurEstimee) {
            stringquery['selectValeurEstimee'] = selectValeurEstimee;
            stringquery['stringValeurEstimee'] = stringValeurEstimee;
            if (selectValeurEstimee == 3) {
                stringquery['stringValeurEstimee2'] = stringValeurEstimee2;
            }
        }
        if (selectArtiste != 0) {
            stringquery['selectArtiste'] = selectArtiste;
        }
        if (selectCategorie != 0) {
            stringquery['selectCategorie'] = selectCategorie;
        }
        if (selectMedium != 0) {
            stringquery['selectMedium'] = selectMedium;
        }
        if (stringHauteur) {
            stringquery['selectHauteur'] = selectHauteur;
            stringquery['stringHauteur'] = stringHauteur;
            if (selectHauteur == 3) {
                stringquery['stringHauteur2'] = stringHauteur2;
            }
        }
        if (stringLargeur) {
            stringquery['selectLargeur'] = selectLargeur;
            stringquery['stringLargeur'] = stringLargeur;
            if (selectLargeur == 3) {
                stringquery['stringLargeur2'] = stringLargeur2;
            }
        }

        router.push({
            path: "/resultatrecherchelots",
            query: {
                data: JSON.stringify(stringquery),
            },
        });
    }

    function affichageInputNumeroEncan() {
        var inputAAfficher = document.querySelectorAll("#inputAAfficherNumeroEncan");
        var selectNumeroEncanEntre = document.querySelector(
            "#selectNumeroEncanEntre"
        );

        if (selectNumeroEncanEntre.selected) {
            inputAAfficher.forEach((el, index) => {
                el.style.display = "inline-block";
            });
        } else if (!selectNumeroEncanEntre.selected) {
            inputAAfficher.forEach((el) => {
                el.style.display = "none";
            });
        }
    }

    function affichageInputValeurEstimee() {
        var inputAAfficher = document.querySelectorAll(
            "#inputAAfficherValeurEstimee"
        );
        var selectValeurestimeeEntre = document.querySelector(
            "#selectValeurEstimeeEntre"
        );

        if (selectValeurEstimeeEntre.selected) {
            inputAAfficher.forEach((el, index) => {
                el.style.display = "inline-block";
            });
        } else if (!selectValeurEstimeeEntre.selected) {
            inputAAfficher.forEach((el) => {
                el.style.display = "none";
            });
        }
    }

    function affichageInputHauteur() {
        var inputAAfficher = document.querySelectorAll("#inputAAfficherHauteur");
        var selectHauteurEntre = document.querySelector("#selectHauteurEntre");

        if (selectHauteurEntre.selected) {
            inputAAfficher.forEach((el, index) => {
                el.style.display = "inline-block";
            });
        } else if (!selectHauteurEntre.selected) {
            inputAAfficher.forEach((el) => {
                el.style.display = "none";
            });
        }
    }

    function affichageInputLargeur() {
        var inputAAfficher = document.querySelectorAll("#inputAAfficherLargeur");
        var selectLargeurEntre = document.querySelector("#selectLargeurEntre");

        if (selectLargeurEntre.selected) {
            inputAAfficher.forEach((el, index) => {
                el.style.display = "inline-block";
            });
        } else if (!selectLargeurEntre.selected) {
            inputAAfficher.forEach((el) => {
                el.style.display = "none";
            });
        }
    }

    // La barre de recherche avancée garde les informations ou non selon le changement de page
    router.beforeEach((to, from, next) => {
        if (from.name == 'ResultatRechercheLots' || from.name == 'Encan' || from.name == 'EncanPresent') {
            var elementsAEffacer = [
                document.querySelector("#rechercheLotsNumeroEncan"),
                document.querySelector("#rechercheLotsNumeroEncan2"),
                document.querySelector("#rechercheLotsValeurEstimee"),
                document.querySelector("#rechercheLotsValeurEstimee2"),
                document.querySelector("#rechercheLotsHauteur"),
                document.querySelector("#rechercheLotsHauteur2"),
                document.querySelector("#rechercheLotsLargeur"),
                document.querySelector("#rechercheLotsLargeur2")
            ]
            var listeDesSelects = document.querySelectorAll(".selectPourListe");
            var listeDesSelectsQuiOntBesoinDeInvalide = document.querySelectorAll(".selectPourListeQuiOntBesoinDeInvalide");
            if (to.name == 'ResultatRechercheLots') {
                if (document.querySelector("#navbarToggleRechercheAvancee")) {
                    document.querySelector("#navbarToggleRechercheAvancee").classList.remove("show");
                }
                next()
            }
            else {
                elementsAEffacer.forEach((element) => {
                    element.value = "";
                });
                listeDesSelects.forEach((select) => {
                    select.value = 0;
                });
                listeDesSelectsQuiOntBesoinDeInvalide.forEach((select) => {
                    select.value = "";
                });
                if (document.querySelectorAll(".inputAAfficher")) {
                    document.querySelectorAll(".inputAAfficher").forEach((el) => {
                        el.style.display = "none";
                    });
                }
                if (document.querySelector("#navbarToggleRechercheAvancee")) {
                    document.querySelector("#navbarToggleRechercheAvancee").classList.remove("show");
                }
                next()
            }
        }
        else if (from.name == 'ResultatRechercheEncans' || from.name == 'EncansPasses' || from.name == 'EncansFuturs' || from.name == 'TousLesEncans') {
            var elementsAEffacer = [
                document.querySelector("#rechercheEncansNumeroEncan"),
                document.querySelector("#rechercheEncansNumeroEncan2"),
            ]
            var datesAEffacer = [
                rechercheEncansDate1,
                rechercheEncansDate2,
            ]
            var listeDesSelects = document.querySelectorAll(".selectPourListe");
            if (to.name == 'ResultatRechercheEncans') {
                if (document.querySelector("#navbarToggleRechercheAvancee")) {
                    document.querySelector("#navbarToggleRechercheAvancee").classList.remove("show");
                }
                next()
            }
            else {
                elementsAEffacer.forEach((element) => {
                    element.value = "";
                });
                datesAEffacer.forEach((date) => {
                    date.value = "";
                });
                listeDesSelects.forEach((select) => {
                    select.value = 0;
                });
                if (document.querySelectorAll(".inputAAfficher")) {
                    document.querySelectorAll(".inputAAfficher").forEach((el) => {
                        el.style.display = "none";
                    });
                }
                if (document.querySelector("#navbarToggleRechercheAvancee")) {
                    document.querySelector("#navbarToggleRechercheAvancee").classList.remove("show");
                }
                next()
            }
        }
        else {
            if (document.querySelector("#navbarToggleRechercheAvancee")) {
                document.querySelector("#navbarToggleRechercheAvancee").classList.remove("show");
            }
            next()
        }
    })

    const desacDateFinEntre = computed(() => {
        if (rechercheEncansDate1.value) {
            const dateDebutDesac = new Date(rechercheEncansDate1.value);
            dateDebutDesac.setDate(dateDebutDesac.getDate() + 1);

            return [dateDebutDesac];
        }
        return [new Date()];
    });

    const desacDateDebutEntre = computed(() => {
        if (rechercheEncansDate2.value) {
            const dateFinDesac = new Date(rechercheEncansDate2.value);
            dateFinDesac.setDate(dateFinDesac.getDate() - 1);

            return [dateFinDesac];
        }
        return null;
    });

    async function rechercheAvanceeEncans() {
        var stringquery = {};
        var selectNumeroEncan = document.querySelector("#selectNumeroEncan").value;
        var stringNumeroEncan = document.querySelector(
            "#rechercheEncansNumeroEncan"
        ).value;
        var stringNumeroEncan2 = document.querySelector(
            "#rechercheEncansNumeroEncan2"
        ).value;
        var selectDate = document.querySelector(
            "#selectDate"
        ).value;
        var stringDate = moment(rechercheEncansDate1.value).format('yyyy-MM-DD');
        var stringDate2 = moment(rechercheEncansDate2.value).format('yyyy-MM-DD');

        if (stringNumeroEncan) {
            stringquery['selectNumeroEncan'] = selectNumeroEncan;
            stringquery['stringNumeroEncan'] = stringNumeroEncan;
            if (selectNumeroEncan == 1) {
                stringquery['stringNumeroEncan2'] = stringNumeroEncan2;
            }
        }
        if (rechercheEncansDate1.value) {
            var stringDate = moment(rechercheEncansDate1.value).format('yyyy-MM-DD');
            stringquery['selectDate'] = selectDate;
            stringquery['stringDate'] = stringDate;
            if (selectDate == 3 && rechercheEncansDate2.value) {
                var stringDate2 = moment(rechercheEncansDate2.value).format('yyyy-MM-DD');
                stringquery['stringDate2'] = stringDate2;
            }
        }

        router.push({
            path: "/resultatrechercheencans",
            query: {
                data: JSON.stringify(stringquery),
            },
        });
    }

    function affichageInputDate() {
        var inputAAfficher = document.querySelectorAll(".inputAAfficherDate");
        var selectDateEntre = document.querySelector(
            "#selectDateEntre"
        );

        if (selectDateEntre.selected) {
            inputAAfficher.forEach((el, index) => {
                el.style.display = "inline-block";
            });
        } else if (!selectDateEntre.selected) {
            inputAAfficher.forEach((el) => {
                el.style.display = "none";
            });
        }
    }

    // Définition de activationRecherche
    const activationRecherche = ref(false);
    const activationDropdownProfil = ref(false);
    const notification = ref(false);

    const deconnecter = async () => {
        await store.dispatch("logout");
        router.push("/"); // Redirige vers la page d'accueil après la déconnexion
    };

    onMounted(async () => {
        document.querySelectorAll(".dropdown-toggle").forEach((dropdownToggle) => {
            new bootstrap.Dropdown(dropdownToggle);
        });
        listeDesArtistes.value = await store.dispatch('obtenirArtistes');
        listeDesMediums.value = await store.dispatch('obtenirMediums');
        listeDesCategories.value = await store.dispatch('obtenirCategories');
    });
</script>
<style scoped>
    .ms-7 {
        margin-left: 7.9rem;
    }

    .start-79 {
        left: 79%;
    }

    .imgProfile {
        width: 40px;
        height: 40px;
    }

    input.form-control {
        height: 25px !important;
        font-size: 15px !important;
    }

    select {
        width: 100% !important;
    }

        select.form-select {
            height: 25px !important;
            margin-right: 10px !important;
            font-size: 15px !important;
        }

        select option {
            height: 25px !important;
            font-size: 15px !important;
        }

    recherchelabel {
        margin-left: 10px !important;
        margin-right: 10px !important;
        font-weight: bold !important;
    }

    .rechercheinput {
        margin-right: 10px !important;
    }

    .margesPourTable {
        padding-left: 15px;
        padding-right: 15px;
    }

    .aucunPaddingPourCarteLots {
        padding-left: 0px !important;
        padding-right: 0px !important;
        width: 1000px;
    }

    .aucunPaddingPourCarteEncans {
        padding-left: 0px !important;
        padding-right: 0px !important;
        width: 1100px;
    }

    .aucuneBarreDeRechercheAnvancee {
        min-height: 30px;
    }

    .selectwidth {
        width: 160px;
    }

    .inputAAfficher {
        display: none;
    }

    .card-body {
        padding-top: 0px !important;
        padding-bottom: 10px !important;
    }

    select,
    select option {
        color: #000000;
    }

        select:invalid,
        select option[value=""] {
            color: #999999;
        }

    .navbarRechercheAvancee {
        padding-top: 3px !important;
        padding-bottom: 2px !important;
        border: none !important;
    }

    .loupetoggler {
        padding-top: 0px !important;
        padding-bottom: 0px !important;
        padding-left: 0px !important;
        padding-right: 0px !important;
        width: 25px;
        height: 25px;
        /*background-image: none;*/
    }

    /*Pour faire changer l'icone du menu hamburger de la recherche*/
    .iconeloupe {
        position: relative !important;
        top: -1px !important;
        width: 20px !important;
        height: 20px !important;
        background-image: url("/icons/Recherche.png") !important;
    }
</style>
