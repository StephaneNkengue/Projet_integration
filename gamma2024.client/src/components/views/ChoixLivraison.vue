<template>
    <div class="container">
        <h1>Choix de livraison</h1>

        <div v-if="chargement">
            <div class="spinner-border" role="status"></div>
        </div>
        <div v-else class="d-flex flex-column">
            <p>Vos achats sont livrables. Veuillez faire votre choix de livraison.</p>

            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="inlineRadioOptions" id="aucuneLivraison" value="aucune" checked @change="montrerFormLivraison = false">
                <label class="form-check-label" for="aucuneLivraison">Aucune livraison</label>
            </div>
            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="inlineRadioOptions" id="livraison" value="livraison" @change="montrerFormLivraison = true">
                <label class="form-check-label" for="livraison">Livraison</label>
            </div>

            <div v-if="montrerFormLivraison" class="d-flex">
                <div class="col-md-6">
                    <table class="table">
                        <tbody>
                            <tr>
                                <th scope="col" class="text-start">Frais de livraison</th>
                                <td scope="col" class="text-start">{{facture.sousTotal}}</td>
                            </tr>
                            <tr>
                                <th scope="col" class="text-start">TPS</th>
                                <td scope="col" class="text-start"> {{facture.tps}}</td>
                            </tr>
                            <tr>
                                <th scope="col" class="text-start">TVQ</th>
                                <td scope="col" class="text-start">{{facture.tvq}}</td>
                            </tr>
                        </tbody>
                    </table>

                    <h2>TOTAL: {{facture.prixFinal}}</h2>
                </div>
            </div>

            <button id="submit" class="btn btn-outline bleuMoyenFond text-white btnSurvolerBleuMoyenFond my-2 col-3">
                <div class="spinner-grow d-none" id="spinner"></div>
                <span id="button-text">Enregistrer</span>
            </button>
        </div>
    </div>

</template>

<script setup>
    import { onMounted, ref } from "vue"
    import { useStore } from "vuex"

    const store = useStore();
    const props = defineProps({
        idFacture: Number
    })
    const facture = ref({})
    const factureLivraison = ref({})
    const chargement = ref(true)
    const montrerFormLivraison = ref(false)

    onMounted(async () => {
        const response = await store.dispatch("chercherPrevisualisationLivraison", props.idFacture)

        facture.value = response.data
        console.log(facture.value)

        chargement.value = false
    })
</script>
<style></style>