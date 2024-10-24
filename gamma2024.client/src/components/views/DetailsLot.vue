<template>
    <div class="container pb-3 d-flex flex-column align-items-center">
        <div class="row pb-3">
            <h1 class="text-center">Lot {{lot.numero}}</h1>
        </div>

        <div class="row gap-5 w-100 cols-lg-2 cols-1">
            <div class="col d-flex pt-3 bleuMoyenFond contourDiv text-white align-items-center">
                <div class="col text-center">
                    <p>{{lot.artiste}}</p>
                    <p>{{lot.hauteur}} x {{lot.largeur}} po</p>
                    <p>{{lot.description}}</p>
                    <p>{{lot.medium}}</p>
                    <p>{{lot.categorie}}</p>
                    <p>Valeur estimée: {{(lot.valeurEstimeMin).toFixed(0)}}$ à {{(lot.valeurEstimeMax).toFixed(0)}}$</p>

                    <img v-if="lot.estLivrable" src="/icons/IconeLivrable.png" alt="livrable" height="80" />
                    <img v-else src="/icons/IconeNonLivrable.png" alt="livrable" height="80" />
                </div>
            </div>
            <div class="col">
                <div id="carouselExampleIndicators" class="carousel">

                    <div class="carousel-inner">

                        <div v-for="(image, index) in lot.photos">
                            <div :class="{active: index==0, 'carousel-item': true}">
                                <div class="d-flex justify-content-center">
                                    <img :src="urlApi + image" height="400" alt="...">
                                </div>
                            </div>
                        </div>

                    </div>

                </div>

                <div class="d-flex justify-content-between align-items-center mt-2 carouselFondIndicators p-1">

                    <a class="opacity-100 d-flex align-items-center text-decoration-none" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
                        <span class="text-black fs-2" aria-hidden="true"><</span>
                        <span class="visually-hidden">Previous</span>
                    </a>

                    <div class="gap-1 d-flex">

                        <div v-for="(image, index) in lot.photos">
                            <a data-bs-target="#carouselExampleIndicators" :data-bs-slide-to="index" class="active " aria-current="true" :aria-label="'Slide ' + (index+1)">
                                <img :src="urlApi + image" height="60" alt="Image du lot">
                            </a>

                        </div>
                    </div>

                    <a class="opacity-100 d-flex align-items-center text-decoration-none" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
                        <span class="text-black fs-2" aria-hidden="true">></span>
                        <span class="visually-hidden">Next</span>
                    </a>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { onMounted, ref } from "vue";
    import { useStore } from "vuex";

    const store = useStore();

    const chargement = ref(true);
    const urlApi = ref("/api")
    const lot = ref({
        "id": 0,
        "numero": "",
        "description": "",
        "valeurEstimeMin": 0,
        "valeurEstimeMax": 0,
        "artiste": "",
        "mise": 0,
        "estVendu": null,
        "estLivrable": null,
        "largeur": 0,
        "hauteur": 0,
        "categorie": "",
        "photos": [
            ""
        ],
        "medium": ""
    });

    const props = defineProps({
        idLot: String,
    });

    onMounted(async () => {
        const id = props.idLot;
        const response = await store.dispatch("chercherDetailsLotParId", id);
        lot.value = response.data
        urlApi.value = await store.state.api.defaults.baseURL.replace("\api", "")

        chargement.value = false;
    });
</script>

<style scoped>
    .carouselFondIndicators {
        background-color: #C3CBD8;
    }
</style>