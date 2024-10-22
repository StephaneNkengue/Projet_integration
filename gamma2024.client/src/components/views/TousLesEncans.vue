<template>
    <div class="d-flex flex-column align-items-center pb-3">
        <h1>
            Tous les encans
        </h1>

        <div class="d-flex gap-2" v-if="chargement">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Chargement des encans...</span>
            </div>
            <p>Chargement des encans en cours...</p>
        </div>

        <div class="w-100 px-3 row row-cols-lg-2 row-cols-1">
            <div v-for="index in encans" class="col py-3">
                <router-link :to="{ name: 'Encan', params: { numeroEncan: index.numeroEncan }}" class="text-decoration-none text-black">
                    <AffichageEncanTuile :encan="index" />
                </router-link>
            </div>
        </div>
        <h5 class="text-center" v-if="encans.length <1 && !chargement">Il n'y a aucun encan pour le moment</h5>
    </div>
</template>

<script setup>
    import AffichageEncanTuile from '@/components/views/AffichageEncanTuile.vue'
    import { onMounted, ref } from 'vue'
    import { useStore } from "vuex";

    const store = useStore();
    const encans = ref([])
    const chargement = ref(true)

    onMounted(async () => {
        const response = await store.dispatch("chercherTousEncansVisibles");
        encans.value = response.data
        chargement.value = false
    })
</script>

<style scoped>
</style>