<template>
  <header class="sticky-top">
    <div>
      <nav
        class="navbar navbar-expand-md bleuMarinSecondaireFond py-0"
        data-bs-theme="dark"
      >
        <div class="container-fluid justify-content-between">
          <router-link :to="{ name: 'Accueil' }" class="text-decoration-none">
            <a class="navbar-brand d-flex align-items-center fs-6">
              <img
                src="/images/Logo.png"
                alt="Les Encans de Nantes"
                height="40"
              />
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

          <div
            class="collapse navbar-collapse justify-content-between"
            id="navbarSupportedContent"
          >
            <ul class="navbar-nav text-center">
              <li class="nav-item">
                <router-link
                  :to="{ name: 'Accueil' }"
                  class="text-decoration-none"
                >
                  <a class="nav-link active"> Accueil </a>
                </router-link>
              </li>

              <li class="nav-item">
                <router-link
                  :to="{ name: 'EncanPresent' }"
                  class="text-decoration-none"
                >
                  <a class="nav-link"> Encan courant </a>
                </router-link>
              </li>

              <li class="nav-item">
                <router-link
                  :to="{ name: 'TousLesEncans' }"
                  class="text-decoration-none"
                >
                  <a class="nav-link"> Tous les encans </a>
                </router-link>
              </li>
            </ul>

            <div class="d-flex justify-content-center gap-3">
              <router-link :to="{ name: 'Inscription' }" v-if="!estConnecte">
                <button
                  class="btn btn-outline bleuMoyenFond text-white py-0 butttonNavBar btnSurvolerBleuMoyenFond"
                  type="button"
                >
                  Inscription
                </button>
              </router-link>
              <router-link :to="{ name: 'Connexion' }" v-if="!estConnecte">
                <button
                  class="btn btn-outline bleuMoyenFond text-white py-0 butttonNavBar btnSurvolerBleuMoyenFond"
                  type="button"
                >
                  Connexion
                </button>
              </router-link>
              <div
                class="collapse navbar-collapse dropdown text-white"
                v-if="estAdmin"
              >
                <a
                  class="nav-link dropdown-toggle"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  Tableau de bord
                </a>
                <ul class="dropdown-menu bleuMarinFond text-center">
                  <li>
                    <router-link
                      :to="{ name: 'TableauDeBordInventaire' }"
                      class="text-decoration-none"
                    >
                      <a class="dropdown-item contenuListeDropdown"
                        >Inventaire</a
                      >
                    </router-link>
                  </li>
                  <!-- <li>
                    <router-link to="Inventaire" class="text-decoration-none">
                      <a class="dropdown-item contenuListeDropdown"
                        >Inventaire</a
                      >
                    </router-link>
                  </li> -->
                  <li>
                    <router-link
                      :to="{ name: 'TousLesEncans' }"
                      class="text-decoration-none"
                    >
                      <a class="dropdown-item contenuListeDropdown">Encans</a>
                    </router-link>
                  </li>
                  <li>
                    <router-link
                      :to="{ name: 'AffichageVendeurs' }"
                      class="text-decoration-none"
                    >
                      <a class="dropdown-item contenuListeDropdown">Vendeurs</a>
                    </router-link>
                  </li>
                  <li>
                    <router-link
                      :to="{ name: 'Accueil' }"
                      class="text-decoration-none"
                    >
                      <a class="dropdown-item contenuListeDropdown">Ventes</a>
                    </router-link>
                  </li>
                  <li>
                    <router-link
                      :to="{ name: 'GestionMembre' }"
                      class="text-decoration-none"
                    >
                      <a class="dropdown-item contenuListeDropdown"
                        >Profils de membre</a
                      >
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

              <div
                class="d-flex flex-column position-absolute top-100 start-79 dropdown-menu bleuMarinSecondaireFond"
                v-if="notification"
              >
                <router-link
                  v-for="index in 5"
                  :key="index"
                  :to="{ name: 'Accueil' }"
                  class="text-decoration-none text-white d-flex align-items-center gap-3"
                >
                  <a
                    class="dropdown-item text-white btnSurvolerBleuMoyenFond"
                    @click="notification = false"
                  >
                    test
                  </a>
                </router-link>
              </div>

              <div
                class="d-flex flex-column position-absolute top-100 end-0 dropdown-menu bleuMarinSecondaireFond"
                v-if="estConnecte && activationDropdownProfil"
              >
                <router-link
                  v-if="estClient"
                  :to="{ name: 'ModificationProfilUtilisateur' }"
                  class="text-decoration-none text-white d-flex align-items-center gap-3"
                >
                  <a
                    class="dropdown-item text-white btnSurvolerBleuMoyenFond"
                    @click="activationDropdownProfil = false"
                  >
                    Profil
                  </a>
                </router-link>
                <a
                  class="dropdown-item text-danger btnSurvolerBleuMoyenFond fw-bold"
                  href="#"
                  @click.prevent="deconnecter"
                >
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
                        <div class="d-flex" v-if="activationRecherche">
                            <input class="form-control me-3 butttonNavBar"
                                   data-bs-theme="light"
                                   type="search"
                                   placeholder="Faire une recherche"
                                   aria-label="Search" />

                            <router-link :to="{name:'Accueil'}">
                                <button class="btn btn-outline bleuMoyenFond me-3 text-white butttonNavBar py-0 btnSurvolerBleuMoyenFond"
                                        type="submit">
                                    Rechercher
                                </button>
                            </router-link>
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
    import { useRouter } from "vue-router";
    import { onMounted } from "vue";
    import * as bootstrap from "bootstrap";

    const store = useStore();
    const router = useRouter();

    const estConnecte = computed(() => store.state.isLoggedIn);
    const estAdmin = computed(() => store.getters.isAdmin);
    const estClient = computed(() => store.getters.isClient);
    const username = computed(() => store.getters.username);
    const avatarUrl = computed(() => store.getters.avatarUrl);

    const currentUser = ref(null);

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
