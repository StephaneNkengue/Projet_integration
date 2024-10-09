<template>
    <div class="d-flex flex-column justify-content-between">
        <div class="d-flex justify-content-between">
            <h2 class="d-flex-1">Liste des encans</h2>
            <button class="btn bleuMoyenFond btnSurvolerBleuMoyenFond text-white d-flex-1"
                    type="button"
                    id="ajouterLotButton">
                Ajouter un lot
            </button>
        </div>
        <table class="table table-striped" data-toggle="table" data-search="true" data-side-pagination="true" data-show-columns="true">
            <thead>
                <tr>
                    <th>Encan</th>
                    <th>Status</th>
                    <th>Date de début</th>
                    <th>Date de fin</th>
                    <th>Soirée de clôture</th>
                    <th>Nombre de lots</th>
                    <th></th>
                    <th></th>
                </tr>
            </thead>
            <tr v-for="(encan, index) in test" :key="encan.id">
                <td>{{encan.numeroEncan}}</td>
                <td>{{encan.dateDebut}}</td>
                <td>{{encan.dateFin}}</td>
                <td>{{encan.dateDebutSoireeCloture}}</td>
                <td>{{encan.dateFinSoireeCloture}}</td>
            </tr>
        </table>
    </div>
</template>
<script setup>
    import { onMounted, ref } from "vue";
    import { useStore } from "vuex";

    const store = useStore();
    const test = ref([]);
    onMounted(async () => {

        try {
            test.value = await store.dispatch("fetchEncanInfo");
        }
        catch (erreur) {
            console.log("Erreur encans" + erreur);
        }


    });



</script>

<style scoped>
</style>
