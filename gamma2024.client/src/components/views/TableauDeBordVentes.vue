<template>
    <div class="container mt-5">
        <h3 class="text-center">Rechercher une vente</h3>
        <input v-model="searchQuery"
               class="form-control row col-10 ms-1"
               type="search"
               placeholder="Rechercher une vente"
               aria-label="Search" />
        <h1 class="text-center mt-5">Liste des ventes</h1>

        <div class="accordion" id="accordionEncan">
            <div class="accordion-item " v-for="facture in filteredVentes" :key="facture.id">
                <h2 class="accordion-header px-0">
                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        {{facture.encan}} (dateAchat)
                    </button>
                </h2>
                <div id="collapseOne" class="accordion-collapse collapse show" data-bs-parent="#accordionEncan">
                    <div class="accordion-body">
                        <div class="accordion" id="accordionClient">
                            <div class="accordion-item">
                                <h2 class="accordion-header px-0">
                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapsetwo" aria-expanded="true" aria-controls="collapseOne">
                                        nom client (pseudonyme) - téléphone courriel
                                    </button>
                                </h2>
                                <div id="collapsetwo" class="accordion-collapse collapse show" data-bs-parent="#accordionClient">
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
                                                <!-- <tr v-for="lot in facture.lots" :key="lot.id">
                                                    <td scope="row">{{ lot.numero }}</td>
                                                    <td>{{lot.description}}</td>
                                                    <td>{{lot.artiste}}</td>
                                                </tr> -->
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
</template>

<script setup>
    import { computed, onMounted, ref } from "vue";
    import { useStore } from 'vuex';
    import DetailsAchats from "../modals/DetailsAchats.vue";

    const store = useStore();
    const listeFactures = ref([]);
    const searchQuery = ref("");

    onMounted(async () => {
        try {
            listeFactures.value = await store.dispatch("fetchFactureInfo");
            console.log("test" + listeFactures.value);
        }
        catch (error) {
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
                vente.dateAchat.toLowerCase().includes(searchLower) ||
                vente.prenom.toLowerCase().includes(searchLower) ||
                vente.nom.toLowerCase().includes(searchLower)
            );
        });
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
</style>