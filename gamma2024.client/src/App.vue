<script setup>
    import BarreDeNavigation from './components/views/BarreDeNavigation.vue'
    import PiedDePage from './components/views/PiedDePage.vue'
</script>

<template>
    <BarreDeNavigation />

    <main class="pt-xs-5">
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

    /*@media (min-width: 1024px) {
        header {
            display: flex;
            place-items: center;
            padding-right: calc(var(--section-gap) / 2);
        }

        .logo {
            margin: 0 2rem 0 0;
        }

        header .wrapper {
            display: flex;
            place-items: flex-start;
            flex-wrap: wrap;
        }
    }*/
</style>
