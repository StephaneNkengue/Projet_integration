<template>
    <div class="contourDiv p-2 align-items-center d-flex flex-column">
        <h2>Encan #105</h2>
        <div class="d-flex w-100 mt-3 flex-sm-row flex-column">
            <div class="col-7 d-flex align-items-center justify-content-center flex-column text-center">
                <div class="row justify-content-start w-100">
                    <p>15 septembre - 25 septembre 2024</p>
                </div>
                <div class="row justify-content-start w-100">
                    <p>Soirée de clôture le 25 septembre à 18h</p>
                </div>
            </div>
            <div class="col d-flex justify-content-center align-items-center">
                <img v-bind:src="lot.photos[0].lien"
                     class="img-fluid"
                     alt="Image d'un lot" />
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref } from 'vue'

    let lotRecu = {
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
    img {
        height: 200px;
    }
</style>