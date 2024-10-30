<template>
    <div class="d-flex flex-column align-items-center pb-3">
        <h1>Encan en cours <span v-if="encan != ''">({{encan.numeroEncan}})</span></h1>

        <div class="d-flex gap-2" v-if="chargement">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Chargement des encans...</span>
            </div>
            <p>Chargement de l'encan en cours...</p>
        </div>

        <div v-if="!chargement" class="w-100">
            <h5 class="text-center" v-if="encan == ''">
                Il n'y a présentement aucun encan en cours
            </h5>

            <div v-else>
                <p class="text-center">Date de debut de la soirée de cloture: {{soireeDate}}</p>

                <AffichageLots :idEncan="encan.id" />
            </div>
        </div>
    </div>
</template>

<script setup>
    import AffichageLots from "@/components/views/AffichageLots.vue";
    import { onMounted, ref, reactive } from "vue";
    import { useStore } from "vuex";

    let mois = [
        "janvier", "février", "mars", "avril", "mai", "juin", "juillet", "août", "septembre", "octobre", "novembre", "décembre"
    ]

    const store = useStore();

    const chargement = ref(true);
    const encan = ref('');
    const soireeDate = ref()

    onMounted(async () => {
        const response = await store.dispatch("chercherEncanEnCours");
        encan.value = response.data
        if (encan.value != "") {
            soireeDate.value = formatageDate(encan.value.dateDebutSoireeCloture, true, true)
        }

        chargement.value = false;
    });

    function formatageDate(dateTexte, siAnnee, siHeure) {
        let sep = dateTexte.split("T")
        let dates = sep[0].split("-")
        let temps = sep[1].split(":")

        let resultat = dates[2] + " " + mois[dates[1] - 1]

        if (siAnnee) {
            resultat += " " + dates[0]
        }
        if (siHeure) {
            resultat += " à " + parseInt(temps[0]) + "h" + temps[1]
        }
        return resultat
    }
</script>

<style scoped></style>