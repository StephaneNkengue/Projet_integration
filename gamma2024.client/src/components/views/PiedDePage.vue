<template>
    <footer class="bleuMarinSecondaireFond text-white py-1 justify-content-between d-flex align-items-center flex-column flex-md-row">

        <p class="col-md-5 mb-0 ps-3">
            © Les Encans de Nantes au Québec 2024
        </p>

        <div class="col-md-7">
            <ul class="nav justify-content-end d-flex align-items-center flex-column flex-md-row">
                <li class="nav-item">
                    <router-link to="Accueil" class="text-decoration-none">
                        <a class="nav-link text-white">
                            Déroulement d'un encan
                        </a>
                    </router-link>
                </li>
                <li>
                    <router-link to="Accueil" class="text-decoration-none">
                        <a class="nav-link text-white">
                            Conditions de la compagnie
                        </a>
                    </router-link>
                </li>
                <li>
                    <router-link to="Contact" class="text-decoration-none">
                        <a class="nav-link text-white">
                            Contact
                        </a>
                    </router-link>
                </li>
                <li>
                    <router-link to="APropos" class="text-decoration-none">
                        <a class="nav-link text-white">
                            À propos
                        </a>
                    </router-link>
                </li>
            </ul>
        </div>

    </footer>
</template>

<script setup></script>

<style scoped>
</style>