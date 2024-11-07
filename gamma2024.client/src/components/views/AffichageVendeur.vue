<template>
    <div class="container mt-5">
        <h1 class="text-center mb-4 fw-bold display-4">Gestion des vendeurs</h1>
        <h3 class="text-center">Rechercher un vendeur</h3>
        <input v-model="searchQuery"
               class="form-control row col-10 ms-1"
               type="search"
               placeholder="Rechercher un vendeur"
               aria-label="Search" />
        <router-link to="/vendeurcreation"
                     class="btn btn-lg btn-block mb-4 w-100 bleuMarinSecondaireFond text-white mt-5">
            Ajouter un vendeur
        </router-link>
        <div class="overflow-y-auto">
            <table class="table table-striped table-borderless text-center mb-5">
                <thead class="table-dark">
                    <tr>
                        <th scope="col">#</th>
                        <th scope="col">Nom</th>
                        <th scope="col">Prénom</th>
                        <th scope="col">Courriel</th>
                        <th scope="col">Téléphone</th>
                        <th scope="col">Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="(vendeur, index) in filteredVendeurs" :key="vendeur.id">
                        <th scope="row" class="align-middle">{{ index + 1 }}</th>
                        <td class="align-middle">{{ vendeur.nom }}</td>
                        <td class="align-middle">{{ vendeur.prenom }}</td>
                        <td class="align-middle">{{ vendeur.courriel }}</td>
                        <td class="align-middle">{{ vendeur.telephone }}</td>
                        <td>
                            <button class="btn bleuMarinSecondaireFond px-3 me-3">
                                <router-link :to="{
                  name: 'vendeurModification',
                  params: { id: vendeur.id.toString() },
                }"
                                             class="text-decoration-none">
                                    <img src="/public/icons/Edit_icon.png"
                                         class="img-fluid"
                                         alt="..." />
                                </router-link>
                            </button>
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
</template>

<script setup>
    import { ref, onMounted, computed } from "vue";
    import { useStore } from "vuex";

    const store = useStore();
    const vendeurs = ref(null);
    const searchQuery = ref("");

    const vendeursAffichage = computed(() => {
        if (vendeurs.value) {
            return vendeurs.value;
        }
        return [];
    });

    onMounted(async () => {
        try {
            vendeurs.value = await store.dispatch("obtenirTousVendeurs");
        } catch (error) {
            console.error("Erreur lors de la récupération des vendeurs:", error);
        }
    });

    const filteredVendeurs = computed(() => {
        return vendeursAffichage.value.filter((vendeur) => {
            const searchLower = searchQuery.value.toLowerCase();
            return (
                vendeur.nom.toLowerCase().includes(searchLower) ||
                vendeur.prenom.toLowerCase().includes(searchLower) ||
                vendeur.courriel.toLowerCase().includes(searchLower) ||
                vendeur.telephone.toLowerCase().includes(searchLower)
            );
        });
    });
</script>

<style scoped>
    .bleuMarinSecondaireFond {
        background-color: #1e3a8a;
        border-color: #1e3a8a;
    }

        .bleuMarinSecondaireFond:hover {
            background-color: #4d6dc4;
            border-color: #1e3a8a;
        }

    /* Ajoutez ces styles pour le texte */
    .container {
        font-family: Arial, sans-serif;
    }

    h1,
    .btn {
        color: #ffffff;
    }

    .table {
        color: #333333;
    }

    h1 {
        color: #1e3a8a;
    }

    table,
    input {
        box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 3px 5px 0 rgba(0, 0, 0, 0.19);
    }

    .btn-like-field {
        display: inline-block;
        padding: 0.375rem 0.75rem;
        font-size: 1rem;
        font-weight: 400;
        line-height: 1.5;
        color: #1e3a8a;
        background-color: #ffffff;
        border: 1px solid #1e3a8a;
        border-radius: 0.25rem;
        transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out;
    }

        .btn-like-field:hover {
            color: #ffffff;
            background-color: #1e3a8a;
        }

    /* Ajoutez ces styles pour supprimer les bordures du tableau */
    .table-borderless th,
    .table-borderless td {
        border: none !important;
    }

    /* Ajustez l'espacement entre les lignes si nécessaire */
    .table-striped {
        margin-bottom: 3px;
        padding-top: 3px;
    }

    img {
        width: 25px;
        height: 30px;
    }

    th {
        font-size: 18px;
    }

    td {
        font-size: 16px;
    }

    .btn_edit:hover {
        background-color: #243e5f;
    }
</style>
