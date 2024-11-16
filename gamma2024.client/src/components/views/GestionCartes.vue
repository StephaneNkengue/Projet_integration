<template>
    <div class="container">
        <h1 class="text-center">Gestion des cartes de crédit</h1>

        <div v-if="chargement" class=text-center>
            <div class="spinner-border" role="status">
                <span class="sr-only"></span>
            </div>
        </div>
        <div v-else>
            <router-link :to="{name: 'EnregistrerCarte'}">
                <button class="btn btn-outline bleuMoyenFond text-white btnSurvolerBleuMoyenFond my-2">
                    Ajouter une carte
                </button>
            </router-link>
            <div class="d-flex flex-wrap w-100 justify-content-between">
                <div v-for="carte in cartes" class="col-6 p-3">
                    <AffichageCarteCredit :carte="carte"></AffichageCarteCredit>
                </div>
            </div>

        </div>

    </div>

</template>
<script setup>
    import { useStore } from "vuex";
    import AffichageCarteCredit from '@/components/views/AffichageCarteCredit.vue'
    import { ref, onMounted } from "vue";
    const store = useStore();
    const cartes = ref([])
    const chargement = ref(true)

    onMounted(async () => {
        try {
            const response = await store.dispatch("chercherCartesUser")
            cartes.value = response.data
            chargement.value = false
        } catch (error) {
            console.error("Erreur lors de la récupération des lots:", error);
        }
    });
</script>
<style></style>