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
                            <li class="nav-item">
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

                            <li class="nav-item">
                                <router-link :to="{ name: 'TousLesLots' }"
                                             class="text-decoration-none">
                                    <a class="nav-link"> Tous les lots </a>
                                </router-link>
                            </li>
                        </ul>

                        <div class="d-flex justify-content-center gap-3 mb-2 mb-lg-0">
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
                            <div class="collapse navbar-collapse dropdown text-white"
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
                                        <router-link :to="{ name: 'Accueil' }"
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

                            <a v-if="estConnecte"
                               @click="notification = !notification"
                               class="d-flex align-items-center">
                                <img src="/icons/IconeCloche.png"
                                     alt="Icon cloche"
                                     height="25" />
                            </a>

                            <div class="d-flex flex-column position-absolute top-100 start-79 dropdown-menu bleuMarinSecondaireFond"
                                 v-if="notification">
                                <router-link v-for="index in 5"
                                             :key="index"
                                             :to="{ name: 'Accueil' }"
                                             class="text-decoration-none text-white d-flex align-items-center gap-3">
                                    <a class="dropdown-item text-white btnSurvolerBleuMoyenFond"
                                       @click="notification = false">
                                        test
                                    </a>
                                </router-link>
                            </div>

                            <div class="d-flex flex-column position-absolute top-100 end-0 dropdown-menu bleuMarinSecondaireFond"
                                 v-if="estConnecte && activationDropdownProfil">
                                <router-link v-if="estClient"
                                             :to="{ name: 'ModificationProfilUtilisateur' }"
                                             class="text-decoration-none text-white d-flex align-items-center gap-3">
                                    <a class="dropdown-item text-white btnSurvolerBleuMoyenFond"
                                       @click="activationDropdownProfil = false">
                                        Profil
                                    </a>
                                </router-link>
                                <a class="dropdown-item text-danger btnSurvolerBleuMoyenFond fw-bold"
                                   href="#"
                                   @click.prevent="deconnecter">
                                    Déconnexion
                                </a>
                            </div>

                            <a @click="activationDropdownProfil = !activationDropdownProfil"
                               class="d-flex text-decoration-none text-white align-items-center gap-3"
                               v-if="estConnecte">
                                <p class="m-0">{{ username }}</p>
                                <img :src="avatarUrl"
                                     alt="Avatar"
                                     class="imgProfile rounded-circle" />
                            </a>
                        </div>
                    </div>
                </div>
            </nav>

            <nav class="navbar bg-white navbar-expand-md py-0" data-bs-theme="dark">
                <div class="container-fluid justify-content-center justify-content-md-between d-flex flex-row-reverse">
                    <form class="d-flex align-items-center">
                        <div class="d-flex input-group mt-1" v-if="activationRecherche">
                            <button class="btn btn-outline bleuMoyenFond text-white butttonNavBar py-0 btnSurvolerBleuMoyenFond dropdown-toggle"
                                    data-bs-toggle="dropdown"
                                    v-if="$route.name == 'EncanPresent' || $route.name == 'Encan'">
                                Recherche Avancée
                            </button>
                            <ul class="dropdown-menu"
                                v-if="$route.name == 'EncanPresent' || $route.name == 'Encan'">
                                <li class="d-flex justify-content-start dropdown-item">
                                    <input class="checkboxTousRecherche d-flex-1"
                                           type="checkbox"
                                           id="tousSelectionnerCheckboxRecherche"
                                           @change="comportementTousSelectionnerRecherche"
                                           checked />
                                    <label class="d-flex-1"
                                           for="tousSelectionnerCheckboxRecherche">
                                        Tous Sélectionner
                                    </label>
                                </li>
                                <li v-for="(visible, colonne) in colonnesDeCriteresLots"
                                    :key="colonne"
                                    class="d-flex justify-content-start dropdown-item">
                                    <input class="checkboxSeulRecherche d-flex-1"
                                           type="checkbox"
                                           :id="`lot${
                      colonne.charAt(0).toUpperCase() + colonne.slice(1)
                    }CheckboxRecherche`"
                                           checked
                                           disabled />
                                    <label class="d-flex-1"
                                           :for="`lot${
                      colonne.charAt(0).toUpperCase() + colonne.slice(1)
                    }CheckboxRecherche`">
                                        {{
                      (
                        colonne.charAt(0).toUpperCase() + colonne.slice(1)
                      ).replace(/([A-Z])/g, " $1")
                                        }}
                                    </label>
                                </li>
                            </ul>
                            <button class="btn btn-outline bleuMoyenFond text-white butttonNavBar py-0 btnSurvolerBleuMoyenFond dropdown-toggle"
                                    data-bs-toggle="dropdown"
                                    v-if="
                  $route.name == 'TousLesEncans' ||
                  $route.name == 'EncansPasses' ||
                  $route.name == 'EncansFuturs'
                ">
                                Recherche Avancée
                            </button>
                            <ul class="dropdown-menu"
                                v-if="
                  $route.name == 'TousLesEncans' ||
                  $route.name == 'EncansPasses' ||
                  $route.name == 'EncansFuturs'
                ">
                                <li class="d-flex justify-content-start dropdown-item">
                                    <input class="checkboxTousRecherche d-flex-1"
                                           type="checkbox"
                                           id="tousSelectionnerCheckboxRecherche"
                                           @change="comportementTousSelectionnerRecherche"
                                           checked />
                                    <label class="d-flex-1"
                                           for="tousSelectionnerCheckboxRecherche">
                                        Tous Sélectionner
                                    </label>
                                </li>
                                <li v-for="(visible, colonne) in colonnesDeCriteresEncans"
                                    :key="colonne"
                                    class="d-flex justify-content-start dropdown-item">
                                    <input class="checkboxSeulRecherche d-flex-1"
                                           type="checkbox"
                                           :id="`lot${
                      colonne.charAt(0).toUpperCase() + colonne.slice(1)
                    }CheckboxRecherche`"
                                           checked
                                           disabled />
                                    <label class="d-flex-1"
                                           :for="`lot${
                      colonne.charAt(0).toUpperCase() + colonne.slice(1)
                    }CheckboxRecherche`">
                                        {{
                      (
                        colonne.charAt(0).toUpperCase() + colonne.slice(1)
                      ).replace(/([A-Z])/g, " $1")
                                        }}
                                    </label>
                                </li>
                            </ul>
                            <input class="form-control butttonNavBar"
                                   id="rechercheInput"
                                   data-bs-theme="light"
                                   type="search"
                                   placeholder="Faire une recherche"
                                   aria-label="Search"
                                   v-model="stringDeRecherche" />
                            <button class="btn bleuMoyenFond me-3 text-white butttonNavBar py-0 btnSurvolerBleuMoyenFond"
                                    v-if="$route.name == 'EncanPresent' || $route.name == 'Encan'"
                                    type="submit"
                                    @click="rechercheAvanceeLots">
                                Rechercher
                            </button>
                            <button class="btn bleuMoyenFond me-3 text-white butttonNavBar py-0 btnSurvolerBleuMoyenFond"
                                    v-else-if="
                  $route.name == 'TousLesEncans' ||
                  $route.name == 'EncansPasses' ||
                  $route.name == 'EncansFuturs'
                "
                                    type="submit"
                                    @click="rechercheAvanceeEncans">
                                Rechercher
                            </button>
                            <button class="btn bleuMoyenFond me-3 text-white butttonNavBar py-0 btnSurvolerBleuMoyenFond"
                                    v-else
                                    type="submit"
                                    @click="rechercheSimple">
                                Rechercher
                            </button>
                        </div>
                        <a @click="activationRecherche = !activationRecherche">
                            <img src="/icons/IconeRechercheAvanceeBleu.png"
                                 alt="Icon recherche avancée"
                                 height="30"
                                 class="my-1" />
                        </a>
                    </form>
                </div>
            </nav>
        </div>
    </header>
</template>

<script setup>
    import { computed, watch, ref } from "vue";
    import { useStore } from "vuex";
    import { useRouter, useRoute } from "vue-router";
    import { onMounted } from "vue";

    const store = useStore();
    const router = useRouter();
    const route = useRoute();

    const estConnecte = computed(() => store.state.isLoggedIn);
    const estAdmin = computed(() => store.getters.isAdmin);
    const estClient = computed(() => store.getters.isClient);
    const username = computed(() => store.getters.username);
    const avatarUrl = computed(() => store.getters.avatarUrl);

    const currentUser = ref(null);

    const colonnesDeCriteresLots = ref({
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
    });

    const colonnesDeCriteresEncans = ref({
        numero: true,
        date: true,
    });

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

    function rechercheSimple(e) {
        e.preventDefault();
        var texte = document.querySelector("#rechercheInput").value;
        window.find(texte, null, null, true);
    }

    async function rechercheAvanceeLots() {
        var informationsDeRecherche = {
            stringDeRecherche: document.querySelector("#rechercheInput").value,
            encan: document.getElementById("lotEncanCheckboxRecherche").checked,
            numero: document.getElementById("lotNumeroCheckboxRecherche").checked,
            prixOuverture: document.getElementById("lotPrixOuvertureCheckboxRecherche")
                .checked,
            valeurMinPourVente: document.getElementById(
                "lotValeurMinPourVenteCheckboxRecherche"
            ).checked,
            estimationMin: document.getElementById("lotEstimationMinCheckboxRecherche")
                .checked,
            estimationMax: document.getElementById("lotEstimationMaxCheckboxRecherche")
                .checked,
            categorie: document.getElementById("lotCategorieCheckboxRecherche").checked,
            artiste: document.getElementById("lotArtisteCheckboxRecherche").checked,
            dimension: document.getElementById("lotDimensionCheckboxRecherche").checked,
            description: document.getElementById("lotDescriptionCheckboxRecherche")
                .checked,
            medium: document.getElementById("lotMediumCheckboxRecherche").checked,
        };
        router.push({
            path: "/resultatrecherchelots",
            query: { data: JSON.stringify(informationsDeRecherche) },
        });
    }

    async function rechercheAvanceeEncans() {
        var informationsDeRecherche = {
            stringDeRecherche: document.querySelector("#rechercheInput").value,
            numero: document.getElementById("lotNumeroCheckboxRecherche").checked,
            date: document.getElementById("lotDateCheckboxRecherche").checked,
        };
        router.push({
            path: "/resultatrechercheencans",
            query: { data: JSON.stringify(informationsDeRecherche) },
        });
    }

    function comportementTousSelectionnerRecherche() {
        var CheckboxeToutSelectionnerRecherche = document.querySelector(
            ".checkboxTousRecherche"
        );
        var listeDesCheckboxesRecherche = document.querySelectorAll(
            ".checkboxSeulRecherche"
        );

        if (CheckboxeToutSelectionnerRecherche.checked) {
            listeDesCheckboxesRecherche.forEach((el, index) => {
                el.checked = true;
                el.disabled = true;
            });
        } else if (!CheckboxeToutSelectionnerRecherche.checked) {
            listeDesCheckboxesRecherche.forEach((el) => {
                el.disabled = false;
                el.checked = false;
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

    onMounted(() => {
        document.querySelectorAll(".dropdown-toggle").forEach((dropdownToggle) => {
            new bootstrap.Dropdown(dropdownToggle);
        });
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
</style>
