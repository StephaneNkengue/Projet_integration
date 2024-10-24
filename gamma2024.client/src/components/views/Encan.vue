<template>
    <div class="d-flex flex-column align-items-center pb-3">
        <h1>Encan {{ numeroEncan }}</h1>

        <div class="d-flex gap-2" v-if="chargement">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Chargement des encans...</span>
            </div>
            <p>Chargement de l'encan en cours...</p>
        </div>

        <div v-if="!chargement">
            <h5 class="text-center" v-if="encan == ''">
                Il n'y a présentement aucun encan ayant ce numéro
            </h5>
            <div v-else>
                <p class="text-center">Date de debut de la soirée de cloture:</p>

                <AffichageLots :idEncan="encan.id" />
            </div>
        </div>
    </div>
</template>

<script setup>
    import AffichageLots from "@/components/views/AffichageLots.vue";
    import { onMounted, ref, reactive } from "vue";
    import { useStore } from "vuex";

    const store = useStore();

    const chargement = ref(true);
    const encan = ref('');

    const props = defineProps({
        numeroEncan: String,
    });

    onMounted(async () => {
        const numero = props.numeroEncan;
        const response = await store.dispatch("chercherEncanParNumero", numero);
        encan.value = response.data

        chargement.value = false;
    });
</script>

<style scoped></style>
