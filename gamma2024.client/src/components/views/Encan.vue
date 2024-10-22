<template>
    <div class="d-flex flex-column align-items-center pb-3">
        <h1>Encan {{numeroEncan}}</h1>
        <p>Date de debut de la soirée de cloture</p>

        <AffichageLots />

    </div>
</template>

<script setup>
    import AffichageLots from '@/components/views/AffichageLots.vue'

    defineProps({
        numeroEncan: Number
    })
</script>

<style scoped></style>