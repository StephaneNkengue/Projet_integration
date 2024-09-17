<template>
    <footer class="bleuMarinFond text-white py-1 justify-content-between d-flex align-items-center flex-column flex-md-row">

        <p class="col-md-5 mb-0 ps-3">
            © Les Encans de Nantes 2024
        </p>

        <div class="col-md-7">
            <ul class="nav justify-content-end d-flex align-items-center flex-column flex-md-row">
                <li>
                    <router-link to="/" class="text-decoration-none">
                        <a class="nav-link text-white">
                            Conditions de la compagnie
                        </a>
                    </router-link>
                </li>
                <li>
                    <router-link to="/contact" class="text-decoration-none">
                        <a class="nav-link text-white">
                            Contact
                        </a>
                    </router-link>
                </li>
                <li>
                    <router-link to="/" class="text-decoration-none">
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