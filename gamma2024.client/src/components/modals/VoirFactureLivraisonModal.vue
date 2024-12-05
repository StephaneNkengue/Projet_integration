<template lang="">
    <div class="modal fade"
         :id="'modalFactureLivraison' + props.idFacture"
         data-bs-backdrop="static"
         data-bs-keyboard="false"
         tabindex="-1"
         aria-labelledby="staticBackdropLabel"
         aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content min-vh-75">
                <div class="modal-header">
                    <button type="button"
                            class="btn-close"
                            data-bs-dismiss="modal"
                            aria-label="Close"></button>
                </div>
                <div class="modal-body d-flex justify-content-center h-100">
                    <div class="container my-2">
                        <div class="d-flex align-items-center">
                            <h5 class="col-9">Les Encans de Nantes au Québec - FACTURE LIVRAISON</h5>
                            <div class="col-3">
                                <img class="img-fluid" src="https://sqlinfocg.cegepgranby.qc.ca/2162067/images/Logo.png" />
                            </div>
                        </div>
                        <hr />
                        <div class="d-flex">
                            <div class="d-flex flex-column col-6 justify-content-between">
                                <h5>Facturé à</h5>
                                <span>{model.Client.Nom}</span>
                                <span>{model.Client.AdresseLigne1}</span>
                                <span>{model.Client.AdresseLigne2}</span>
                                <span>{model.Client.AdresseLigne3}</span>
                                <span>{model.Client.CodePostal}</span>
                                <span>{model.Client.Courriel}</span>
                                <span>{model.Client.Telephone}</span>
                            </div>
                            <div class="col-6 d-flex justify-content-end">
                                <h5>{model.DateAchat}</h5>
                            </div>
                        </div>
                        <br />
                        <h3 class="text-center">
                            {model.PrixFinal} $ payé le {model.DateAchat}
                        </h3>
                        <div class="d-flex w-100">
                            <table class="table">
                                <tbody id="tableProduits">
                                    <tr>
                                        <th scope="col" class="text-start">Frais de livraison</th>
                                        <td scope="col" class="text-end">{model.FraisLivraison} $</td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <br />
                        <div class="d-flex flex-column">
                            <table class="table col-6">
                                <tbody>
                                    <tr>
                                        <th scope="col" class="text-start">Sous-total</th>
                                        <td scope="col" class="text-end">{model.SousTotal} $</td>
                                    </tr>
                                    <tr>
                                        <th scope="col" class="text-start">TPS</th>
                                        <td scope="col" class="text-end">{model.TPS} $</td>
                                    </tr>
                                    <tr>
                                        <th scope="col" class="text-start">TVQ</th>
                                        <td scope="col" class="text-end">{model.TVQ} $</td>
                                    </tr>
                                    <tr>
                                        <th scope="col" class="text-start">Don</th>
                                        <td scope="col" class="text-end">{model.Don} $</td>
                                    </tr>
                                </tbody>
                            </table>
                            <h4>TOTAL: {model.PrixFinal} $</h4>
                        </div>
                    </div>
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
        idFacture: String
    });

    const facture = ref("")

    onMounted(async () => {

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
