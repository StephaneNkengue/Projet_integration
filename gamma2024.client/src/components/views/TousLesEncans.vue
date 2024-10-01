<template>
    <div class="d-flex flex-column align-items-center pb-3">
        <h1>
            Tous les encans
        </h1>
        <div class="w-100 px-3 row row-cols-lg-2 row-cols-1">
            <div v-for="index in encanParPage" class="col py-3">
                <router-link to="EncanPresent" class="text-decoration-none text-black">
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