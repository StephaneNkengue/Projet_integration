<template>
    <router-link class="text-decoration-none" to="DetailsLot">
        <div class="card">
            <div class="card-body align-items-middle">
                <div class="d-flex align-items-middle justify-content-between">
                    <p class="fs-7 pe-2 mt-1 mb-0">Lot {{lot.numero}} {{lot.artiste}} {{lot.hauteur}} x {{lot.largeur}} po</p>

                    <img src="/icons/IconeLivrable.png"
                         height="30"
                         alt="Livrable"
                         v-if="lot.estLivrable" />

                    <img src="/icons/IconeNonLivrable.png"
                         height="30"
                         alt="Non livrable"
                         v-else />
                </div>

                <div class="row justify-content-around align-items-middle">
                    <img v-bind:src="urlApi + lot.photos[0].lien"
                         class="img-fluid my-2 col-8 col-sm-10 col-md-12 "
                         alt="Image du lot" />

                </div>

                <p class="text-center mb-0">Valeur: {{(lot.valeurEstimeMin).toFixed(0)}}$ - {{(lot.valeurEstimeMax).toFixed(0)}}$</p>
                <p v-if="lot.mise != null" class="text-center mb-0">Mise actuelle: {{(lot.mise).toFixed(0)}}$</p>

                <div class="d-flex justify-content-around pt-2">
                    <button type="button" class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond">Miser</button>
                </div>

            </div>

        </div>

    </router-link>

</template>

<script setup>
    import { onMounted, ref } from 'vue'
    import { useStore } from "vuex";

    const store = useStore();

    const props = defineProps({
        lotRecu: Object
    })

    const urlApi = ref("/api")
    const lot = ref({
        "id": 0,
        "numero": "",
        "description": "",
        "valeurEstimeMin": 0,
        "valeurEstimeMax": 0,
        "artiste": " ",
        "mise": 0,
        "estVendu": true,
        "dateFinVente": "",
        "estLivrable": true,
        "largeur": 0,
        "hauteur": 0,
        "photos": [
            {
                "id": 0,
                "lien": "",
                "idLot": 0,
                "lot": null
            }
        ]
    })

    onMounted(async () => {
        lot.value = props.lotRecu
        urlApi.value = await store.state.api.defaults.baseURL.replace("\api", "")
    })

</script>

<style scoped>
    .fs-7 {
        font-size: 0.89rem;
    }
</style>