<template>
    <div class="home">
        <h1>Bienvenue sur notre site</h1>
        <p>Ceci est la page d'accueil de notre application.</p>
        <router-link to="/login">Aller à la page de connexion</router-link>

    </div>
</template>

<script>
    export default {
        name: 'Accueil',
        created() {
            console.log('Home component created')
        },
        // Vous pouvez ajouter ici d'autres options de composant si nécessaire
    }
</script>

<style scoped>
    .home {
        text-align: center;
        padding: 20px;
    }

    h1 {
        color: #333;
    }

    p {
        margin-bottom: 20px;
    }

    a {
        color: #42b983;
        text-decoration: none;
    }

        a:hover {
            text-decoration: underline;
        }
</style>
