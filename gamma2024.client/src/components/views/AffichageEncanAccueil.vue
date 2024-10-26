<template>
    <div class="d-flex flex-column align-items-center contourDiv px-5 pb-3 pt-3">
        <h3>Encan {{titre}}</h3>

        <div class="bleuMoyenFond p-5 my-4 d-flex flex-column align-items-center" @click="voirEncan()">
            <div v-if="chargement">
                <div class="spinner-border" role="status">
                    <span class="sr-only"></span>
                </div>
            </div>
            <div v-else>
                <h4 v-if="encan != ''">Encan {{encan.numeroEncan}}</h4>
                <h4 v-else>Aucun encan trouvée</h4>
            </div>
        </div>

        <a class="text-decoration-none" v-if="type!=0" @click="voirEncans()">
            <p class="text-black"> Voir les encans {{titre}}s -></p>
        </a>
    </div>
</template>

<script setup>
    import { ref, onMounted } from 'vue'
    import { useStore } from "vuex";
    import { useRouter } from "vue-router";

    const store = useStore();
    const props = defineProps({
        type: Number,
    });
    const router = useRouter()

    const encan = ref('')
    const titre = ref('')
    const voirEncan = ref(function () { })
    const voirEncans = ref(function () { })
    const chargement = ref(true)

    if (props.type == 0) {
        titre.value = "présent"
    } else if (props.type == -1) {
        titre.value = "passé"
    } else {
        titre.value = "futur"
    }

    onMounted(async () => {
        if (props.type == 0) {
            const response = await store.dispatch("chercherEncanEnCours");
            encan.value = response.data

            if (encan.value != '') {
                voirEncan.value = function () { router.push({ name: 'EncanPresent' }) }
            }
        }
        else if (props.type == -1) {
            const response = await store.dispatch("chercherEncansPasses");

            if (response.data != '') {
                encan.value = response.data[0]
                voirEncan.value = function () { router.push({ name: 'Encan', params: { numeroEncan: encan.value.numeroEncan } }) }
                voirEncans.value = function () { router.push({ name: 'EncansPasses' }) }
            }
        }
        else {
            const response = await store.dispatch("chercherEncansFuturs");

            if (response.data != '') {
                encan.value = response.data[0]
                voirEncan.value = function () { router.push({ name: 'Encan', params: { numeroEncan: encan.value.numeroEncan } }) }
                voirEncans.value = function () { router.push({ name: 'EncansFuturs' }) }
            }
        }

        chargement.value = false
    });

</script>

<style scoped>
</style>