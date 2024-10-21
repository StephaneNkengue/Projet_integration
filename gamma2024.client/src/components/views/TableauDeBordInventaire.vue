<template>
    <div class="d-flex justify-content-between">
        <h2 class="d-flex-1">Liste des lots</h2>
        <div class="d-flex d-flex-1 align-items-end">
            <button class="bleuMoyenFond btnSurvolerBleuMoyenFond boutonPersonnalise text-white d-flex-1"
                    type="button"
                    id="ajouterLotButton">
                Ajouter un lot
            </button>
            <div class="dropdown d-flex-1">
                <button class="bleuMoyenFond btnSurvolerBleuMoyenFond boutonPersonnalise text-white dropdown-toggle"
                        type="button"
                        id="dropdownMenuButton"
                        data-toggle="dropdown"
                        data-bs-toggle="dropdown"
                        aria-haspopup="true"
                        aria-expanded="false">
                    Sélectionner les colonnes
                </button>
                <ul class="dropdown-menu">
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxTous d-flex-1"
                               type="checkbox"
                               id="tousSelectionnerCheckbox" />
                        <label class="d-flex-1 labelpadding"
                               for="tousSelectionnerCheckbox">
                            Tous Sélectionner
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEncanCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotEncanCheckbox">
                            Encan
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotNumeroCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotNumeroCheckbox">
                            Lot #
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotPrixOuvertureCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding"
                               for="lotPrixOuvertureCheckbox">
                            Prix Ouverture
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotValeurMinPourVenteCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding"
                               for="lotValeurMinPourVenteCheckbox">
                            Valeur Min Pour Vente
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEstimationMinCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding"
                               for="lotEstimationMinCheckbox">
                            Estimation Min
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEstimationMaxCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding"
                               for="lotEstimationMaxCheckbox">
                            Estimation Max
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotCategorieCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotCategorieCheckbox">
                            Catégorie
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotArtisteCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotArtisteCheckbox">
                            Artiste
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotDimensionCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotDimensionCheckbox">
                            Dimension
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotDescriptionCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding"
                               for="lotDescriptionCheckbox">
                            Description
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotMediumCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotMediumCheckbox">
                            Medium
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotVendeurCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotVendeurCheckbox">
                            Vendeur
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEstVenduCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotEstVenduCheckbox">
                            EstVendu
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotLivraisonCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotLivraisonCheckbox">
                            Livraison
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotModifierCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotModifierCheckbox">
                            Modifier
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotSupprimerCheckbox"
                               checked />
                        <label class="d-flex-1 labelpadding" for="lotSupprimerCheckbox">
                            Supprimer
                        </label>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="margesPourTable">
        <table class="table">
            <colgroup id="colgroup">
                <col id="colEncan">
                <col id="colNumero">
                <col id="colPrixOuverture">
                <col id="colValeurMinPourVente">
                <col id="colEstimationMin">
                <col id="colEstimationMax">
                <col id="colCategorie">
                <col id="colArtiste">
                <col id="colDimensions">
                <col id="colDescription">
                <col id="colMedium">
                <col id="colVendeur">
                <col id="colVendu">
                <col id="colLivraison">
                <col id="colModifier">
                <col id="colSupprimer">
            </colgroup>
            <thead>
                <tr>
                    <th>Encan</th>
                    <th>Lot #</th>
                    <th>Prix Ouverture</th>
                    <th>Valeur Min Pour Vente</th>
                    <th>Estimation Min</th>
                    <th>Estimation Max</th>
                    <th>Catégorie</th>
                    <th>Artiste</th>
                    <th>Dimension (en po)</th>
                    <th>Description</th>
                    <th>Medium</th>
                    <th>Vendeur</th>
                    <th>Vendu</th>
                    <th>Livraison</th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tr v-for="lot in test" :key="lot.id">
                <td>{{lot.encan}}</td>
                <td>{{ lot.numero }}</td>
                <td>{{ lot.prixOuverture }}</td>
                <td>{{ lot.valeurMinPourVente }}</td>
                <td>{{ lot.valeurEstimeMin }}</td>
                <td>{{ lot.valeurEstimeMax }}</td>
                <td>{{ lot.categorie }}</td>
                <td>{{ lot.artiste }}</td>
                <td>{{ lot.dimensions }}</td>
                <td>{{ lot.description }}</td>
                <td>{{ lot.medium }}</td>
                <td>{{ lot.vendeur }}</td>
                <td>
                    <div class="bg-transparent">
                        <img v-if="lot.estVendu == true" src="C:\Users\marca\Desktop\Gamma2024\gamma2024.client\public\icons\IconeLivrable.png" width="50" height="50">
                        <img v-else src="C:\Users\marca\Desktop\Gamma2024\gamma2024.client\public\icons\IconeNonLivrable.png" width="50" height="50">
                    </div>
                </td>
                <td>
                    <div class="bg-transparent">
                        <img v-if="lot.estLivrable == true" src="C:\Users\marca\Desktop\Gamma2024\gamma2024.client\public\icons\IconeLivrable.png" width="50" height="50">
                        <img v-else src="C:\Users\marca\Desktop\Gamma2024\gamma2024.client\public\icons\IconeNonLivrable.png" width="50" height="50">
                    </div>
                </td>
                <td>
                    <div class="bg-transparent">
                        <img src="C:\Users\marca\Desktop\Gamma2024\gamma2024.client\public\icons\bluepenpaper.png" width="50" height="50">
                    </div>
                </td>
                <td>
                    <div class="bg-transparent">
                        <img src="C:\Users\marca\Desktop\Gamma2024\gamma2024.client\public\icons\redbin.png" width="50" height="50">
                    </div>
                </td>
            </tr>
        </table>
    </div>
</template>

<style>
    @import "datatables.net-dt";

    .boutonPersonnalise {
        margin: 5px;
        padding-left: 15px;
        padding-right: 15px;
        border: none;
        border-radius: 5px;
        font-size: 15px;
        height: 25px;
    }

    h2 {
        padding-left: 15px;
    }

    li label {
        font-size: 15px;
    }

    .margesPourTable {
        padding-left: 15px;
        padding-right: 15px;
    }

    th {
        font-weight: bold;
        font-size: 13px;
        text-align: center !important;
    }

    td {
        font-size: 13px;
        text-align: center !important;
    }

    tr:nth-child(odd) {
        background-color: #83a0ba;
    }

    .cacher {
        visibility: collapse;
    }
</style>

<script setup>
    import { onMounted, ref } from "vue";
    import { useStore } from "vuex";

    const store = useStore();
    const test = ref([]);

    onMounted(async () => {
        const checkboxQuiSelectionneTout = document.querySelector(".checkboxTous");
        const listeDesCheckboxes = document.querySelectorAll(".checkboxSeul");
        const listeDesColonnes = document.querySelectorAll("col");

        try {
            test.value = await store.dispatch("fetchListeDeLotsPourAdministrateur");
            console.log(test.value);
        } catch (erreur) {
            console.log(
                "Erreur avec la génération de la liste des lots pour l'administrateur." +
                erreur
            );
        }

        checkboxQuiSelectionneTout.addEventListener("click", function (e) {
            if (this.checked) {
                listeDesCheckboxes.forEach((el, index) => {
                    el.checked = true;
                    listeDesColonnes[index].classList.remove("cacher");
                    el.disabled = true;
                });
            }
            else {
                listeDesCheckboxes.forEach((el) => {
                    el.disabled = false;
                });
            }
        });

        listeDesCheckboxes.forEach((el, index) => {
            el.addEventListener("click", function (e) {
                if (this.checked) {
                    listeDesColonnes[index].classList.remove("cacher");
                }
                else {
                    listeDesColonnes[index].classList.add("cacher");
                }
            });
        });
    });
</script>
