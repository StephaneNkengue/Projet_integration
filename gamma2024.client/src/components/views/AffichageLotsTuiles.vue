<template>

    <div class="row row-cols-lg-5 row-cols-md-3 row-cols-sm-2 row-cols-1 w-100 px-3">
        <div v-for="index in lotsParPage" class="col p-2">
            <LotTuile />
        </div>
    </div>

    <Pagination :nbPages="nbPages" @nouvellePageCourante="infoPage" />

</template>

<script setup>
    import LotTuile from '@/components/views/LotTuile.vue'
    import Pagination from '@/components/views/Pagination.vue'
    import { ref, watch } from 'vue'

    //code temporaire afin de tester l'affichage des lots par page
    let unLot = {
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
    let listeLots = [];
    for (let i = 0; i < 30; i++) {
        listeLots.push(unLot)
    }
    //fin du code temporaire


    const lotsParPage = ref(20)
    const nbLotsRecus = ref(listeLots.length)
    const pageCourante = ref(1)

    const nbPages = ref(Math.ceil(nbLotsRecus.value / lotsParPage.value))

    const infoPage = ref(function (nouvPage) {
        pageCourante.value = nouvPage
    })

    watch(pageCourante, async (nouvPage, viellePage) => {
        console.log("page " + viellePage + " -> page" + nouvPage)
    })
</script>

<style scoped>
</style>