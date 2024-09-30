<template>

    <div class="d-flex flex-row-reverse w-100 px-4 me-2 gap-2">
        <button class="rounded bleuMoyenFond contourDiv btnSurvolerBleuMoyenFond">
            <img src="/icons/IconTableau.png" alt="Affichage en tableau" height="25" />
        </button>
        <button class="rounded bleuMoyenFond contourDiv btnSurvolerBleuMoyenFond">
            <img src="/icons/IconListe.png" alt="Affichage en liste" height="25" />
        </button>
        <button class="d-flex align-items-center text-center rounded bleuMoyenFond text-white contourDiv btnSurvolerBleuMoyenFond"
                @click="afficherTousLots"
                v-bind:disabled="lotsParPage == nbLotsRecus">
            Tous
        </button>
        <button class="d-flex align-items-center text-center rounded bleuMoyenFond text-white contourDiv btnSurvolerBleuMoyenFond"
                @click="changerNbLotParPage(100)"
                v-bind:disabled="lotsParPage == 100">
            100
        </button>
        <button class="d-flex align-items-center text-center rounded bleuMoyenFond text-white contourDiv btnSurvolerBleuMoyenFond"
                @click="changerNbLotParPage(50)"
                v-bind:disabled="lotsParPage == 50">
            50
        </button>
        <button class="d-flex align-items-center text-center rounded bleuMoyenFond text-white contourDiv btnSurvolerBleuMoyenFond"
                @click="changerNbLotParPage(20)"
                v-bind:disabled="lotsParPage == 20">
            20
        </button>
    </div>

    <div class="row row-cols-lg-5 row-cols-md-3 row-cols-sm-2 row-cols-1 w-100 px-3">
        <div v-for="index in lotsParPage" class="col p-2">
            <LotTuile />
        </div>
    </div>

    <div class="d-flex flex-row justify-content-around gap-1 flex-wrap p-3">
        <button type="button"
                class="btn btn-primary"
                @click="reculerPage"
                v-bind:disabled="pageCourante == 1">
            <
        </button>

        <div v-for="item in listePagination">
            <button type="button"
                    class="btn btn-primary"
                    :pageId="item"
                    @click="changerPage()"
                    v-bind:disabled="pageCourante == item || item == '...'">
                {{item}}
            </button>

        </div>

        <button type="button"
                class="btn btn-primary"
                @click="avancerPage"
                v-bind:disabled="pageCourante == nbPages">
            >
        </button>
    </div>

</template>

<script setup>
    import LotTuile from '@/components/views/LotTuile.vue'
    import { ref } from 'vue'

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
    for (let i = 0; i < 300; i++) {
        listeLots.push(unLot)
    }
    //fin du code temporaire


    const nbLotsRecus = ref(listeLots.length)
    const lotsParPage = ref(nbLotsRecus.value)
    const pageCourante = ref(1)

    const nbPages = ref(recalculerNbPages())
    const listePagination = ref([])

    genererListePagination

    const changerNbLotParPage = ref(function (nouvLotsParPage) {
        lotsParPage.value = nouvLotsParPage;
        nbPages.value = recalculerNbPages();
        pageCourante.value = 1
        genererListePagination()
    })

    const afficherTousLots = ref(function () {
        lotsParPage.value = nbLotsRecus.value;
        nbPages.value = recalculerNbPages();
        pageCourante.value = 1
        genererListePagination()
    })

    const reculerPage = ref(function () {
        if (pageCourante.value > 1) {
            pageCourante.value--
            genererListePagination()
        }
    })

    const avancerPage = ref(function () {
        if (pageCourante.value < nbPages.value) {
            pageCourante.value++
            genererListePagination()
        }
    })

    const changerPage = ref(function () {
        pageCourante.value = parseInt(event.srcElement.getAttribute('pageId'))
        genererListePagination()
    })

    function recalculerNbPages() {
        return Math.ceil(nbLotsRecus.value / lotsParPage.value)
    }

    function genererListePagination() {

        listePagination.value = []

        for (let i = 1; i <= nbPages.value; i++) {
            if (i == 1
                || (i >= pageCourante.value - 1 && i <= pageCourante.value + 1)
                || (i == nbPages.value)) {
                listePagination.value.push(i)
            }
            else if (listePagination.value[listePagination.value.length - 1] != '...') {
                listePagination.value.push('...')
            }
        }
    }


</script>

<style scoped>
</style>