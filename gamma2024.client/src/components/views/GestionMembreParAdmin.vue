<template lang="">
    <div class="container mt-5">
        <h3 class="text-center">Rechercher un membre</h3>
        <input v-model="searchQuery"
               class="form-control row col-10 ms-4"
               type="search"
               placeholder="Rechercher un membre"
               aria-label="Search" />
        <h1 class="text-center mt-5">Liste des membres</h1>
        <table class="table table-striped col-md-12 text-center">
            <thead class="fs-4 fw-bold">
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
                    <td>{{ membre.name }}</td>
                    <td>{{ membre.firstName }}</td>
                    <td>{{ membre.userName }}</td>
                    <td>{{ membre.email }}</td>
                    <td>
                        <span>
                            <button class="btn btn-info" @click="detailsDuMembre(membre.id)">
                                <img src="/images/ice.png"
                                     class="img-fluid"
                                     alt="..." />
                            </button>
                        </span>
                    </td>
                    <td>
                        <span v-if="membre.estBloque">
                            <button class="btn btn-danger"
                                    @click="debloquerUnMembre(membre)">
                                <img src="/images/Locked.png"
                                     class="img-fluid"
                                     alt="..." />
                            </button>
                        </span>
                        <span v-else>
                            <button class="btn btn-success" @click="bloquerUnMembre(membre)">
                                <img src="/images/Unlocked.png"
                                     class="img-fluid"
                                     alt="..." />
                            </button>
                        </span>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script setup>
    import { ref, onMounted, computed } from "vue";
    import { useStore } from "vuex";
    import { useRouter } from "vue-router";

    const store = useStore();
    const router = useRouter();
    let tousLesMembres = ref([]);
    const searchQuery = ref("");

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
        const response = await store.dispatch("bloquerUnMembre", membre.id);
        updateData();
    };

    const debloquerUnMembre = async (membre) => {
        const response = await store.dispatch("debloquerUnMembre", membre.id);
        updateData();
    };

    const updateData = async function () {
        const response = await store.dispatch("ObtenirTousLesMembres");
        tousLesMembres.value = response.map((membre) => ({
            ...membre,
        }));
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
</style>
