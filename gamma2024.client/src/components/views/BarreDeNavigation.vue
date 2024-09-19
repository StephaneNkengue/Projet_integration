<template>
    <header class="sticky-top">
        <div>
            <nav class="navbar navbar-expand-md bleuMarinFond" data-bs-theme="dark">
                <div class="container-fluid justify-content-between">
                    <router-link to="Accueil" class="text-decoration-none">
                        <a class="navbar-brand d-flex ">
                            <img src="/images/Logo.png"
                                 alt="Les Encans de Nantes"
                                 height="60" />
                            Les Encans <br /> de Nantes
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
                                <router-link to="Accueil" class="text-decoration-none">
                                    <a class="nav-link">
                                        Encan présent
                                    </a>
                                </router-link>
                            </li>

                            <li class="nav-item">
                                <router-link to="Accueil" class="text-decoration-none">
                                    <a class="nav-link">
                                        Encans futurs
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

                        </ul>

                        <div class="d-flex justify-content-center gap-3">
                            <router-link to="Inscription">
                                <button class="btn btn-outline bleuMoyenFond text-white" type="button">Inscription</button>
                            </router-link>
                            <router-link to="Connexion">
                                <button class="btn btn-outline bleuMoyenFond text-white" type="button">Connexion</button>
                            </router-link>
                        </div>
                    </div>
                </div>
            </nav>

            <nav class="navbar navbar-expand-md bleuMarinSecondaireFond" data-bs-theme="dark">
                <div class="container-fluid justify-content-center justify-content-md-between">
                    <ul class="navbar-nav d-none d-md-block">
                        <li class="nav-item">
                            <router-link to="Accueil" class="text-decoration-none">
                                <a class="nav-link">
                                    Déroulement d'un encan
                                </a>
                            </router-link>
                        </li>
                    </ul>

                    <form class="d-flex">
                        <input class="form-control me-3" data-bs-theme="light" type="search" placeholder="Faire une recherche" aria-label="Search">
                        <router-link to="Accueil">
                            <button class="btn btn-outline bleuMoyenFond me-3 text-white" type="submit">Rechercher</button>
                        </router-link>

                        <router-link to="Accueil">
                            <img src="/icons/IconeRechercheAvanceeBleu.png"
                                 alt="Icon recherche avancée"
                                 height="38" />
                        </router-link>
                    </form>
                </div>
            </nav>
        </div>
    </header>
</template>

<script setup>
</script>

<style scoped></style>