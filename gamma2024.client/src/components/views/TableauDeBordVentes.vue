<template>
    <div class="container mt-5">
        <h3 class="text-center">Rechercher une vente</h3>
        <input v-model="searchQuery"
               class="form-control row col-10 ms-1"
               type="search"
               placeholder="Rechercher une vente"
               aria-label="Search" />
        <h1 class="text-center mt-5">Liste des ventes</h1>

        <div class="accordion" id="accordionEncan">
            <div class="accordion-item " v-for="encan in numerosEncans" :key="encan">
                <h2 class="accordion-header px-0">
                    <button class="accordion-button" type="button" data-bs-toggle="collapse" :data-bs-target="'#collapse'+encan" aria-expanded="true" :aria-controls="'collapse'+encan">
                        {{encan}} ()
                    </button>
                </h2>
                <!--Changer le 233 en le numero de l'encan le plus récent-->
                <div :id="'collapse'+encan" class="accordion-collapse collapse" :class="{ show: encan == numerosEncans[0]}" data-bs-parent="#accordionEncan">
                    <div class="accordion-body">
                        <div class="accordion" id="accordionClient" v-for="(facture, index) in filteredVentes.filter((x)=>x.encan == encan)">
                            <div class="accordion-item">
                                <h2 class="accordion-header px-0">
                                    <button class="accordion-button" type="button" data-bs-toggle="collapse" :data-bs-target="'#collapseFacture'+facture.id" aria-expanded="true" :aria-controls="'collapseFacture'+facture.id">
                                        {{facture.prenom}} {{facture.nom}} ({{facture.pseudonyme}})<br />{{facture.courriel}}<br />{{facture.telephone}}
                                    </button>
                                </h2>
                                <div :id="'collapseFacture'+facture.id" class="accordion-collapse collapse" data-bs-parent="#accordionClient">
                                    <div class="accordion-body">
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
                                                    <td>{{lot.mise}}</td>
                                                    <td>
                                                        <img v-if="lot.estLivrable" src="/icons/livrable.png" />
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

    </div>
</template>

<script setup>
    import { computed, onMounted, ref } from "vue";
    import { useStore } from 'vuex';
    import DetailsAchats from "../modals/DetailsAchats.vue";

    const store = useStore();
    const listeFactures = ref([]);
    const searchQuery = ref("");
    const numerosEncans = ref([])

    onMounted(async () => {
        try {
            listeFactures.value = await store.dispatch("fetchFactureInfo");

            listeFactures.value[0].encan = '233'
            listeFactures.value[0].lots[0].estLivrable = 0

            listeFactures.value.forEach(x => numerosEncans.value.push(parseInt(x.encan)))
            numerosEncans.value = numerosEncans.value.filter((value, index, self) => self.indexOf(value) === index).sort(function (a, b) { return b - a; })
        }
        catch (error) {
            console.log("Erreur factures" + error);
        }

    });

    const tousLesVentes = computed(() => {
        if (listeFactures.value) {
            return listeFactures.value;
        }
        return [];
    });

    const filteredVentes = computed(() => {
        return tousLesVentes.value.filter((vente) => {
            const searchLower = searchQuery.value.toLowerCase();
            return (
                vente.dateAchat.toLowerCase().includes(searchLower) ||
                vente.prenom.toLowerCase().includes(searchLower) ||
                vente.nom.toLowerCase().includes(searchLower)
            );
        });
    });
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

    .btn {
        background-color: #0dcaf0;
    }
</style>