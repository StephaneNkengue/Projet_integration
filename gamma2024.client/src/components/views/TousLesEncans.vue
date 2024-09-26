<template>
    <div class="d-flex flex-column align-items-center pb-3">
        <h1>
            Tous les encans
        </h1>
        <div class="d-flex flex-row-reverse w-100 px-4 me-2 gap-2">
            <button class="d-flex align-items-center text-center rounded bleuMoyenFond text-white contourDiv btnSurvolerBleuMoyenFond" @click="encanParPage = 200">Tous</button>
            <button class="d-flex align-items-center text-center rounded bleuMoyenFond text-white contourDiv btnSurvolerBleuMoyenFond" @click="encanParPage = 100">100</button>
            <button class="d-flex align-items-center text-center rounded bleuMoyenFond text-white contourDiv btnSurvolerBleuMoyenFond" @click="encanParPage = 50">50</button>
            <button class="d-flex align-items-center text-center rounded bleuMoyenFond text-white contourDiv btnSurvolerBleuMoyenFond" @click="encanParPage = 20">20</button>
        </div>
        <div class="w-100 px-3 row row-cols-2">
            <div v-for="index in encanParPage" class="col w-50 py-3">
                <router-link to="Accueil" class="text-decoration-none text-black">
                    <AffichageEncanTuile />
                </router-link>
            </div>
        </div>
    </div>
</template>

<script setup>
    import AffichageEncanTuile from '@/components/views/AffichageEncanTuile.vue'
    import { ref } from 'vue'

    const encanParPage = ref(20);

</script>

<style scoped>
</style>