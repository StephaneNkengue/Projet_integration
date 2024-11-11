<template>
    <div class="modal fade" :id="props.f.id" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h1 class="modal-title fs-5" id="staticBackdropLabel">Facture {{ props.f.id }}</h1>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <table class="table">
                        <thead>
                            <tr>
                                <th scope="col">Numéro du lot</th>
                                <th scope="col">Description</th>
                                <th scope="col">Artiste</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr v-for="lot in props.f.lots" :key="lot.id">
                                <td scope="row">{{ lot.numero }}</td>
                                <td>{{lot.description}}</td>
                                <td>{{lot.artiste}}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fermer</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script setup>
    const props = defineProps({
        f: {
            type: Object,
            required: true,
        }
    });
    console.log(props);
</script>

<style scoped>
</style>