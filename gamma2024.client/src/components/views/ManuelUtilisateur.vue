<template>
    <div class="d-flex flex-column align-items-center pb-3 w-100">
        <div class="d-flex flex-column align-items-center w-100 col-md-6 fs-5">
            <div class="d-flex flex-column align-items-center col-6 col-sm-9 pb-4">
                <h1>Manuel de l'utilisateur</h1>

                <p class="justifyText">
                    Cette page est dédiée aux utilisateurs qui souhaitent participer aux enchères. Elle offre un mode d'emploi pour miser, faire des surenchères et participer à la soirée de clôture.
                </p>
            </div>

            <div class="w-100 px-5 mb-4">
                <div class="accordion" id="accordionCommentMiser">
                    <div class="accordion-item">
                        <h2 class="accordion-header">
                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseCommentMiser" aria-expanded="true" aria-controls="collapseCommentMiser">
                                <h2>Vous devez vous connecter pour miser</h2>
                            </button>
                        </h2>
                        <div id="collapseCommentMiser" class="accordion-collapse collapse" data-bs-parent="#accordionCommentMiser">
                            <div class="accordion-body">

                                <div class="w-100">
                                    <ol class="list list-group-numbered fs-5">
                                        <li class="list-group-item">
                                            Pour pouvoir faire des mises, vous devez être connecté.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex col-5 justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/ConnecterPourMiser.png" class="img-fluid" alt="Image qui affiche une notification qui indique qu'on doit se connecter pour pouvoir miser" />
                                                </div>
                                            </div>
                                        </li>
                                        <li class="list-group-item">
                                            Pour vous connectez, vous devez appyer sur bouton Connexion en haut à droite.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex col-5 justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/BoutonConnexion.png" class="img-fluid" alt="Image qui affiche le bouton de connexion" />
                                                </div>
                                            </div>
                                        </li>
                                        <li class="list-group-item">
                                            Entrez votre pseudonyme ou votre adresse courriel puis votre mot de passe. Ensuite, cliquez sur le bouton Connexion.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex col-5 justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/PageConnexion.png" class="img-fluid" alt="Image qui affiche la page de connexion" />
                                                </div>
                                            </div>
                                        </li>
                                        <li class="list-group-item">
                                            Si c'est la première fois que vous vous connectez, vous allez devoir entrer les informations de votre carte. Par la suite, vous serez rediriger vers l'encan courant. S'il y a un encan courant, vous allez voir les lots qui sont mis en enchères, sinon il y aura un message qui vous indiquera qu'il n'y a pas d'encan courant.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex  justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/EnregistrementCarte.png" class="img-fluid" alt="Image qui affiche la page d'ajout d'une carte" />
                                                </div>
                                            </div>
                                        </li>
                                    </ol>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

            <div class="w-100 px-5">
                <div class="accordion" id="accordionExample">
                    <div class="accordion-item">
                        <h2 class="accordion-header">
                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                <h2>Comment faire des mises</h2>
                            </button>
                        </h2>
                        <div id="collapseOne" class="accordion-collapse collapse" data-bs-parent="#accordionExample">
                            <div class="accordion-body">

                                <div class="w-100">
                                    <ol class="list list-group-numbered fs-5">
                                        <li class="list-group-item">
                                            Pour pouvoir faire des mises, vous devez être connecté.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex col-5 justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/ConnecterPourMiser.png" class="img-fluid" alt="Image qui affiche une notification qui indique qu'on doit se connecter pour pouvoir miser" />
                                                </div>
                                            </div>
                                        </li>
                                        <li class="list-group-item">
                                            Pour vous connectez, vous devez appyer sur bouton Connexion en haut à droite.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex col-5 justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/BoutonConnexion.png" class="img-fluid" alt="Image qui affiche le bouton de connexion" />
                                                </div>
                                            </div>
                                        </li>
                                        <li class="list-group-item">
                                            Entrez votre pseudonyme ou votre adresse courriel puis votre mot de passe. Ensuite, cliquez sur le bouton Connexion.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex col-5 justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/PageConnexion.png" class="img-fluid" alt="Image qui affiche la page de connexion" />
                                                </div>
                                            </div>
                                        </li>
                                        <li class="list-group-item">
                                            Si c'est la première fois que vous vous connectez, vous allez devoir entrer les informations de votre carte. Par la suite, vous serez rediriger vers l'encan courant. S'il y a un encan courant, vous allez voir les lots qui sont mis en enchères, sinon il y aura un message qui vous indiquera qu'il n'y a pas d'encan courant.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex  justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/EnregistrementCarte.png" class="img-fluid" alt="Image qui affiche la page d'ajout d'une carte" />
                                                </div>
                                            </div>
                                        </li>
                                    </ol>
                                </div>
                            </div>
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
    .accordion-button {
        background-color: transparent;
    }
</style>