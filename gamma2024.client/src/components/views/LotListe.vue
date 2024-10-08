<template>
    <router-link class="text-decoration-none" to="DetailsLot">
        <div class="card">
            <div class="card-body">
                <div class="d-flex flex-md-row flex-column gap-5 flex-wrap justify-content-between">
                    <div class="d-flex flex-column justify-content-center align-items-center">
                        <img v-bind:src="lot.photos[0].lien"
                             height="150"
                             alt="Image du lot" />
                    </div>


                    <div class="d-flex align-items-center gap-3 justify-content-between">
                        <p class=" mt-1 mb-0">Lot {{lot.code}}</p>
                        <p class=" mt-1 mb-0">{{lot.artiste}}</p>
                        <p class=" mt-1 mb-0">{{lot.dimension}} po</p>
                    </div>



                    <div class="d-flex flex-column flex-md-row align-items-center gap-4">
                        <p class=" text-center mb-0">Valeur: {{(lot.valeurEstimeMin).toFixed(0)}}$ - {{(lot.valeurEstimeMax).toFixed(0)}}$</p>
                        <p class=" text-center mb-0">Mise actuelle: {{(lot.mise).toFixed(0)}}$</p>
                        <button type="button" class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond">Miser</button>
                    </div>

                    <div class="d-flex flex-column align-self-center pe-md-5">
                        <img src="/icons/IconeLivrable.png"
                             height="50"
                             width="50"
                             alt="Livrable"
                             v-if="lot.livrable" />

                        <img src="/icons/IconeNonLivrable.png"
                             height="50"
                             width="50"
                             alt="Non livrable"
                             v-else />
                    </div>
                </div>
            </div>
        </div>

    </router-link>
</template>

<script setup>
    import { ref } from 'vue'

    let lotRecu = {
        code: "1a",
        artiste: "Nom de l'artiste",
        dimension: "12 x 13",
        valeurEstimeMin: 1000.00,
        valeurEstimeMax: 2000.00,
        mise: 1100.03,
        livrable: false,
        photos: [
            {
                lien: "https://placehold.co/9000",
            },
            {
                lien: "https://placehold.co/6000",
            },
        ]
    };

    const lot = ref(lotRecu)

</script>

<style scoped>
    .fs-7 {
        font-size: 0.89rem;
    }
</style>