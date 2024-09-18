<script setup>
    import BarreDeNavigation from './components/views/BarreDeNavigation.vue'
    import PiedDePage from './components/views/PiedDePage.vue'
</script>

<template>
    <BarreDeNavigation />

    <main class="mh-100 flex-grow-1">
        <router-view></router-view>
    </main>

    <PiedDePage />

</template>

<script>
    export default {
        name: 'App'
    }
</script>

<style scoped>
</style>
