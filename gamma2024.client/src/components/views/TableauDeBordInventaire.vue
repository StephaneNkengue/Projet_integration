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
                <button class=" bleuMoyenFond btnSurvolerBleuMoyenFond boutonPersonnalise text-white dropdown-toggle"
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
                        <label class="form-check-label d-flex-1" for="tousSelectionnerCheckbox"> Tous Sélectionner </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEncanCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotEncanCheckbox"> Encan </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotNumeroCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotNumeroCheckbox"> Lot # </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotPrixOuvertureCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotPrixOuvertureCheckbox"> Prix Ouverture </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotValeurMinPourVenteCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotValeurMinPourVenteCheckbox"> Valeur Min Pour Vente </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEstimationMinCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotEstimationMinCheckbox"> Estimation Min </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotEstimationMaxCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotEstimationMaxCheckbox"> EstimationMax </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotCategorieCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotCategorieCheckbox"> Catégorie </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotArtisteCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotArtisteCheckbox"> Artiste </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotDimensionCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotDimensionCheckbox"> Dimension </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotDescriptionCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotDescriptionCheckbox"> Description </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotMediumCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotMediumCheckbox"> Medium </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotLivraisonCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotLivraisonCheckbox"> Livraison </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotModifierCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotModifierCheckbox"> Modifier </label>
                    </li>
                    <li class="d-flex justify-content-start dropdown-item">
                        <input class="toggle-vis checkboxSeul d-flex-1"
                               type="checkbox"
                               id="lotSupprimerCheckbox"
                               checked />
                        <label class="form-check-label d-flex-1" for="lotSupprimerCheckbox"> Supprimer </label>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="w-100">
        <table class="table table-striped" data-toggle="table" data-search="true" data-side-pagination="true" data-show-columns="true">
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
                    <th>Dimension</th>
                    <th>Description</th>
                    <th>Medium</th>
                    <th>Vendeur</th>
                    <th>Vendu</th>
                    <th>Livraison</th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tr v-for="(lot, index) in test" :key="lot.id">
                <td>{{lot.Encan}}</td>
                <td>{{lot.Numero}}</td>
                <td>{{lot.PrixOuverture}}</td>
                <td>{{lot.ValeurMinPourVente}}</td>
                <td>{{lot.ValeurEstimeMin}}</td>
                <td>{{lot.ValeurEstimeMax}}</td>
                <td>{{lot.Categorie}}</td>
                <td>{{lot.Artiste}}</td>
                <td>{{lot.Dimension}}</td>
                <td>{{lot.Description}}</td>
                <td>{{lot.Medium}}</td>
                <td>{{lot.Vendeur}}</td>
                <td>{{lot.EstVendu}}</td>
                <td>{{lot.EstLivrable}}</td>
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

    .margesPourLaTable {
        padding-left: 15px;
        padding-right: 15px;
    }

    table {
        border-collapse: collapse;
        font-size: 13px;
        text-align: center;
        margin-left: 15px;
        margin-right: 15px;
    }

        table label {
            font-size: 13px;
        }

    li label {
        font-size: 13px;
        padding-left: 5px;
    }

    .dt-column-title {
        font-weight: bold;
    }

    th, td {
        text-align: center !important;
    }
</style>

<script setup>
    import { onMounted, ref } from "vue";
    import { useStore } from "vuex";

    const store = useStore();
    const test = ref([]);
    //const options = {
    //    ordering: false,
    //    language: {
    //        //url: 'https://cdn.datatables.net/plug-ins/2.1.8/i18n/fr-FR.json'
    //        "search": "Rechercher :",
    //        "info": "Affichage de _START_ à _END_ sur _TOTAL_ entrées",
    //        "infoEmpty": "Affichage de 0 à 0 sur 0 entrées",
    //        "infoFiltered": "(filtrées depuis un total de _MAX_ entrées)",
    //        "lengthMenu": "Afficher _MENU_ entrées",
    //        "zeroRecords": "Aucune entrée correspondante trouvée",
    //    }
    //};

    //let data = ref([
    //    [232, "1", "200$", "", "600$", "800$", "Paintings", "Guy Paquet", "12po x 16po", "1984/La nouvelle pratique", "Huile sur panneau", "Oui", "Modifier", "Supprimer"],
    //    [232, "1a", "2200$", "3800$", "3500$", "5000$", "Paintings", "Vladimir Horik", "20po x 30po", "La messe du soir", "Huile sur panneau", "Oui", "Modifier", "Supprimer"],
    //    [232, "1b", "400$", "", "700$", "1200$", "Paintings", "Vladimir Horik", "12po x 16po", "1979/Paysage", "Huile sur panneau", "Oui", "Modifier", "Supprimer"],
    //    [232, "2", "150$", "", "250$", "350$", "Paintings", "Marcel Poirier", "16po x 20po", "1979/Début d'automne", "Huile sur toile", "Oui", "Modifier", "Supprimer"],
    //    [232, "3", "3000$", "", "5500$", "6500$", "Paintings", "Normand Hudon", "16po x 20po", "1989/Le beau bonhomme", "Huile sur panneau", "Oui", "Modifier", "Supprimer"],
    //    [232, "4", "1500$", "", "3000$", "4000$", "Paintings", "Normand Hudon", "16po x 12po", "1988/Coup de vent", "Huile sur panneau", "Oui", "Modifier", "Supprimer"],
    //    [232, "5", "800$", "", "1500$", "2000$", "Paintings", "Normand Hudon", "12po x 16po", "1990/Le père, le fils et le saint esprit. Amen", "Huile sur panneau", "Oui", "Modifier", "Supprimer"],
    //    [232, "6", "800$", "", "1500$", "2000$", "Paintings", "Claude Langevin", "20po x 24po", "Sous-bois", "Huile sur toile", "Oui", "Modifier", "Supprimer"],
    //    [233, "1", "200$", "", "750$", "1250$", "Paintings", "Denis Juneau", "26po x 20po", "1979/Envolée", "Aquarelle", "Oui", "Modifier", "Supprimer"],
    //    [233, "2", "600$", "", "1400$", "1800$", "Paintings", "Serge Lemoyne", "12po x 12po", "1996/Hommage à Matis", "Huile sur panneau", "Oui", "Modifier", "Supprimer"],
    //    [233, "3", "120$", "", "400$", "600$", "Paintings", "Marc Siméon", "48po x 40po", "Pointe bleue", "Huile sur toile", "Oui", "Modifier", "Supprimer"],
    //    [233, "4", "150$", "", "400$", "600$", "Paintings", "Jean Gaudreau", "40po x 28po", "1996/Visage", "Huile sur toile", "Oui", "Modifier", "Supprimer"],
    //    [233, "5", "350$", "", "600$", "800$", "Paintings", "Jean-Paul Jérome", "5.5po x 7po", "1996/Abstraction", "Encre", "Oui", "Modifier", "Supprimer"],
    //    [233, "6", "3500$", "6500$", "5000$", "7000$", "Lithograph", "Jean-Paul Riopelle", "29po x 41po", "Jazz", "Lithographie", "Oui", "Modifier", "Supprimer"],
    //    [233, "7", "1750$", "3000$", "2500$", "3500$", "Lithograph", "Jean-Paul Riopelle", "23po x 28po", "19781/Les oies 1", "Lithographie (signé dans la plaque)", "Oui", "Modifier", "Supprimer"],
    //]);

    onMounted(async () => {
        //const dt = table.value.dt;
        //const checkboxQuiSelectionneTout = document.querySelector(".checkboxTous");
        //const listeDesCheckboxes = document.querySelectorAll(".checkboxSeul");

        try {
            test.value = await store.dispatch("fetchListeDeLotsPourAdministrateur");
        }
        catch (erreur) {
            console.log("Erreur avec la génération de la liste des lots pour l'administrateur." + erreur);
        }

        //checkboxQuiSelectionneTout.addEventListener("click", function (e) {
        //    if (this.checked) {
        //        listeDesCheckboxes.forEach((el, index) => {
        //            el.checked = true;
        //            dt.column(index).visible(true);
        //            el.disabled = true;
        //        });
        //    }
        //    else {
        //        listeDesCheckboxes.forEach((el) => {
        //            el.disabled = false;
        //        });
        //    }
        //});

        //listeDesCheckboxes.forEach((el, index) => {
        //    el.addEventListener("click", function (e) {
        //        if (this.checked) {
        //            dt.column(index).visible(true);
        //        }
        //        else {
        //            dt.column(index).visible(false);
        //        }
        //    });
        //});
    })
</script>
