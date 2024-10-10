<template>
    <header class="sticky-top">
        <div>
            <nav class="navbar navbar-expand-md bleuMarinSecondaireFond py-0" data-bs-theme="dark">
                <div class="container-fluid justify-content-between">
                    <router-link to="Accueil" class="text-decoration-none">
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

                    <div class="collapse navbar-collapse justify-content-between" id="navbarSupportedContent">
                        <ul class="navbar-nav text-center">

                            <li class="nav-item">
                                <router-link to="Accueil" class="text-decoration-none">
                                    <a class="nav-link active">
                                        Accueil
                                    </a>
                                </router-link>
                            </li>

                            <li class="nav-item">
                                <router-link to="EncanPresent" class="text-decoration-none">
                                    <a class="nav-link">
                                        Encan courant
                                    </a>
                                </router-link>
                            </li>

                            <li class="nav-item">
                                <router-link to="TousLesEncans" class="text-decoration-none">
                                    <a class="nav-link">
                                        Tous les encans
                                    </a>
                                </router-link>
                            </li>

                            <li class="nav-item">
                                <router-link to="Accueil" class="text-decoration-none">
                                    <a class="nav-link">
                                        Encans passés
                                    </a>
                                </router-link>
                            </li>
                            <li class="nav-item d-md-none">
                                <router-link to="Accueil" class="text-decoration-none">
                                    <a class="nav-link">
                                        Déroulement d'un encan
                                    </a>
                                </router-link>
                            </li>
                            <li class="nav-item d-md-none" v-if="estAdmin">
                                <router-link to="Accueil" class="text-decoration-none">
                                    <a class="nav-link">
                                        Tableau de bord
                                    </a>
                                </router-link>
                            </li>
                            <!-- Ajoutez cet élément de menu pour l'administration -->
                            <li class="nav-item" v-if="estAdmin">
                                <router-link to="AffichageVendeurs" class="text-decoration-none">
                                    <a class="nav-link">
                                        Administration
                                    </a>
                                </router-link>
                            </li>
                        </ul>

                        <div class="d-flex justify-content-center gap-3">
                            <!--<router-link to="Inscription" v-if="estConnecte">-->
                            <router-link to="Inscription" v-if="!estConnecte">
                                <button class="btn btn-outline bleuMoyenFond text-white" type="button">Inscription</button>
                            </router-link>
                            <router-link to="Connexion" v-if="!estConnecte">
                                <button class="btn btn-outline bleuMoyenFond text-white" type="button">Connexion</button>
                            </router-link>
                            <div v-if="estConnecte" class="dropdown">
                                <a class="nav-link dropdown-toggle d-flex align-items-center gap-3" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    <span class="text-white">{{ username }}</span>
                                    <img :src="avatarUrl" alt="Avatar" height="40" class="rounded-circle" />
                                </a>
                                <ul class="dropdown-menu dropdown-menu-end custom-dropdown">
                                    <li v-if="estClient"><router-link class="dropdown-item" :to="{ name: 'Modification' }">Profil</router-link></li>
                                    <li v-if="estClient"><hr class="dropdown-divider"></li>
                                    <li><a class="dropdown-item" href="#" @click.prevent="deconnecter">Déconnexion</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </nav>

            <nav class="navbar bg-white navbar-expand-md py-0" data-bs-theme="dark">
                <div class="container-fluid justify-content-center justify-content-md-between d-flex flex-row-reverse ">
                    <form class="d-flex align-items-center">
                        <div class="d-flex" v-if="activationRecherche">
                            <input class="form-control me-3 butttonNavBar" data-bs-theme="light" type="search" placeholder="Faire une recherche" aria-label="Search">

                            <router-link to="Accueil">
                                <button class="btn btn-outline bleuMoyenFond me-3 text-white butttonNavBar py-0 btnSurvolerBleuMoyenFond" type="submit">Rechercher</button>
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
import { computed, watch, ref } from 'vue'
import { useStore } from 'vuex'
import { useRouter } from 'vue-router'

const store = useStore()
const router = useRouter()

const estConnecte = computed(() => store.state.isLoggedIn)
const estAdmin = computed(() => store.getters.isAdmin)
const estClient = computed(() => store.getters.isClient)
const username = computed(() => store.getters.username)
const avatarUrl = computed(() => store.getters.avatarUrl)

const currentUser = ref(null)

watch(() => store.state.user, (newUser) => {
    console.log("User mis à jour dans le store:", newUser)
    currentUser.value = newUser
}, { deep: true, immediate: true })

// Fonction pour rafraîchir les informations de l'utilisateur
const refreshUserInfo = async () => {
    if (estConnecte.value) {
        try {
            await store.dispatch('fetchClientInfo')
        } catch (error) {
            console.error("Erreur lors de la récupération des informations client:", error)
        }
    }
}

// Observer les changements dans l'état de connexion
watch(() => store.state.isLoggedIn, (newValue) => {
    if (newValue) {
        refreshUserInfo()
    }
})

// Définition de activationRecherche
const activationRecherche = ref(false)

const deconnecter = async () => {
    await store.dispatch('logout')
    router.push('/') // Redirige vers la page d'accueil après la déconnexion
}
</script>

<style scoped>
.dropdown-menu {
    min-width: 200px;
    padding: 0.5rem 0;
    margin: 0.125rem 0 0;
    font-size: 0.875rem;
    color: #333;
    text-align: left;
    background-color: #fff;
    background-clip: padding-box;
    border: 1px solid rgba(0,0,0,.15);
    border-radius: 0.25rem;
    box-shadow: 0 2px 10px rgba(0,0,0,.1);
}

.custom-dropdown {
    padding: 0;
}

.dropdown-item, .dropdown-item-text {
    display: block;
    width: 100%;
    padding: 0.75rem 1rem;
    clear: both;
    font-weight: 400;
    color: #333;
    text-align: inherit;
    white-space: nowrap;
    background-color: transparent;
    border: 0;
}

.dropdown-item:hover, .dropdown-item:focus {
    color: #16181b;
    text-decoration: none;
    background-color: #f8f9fa;
}

.dropdown-divider {
    height: 0;
    margin: 0.5rem 0;
    overflow: hidden;
    border-top: 1px solid #e9ecef;
}

.nav-link {
    color: #fff;
    text-decoration: none;
}

.nav-link:hover {
    color: #f8f9fa;
}
</style>