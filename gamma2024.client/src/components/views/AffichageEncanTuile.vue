<template>

    <div class="contourDiv p-2 align-items-center d-flex d-flex flex-column">
        <h2>Encan #105</h2>
        <div class="d-flex w-100 ps-5 mt-3">
            <div class="col d-flex align-items-center justify-content-center flex-column">
                <div class="row justify-content-start w-100">
                    <p>Encan d'oeuvres d'art</p>
                </div>
                <div class="row justify-content-start w-100">
                    <p>Date de début : <br />15 septembre 2024</p>
                </div>
                <div class="row justify-content-start w-100">
                    <p>Date de fin : <br />25 septembre 2024</p>
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
        width: 200px;
        height: 200px;
    }
</style>