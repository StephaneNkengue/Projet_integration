<template>
    <div class="d-flex flex-column align-items-center contourDiv px-5 pb-3 pt-3">
        <h3>Encan {{titre}}</h3>

        <div class="bleuMoyenFond p-5 my-4 d-flex flex-column align-items-center">
            <h4>Encan #</h4>
            <p class="fs-6 mb-0">Encan d'oeuvres d'art</p>
        </div>

        <router-link to="/" class="text-decoration-none" v-if="type!=0">
            <p class="text-black"> Voir les encans {{titre}}s -></p>
        </router-link>
    </div>
</template>

<script setup>
    import { ref } from 'vue'


    const props = defineProps({
        type: Number,
    });

    const titre = typeEncan()

    function typeEncan() {
        if (props.type == 0) {

            return ref("présent")
        }
        else if (props.type == -1) {

            return ref("passé")
        }
        else {

            return ref("futur")
        }
    }

</script>

<style scoped>
</style>