<template>
    <div class="d-flex flex-column justify-content-between">
        <h2 class="d-flex justify-content-center">Ajout d'un encan</h2>

        <div class="d-flex justify-content-center mt-4">
            <form class="text-center">
                <div class="mb-3">
                    <label for="NumeroEncan" class="form-label">Numéro de l'encan:</label>
                    <input type="number" class="form-control" aria-label="numeroEncan" required>
                </div>
                <div class="mb-3">
                    <label for="dateDebut" class="form-label">Date de début:</label>
                    <input type="date" class="form-control" required>
                </div>
                <div class="mb-3">
                    <label for="dateFin" class="form-label">Date de fin:</label>
                    <input type="date" class="form-control" required>
                </div>
                <div class="mb-3">
                    <label for="dateSoireeCloture" class="form-label">Soirée de clôture:</label>
                    <input type="datetime-local" class="form-control" required>
                </div>
                <router-link to="TableauDeBordEncans" class="text-decoration-none">
                    <button type="button" class="btn bleuMarinSecondaireFond btnSurvolerBleuMoyenFond btnClick text-white">Annuler</button>
                </router-link>
                <button type="submit" class="btn bleuMarinSecondaireFond btnSurvolerBleuMoyenFond btnClick text-white">Ajouter</button>
            </form>
        </div>
    </div>
</template>

<script setup>
</script>

<style scoped>
</style>