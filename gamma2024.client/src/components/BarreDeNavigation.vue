<template>
    <nav class="navbar navbar-light bleuMarinFond">
        <div class="container-fluid justify-content-start">
            <router-link to="/" class="textLogoDecoration">
                <a class="navbar-brand text-white d-flex ">
                    <img src="/images/Logo.png"
                         alt="Les Encans de Nantes"
                         height="60" />
                    <p class="textLogoDecoration"> Les Encans <br /> de Nantes</p>
                </a>
            </router-link>
        </div>
    </nav>

    <nav class="navbar justify-content-end navbar-light bleuMarinSecondaireFond">

        <form class="d-flex">
            <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
            <button class="btn btn-outline bleuMoyenFond text-white" type="submit">Search</button>
        </form>
    </nav>
</template>

<script setup>

</script>

<style scoped>
    .textLogoDecoration {
        text-decoration: none;
        font-size: 18px;
    }
</style>