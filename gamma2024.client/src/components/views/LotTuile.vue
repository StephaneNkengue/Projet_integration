<template>
    <router-link class="text-decoration-none" to="DetailsLot">
        <div class="card">
            <div class="card-body align-items-middle">
                <div class="d-flex align-items-middle justify-content-between">
                    <p class="fs-7 pe-2 mt-1 mb-0">{{lot.code}}: {{lot.artiste}} ({{lot.hauteur}} X {{lot.largeur}} po)</p>

                    <img src="/icons/IconeLivrable.png"
                         height="30"
                         alt="Livrable"
                         v-if="lot.livrable" />

                    <img src="/icons/IconeNonLivrable.png"
                         height="30"
                         alt="Non livrable"
                         v-else />
                </div>

                <div class="row justify-content-around align-items-middle">
                    <img v-bind:src="lot.photos[0].lien"
                         class="img-fluid my-2 col-8 col-sm-10 col-md-12 "
                         alt="Image du lot" />

                </div>

                <p class="text-center mb-0">Valeur: {{(lot.valeurEstimeMin).toFixed(0)}}$ - {{(lot.valeurEstimeMax).toFixed(0)}}$</p>
                <p class="text-center mb-0">Mise actuelle: {{(lot.mise).toFixed(0)}}$</p>

                <div class="d-flex justify-content-around pt-2">
                    <button type="button" class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond">Miser</button>
                </div>

            </div>

        </div>

    </router-link>

</template>

<script setup>
    import { ref } from 'vue'

    let lotRecu = {
        code: "1-a",
        artiste: "Nom de l'artiste",
        hauteur: 12,
        largeur: 13,
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