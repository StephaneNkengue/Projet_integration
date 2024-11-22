<template lang="">
    <div class="modal fade"
         :id="'modal' + props.idFacture"
         data-bs-backdrop="static"
         data-bs-keyboard="false"
         tabindex="-1"
         aria-labelledby="staticBackdropLabel"
         aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content min-vh-75">
                <div class="modal-header">
                    <a :href="pdf" target="_blank">Voir le pdf dans un nouveau onglet</a>
                    <button type="button"
                            class="btn-close"
                            data-bs-dismiss="modal"
                            aria-label="Close"></button>
                </div>
                <div class="modal-body d-flex justify-content-center h-100">
                    <div v-if="chargement">
                        <p>Le PDF de votre facture est présentement en génération. Veuillez revenir plus tard.</p>
                    </div>
                    <iframe :src="pdf" class="w-100 min-vh-50" scrolling="no" v-else />
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref, onMounted } from "vue";
    import { useStore } from "vuex";

    const store = useStore();

    const props = defineProps({
        facturePdfPath: String,
        idFacture: Number
    });

    const pdf = ref("")
    const chargement = ref(true)

    onMounted(async () => {
        const response = await store.state.api.defaults.baseURL.replace("/api", "")
        pdf.value = response + props.facturePdfPath

        if (pdf.value != response) {
            chargement.value = false
        }
    });
</script>
<style scoped>
    .min-vh-75 {
        min-height: 75vh
    }

    .min-vh-50 {
        min-height: 50vh
    }
</style>
