<template>
    <div class="container pb-3 d-flex flex-column align-items-center">
        <div class="row pb-3">
            <h1 class="text-center">Lot 1</h1>
        </div>
        <div class="row d-flex mb-3 pt-3 bleuMoyenFond contourDiv text-white w-50">
            <div class="col text-center">
                <p>Guy Paquet</p>
                <p>12 x 16 po</p>
                <p>1984/La nouvelle pratique</p>
                <p>Huile sur panneau</p>
                <p>Art-Paintings</p>
                <p>Valeur estimée: 600$ à 800$</p>
                <img src="/icons/IconeLivrable.png" alt="livrable" height="80" />
            </div>
        </div>
        <div class="d-flex flex-column">


            <div id="carouselExampleIndicators" class="carousel">

                <div class="carousel-inner">
                    <div class="carousel-item active">
                        <div class="d-flex justify-content-center">
                            <img :src="'/images'+lot.photos[0].lien" height="400" alt="...">
                        </div>
                    </div>
                    <div class="carousel-item">
                        <div class="d-flex justify-content-center">
                            <img :src="'/images'+lot.photos[1].lien" height="400" alt="...">
                        </div>
                    </div>
                    <div class="carousel-item">
                        <div class="d-flex justify-content-center">
                            <img :src="'/images'+lot.photos[2].lien" height="400" alt="...">
                        </div>
                    </div>
                    <div class="carousel-item">
                        <div class="d-flex justify-content-center">
                            <img :src="'/images/' + lot.photos[3].lien" height="400" alt="...">
                        </div>
                    </div>
                    <div class="carousel-item">
                        <div class="d-flex justify-content-center">
                            <img :src="'/images'+lot.photos[4].lien" height="400" alt="...">
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
                    <a data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1">
                        <img :src="'/images'+lot.photos[0].lien" height="60" alt="...">
                    </a>
                    <a data-bs-target="#carouselExampleIndicators" data-bs-slide-to="1" aria-label="Slide 2">
                        <img :src="'/images'+lot.photos[1].lien" height="60" alt="...">
                    </a>
                    <a data-bs-target="#carouselExampleIndicators" data-bs-slide-to="2" aria-label="Slide 3">
                        <img :src="'/images'+lot.photos[2].lien" height="60" alt="...">
                    </a>
                    <a data-bs-target="#carouselExampleIndicators" data-bs-slide-to="3" aria-label="Slide 4">
                        <img :src="'/images'+lot.photos[3].lien" height="60" alt="...">
                    </a>
                    <a data-bs-target="#carouselExampleIndicators" data-bs-slide-to="4" aria-label="Slide 5 ">
                        <img :src="'/images'+lot.photos[4].lien" height="60" alt="...">
                    </a>
                </div>

                <a class="opacity-100 d-flex align-items-center text-decoration-none" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
                    <span class="text-black fs-2" aria-hidden="true">></span>
                    <span class="visually-hidden">Next</span>
                </a>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref } from 'vue'

    let lotRecu = {
        photos: [
            {
                lien: "/lotsExemple/1a_1.png",
            },
            {
                lien: "/lotsExemple/1_2.png",
            },
            {
                lien: "/lotsExemple/1b_2.png",
            },
            {
                lien: "/lotsExemple/1b_3.png"
            },
            {
                lien: "/lotsExemple/2_2.png"
            }
        ]
    };

    const lot = ref(lotRecu)
</script>

<style scoped>
    .carouselFondIndicators {
        background-color: #C3CBD8;
    }
</style>