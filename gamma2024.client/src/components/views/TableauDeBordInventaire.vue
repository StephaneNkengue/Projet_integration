<template>
    <div class="pt-5">
        <h2>Liste des lots</h2>
        <div class="dropdown">
            <button class="btn btn-primary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                Sélectionner les colonnes
            </button>
            <ul class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                <li>
                    <input type="checkbox" id="lotIdCheckbox" checked onchange="basculerLaPresentationDeLaColonneLotId()">
                    <label class="form-check-label">
                        Id
                    </label>
                </li>
                <li>
                    <input type="checkbox" id="lotEncanCheckbox" checked>
                    <label class="form-check-label">
                        Encan
                    </label>
                </li>
                <li>
                    <input type="checkbox" id="lot#Checkbox" checked>
                    <label class="form-check-label">
                        Lot #
                    </label>
                </li>
                <li>
                    <input type="checkbox" id="lotPrixOuvertureCheckbox" checked>
                    <label class="form-check-label">
                        Prix Ouverture
                    </label>
                </li>
                <li>
                    <input type="checkbox" name="lotValeurMinPourVendre" value="lotValeurMinPourVendre" checked>
                    <label class="form-check-label">
                        Valeur Minimum Pour Vendre
                    </label>
                </li>
                <li>
                    <input type="checkbox" name="lotEstimationMin" value="lotEstimationMinimale" checked>
                    <label class="form-check-label">
                        Estimation Minimale
                    </label>
                </li>
                <li>
                    <input type="checkbox" name="lotEstimationMax" value="lotEstimationMax" checked>
                    <label class="form-check-label">
                        Estimation Maximale
                    </label>
                </li>
                <li>
                    <input type="checkbox" name="lotCategorie" value="lotCategorie" checked>
                    <label class="form-check-label">
                        Catégorie
                    </label>
                </li>
                <li>
                    <input type="checkbox" name="lotArtiste" value="lotArtiste" checked>
                    <label class="form-check-label">
                        Artiste
                    </label>
                </li>
                <li>
                    <input type="checkbox" name="lotDimension" value="lotDimension" checked>
                    <label class="form-check-label">
                        Dimension
                    </label>
                </li>
                <li>
                    <input type="checkbox" name="lotDescription" value="lotDescription" checked>
                    <label class="form-check-label">
                        Description
                    </label>
                </li>
                <li>
                    <input type="checkbox" name="lotMedium" value="lotMedium" checked>
                    <label class="form-check-label">
                        Medium
                    </label>
                </li>
                <li>
                    <input type="checkbox" name="lotLivraison" value="lotLivraison" checked>
                    <label class="form-check-label">
                        Livraison
                    </label>
                </li>
            </ul>
        </div>
        <div class="table-responsive">
            <table class="table table-striped" style="width: 100%;">
                <colgroup>
                    <col span="1" id="lotIdColonne" class="lotId"> <!--style="visibility:collapse;"-->
                    <col span="1" id="lotEncan" class="lotEncan">
                    <col span="1" id="lot# " class="lot#">
                    <col span="1" id="lotPrixOuverture" class="lotPrixOuverture">
                    <col span="1" id="lotValeurMinPourVendre" class="lotValeurMinPourVendre">
                    <col span="1" id="lotEstimationMin" class="lotEstimationMin">
                    <col span="1" id="lotEstimationMax" class="lotEstimationMax">
                    <col span="1" id="lotCategorie" class="lotCategorie">
                    <col span="1" id="lotArtiste" class="lotArtiste">
                    <col span="1" id="lotDimension" class="lotDimension">
                    <col span="1" id="lotDescription" class="lotDescription">
                    <col span="1" id="lotMedium" class="lotMedium">
                    <col span="1" id="lotLivraison" class="lotLivraison">
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
        function basculerLaPresentationDeLaColonneLotId() {
            if (this.isChecked) {
                document.querySelector("#lotIdColonne").style.visibility = "visible";
            }
            else { document.querySelector("#lotIdColonne").style.visibility = "collapse"; }
        }
    })

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