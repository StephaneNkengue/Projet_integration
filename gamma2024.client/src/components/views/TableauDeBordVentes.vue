<template>
    <div class="d-flex flex-column justify-content-between">
        <div class="d-flex justify-content-between">
            <h2 class="d-flex-1">Liste des ventes</h2>
        </div>

        <div class="d-flex flex-row-reverse me-1 gap-1 align-items-center">

            <input data-bs-theme="light"
                   type="search"
                   aria-label="Recherche" /><label for="Recherche">Rechercher: </label>
        </div>

        <table class="table table-striped mt-5 mx-0 text-center">
            <thead>
                <tr>
                    <th data-field="dateAchat">Date de l'achat</th>
                    <th data-field="prixFinal">Prix final</th>
                    <th data-field="fraisEncanteur">Frais d'encanteur</th>
                    <th data-field="fraisLivraison">Frais de livraison</th>
                    <th data-field="tps">TPS</th>
                    <th data-field="tvq">TVQ</th>
                    <th data-field="don">Don</th>
                    <th data-field="client">Client</th>
                    <th>Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td class="align-middle">
                        <span>
                            <button class="btn btn-info">
                                <img src="/images/ice.png" class="img-fluid" alt="..." />
                            </button>
                        </span>

                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script setup>

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
</style>