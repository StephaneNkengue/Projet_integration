<template>
    <div class="container mt-5">
        <h3 class="text-center">Rechercher une vente</h3>
        <input v-model="searchQuery"
               class="form-control row col-10 ms-1"
               type="search"
               placeholder="Rechercher une vente"
               aria-label="Search" />
        <h1 class="text-center mt-5">Liste des ventes</h1>

        <div class="d-flex gap-2 justify-content-center" v-if="chargement">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Chargement des ventes...</span>
            </div>
            <p>Chargement des ventes en cours...</p>
        </div>

        <div v-if="!chargement" class="w-100">
            <div class="d-flex justify-content-center" v-if="!filteredVentes.length">
                <h2>Aucun résultat trouvé</h2>
            </div>

            <div v-else class="mb-5">
                <div class="accordion" id="accordionEncan">
                    <div class="accordion-item" v-for="encan in numerosEncans" :key="encan">
                        <div v-if="filteredVentes.filter((x) => x.encan == encan.encan) != 0">
                            <h2 class="accordion-header px-0" :style="{border: styleBorder}">
                                <button class="accordion-button"
                                        type="button"
                                        data-bs-toggle="collapse"
                                        :data-bs-target="'#collapse' + encan.encan"
                                        aria-expanded="true"
                                        :aria-controls="'collapse' + encan.encan">
                                    {{ encan.encan }} ({{ encan.dateAchat.split("T")[0] }})
                                </button>
                            </h2>
                            <!--Changer le 233 en le numero de l'encan le plus récent-->
                            <div :id="'collapse' + encan.encan"
                                 class="accordion-collapse collapse"
                                 :class="{ show: encan == numerosEncans[0] }"
                                 data-bs-parent="#accordionEncan">
                                <div class="accordion-body">
                                    <div class="accordion" id="accordionClient" v-for="facture in filteredVentes.filter((x)=> x.encan == encan.encan)" :key="facture.id">
                                        <div class="accordion-item">
                                            <h2 class="accordion-header px-0">
                                                <button class="accordion-button d-flex"
                                                        type="button"
                                                        data-bs-toggle="collapse"
                                                        :data-bs-target="'#collapseFacture' + facture.id"
                                                        aria-expanded="true"
                                                        :aria-controls="'collapseFacture' + facture.id">
                                                    <div class="col-11">
                                                        {{ facture.prenom }} {{ facture.nom }} ({{facture.pseudonyme}})<br />{{ facture.courriel }}<br />{{facture.telephone}}
                                                    </div>
                                                    <div class="col">
                                                        <button class="btn btn-info">
                                                            <img src="/images/ice.png" class="img-fluid" alt="..." />
                                                        </button>
                                                    </div>
                                                </button>
                                            </h2>
                                            <div :id="'collapseFacture' + facture.id"
                                                 class="accordion-collapse collapse"
                                                 data-bs-parent="#accordionClient">
                                                <div class="accordion-body">
                                                    <table class="table">
                                                        <thead>
                                                            <tr>
                                                                <th scope="col">Numéro du lot</th>
                                                                <th scope="col">Prix vendu</th>
                                                                <th scope="col">Livraison</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            <tr v-for="lot in facture.lots" :key="lot.id">
                                                                <td scope="row">{{ lot.numero }}</td>
                                                                <td>{{ lot.mise }}</td>
                                                                <td>
                                                                    <img v-if="lot.estLivrable"
                                                                         src="/icons/livrable.png" />
                                                                    <img v-else src="/icons/nonlivrable.png" />
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
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
    import { computed, watch, onMounted, ref } from "vue";
    import { useStore } from "vuex";

    const store = useStore();
    const listeFactures = ref([]);
    const searchQuery = ref("");
    const numerosEncans = ref([]);
    const styleBorder = ref('');
    const chargement = ref(true);

    onMounted(async () => {
        try {
            listeFactures.value = await store.dispatch("fetchFactureInfo");

            listeFactures.value.filter(function (value, index, self) {
                if ((index === self.findIndex((t) => t.encan === value.encan)) == true) {
                    numerosEncans.value.push(value);
                }
            });

            chargement.value = false;
        } catch (error) {
            console.log("Erreur factures" + error);
        }
    });

    const tousLesVentes = computed(() => {
        if (listeFactures.value) {
            return listeFactures.value;
        }
        return [];
    });

    const filteredVentes = computed(() => {
        return tousLesVentes.value.filter((vente) => {
            const searchLower = searchQuery.value.toLowerCase();
            return (
                vente.encan.toLowerCase().startsWith(searchLower) ||
                vente.dateAchat.toLowerCase().startsWith(searchLower) ||
                vente.prenom.toLowerCase().startsWith(searchLower) ||
                vente.nom.toLowerCase().startsWith(searchLower) ||
                vente.pseudonyme.toLowerCase().startsWith(searchLower) ||
                vente.courriel.toLowerCase().startsWith(searchLower) ||
                vente.telephone.toLowerCase().startsWith(searchLower)
            );
        });
    });

    watch(filteredVentes, () => {
        if (listeFactures.value.length == filteredVentes.value.length) {
            styleBorder.value = 'none';
        }
        else {
            styleBorder.value = '2px solid green'
        }
    });
</script>

<style scoped>
    img {
        width: 25px;
        height: 30px;
    }

    table,
    input {
        box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 3px 5px 0 rgba(0, 0, 0, 0.19);
    }

    th {
        font-size: 18px;
    }

    td {
        font-size: 16px;
    }

    .btn {
        background-color: #0dcaf0;
    }

    .accordion {
        --bs-accordion-active-bg: none;
    }
</style>
