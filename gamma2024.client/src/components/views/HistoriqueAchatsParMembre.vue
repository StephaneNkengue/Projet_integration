<template>
    <FactureModal v-for="encan in numerosEncans" :facturePdfPath="encan.pdfPath" :idFacture="encan.id"></FactureModal>
    <div class="container mt-5">
        <router-link class="text-decoration-none" :to="{ name: 'ChoixLivraison', params: {idFacture:facture.id} }" v-for="facture in listeFacturesChoixAFaire" :key="facture.id">
            <div class="alert alert-danger" role="alert">
                Veuillez faire votre choix de livraison pour l'encan {{ facture.numeroEncan }}
            </div>
        </router-link>

        <h3 class="text-center">Rechercher une vente</h3>

        <input v-model="searchQuery"
               class="form-control row col-10 ms-1"
               type="search"
               placeholder="Rechercher une vente"
               aria-label="Search" />

        <h1 class="text-center mt-5">Liste des achats</h1>

        <div class="d-flex flex-row gap-2 justify-content-end mb-3">
            <button class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
                    @click="changerNbVenteParPage(20)"
                    v-bind:disabled="ventesParPage == 20">
                20
            </button>
            <button class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
                    @click="changerNbVenteParPage(50)"
                    v-bind:disabled="ventesParPage == 50">
                50
            </button>
            <button class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
                    @click="changerNbVenteParPage(100)"
                    v-bind:disabled="ventesParPage == 100">
                100
            </button>
            <button class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
                    type="button"
                    @click="afficherTousVentes"
                    v-bind:disabled="ventesParPage == nbVentesRecus">
                Tous
            </button>
        </div>

        <div class="d-flex gap-2 justify-content-center" v-if="chargement">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Chargement des ventes...</span>
            </div>
            <p>Chargement des ventes en cours...</p>
        </div>

        <div v-if="!chargement" class="w-100">
            <div class="d-flex justify-content-center" v-if="!filteredVentes.length">
                <h2>Aucun résultat trouvé</h2>
            </div>

            <div v-else class="mb-5">
                <div class="accordion" id="accordionEncan">
                    <div class="accordion-item" v-for="encan in numerosEncans" :key="encan">
                        <div v-if="filteredVentes.filter((x) => x.encan == encan.encan) != 0 ">
                            <h2 class="accordion-header px-0 d-flex" :style="{border: styleBorder}">
                                <button class="accordion-button h-52" type="button" data-bs-toggle="collapse" :data-bs-target="'#collapse' + encan.encan"
                                        aria-expanded="true" :aria-controls="'collapse' + encan.encan">
                                    <div class="col-11 d-flex flex-row">
                                        <div class="pe-5">
                                            Encan : {{ encan.encan }}
                                        </div>
                                        <div>
                                            Date de la facturation : {{ encan.dateAchat.split("T")[0] }}
                                        </div>
                                    </div>

                                    <div class="col">
                                        <button class="btn btn-info"
                                                data-bs-toggle="modal"
                                                :data-bs-target="'#modal'+encan.id">
                                            <img src="/images/ice.png" class="btnVisuel img-fluid" alt="..." />
                                        </button>
                                    </div>
                                </button>
                            </h2>
                            <!--Changer le 233 en le numero de l'encan le plus récent-->
                            <div :id="'collapse' + encan.encan"
                                 class="accordion-collapse collapse"
                                 :class="{ show: encan == numerosEncans[0] }"
                                 data-bs-parent="#accordionEncan">
                                <div class="accordion-body">
                                    <div class="accordion" id="accordionClient" v-for="facture in ventesAffiche.filter((x)=> x.encan == encan.encan)" :key="facture.id">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th scope="col">Numéro du lot</th>
                                                    <th scope="col">Prix vendu</th>
                                                    <th scope="col">Livraison</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr v-for="lot in facture.lots" :key="lot.id">
                                                    <td scope="row">{{ lot.numero }}</td>
                                                    <td>{{ lot.mise }}$</td>
                                                    <td>
                                                        <img v-if="lot.estLivrable"
                                                             src="/icons/livrable.png" />
                                                        <img v-else src="/icons/nonlivrable.png" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="d-flex flex-row justify-content-center gap-1 flex-wrap p-3" v-if="ventesAffiche.length != 0">
            <button type="button"
                    class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
                    @click="reculerPage"
                    v-bind:disabled="pageCourante == 1">
                <
            </button>

            <div v-for="item in listePagination" :key="item.id">
                <button type="button"
                        class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
                        :pageId="item"
                        @click="changerPage()"
                        v-bind:disabled="pageCourante == item || item == '...'">
                    {{ item }}
                </button>
            </div>

            <button type="button"
                    class="btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
                    @click="avancerPage"
                    v-bind:disabled="pageCourante == nbPages">
                >
            </button>
        </div>
    </div>
</template>

<script setup>
    import { computed, watch, onMounted, ref } from "vue";
    import { useStore } from "vuex";
    import FactureModal from "@/components/modals/VoirFactureModal.vue";

    const store = useStore();
    const listeFacturesMembre = ref([]);
    const numerosEncans = ref([]);
    const searchQuery = ref("");
    const styleBorder = ref('');
    const chargement = ref(true);

    const pageCourante = ref(1);
    const ventesParPage = ref();
    const nbVentesRecus = ref();
    const nbPages = ref();
    const listePagination = ref([]);
    const ventesAffiche = ref([]);

    const listeFacturesChoixAFaire = ref([]);

    onMounted(async () => {
        try {
            listeFacturesMembre.value = await store.dispatch("fetchFactureInfoMembre");

            listeFacturesMembre.value.filter(function (value, index, self) {
                if ((index === self.findIndex((t) => t.encan === value.encan)) == true) {
                    numerosEncans.value.push(value);
                }
            });

            const response = await store.dispatch("chercherFacturesChoixAFaire");
            listeFacturesChoixAFaire.value = response.data;

            nbVentesRecus.value = filteredVentes.value.length;
            ventesParPage.value = nbVentesRecus.value;
            nbPages.value = recalculerNbPages();

            genererListePagination();
            chercherVentesAAfficher();

            chargement.value = false;
        } catch (error) {
            console.log("Erreur factures" + error);
        }
    });

    const tousLesVentes = computed(() => {
        if (listeFacturesMembre.value) {
            return listeFacturesMembre.value;
        }
        return [];
    });

    const filteredVentes = computed(() => {
        return tousLesVentes.value.filter((vente) => {
            const searchLower = searchQuery.value.toLowerCase();
            return (
                vente.encan.toString().startsWith(searchLower) ||
                vente.dateAchat.toLowerCase().startsWith(searchLower) ||
                vente.lots.find(l => l.numero.startsWith(searchLower))
            );
        });
    });

    watch(filteredVentes, () => {
        if (listeFacturesMembre.value.length == filteredVentes.value.length) {
            styleBorder.value = 'none';
        }
        else {
            styleBorder.value = '2px solid green'
        }

        nbVentesRecus.value = filteredVentes.value.length;
        pageCourante.value = 1;
        AjusterPagination();
    });

    watch(pageCourante, () => {
        genererListePagination();
        chercherVentesAAfficher();
    });

    const changerNbVenteParPage = ref(function (nouvVentesParPage) {
        ventesParPage.value = nouvVentesParPage;
        nbPages.value = recalculerNbPages();
        pageCourante.value = 1;
        AjusterPagination();
    });

    const afficherTousVentes = ref(function () {
        ventesParPage.value = nbVentesRecus.value;
        nbPages.value = recalculerNbPages();
        pageCourante.value = 1;
        AjusterPagination();
    });

    const reculerPage = ref(function () {
        if (pageCourante.value > 1) {
            pageCourante.value--;
        }
    });

    const avancerPage = ref(function () {
        if (pageCourante.value < nbPages.value) {
            pageCourante.value++;
        }
    });

    const changerPage = ref(function () {
        pageCourante.value = parseInt(event.srcElement.getAttribute("pageId"));
    });

    function recalculerNbPages() {
        return Math.ceil(nbVentesRecus.value / ventesParPage.value);
    }

    function genererListePagination() {
        listePagination.value = [];

        for (let i = 1; i <= nbPages.value; i++) {
            if (
                i == 1 ||
                (i >= pageCourante.value - 1 && i <= pageCourante.value + 1) ||
                i == nbPages.value
            ) {
                listePagination.value.push(i);
            } else if (
                listePagination.value[listePagination.value.length - 1] != "..."
            ) {
                listePagination.value.push("...");
            }
        }
    }

    function AjusterPagination() {
        nbPages.value = recalculerNbPages();
        genererListePagination();
        chercherVentesAAfficher();
    }

    function chercherVentesAAfficher() {
        ventesAffiche.value = [];

        let positionDebut = (pageCourante.value - 1) * ventesParPage.value;
        let positionFin = pageCourante.value * ventesParPage.value;

        for (let i = positionDebut; i < positionFin && i < filteredVentes.value.length; i++) {
            ventesAffiche.value.push(filteredVentes.value[i]);
        }
    }
</script>

<style scoped>
    img {
        width: 25px;
        height: 30px;
    }

    table,
    input {
        box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 3px 5px 0 rgba(0, 0, 0, 0.19);
    }

    th {
        font-size: 18px;
    }

    td {
        font-size: 16px;
    }

    .btnVisuel {
        background-color: #0dcaf0;
    }

    .accordion {
        --bs-accordion-active-bg: none;
    }

    .h-52 {
        height: 52px;
    }
</style>