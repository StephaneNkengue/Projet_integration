<template>
    <div class="d-flex justify-content-between">
        <div v-if="messageConfirmation" class="alert alert-success">
        {{ messageConfirmation }}
        </div>
        <h2 class="d-flex-1">Liste des lots</h2>
        <div class="d-flex d-flex-1 align-items-end">
            <button
                class="bleuMoyenFond btnSurvolerBleuMoyenFond boutonPersonnalise text-white d-flex-1"
                type="button"
                id="ajouterLotButton"
                @click="redirigerVersCreationLot"
            >
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
                               id="tousSelectionnerCheckbox"
                               :checked="Object.values(colonnesVisibles).every(v => v)"
                               @change="toggleToutesColonnes" />
                        <label class="d-flex-1 labelpadding"
                               for="tousSelectionnerCheckbox">
                            Tous Sélectionner
                        </label>
                    </li>
                    <li v-for="(visible, colonne) in colonnesVisibles" :key="colonne" class="d-flex justify-content-start dropdown-item">
                        <input class="checkboxSeul d-flex-1"
                               type="checkbox"
                               :id="`lot${colonne.charAt(0).toUpperCase() + colonne.slice(1)}Checkbox`"
                               :checked="visible"
                               @change="toggleColonne(colonne)" />
                        <label class="d-flex-1 labelpadding" :for="`lot${colonne.charAt(0).toUpperCase() + colonne.slice(1)}Checkbox`">
                            {{ colonne.charAt(0).toUpperCase() + colonne.slice(1) }}
                        </label>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class="margesPourTable">
        <table class="table table-striped">
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
                    <th v-if="colonnesVisibles.encan">Encan</th>
                    <th v-if="colonnesVisibles.numero">Lot #</th>
                    <th v-if="colonnesVisibles.prixOuverture">Prix Ouverture</th>
                    <th v-if="colonnesVisibles.valeurMinPourVente">Valeur Min Pour Vente</th>
                    <th v-if="colonnesVisibles.estimationMin">Estimation Min</th>
                    <th v-if="colonnesVisibles.estimationMax">Estimation Max</th>
                    <th v-if="colonnesVisibles.categorie">Catégorie</th>
                    <th v-if="colonnesVisibles.artiste">Artiste</th>
                    <th v-if="colonnesVisibles.dimension">Dimension (en po)</th>
                    <th v-if="colonnesVisibles.description">Description</th>
                    <th v-if="colonnesVisibles.medium">Medium</th>
                    <th v-if="colonnesVisibles.vendeur">Vendeur</th>
                    <th v-if="colonnesVisibles.estVendu">Vendu</th>
                    <th v-if="colonnesVisibles.livraison">Livraison</th>
                    <th v-if="colonnesVisibles.modifier"></th>
                    <th v-if="colonnesVisibles.supprimer"></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="lot in lots" :key="lot.id">
                    <td v-if="colonnesVisibles.encan">{{ lot.numeroEncan }}</td>
                    <td v-if="colonnesVisibles.numero">{{ lot.code }}</td>
                    <td v-if="colonnesVisibles.prixOuverture">{{ lot.prixOuverture }}</td>
                    <td v-if="colonnesVisibles.valeurMinPourVente">{{ lot.prixMinPourVente }}</td>
                    <td v-if="colonnesVisibles.estimationMin">{{ lot.valeurEstimeMin }}</td>
                    <td v-if="colonnesVisibles.estimationMax">{{ lot.valeurEstimeMax }}</td>
                    <td v-if="colonnesVisibles.categorie">{{ lot.categorie }}</td>
                    <td v-if="colonnesVisibles.artiste">{{ lot.artiste }}</td>
                    <td v-if="colonnesVisibles.dimension">{{ lot.dimension }}</td>
                    <td v-if="colonnesVisibles.description">{{ lot.description }}</td>
                    <td v-if="colonnesVisibles.medium">{{ lot.medium }}</td>
                    <td v-if="colonnesVisibles.vendeur">{{ lot.vendeur }}</td>
                    <td v-if="colonnesVisibles.estVendu">
                        {{ lot.estVendu ? 'Oui' : 'Non' }}
                    </td>
                    <td v-if="colonnesVisibles.livraison">
                        {{ lot.estLivrable ? 'Oui' : 'Non' }}
                    </td>
                    <td v-if="colonnesVisibles.modifier">
                        <router-link :to="{ name: 'ModificationLot', params: { id: lot.id } }">
                            Modifier
                        </router-link>
                    </td>
                    <td v-if="colonnesVisibles.supprimer">
                        <button @click="ouvrirBoiteModale(lot.id)" class="btn btn-danger">Supprimer</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <!-- Modal de confirmation -->
    <div v-if="lotASupprimer !== null" class="modal-overlay">
        <div class="modal-content">
            <p>Êtes-vous sûr de vouloir supprimer ce lot ?</p>
            <div class="modal-buttons">
                <button @click="confirmerSuppression" class="btn btn-danger">OK</button>
                <button @click="annulerSuppression" class="btn btn-secondary">Annuler</button>
            </div>
        </div>
    </div>

    
</template>

<script setup>
    import { onMounted, ref } from "vue";
    import { useStore } from "vuex";
    import { useRouter } from 'vue-router';

    const store = useStore();
    const router = useRouter();
    const lots = ref([]);
    const lotASupprimer = ref(null);
    const messageConfirmation = ref(null);
    const colonnesVisibles = ref({
        encan: true,
        numero: true,
        prixOuverture: true,
        valeurMinPourVente: true,
        estimationMin: true,
        estimationMax: true,
        categorie: true,
        artiste: true,
        dimension: true,
        description: true,
        medium: true,
        vendeur: true,
        estVendu: true,
        livraison: true,
        modifier: true,
        supprimer: true
    });

    const toggleToutesColonnes = () => {
        const nouvelEtat = !Object.values(colonnesVisibles.value).every(v => v);
        Object.keys(colonnesVisibles.value).forEach(key => {
            colonnesVisibles.value[key] = nouvelEtat;
        });
    };

    const toggleColonne = (colonne) => {
        colonnesVisibles.value[colonne] = !colonnesVisibles.value[colonne];
    };

    onMounted(async () => {
        try {
            lots.value = await store.dispatch('obtenirTousLots');
        } catch (error) {
            console.error("Erreur lors de la récupération des lots:", error);
        }
    });

    const ouvrirBoiteModale = (id) => {
        lotASupprimer.value = id;
    };

    const confirmerSuppression = async () => {
        if (lotASupprimer.value !== null) {
            try {
                await store.dispatch('supprimerLot', lotASupprimer.value);
                lots.value = lots.value.filter(lot => lot.id !== lotASupprimer.value);
                messageConfirmation.value = "Lot supprimé avec succès.";
                setTimeout(() => {
                    messageConfirmation.value = null;
                }, 2000);
            } catch (error) {
                console.error("Erreur lors de la suppression du lot:", error);
            } finally {
                lotASupprimer.value = null;
            }
        }
    };

    const annulerSuppression = () => {
        lotASupprimer.value = null;
    };

    const redirigerVersCreationLot = () => {
        router.push({ name: 'CreationLot' });
    };
</script>


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

    tr:nth-child(even) {
        background-color: #f8f9fa;
    }

    tr:nth-child(odd) {
        background-color: #e9ecef;
    }

    tr:hover {
        background-color: #d1e7ff;
    }

    .cacher {
        visibility: collapse;
    }
</style>

<style scoped>
.modal-overlay {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba(0, 0, 0, 0.5);
    display: flex;
    justify-content: center;
    align-items: center;
}

.modal-content {
    background-color: white;
    padding: 20px;
    border-radius: 5px;
    text-align: center;
}

.modal-buttons {
    margin-top: 15px;
}

.modal-buttons button {
    margin: 0 10px;
}

.alert {
    margin: 10px 0;
    padding: 10px;
    border-radius: 5px;
}

.alert-success {
    background-color: #d4edda;
    color: #155724;
}
</style>

