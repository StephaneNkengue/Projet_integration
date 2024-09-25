<template>
    <router-link class="text-decoration-none" to="/">
        <div class="card">
            <div class="card-body align-items-middle">
                <div class="d-flex justify-content-between align-items-middle">
                    <p class="text-start mb-0">Lot {{lot.code}}</p>
                    <p class="text-end mb-0">{{lot.artiste}}</p>
                </div>

                <div class="d-flex justify-content-between align-items-middle">
                    <p class="text-start mb-0">{{lot.hauteur}} X {{lot.largeur}}</p>

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
                    <button type="button" class="btn btn-primary">Miser</button>
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

<style scoped></style>