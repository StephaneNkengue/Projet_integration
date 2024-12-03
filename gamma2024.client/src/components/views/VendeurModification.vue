<template>
    <section class="h-100">
        <div class="container py-5 h-100">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col">
                    <form @submit.prevent="submitForm">
                        <div class="card my-4">
                            <div class="row g-0">
                                <div class="col-xl-12">
                                    <div class="card-header">
                                        <h1 class="mb-5 mt-2 text-center">
                                            Modification du vendeur
                                        </h1>
                                    </div>
                                    <div class="card-body p-md-3 text-black">
                                        <div v-if="message"
                                             :class="['alert', message.type === 'success' ? 'alert-success' : 'alert-danger', ]">
                                            {{ message.text }}
                                        </div>

                                        <div class="card">
                                            <div class="card-header fw-bold fs-5">
                                                Informations personnelles
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-6 mb-4">
                                                        <div class="form-outline">
                                                            <label class="form-label" for="nom">Nom</label>
                                                            <input v-model="vendeur.nom"
                                                                   type="text"
                                                                   id="nom"
                                                                   class="form-control form-control-lg"
                                                                   required />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 mb-4">
                                                        <div class="form-outline">
                                                            <label class="form-label" for="prenom">Prénom</label>
                                                            <input v-model="vendeur.prenom"
                                                                   type="text"
                                                                   id="prenom"
                                                                   class="form-control form-control-lg"
                                                                   required />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-outline mb-4">
                                                            <label class="form-label" for="telephone">Téléphone</label>
                                                            <input v-model="vendeur.telephone"
                                                                   type="tel"
                                                                   id="telephone"
                                                                   class="form-control form-control-lg"
                                                                   required />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-outline mb-4">
                                                            <label class="form-label" for="courriel">Courriel</label>
                                                            <input v-model="vendeur.courriel"
                                                                   type="email"
                                                                   id="courriel"
                                                                   class="form-control form-control-lg"
                                                                   required />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="card mt-4">
                                            <div class="card-header fw-bold fs-5">Adresse</div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-outline mb-4">
                                                            <label class="form-label" for="numeroCivique">Numéro civique</label>
                                                            <input v-model="vendeur.adresse.numeroCivique"
                                                                   type="text"
                                                                   id="numeroCivique"
                                                                   class="form-control form-control-lg"
                                                                   required />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-outline mb-4">
                                                            <label class="form-label" for="rue">Rue</label>
                                                            <input v-model="vendeur.adresse.rue"
                                                                   type="text"
                                                                   id="rue"
                                                                   class="form-control form-control-lg"
                                                                   required />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <div class="form-outline mb-4">
                                                            <label class="form-label" for="appartement">Appartement (optionnel)</label>
                                                            <input v-model="vendeur.adresse.appartement"
                                                                   type="text"
                                                                   id="appartement"
                                                                   class="form-control form-control-lg" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 mb-4">
                                                        <div class="form-outline">
                                                            <label class="form-label" for="ville">Ville</label>
                                                            <input v-model="vendeur.adresse.ville"
                                                                   type="text"
                                                                   id="ville"
                                                                   class="form-control form-control-lg"
                                                                   required />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-6 mb-4">
                                                        <div class="form-outline">
                                                            <label class="form-label" for="province">Province</label>
                                                            <select v-model="vendeur.adresse.province"
                                                                    id="province"
                                                                    class="form-select form-select-lg"
                                                                    required>
                                                                <option value="" disabled>Province</option>
                                                                <option v-for="province in provinces"
                                                                        :key="province"
                                                                        :value="province">
                                                                    {{ province }}
                                                                </option>
                                                            </select>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="form-outline mb-4">
                                                            <label class="form-label" for="pays">Pays</label>
                                                            <input v-model="vendeur.adresse.pays"
                                                                   type="text"
                                                                   id="pays"
                                                                   class="form-control form-control-lg"
                                                                   required />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-outline mb-4">
                                                    <label class="form-label" for="codePostal">Code postal</label>
                                                    <input v-model="formattedCodePostal"
                                                           type="text"
                                                           id="codePostal"
                                                           class="form-control form-control-lg"
                                                           required
                                                           maxlength="7"
                                                           @input="formatCodePostal" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer text-body-secondary">
                                        <div class="d-flex justify-content-end pt-3">
                                            <button type="button"
                                                    class="btn btn-light btn-lg"
                                                    @click="resetForm">
                                                Réinitialiser
                                            </button>
                                            <button type="submit" class="btn btnSurvolerBleuMoyenFond text-white btn-lg ms-2">
                                                Enregistrer
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </section>
</template>

<script setup>
    import { ref, onMounted, computed } from "vue";
    import { useStore } from "vuex";
    import { useRouter, useRoute } from "vue-router";

    const store = useStore();
    const router = useRouter();
    const route = useRoute();

    const provinces = [
        "Alberta",
        "Colombie-Britannique",
        "Île-du-Prince-Édouard",
        "Manitoba",
        "Nouveau-Brunswick",
        "Nouvelle-Écosse",
        "Ontario",
        "Québec",
        "Saskatchewan",
        "Terre-Neuve-et-Labrador",
        "Territoires du Nord-Ouest",
        "Nunavut",
        "Yukon",
    ];

    // Ajoutez cet objet pour mapper les abréviations aux noms complets
    const provinceAbbreviations = {
        "AB": "Alberta",
        "BC": "Colombie-Britannique",
        "PE": "Île-du-Prince-Édouard",
        "MB": "Manitoba",
        "NB": "Nouveau-Brunswick",
        "NS": "Nouvelle-Écosse",
        "ON": "Ontario",
        "QC": "Québec",
        "SK": "Saskatchewan",
        "NL": "Terre-Neuve-et-Labrador",
        "NT": "Territoires du Nord-Ouest",
        "NU": "Nunavut",
        "YT": "Yukon"
    };

    const normalizeString = (str) =>
        str
            .toLowerCase()
            .normalize("NFD")
            .replace(/[\u0300-\u036f]/g, "");

    const normalizedProvinces = computed(() =>
        provinces.map((province) => ({
            original: province,
            normalized: normalizeString(province),
        }))
    );

    const vendeur = ref({
        id: "",
        nom: "",
        prenom: "",
        courriel: "",
        telephone: "",
        adresse: {
            numeroCivique: "",
            rue: "",
            ville: "",
            province: "",
            pays: "",
            codePostal: "",
            appartement: "",
        },
    });

    onMounted(async () => {
        try {
            const vendeurData = await store.dispatch("obtenirVendeur", route.params.id);
            vendeur.value = {
                id: vendeurData.id,
                nom: vendeurData.nom,
                prenom: vendeurData.prenom,
                courriel: vendeurData.courriel,
                telephone: vendeurData.telephone,
                adresse: {
                    numeroCivique: vendeurData.adresse.numeroCivique,
                    rue: vendeurData.adresse.rue,
                    ville: vendeurData.adresse.ville,
                    province: provinceAbbreviations[vendeurData.adresse.province] || vendeurData.adresse.province,
                    pays: vendeurData.adresse.pays,
                    codePostal: vendeurData.adresse.codePostal,
                    appartement: vendeurData.adresse.appartement,
                },
            };

            // Normaliser et trouver la correspondance
            const normalizedLoadedProvince = normalizeString(
                vendeur.value.adresse.province
            );
            const matchedProvince = normalizedProvinces.value.find(
                (p) => p.normalized === normalizedLoadedProvince
            );
            if (matchedProvince) {
                vendeur.value.adresse.province = matchedProvince.original;
            }
        } catch (error) {
            console.error(
                "Erreur lors de la récupération des données du vendeur:",
                error
            );
        }
    });

    const message = ref(null);

    const submitForm = async () => {
        try {
            const result = await store.dispatch("modifierVendeur", vendeur.value);
            if (result.success) {
                message.value = { type: "success", text: result.message };
                setTimeout(() => {
                    router.push("/affichagevendeurs");
                }, 2000);
            } else {
                console.log("test 3");
                message.value = { type: "danger", text: result.error };
            }
        } catch (error) {
            console.error("Erreur lors de la modification du vendeur:", error);
            message.value = {
                type: "danger",
                text: "Une erreur est survenue lors de la modification du vendeur.",
            };
        }
    };

    const resetForm = () => {
        // Réinitialiser le formulaire aux valeurs originales du vendeur
        onMounted();
    };

    const formattedCodePostal = computed({
        get: () => {
            const code = vendeur.value.adresse.codePostal;
            if (!code) return "";
            // Format: "A1A 1A1"
            const cleanCode = code.replace(/[^A-Za-z0-9]/g, "").toUpperCase();
            if (cleanCode.length <= 3) {
                return cleanCode;
            }
            return `${cleanCode.slice(0, 3)} ${cleanCode.slice(3)}`;
        },
        set: (value) => {
            // Stocke la valeur sans espace dans le modèle
            vendeur.value.adresse.codePostal = value.replace(/\s/g, "").toUpperCase();
        },
    });

    const formatCodePostal = (event) => {
        let value = event.target.value.toUpperCase();
        // Supprime tous les caractères non alphanumériques
        value = value.replace(/[^A-Z0-9]/g, "");

        // Ajoute l'espace après les 3 premiers caractères
        if (value.length > 3) {
            value = value.slice(0, 3) + " " + value.slice(3);
        }

        // Met à jour la valeur dans le champ
        event.target.value = value;
        // Met à jour le modèle
        vendeur.value.adresse.codePostal = value.replace(/\s/g, "");
    };
</script>

<style scoped>
    .card-registration .select-input.form-control[readonly]:not([disabled]) {
        font-size: 1rem;
        line-height: 2.15;
        padding-left: 0.75em;
        padding-right: 0.75em;
    }

    .form-select {
        appearance: none;
        background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'%3e%3cpath fill='none' stroke='%23343a40' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M2 5l6 6 6-6'/%3e%3c/svg%3e");
        background-repeat: no-repeat;
        background-position: right 0.75rem center;
        background-size: 16px 12px;
        border: 1px solid #ced4da;
        border-radius: 0.25rem;
        padding: 0.375rem 2.25rem 0.375rem 0.75rem;
    }

        .form-select:focus {
            border-color: #86b7fe;
            outline: 0;
            box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
        }

    .card {
        box-shadow: 0 2px 2px 0 rgba(0, 0, 0, 0.2), 0 3px 5px 0 rgba(0, 0, 0, 0.19);
    }

    label {
        font-weight: 600;
        margin-left: 5px;
    }
</style>
