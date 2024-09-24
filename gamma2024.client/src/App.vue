<script setup>
    import BarreDeNavigation from './components/views/BarreDeNavigation.vue'
    import PiedDePage from './components/views/PiedDePage.vue'
</script>

<template>
    <header>
        <BarreDeNavigation />
    </header>

    <main class="mh-100 flex-grow-1">
        <router-view></router-view>
    </main>

    <footer>
        <PiedDePage />
    </footer>

</template>

<script>
    export default {
        name: 'App'
    }
</script>

<style scoped>
</style>
