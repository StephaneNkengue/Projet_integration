<template>
    <div class="d-flex flex-column align-items-center pb-3">
        <h1>Tous les encans</h1>

        <div class="d-flex gap-2" v-if="chargement">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Chargement des encans...</span>
            </div>
            <p>Chargement des encans en cours...</p>
        </div>
        <div v-else class="d-flex justify-content-center w-100">
            <h5 class="text-center" v-if="encans.length < 1">
                Votre demande de recherhce ne correspond à aucun encan.
            </h5>

            <div v-else class="w-100 px-3 row row-cols-lg-2 row-cols-1">
                <div v-for="index in encans" class="col py-3">
                    <span @click="voirEncan(index.numeroEncan)">
                        <AffichageEncanTuile :encan="index" />
                    </span>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
    import AffichageEncanTuile from "@/components/views/AffichageEncanTuile.vue";
    import { computed, watch, onMounted, ref } from "vue";
    import { useStore } from "vuex";
    import { useRouter, useRoute } from "vue-router";

    const store = useStore();
    const router = useRouter();

    const route = useRoute();
    const encans = ref([]);
    const encansFiltres = ref([]);
    const chargement = ref(true);
    const numEncanCours = ref(0);
    let genererListeDEncansFiltree = function () { };

    const voirEncan = ref(function (numeroEncanRecu) {
        if (numeroEncanRecu == numEncanCours.value) {
            router.push({ name: "EncanPresent" });
        } else {
            router.push({
                name: "Encan",
                params: { numeroEncan: numeroEncanRecu },
            });
        }
    });

    onMounted(async () => {
        try {
            initialiseData();
        } catch (error) {
        }
    });

    const queryChangement = computed(() => route.query)
    watch(queryChangement, initialiseData)

    async function initialiseData() {
        const response = await store.dispatch("chercherTousEncansVisibles");
        encans.value = response.data.map((encan) => ({
            ...encan,
        }));
        genererListeDEncansFiltree();
        //nbLotsRecus.value = lotsFiltres.value.length;
        //lotsParPage.value = nbLotsRecus.value;
        //nbPages.value = recalculerNbPages();

        //genererListePagination();

        //chercherLotsAAfficher();
        chargement.value = false;
    }

    genererListeDEncansFiltree = function () {
        var encansAFiltres = encans.value;
        encansFiltres.value = [];
        let rechercheEnMinuscule = route.query.stringDeRecherche
            .toString()
            .toLowerCase();
        encansFiltres.value = encansAFiltres.filter((encan) => {
            if (
                route.query.numero == 'true' &&
                encan.numero.toString().toLowerCase().startsWith(rechercheEnMinuscule)
            ) {
                return true;
            } else if (
                route.query.date == 'true' &&
                encan.numero.toString().toLowerCase().startsWith(rechercheEnMinuscule)
            ) {
                return true;
            }
            return false;
        });
    };
</script>

<style scoped></style>
