<template>
    <div class="container">
        <h1>Choix de livraison</h1>

        <p id="message" v-show="siMessage"></p>

        <div v-if="chargement">
            <div class="spinner-border" role="status"></div>
        </div>
        <div v-else-if="!siMessage" class="d-flex flex-column">
            <p>Vos achats durant l'encan {{facture.facture.numeroEncan}} sont livrables. Veuillez faire votre choix de livraison.</p>

            <div class="form-check form-check-inline">
                <input class="form-check-input" type="radio" name="inlineRadioOptions" id="aucuneLivraison" value="aucune" checked @change="montrerFormLivraison = false; siDonation= false">
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
                                <td scope="col" class="text-end">{{facture.sousTotal.toFixed(2)}} $</td>
                            </tr>
                            <tr>
                                <th scope="col" class="text-start">TPS</th>
                                <td scope="col" class="text-end"> {{facture.tps.toFixed(2)}} $</td>
                            </tr>
                            <tr>
                                <th scope="col" class="text-start">TVQ</th>
                                <td scope="col" class="text-end">{{facture.tvq.toFixed(2)}} $</td>
                            </tr>
                            <tr v-if="siDonation">
                                <th scope="col" class="text-start">Don</th>
                                <td scope="col" class="text-end">{{facture.don.toFixed(2)}} $</td>
                            </tr>
                        </tbody>
                    </table>

                    <div class="form-check">
                        <input class="form-check-input" type="checkbox" value="" id="donationCheck" @change="toggleDonation">
                        <label class="form-check-label" for="donationCheck">
                            Donation
                        </label>
                    </div>

                    <div v-if="siDonation">
                        <select class="form-select py-0"
                                aria-label="Charite select"
                                id="chariteSelList">
                            <option v-for="charite in facture.charites" class="py-0" :value="charite.id" selected>{{charite.nomOrganisme}}</option>
                        </select>
                    </div>

                    <select class="form-select py-0"
                            aria-label="Default select example"
                            id="adresseSelList">
                        <option v-for="adresse in adresses" class="py-0" :value="adresse.id" selected>{{adresse.appartement}} {{adresse.numeroCivique}} {{adresse.rue}}, {{adresse.ville}}, {{adresse.province}}, {{adresse.pays}}, {{adresse.codePostal}}</option>
                    </select>

                    <h2 v-if="siDonation">TOTAL: {{facture.prixFinal.toFixed(2)}} $</h2>
                    <h2 v-else>TOTAL: {{facture.prixFinalSansDon.toFixed(2)}} $</h2>
                </div>
            </div>

            <button id="submit" class="btn btn-outline bleuMoyenFond text-white btnSurvolerBleuMoyenFond my-2 col-3" @click="enregistrerChoixLivraison">
                <div class="spinner-grow" id="spinner" v-if="chargementSauvegarde"></div>
                <span id="button-text" v-else>Enregistrer</span>
            </button>
        </div>
    </div>

</template>

<script setup>
    import { onMounted, ref } from "vue"
    import { useStore } from "vuex"

    const store = useStore();
    const props = defineProps({
        idFacture: String
    })
    const facture = ref({})
    const chargement = ref(true)
    const montrerFormLivraison = ref(false)
    const siDonation = ref(false)
    const toggleDonation = ref(function () {
        siDonation.value = document.getElementById('donationCheck').checked
    })
    const adresses = ref([]);
    const siMessage = ref(false)
    const chargementSauvegarde = ref(false)

    onMounted(async () => {
        try {
            const response = await store.dispatch("chercherPrevisualisationLivraison", parseInt(props.idFacture))
            facture.value = response.data

            const adresseResponse = await store.dispatch("chercherAdressesClient");
            adresses.value = adresseResponse.data
        } catch (error) {
            siMessage.value = true
            document.getElementById("message").innerText = error.response.data
        }
        chargement.value = false
    })

    const enregistrerChoixLivraison = ref(async function () {
        chargementSauvegarde.value = true
        try {
            var choixLivraison = {}
            if (montrerFormLivraison.value) {
                var chariteSelList = document.getElementById("chariteSelList")
                var adresseSelList = document.getElementById("adresseSelList")
                var idChariteSel = null

                if (chariteSelList != undefined) {
                    idChariteSel = chariteSelList.value
                }
                choixLivraison =
                {
                    don: siDonation.value,
                    idCharite: idChariteSel,
                    idFacture: props.idFacture,
                    idAdresse: adresseSelList.value
                }
            } else {
                choixLivraison = {
                    don: null,
                    idCharite: null,
                    idFacture: props.idFacture,
                    idAdresse: null
                }
            }

            const factureLivraisonId = await store.dispatch("enregistrerChoixLivraison", choixLivraison)

            if (factureLivraisonId.data != "") {
                const payment = await store.dispatch("payerFactureLivraison", factureLivraisonId.data)
            }

            siMessage.value = true
            document.getElementById("message").innerText = "Choix de livraison sauvegardé et payé."
        } catch (error) {
            siMessage.value = true
            document.getElementById("message").innerText = error.response.data
        }
    })
</script>
<style></style>