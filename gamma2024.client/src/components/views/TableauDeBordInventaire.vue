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
                        <input class="toggle-vis checkboxTous d-flex-1"
                               type="checkbox"
                               id="tousSelectionnerCheckbox" />
                        <label class="form-check-label d-flex-1"
                               for="tousSelectionnerCheckbox">
                            Tous Sélectionner
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEncanCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotEncanCheckbox">
                            Encan
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotNumeroCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotNumeroCheckbox">
                            Lot #
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotPrixOuvertureCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1"
                               for="lotPrixOuvertureCheckbox">
                            Prix Ouverture
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotValeurMinPourVenteCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1"
                               for="lotValeurMinPourVenteCheckbox">
                            Valeur Min Pour Vente
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEstimationMinCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1"
                               for="lotEstimationMinCheckbox">
                            Estimation Min
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEstimationMaxCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1"
                               for="lotEstimationMaxCheckbox">
                            EstimationMax
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotCategorieCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotCategorieCheckbox">
                            Catégorie
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotArtisteCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotArtisteCheckbox">
                            Artiste
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotDimensionCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotDimensionCheckbox">
                            Dimension
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotDescriptionCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1"
                               for="lotDescriptionCheckbox">
                            Description
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotMediumCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotMediumCheckbox">
                            Medium
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotVendeurCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotVendeurCheckbox">
                            Vendeur
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEstVenduCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotEstVenduCheckbox">
                            EstVendu
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotLivraisonCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotLivraisonCheckbox">
                            Livraison
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotModifierCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotModifierCheckbox">
                            Modifier
                        </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotSupprimerCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotSupprimerCheckbox">
                            Supprimer
                        </label>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="w-100">
        <table class="table w-100">
            <colgroup>
                <col id="colEncan" />
                <col id="colNumero" />
                <col id="colPrixOuverture" />
                <col id="colValeurMinPourVente" />
                <col id="colEstimationMin" />
                <col id="colEstimationMax" />
                <col id="colCategorie" />
                <col id="colArtiste" />
                <col id="colDimensions" />
                <col id="colDescription" />
                <col id="colMedium" />
                <col id="colVendeur" />
                <col id="colVendu" />
                <col id="colLivraison" />
                <col>
                <col>
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
                <td>{{ lot.encan }}</td>
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
                <td>{{ lot.estVendu }}</td>
                <td>{{ lot.estLivrable }}</td>
                <td>true</td>
                <td>true</td>
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

    th {
        font-weight: bold;
        font-size: 13px;
        text-align: center !important;
    }

    td {
        font-size: 13px;
        text-align: center !important;
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
