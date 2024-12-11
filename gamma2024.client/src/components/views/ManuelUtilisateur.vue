<template>
    <div class="d-flex flex-column align-items-center pb-3 w-100">
        <div class="d-flex flex-column align-items-center w-100 col-md-6 fs-5">
            <div class="d-flex flex-column align-items-center col-6 col-sm-9 pb-4">
                <h1>Manuel de l'utilisateur</h1>

                <p class="justifyText">
                    Cette page est dédiée aux utilisateurs qui souhaitent participer aux enchères. Elle offre un mode d'emploi pour miser, faire des surenchères et participer à la soirée de clôture.
                </p>
            </div>

            <div class="w-100 px-5">
                <div class="accordion" id="accordionCommentMiser">
                    <div class="accordion-item">
                        <h2 class="accordion-header">
                            <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseCommentMiser" aria-expanded="true" aria-controls="collapseCommentMiser">
                                <h2>Comment faire des mises</h2>
                            </button>
                        </h2>
                        <div id="collapseCommentMiser" class="accordion-collapse collapse" data-bs-parent="#accordionCommentMiser">
                            <div class="accordion-body">

                                <div class="w-100">
                                    <ol class="list list-group-numbered fs-5 justifyText">
                                        <li class="list-group-item">
                                            Pour faire une mise, tout d'abord, vous devez être connecté. Par la suite, vous devez cliquer sur le bouton Miser. Une fenêtre contextuelle va apparaître. Dans celle-ci, vous pouvez faire une enchère en cliquant sur le bouton Miser. Cette mise a une valeur prédéterminée car il faut respecter les pas de l'enchère. Le pas de l'enchère est le prix minimum pour une nouvelle enchère. Veuillez-vous référer à la page des Conditions de la compagnie sous la Majoration de l'enchère pour la valeur du pas selon la valeur du lot.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex col-5 justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/CliquerSurMiser.png" class="img-fluid" alt="Image qui affiche un lot avec une souris qui clique sur le bouton miser" />

                                                    <img src="/images/ImageManuelUtilisateur/MiseReguliere.png" class="img-fluid" alt="Image qui affiche une fenêtre contextuelle pour les mises" />
                                                </div>
                                            </div>
                                        </li>
                                        <li class="list-group-item">
                                            Lorsque vous voyez que le lot est entouré d'une couleur verte, cela veut dire que vous avez la plus haute mise. De plus, vous allez voir une notification de succès s'affichée en haut à droite de la page.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex col-5 justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/MiseReguliereEffectue.png" class="img-fluid" alt="Image qui affiche un lot dont un membre a miser dessus" />
                                                </div>
                                            </div>
                                        </li>
                                        <li class="list-group-item">
                                            Lorsque vous voyez que le lot est entouré d'une couleur rouge, cela veut dire que votre mise a été dépassée. De plus, vous allez voir une notification s'affichée en haut à droite de la page où se trouve une petite cloche pour vous avisez que quelqu'un vous a dépasser sur le lot dont vous avez miser.
                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex col-5 justify-content-center">
                                                    <img src="/images/ImageManuelUtilisateur/MiseReguliereDepasse.png" class="img-fluid" alt="Image qui affiche la page de connexion" />
                                                </div>
                                            </div>
                                        </li>
                                        <li class="list-group-item">

                                            <div class="w-100 d-flex justify-content-center">
                                                <div class="d-flex  justify-content-center">
                                                    <!--<img src="/images/ImageManuelUtilisateur/EnregistrementCarte.png" class="img-fluid" alt="Image qui affiche la page d'ajout d'une carte" />-->
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