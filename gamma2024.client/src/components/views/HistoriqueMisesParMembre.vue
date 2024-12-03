<template>
    <div class="container mt-4">
        <h2 class="text-center mb-4">Historique des mises</h2>

        <div class="d-flex gap-2 w-100 justify-content-center" v-if="chargement">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Chargement des lots...</span>
            </div>
            <p>Chargement des lots en cours...</p>
        </div>

        <div v-else class="w-100">
            <h5 class="text-center" v-if="lotsAvecMises.length === 0">
                Aucune mise trouvée
            </h5>

            <div v-else class="d-flex flex-column align-items-center">
                <div class="d-flex flex-row-reverse w-100 px-4 me-2 gap-2">
                    <button class="rounded btn btnSurvolerBleuMoyenFond"
                            v-if="!siTuile"
                            @click="changerTypeAffichage('tuile')">
                        <img src="/icons/TableauOption.png"
                             alt="Affichage en tableau"
                             height="25" />
                    </button>
                    <button class="rounded btn btnSurvolerBleuMoyenFond"
                            v-else
                            @click="changerTypeAffichage('liste')">
                        <img src="/icons/IconListe.png"
                             alt="Affichage en liste"
                             height="25" />
                    </button>
                </div>

                <div v-if="siTuile"
                     class="row row-cols-lg-5 row-cols-md-3 row-cols-sm-2 row-cols-1 w-100 px-3">
                    <div v-for="lot in lotsAvecMises"
                         :key="lot.id"
                         class="col p-2 d-flex">
                        <LotTuile :lotRecu="lot" />
                    </div>
                </div>

                <div v-else class="d-flex flex-column px-5 w-100">
                    <div v-for="lot in lotsAvecMises"
                         :key="lot.id"
                         class="p-2">
                        <LotListe :lotRecu="lot" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref, onMounted, computed, watch } from 'vue';
    import { useStore } from 'vuex';
    import LotTuile from './LotTuile.vue';
    import LotListe from './LotListe.vue';

    const store = useStore();
    const chargement = ref(true);
    const siTuile = ref(true);

    // Computed property pour filtrer les lots avec mises
    const lotsAvecMises = computed(() => {
        const userBids = store.state.userBids;
        return Object.values(store.state.lots)
            .filter(lot => userBids.includes(lot.id))
            .map(lot => ({
                ...lot,
                userHasBid: true
            }));
    });

    // Watch pour les changements dans le store
    watch(() => store.state.lots, () => {
        if (store.state.isLoggedIn) {
            store.dispatch('fetchUserBids');
        }
    }, { deep: true });

    const changerTypeAffichage = (type) => {
        siTuile.value = type === 'tuile';
    };

    onMounted(async () => {
        try {
            if (store.state.isLoggedIn) {
                await store.dispatch('fetchUserBids');
            }
        } catch (error) {
            console.error("Erreur lors du chargement des mises:", error);
        } finally {
            chargement.value = false;
        }
    });
</script>

<style scoped>
    /* Réutiliser les styles d'AffichageLots si nécessaire */
</style> 