<template>
    <div class="container">
        <h1 class="text-center">Gestion des cartes de crédit</h1>

        <div class="alert alert-danger" role="alert" id="message" v-show="siMessage">
        </div>

        <div v-if="chargement" class=text-center>
            <div class="spinner-border" role="status">
                <span class="sr-only"></span>
            </div>
        </div>
        <div v-else>
            <router-link :to="{name: 'EnregistrerCarte'}">
                <button class="btn btn-outline bleuMoyenFond text-white btnSurvolerBleuMoyenFond my-2">
                    Ajouter une carte
                </button>
            </router-link>
            <div class="d-flex flex-wrap w-100 justify-content-between">
                <div v-for="carte in cartes" class="col-6 p-3">
                    <AffichageCarteCredit :carte="carte" @supprimerCarte="supprimerCarte"></AffichageCarteCredit>
                </div>
            </div>

        </div>

    </div>

</template>
<script setup>
    import { useStore } from "vuex";
    import AffichageCarteCredit from '@/components/views/AffichageCarteCredit.vue'
    import { ref, onMounted } from "vue";
    const store = useStore();
    const cartes = ref([])
    const chargement = ref(true)
    const siMessage = ref(false)

    onMounted(async () => {
        try {
            const response = await store.dispatch("chercherCartesUser")
            cartes.value = response.data
            chargement.value = false
        } catch (error) {
            console.error("Erreur lors de la récupération des lots:", error);
        }
    });

    const supprimerCarte = ref(async function (pmId) {
        try {
            if (cartes.value.length > 1) {
                siMessage.value = false
                const carteSuppression = await store.dispatch("supprimerCarte", pmId)
                const response = await store.dispatch("chercherCartesUser")
                cartes.value = response.data
            }
            else {
                siMessage.value = true
                document.querySelector("#message").innerHTML = "Vous devez avoir au moins une carte dans votre profil afin d'utiliser nos services"
            }

        } catch (error) {
            siMessage.value = true
            document.querySelector("#message").innerHTML = error
        }
    })
</script>
<style></style>