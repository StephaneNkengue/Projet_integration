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


                        </ul>

                        <div class="d-flex justify-content-center gap-3">
                            <router-link to="Inscription" v-if="!estConnecte">
                                <button class="btn btn-outline bleuMoyenFond text-white py-0 butttonNavBar btnSurvolerBleuMoyenFond" type="button">Inscription</button>
                            </router-link>
                            <router-link to="Connexion" v-if="!estConnecte">
                                <button class="btn btn-outline bleuMoyenFond text-white py-0 butttonNavBar btnSurvolerBleuMoyenFond" type="button">Connexion</button>
                            </router-link>
                            <div class="collapse navbar-collapse dropdown text-white" v-if="estAdmin">
                                <a class="nav-link dropdown-toggle" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    Tableau de bord
                                </a>
                                <ul class="dropdown-menu bleuMarinFond text-center">
                                    <li>
                                        <router-link to="Inventaire" class="text-decoration-none">
                                            <a class="dropdown-item contenuListeDropdown">Inventaire</a>
                                        </router-link>
                                    </li>
                                    <li>
                                        <router-link to="Accueil" class="text-decoration-none">
                                            <a class="dropdown-item contenuListeDropdown">Encans</a>
                                        </router-link>
                                    </li>
                                    <li>
                                        <router-link to="Accueil" class="text-decoration-none">
                                            <a class="dropdown-item contenuListeDropdown">Vendeurs</a>
                                        </router-link>
                                    </li>
                                    <li>
                                        <router-link to="Accueil" class="text-decoration-none">
                                            <a class="dropdown-item contenuListeDropdown">Ventes</a>
                                        </router-link>
                                    </li>
                                    <li>
                                        <router-link to="Accueil" class="text-decoration-none">
                                            <a class="dropdown-item contenuListeDropdown">Profils de membre</a>
                                        </router-link>
                                    </li>
                                </ul>
                            </div>


                            <router-link to="Accueil" v-if="estConnecte" class="text-decoration-none text-white d-flex align-items-center gap-3">
                                <a class="nav-link">USERNAME</a>
                                <img src="/icons/Avatar.png" alt="Avatar" height="40" />
                            </router-link>
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
    import { ref } from 'vue'

    const estConnecte = ref(true);
    const estAdmin = ref(true);
    const activationRecherche = ref(false);
</script>

<style scoped>
</style>