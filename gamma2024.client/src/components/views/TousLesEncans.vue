<template>
    <div class="d-flex flex-column align-items-center pb-3">
        <h1>
            Tous les encans
        </h1>
        <div class="w-100 px-3 row row-cols-lg-2 row-cols-1">
            <div v-for="index in encans" class="col py-3">
                <router-link to="EncanPresent" class="text-decoration-none text-black">
                    <AffichageEncanTuile :encan="index" />
                </router-link>
            </div>
        </div>
    </div>
</template>

<script setup>
    import AffichageEncanTuile from '@/components/views/AffichageEncanTuile.vue'
    import { onMounted, ref } from 'vue'
    import { useStore } from "vuex";

    const store = useStore();
    const encans = ref()

    onMounted(async () => {
        const response = await store.dispatch("chercherTousEncans");
        encans.value = response.data
    })
</script>

<style scoped>
</style>