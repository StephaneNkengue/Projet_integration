<template>
    <div class="bg-image pt-5 imageDeFondEsquise">
        <div class="container d-flex flex-column justify-content-start align-items-stretch bg-white bg-opacity-75 cadreBlanc">
            <h2 class="fs-1 text-center fw-bold pt-4">Connexion</h2>
            <p class="text-center">Se connecter pour continuer</p>

            <div class="d-flex flex-column justify-content-start align-items-stretch">
                <div class="px-4 py-2 mb-4">
                    <div class="d-flex flex-row justify-content-center mb-3">
                        <div class="form-group w-90">
                            <label for="nomutilisateur" class="fw-bold ms-3">Nom d'utilisateur</label>
                            <input type="text" class="form-control" id="nomutilisateur" />
                        </div>
                    </div>
                    <div class="d-flex flex-row justify-content-center mb-3">
                        <div class="form-group w-90">
                            <label for="motdepasse" class="fw-bold ms-3">Mot de passe</label>
                            <input type="text" class="form-control" id="motdepasse" />
                        </div>
                    </div>
                    <div class="d-flex flex-row justify-content-center mt-4 mb-1">
                        <div class="form-group w-80">
                            <router-link to="/" class="text-decoration-none">
                                <p class="text-center lienEnTexteNoir">J'ai oublié mon Mot de passe</p>
                            </router-link>
                        </div>
                    </div>
                    <div class="d-flex flex-row justify-content-center mb-2">
                        <router-link to="/">
                            <button class="btn btnSurvolerBleuMoyenFond rounded-pill px-5 bleuMoyenFond text-white" @click="submitForm">Connexion</button>
                        </router-link>
                    </div>
                    <div class="d-flex flex-row justify-content-center mb-3">
                        <div class="form-group w-80">
                            <router-link to="/" class="text-decoration-none">
                                <p class="fs-5 text-center fw-bold lienEnTexteNoir">M'inscrire</p>
                            </router-link>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
</template>
<script setup>

</script>
<style scoped>
    .cadreBlanc {
        border-radius: 15px;
        overflow: hidden;
        height: 450px;
        width: 410px;
    }

    .w-80 {
        width: 80%;
    }

    .w-90 {
        width: 90%;
    }
</style>
