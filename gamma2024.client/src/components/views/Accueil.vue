<template>
    <div class="d-flex flex-column align-items-center">
        <div class="text-center">
            <h1>Les Encans de Nantes</h1>

            <h2>Encans d'oeuvres d'art</h2>
        </div>

        <div class="w-50 text-center">
            <p class="justifyText">
                Les Encans de Nantes au Québec est une entreprise spécialisée dans la gestion d'encans virtuels, axée principalement sur la vente d'œuvres d'art. Elle est co-propriétée par Frédérique Nantes, en collaboration avec deux autres associées, Monique Inverness et Patricia DeNice. L'entreprise est située à Stanstead, dans un bâtiment annexé à la bibliothèque Haskell, un lieu historique partagé entre le Canada et les États-Unis.
            </p>

            <router-link to="/" class="text-decoration-none text-black">
                <a>Lire plus -></a>
            </router-link>
        </div>

        <div class="w-50 pt-3 px-5 mt-4 bleuMoyenFond text-center text-white">
            Déroulement d'un encan

            <p class="justifyText">
                Les membres peuvent parcourir les lots d'œuvres d'art, faire des mises et des surenchères pendant la durée de l'encan, avec une soirée de clôture en direct. Pendant cette soirée, chaque lot est soumis à un intervalle de temps avant la fermeture définitive, avec la possibilité d'ajouter une minute en cas de surenchère. Un effet visuel indique les 20 dernières secondes pour chaque lot, et une notification par courriel est envoyée en cas de surenchère ou d'achat conclu.
            </p>

            <router-link to="/" class="text-decoration-none text-white">
                <a>Lire plus -></a>
            </router-link>
        </div>
    </div>
</template>

<script setup>
</script>

<style scoped>
    .justifyText {
        text-align: justify;
    }
</style>