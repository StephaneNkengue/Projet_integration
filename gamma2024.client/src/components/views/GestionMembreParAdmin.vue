<template lang="">
    <div class="container">
        <h1 class="text-center mb-5">Liste des membres</h1>

        <h3 class="text-center">Rechercher un membre</h3>

        <input v-model="searchQuery"
               class="form-control row col-10 ms-1"
               type="search"
               placeholder="Rechercher un membre"
               aria-label="Search" />

        <div class="d-flex gap-2 justify-content-center" v-if="chargement">
            <div class="spinner-border" role="status">
                <span class="visually-hidden">Chargement des membres...</span>
            </div>
            <p>Chargement des membres en cours...</p>
        </div>

        <div v-if="!chargement" class="w-100">
            <div class="d-flex justify-content-center" v-if="!filteredMembres.length">
                <h2>Aucun résultat trouvé</h2>
            </div>

            <div v-else class="overflow-auto">
                <table class="table table-striped mt-5 mb-5 col-md-12 text-center">
                    <thead>
                        <tr>
                            <th scope="col">Nom</th>
                            <th scope="col">Prénom</th>
                            <th scope="col">Pseudonyme</th>
                            <th scope="col">Courriel</th>
                            <th scope="col">Voir</th>
                            <th scope="col">État</th>
                        </tr>
                    </thead>
                    <tbody class="fs-6">
                        <tr v-for="membre in filteredMembres" :key="membre.id">
                            <td class="align-middle">{{ membre.name }}</td>
                            <td class="align-middle">{{ membre.firstName }}</td>
                            <td class="align-middle">{{ membre.userName }}</td>
                            <td class="align-middle">{{ membre.email }}</td>
                            <td class="align-middle">
                                <span>
                                    <button class="btn btn-info" @click="detailsDuMembre(membre.id)">
                                        <img src="/images/ice.png" class="img-fluid" alt="..." />
                                    </button>
                                </span>
                            </td>
                            <td class="align-middle">
                                <span v-if="membre.estBloque">
                                    <button class="btn btn-danger" @click="debloquerUnMembre(membre)">
                                        <img src="/images/Locked.png" class="img-fluid" alt="..." />
                                    </button>
                                </span>
                                <span v-else>
                                    <button class="btn btn-success" @click="bloquerUnMembre(membre)">
                                        <img src="/images/Unlocked.png" class="img-fluid" alt="..." />
                                    </button>
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref, onMounted, computed } from "vue";
    import { useStore } from "vuex";
    import { useRouter } from "vue-router";

    const store = useStore();
    const router = useRouter();
    let mesMembres = ref([]);
    const searchQuery = ref("");

    const chargement = ref(true);

    onMounted(async () => {
        try {
            updateData();
        } catch (error) {
            console.error("Erreur lors de la récupération des membres:", error);
        }
    });

    function detailsDuMembre(idMembre) {
        router.push({ name: "detailsMembre", params: { id: idMembre } });
    }

    const bloquerUnMembre = async (membre) => {
        await store.dispatch("bloquerUnMembre", membre.id);
        updateData();
    };

    const debloquerUnMembre = async (membre) => {
        await store.dispatch("debloquerUnMembre", membre.id);
        updateData();
    };

    const updateData = async function () {
        const response = await store.dispatch("ObtenirTousLesMembres");
        mesMembres.value = response.map((membre) => ({
            ...membre,
        }));

        chargement.value = false;
    };

    // Propriété calculée pour filtrer les membres en fonction de la recherche
    const filteredMembres = computed(() => {
        return tousLesMembres.value.filter((membre) => {
            const searchLower = searchQuery.value.toLowerCase();
            return (
                membre.name.toLowerCase().includes(searchLower) ||
                membre.firstName.toLowerCase().includes(searchLower) ||
                membre.userName.toLowerCase().includes(searchLower) ||
                membre.email.toLowerCase().includes(searchLower)
            );
        });
    });

    const tousLesMembres = computed(() => {
        if (mesMembres.value) {
            return mesMembres.value;
        }
        return [];
    });
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
