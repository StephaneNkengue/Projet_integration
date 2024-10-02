<template>
    <div class="pt-5">
        <div class="d-flex justify-content-between">
            <h2 class="d-flex-1">Liste des lots</h2>
            <div class="dropdown d-flex-1">
                <button class="btn bleuMoyenFond btnSurvolerBleuMoyenFond text-white dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Sélectionner les colonnes
                </button>
                <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                    <li>
                        <input type="checkbox" id="lotIdCheckbox" checked>
                        <label class="form-check-label" for="lotIdCheckbox">
                            Id
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotEncanCheckbox" checked>
                        <label class="form-check-label" for="lotEncanCheckbox">
                            Encan
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotNumeroCheckbox" checked>
                        <label class="form-check-label" for="lotNumeroCheckbox">
                            Lot #
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotPrixOuvertureCheckbox" checked>
                        <label class="form-check-label" for="lotPrixOuvertureCheckbox">
                            Prix Ouverture
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotValeurMinPourVendreCheckbox" checked>
                        <label class="form-check-label" for="lotValeurMinPourVendreCheckbox">
                            Valeur Minimum Pour Vendre
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotEstimationMinCheckbox" checked>
                        <label class="form-check-label" for="lotEstimationMinCheckbox">
                            Estimation Minimale
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotEstimationMaxCheckbox" checked>
                        <label class="form-check-label" for="lotEstimationMaxCheckbox">
                            Estimation Maximale
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotCategorieCheckbox" checked>
                        <label class="form-check-label" for="lotCategorieCheckbox">
                            Catégorie
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotArtisteCheckbox" checked>
                        <label class="form-check-label" for="lotArtisteCheckbox">
                            Artiste
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotDimensionCheckbox" checked>
                        <label class="form-check-label" for="lotDimensionCheckbox">
                            Dimension
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotDescriptionCheckbox" checked>
                        <label class="form-check-label" for="lotDimensionCheckbox">
                            Description
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotMediumCheckbox" checked>
                        <label class="form-check-label" for="lotMediumCheckbox">
                            Medium
                        </label>
                    </li>
                    <li>
                        <input type="checkbox" id="lotLivraisonCheckbox" checked>
                        <label class="form-check-label" for="lotLivraisonCheckbox">
                            Livraison
                        </label>
                    </li>
                </ul>
            </div>
        </div>
        <div class="d-flex">
            <div class="table-responsive d-flex-1 d-flex flex-column">
                <table class="table table-striped flex-grow-1 w-100">
                    <colgroup>
                        <col span="1" id="lotIdColonne"> <!--style="visibility:collapse;"-->
                        <col span="1" id="lotEncanColonne">
                        <col span="1" id="lotNumeroColonne">
                        <col span="1" id="lotPrixOuvertureColonne">
                        <col span="1" id="lotValeurMinPourVendreColonne">
                        <col span="1" id="lotEstimationMinColonne">
                        <col span="1" id="lotEstimationMaxColonne">
                        <col span="1" id="lotCategorieColonne">
                        <col span="1" id="lotArtisteColonne">
                        <col span="1" id="lotDimensionColonne">
                        <col span="1" id="lotDescriptionColonne">
                        <col span="1" id="lotMediumColonne">
                        <col span="1" id="lotLivraisonColonne">
                    </colgroup>
                    <thead>
                        <tr>
                            <th class="lotId">Lot Id</th>
                            <th class="lotEncan">Encan</th>
                            <th class="lotNumero">Lot #</th>
                            <th class="lotPrixOuverture">Prix Ouverture</th>
                            <th class="lotValeurMinPourVendre">Valeur Min Pour Vente</th>
                            <th class="lotEstimationMin">Estimation Min</th>
                            <th class="lotEstimationMax">Estimation Max</th>
                            <th class="lotCategorie">Categorie</th>
                            <th class="lotArtiste">Artiste</th>
                            <th class="lotDimension">Dimension (en po)</th>
                            <th class="lotDescription">Description</th>
                            <th class="lotMedium">Medium</th>
                            <th class="lotLivraison">Livraison</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td class="lotId">1</td>
                            <td class="lotEncan">232</td>
                            <td class="lotNumero">1</td>
                            <td class="lotPrixOuverture">200$</td>
                            <td class="lotValeurMinPourVendre"></td>
                            <td class="lotEstimationMin">600$</td>
                            <td class="lotEstimationMax">800$</td>
                            <td class="lotCategorie">Art-Paintings</td>
                            <td class="lotArtiste">Guy Paquet</td>
                            <td class="lotDimension">12 x 16</td>
                            <td class="lotDescription">1984/La nouvelle pratique</td>
                            <td class="lotMedium">Huile sur panneau</td>
                            <td class="lotLivraison">oui</td>
                        </tr>
                        <tr>
                            <td class="lotId">2</td>
                            <td class="lotEncan">232</td>
                            <td class="lotNumero">1a</td>
                            <td class="lotPrixOuverture">2200$</td>
                            <td class="lotValeurMinPourVendre">3800$</td>
                            <td class="lotEstimationMin">3500$</td>
                            <td class="lotEstimationMax">5000$</td>
                            <td class="lotCategorie">Art-Paintings</td>
                            <td class="lotArtiste">Vladimir Horik</td>
                            <td class="lotDimension">20 x 30</td>
                            <td class="lotDescription">La messe du soir</td>
                            <td class="lotMedium">Huile sur panneau</td>
                            <td class="lotLivraison">oui</td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <!--<div class="d-flex d-flex-1" style="background-color: red;">
                <div class="d-flex-1 bleuMoyenFond ongletEncans">
                    <button class="btn d-flex-1 bleuMoyenFond btnSurvolerBleuMoyenFond text-white boutonOngletEncans" type="button">
                        Encans
                    </button>
                </div>
            </div>-->
        </div>
    </div>
</template>

<style>
    table {
        border-collapse: collapse;
        margin-bottom: 50px;
        font-size: 15px;
        text-align: center;
    }

    table, th, td {
        border: 1px solid black;
    }

    th, td {
        padding: 5px;
    }

    th {
        font-weight: bold;
    }

    col.cacherColonne {
        visibility: collapse;
    }

    /*.ongletEncans {
        height: 200px;
        width: 100px;
    }

    .boutonOngletEncans {
        position: absolute;
        left: 10px;
        top: 50px;
        border-top-left-radius: 50px;
        border-top-right-radius: 50px;
        height: 40px;
        width: 180px;
        transform: rotate(-90deg);
    }*/
</style>

<script setup>
    import { onMounted } from 'vue';

    onMounted(() => {
        document.querySelector("#lotIdCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotIdColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotEncanCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotEncanColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotNumeroCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotNumeroColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotPrixOuvertureCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotPrixOuvertureColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotValeurMinPourVendreCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotValeurMinPourVendreColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotEstimationMinCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotEstimationMinColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotEstimationMaxCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotEstimationMaxColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotCategorieCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotCategorieColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotArtisteCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotArtisteColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotDimensionCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotDimensionColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotDescriptionCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotDescriptionColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotMediumCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotMediumColonne").classList.toggle("cacherColonne");
            })
        document.querySelector("#lotLivraisonCheckbox")
            .addEventListener("change", function () {
                document.querySelector("#lotLivraisonColonne").classList.toggle("cacherColonne");
            })
    })

    //const listeDesCheckboxes = document.querySelector("input");
    //const listeDesColonnes = document.querySelector("col");

    //listeDesCheckboxes.forEach(attribuerLesCheckboxesAuxColonnes);
    //function attribuerLesCheckboxesAuxColonnes(item, index) {
    //    item.addEventListener("change", function () { basculerLaPresentationDeLaColonne(index) })
    //}
    //function basculerLaPresentationDeLaColonne(index) {
    //    listeDesColonnes[index].classList.toggle("cacherColonne");
    //}
</script>