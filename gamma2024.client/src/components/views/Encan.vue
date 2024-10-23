<template>
    <div class="d-flex flex-column align-items-center pb-3">
        <h1>Encan {{ numeroEncan }}</h1>

        <div class="d-flex gap-2" v-if="chargement">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Chargement des encans...</span>
            </div>
            <p>Chargement de l'encan en cours...</p>
        </div>

        <h5 class="text-center" v-if="encan == '' && !chargement">Il n'y a présentement aucun encan ayant ce numéro</h5>

        <div v-else>
            <p class="text-center">Date de debut de la soirée de cloture:</p>
            <AffichageLots />
        </div>


    </div>
</template>

<script setup>
    import AffichageLots from "@/components/views/AffichageLots.vue";
    import { onMounted, ref } from "vue";
    import { useStore } from "vuex";

    const store = useStore();

    const chargement = ref(true)
    const encan = ref('')

    const props = defineProps({
        numeroEncan: String,
    });

    onMounted(async () => {
        const numero = props.numeroEncan;
        const response = await store.dispatch("chercherEncanParNumero", numero);
        chargement.value = false
        encan.value = response.data
    });
</script>

<style scoped></style>
