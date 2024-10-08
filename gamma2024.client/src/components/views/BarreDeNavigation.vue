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
                            <router-link v-if="estConnecte && estClient" :to="{ name: 'Modification' }" class="text-decoration-none text-white d-flex align-items-center gap-3">
                                <a class="nav-link">{{ username }}</a>
                                <img :src="avatarUrl" alt="Avatar" height="40" />
                            </router-link>
                            <span v-if="estConnecte && !estClient" class="text-white d-flex align-items-center gap-3">
                                <span class="nav-link">{{ username }}</span>
                                <img :src="avatarUrl" alt="Avatar" height="40" />
                            </span>
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

const store = useStore()

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

</script>

<style scoped>
</style>