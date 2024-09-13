<template>
    <div class="bg-image pt-5" style="background-image: url('/public/images/DessinGris.PNG'); background-size:cover; background-position:0px -100px;position: fixed; height: 100vh; width: 100%;">
        <div class="container d-flex flex-column justify-content-start align-items-stretch bg-white bg-opacity-75" style="border-radius:15px; overflow: hidden; height: 450px; width: 410px;">
            <h2 class="fs-1 text-center fw-bold pt-4">Connexion</h2>
            <p class="text-center">Se connecter pour continuer</p>

            <div class="d-flex flex-column justify-content-start align-items-stretch">
                <div class="px-4 py-2 mb-4">
                    <div class="d-flex flex-row justify-content-center mb-3">
                        <div class="form-group" style="width: 90%;">
                            <label for="nomutilisateur" class="fw-bold ms-3">Nom d'utilisateur</label>
                            <input type="text" class="form-control" id="nomutilisateur" />
                        </div>
                    </div>
                    <div class="d-flex flex-row justify-content-center mb-3">
                        <div class="form-group" style="width: 90%;">
                            <label for="motdepasse" class="fw-bold ms-3">Mot de passe</label>
                            <input type="text" class="form-control" id="motdepasse" />
                        </div>
                    </div>
                    <div class="d-flex flex-row justify-content-center mt-4 mb-1">
                        <div class="form-group" style="width: 80%;">
                            <p class="text-center">J'ai oublié mon Mot de passe</p>
                        </div>
                    </div>
                    <div class="d-flex flex-row justify-content-center mb-2">
                        <button class="btn rounded-pill px-5 bleuMoyenFond text-white" @click="submitForm">Connexion</button>
                    </div>
                    <div class="d-flex flex-row justify-content-center mb-3">
                        <div class="form-group" style="width: 80%;">
                            <p class="fs-5 text-center fw-bold">M'inscrire</p>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>
</template>
<script setup>

</script>
