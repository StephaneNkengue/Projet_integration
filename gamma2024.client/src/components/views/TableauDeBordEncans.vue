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
        <DataTable ref="table" :data="data" :options="options" class="display">
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
        </DataTable>
    </div>
</template>
<script setup>
    import { onMounted, ref } from "vue";
    import DataTable from "datatables.net-vue3";
    import DataTablesCore from "datatables.net-dt";
    import "datatables.net-select-dt";
    import "datatables.net-responsive-dt";
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

    DataTable.use(DataTablesCore);
    let dt;
    const table = ref();
    const options = {
        ordering: false,
        language: {
            url: 'https://cdn.datatables.net/plug-ins/2.1.8/i18n/fr-FR.json'
        },
    };



</script>
<style scoped>
</style>
