<template>

    <div class="row row-cols-lg-5 row-cols-md-3 row-cols-sm-2 row-cols-1 w-100 px-3">
        <div v-for="index in 15" class="col p-2">
            <LotTuile />
        </div>
    </div>
</template>

<script setup>
    import LotTuile from '@/components/views/LotTuile.vue'
</script>

<style scoped>
</style>