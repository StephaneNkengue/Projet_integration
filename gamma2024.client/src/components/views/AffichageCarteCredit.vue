<template>
    <div class="card">
        <div class="card-header">Carte de crédit ({{carte.marque}})</div>
        <div class="card-body">
            <div class="row">
                <div class="col-md-6">
                    <label for="numeroCarte">Dernier 4 chiffres de la carte</label>
                    <div class="input-wrapper">
                        <input type="text"
                               mask="9999"
                               id="numeroCarte"
                               disabled
                               class="form-control"
                               v-model="carte.dernier4Numero" />
                    </div>
                </div>
                <div class="col-md-6">
                    <label for="dateExpiration">Date d'expiration</label>
                    <div class="input-wrapper">
                        <input type="text"
                               mask="99/9999"
                               class="form-control"
                               placeholder="MM/YY"
                               id="dateExpiration"
                               disabled
                               v-model="carte.expirationDate" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script setup>
    const props = defineProps({
        carte: Object
    });
</script>
<style></style>