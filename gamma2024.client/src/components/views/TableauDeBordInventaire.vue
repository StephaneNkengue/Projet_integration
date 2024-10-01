<template>
    <div class="pt-5">
        <h2>Liste des lots</h2>
        <div class="dropdown">
            <button class="btn btn-primary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
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
                    <input type="checkbox" id="lot#Checkbox" checked>
                    <label class="form-check-label" for="lot#Checkbox">
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
        <div>
            <table class="table table-striped" style="width: 100%;">
                <colgroup>
                    <col span="1" id="lotIdColonne" class="lotId"> <!--style="visibility:collapse;"-->
                    <col span="1" id="lotEncanColonne" class="lotEncan">
                    <col span="1" id="lot#Colonne" class="lot#">
                    <col span="1" id="lotPrixOuvertureColonne" class="lotPrixOuverture">
                    <col span="1" id="lotValeurMinPourVendreColonne" class="lotValeurMinPourVendre">
                    <col span="1" id="lotEstimationMinColonne" class="lotEstimationMin">
                    <col span="1" id="lotEstimationMaxColonne" class="lotEstimationMax">
                    <col span="1" id="lotCategorieColonne" class="lotCategorie">
                    <col span="1" id="lotArtisteColonne" class="lotArtiste">
                    <col span="1" id="lotDimensionColonne" class="lotDimension">
                    <col span="1" id="lotDescriptionColonne" class="lotDescription">
                    <col span="1" id="lotMediumColonne" class="lotMedium">
                    <col span="1" id="lotLivraisonColonne" class="lotLivraison">
                </colgroup>
                <thead>
                    <tr>
                        <th class="lotId">Lot Id</th>
                        <th class="lotEncan">Encan</th>
                        <th class="lot#">Lot #</th>
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
                        <td class="lot#">1</td>
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
                        <td class="lot#">1a</td>
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
    </div>
</template>

<style>
    table {
        border-collapse: collapse;
        margin-bottom: 50px;
    }

    table, th, td {
        border: 1px solid black;
    }

    th, td {
        display: none;
        padding: 5px;
    }
</style>

<script setup>
    import { onMounted } from 'vue';

    onMounted(() => {
        document.querySelector("#lotIdCheckbox").addEventListener("change", basculerLaPresentationDeLaColonneLotId)
    })

    function basculerLaPresentationDeLaColonneLotId() {
        if (this.isChecked) {
            document.querySelector("#lotIdColonne").style.visibility = "visible";
        }
        else { document.querySelector("#lotIdColonne").style.visibility = "collapse"; }
    }

    //const listeDesCheckboxes = document.getElementByType("checkbox");
    //const listeDesColonnes = document.getElementByType("col");

    //function attribuerCheckboxesAuxColonnes(itemCheckbox, index) {
    //document.querySelector("#lotIdCheckbox").addEventListener("change", () => {
    //    if (this.isChecked) {
    //        document.querySelector("#lotIdColonne").style.visibility = "visible";
    //    }
    //    else { document.querySelector("#lotIdColonne").style.visibility = "collapse"; }
    //})
    //}

    //listeDesCheckboxes.forEach(attribuerCheckboxesAuxColonnes);
</script>