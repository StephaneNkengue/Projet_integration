<template>
    <div class="container mt-5">
        <h3 class="text-center">Rechercher une vente</h3>
        <input v-model="searchQuery"
               class="form-control row col-10 ms-1"
               type="search"
               placeholder="Rechercher une vente"
               aria-label="Search" />
        <h1 class="text-center mt-5">Liste des ventes</h1>
        <table class="table table-striped mt-5 mb-5 col-md-12 text-center">
            <thead>
                <tr>
                    <th scope="col">Encan</th>
                    <th scope="col">Date</th>
                    <th scope="col">Client</th>
                    <th scope="col">Action</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="facture in filteredVentes" :key="facture.id" data-bs-toggle="modal" :data-bs-target="'#'+facture.id">
                    <td scope="row">1</td>
                    <td>{{facture.dateAchat.split("T")[0]}}</td>
                    <td>{{facture.prenom}} {{facture.nom}}</td>
                    <td>
                        <button class="btn btn-info">
                            <img src="/images/ice.png" class="img-fluid" alt="..." />
                        </button>
                    </td>
                    <DetailsAchats :f="facture" />
                </tr>
            </tbody>
        </table>
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
</style>