<template>
    <div class="bg-image pt-5 imageDeFondEsquise">
        <div class="container d-flex flex-column justify-content-start align-items-stretch container col-md-6">
            <h2 class="fs-1 text-center fw-bold mt-5">Modification des informations</h2>
            <p class="text-center">Modifier vos informations personnelles</p>

            <div v-if="errorMessage" class="alert alert-danger">
                {{ errorMessage }}
            </div>

            <div class="mt-3">
                <form @submit.prevent="submitForm">
                    <!-- Informations générales -->
                    <div class="d-flex px-4 py-2 flex-column justify-content-start align-items-stretch .i">
                        <div class="d-flex flex-row justify-content-between mb-3 i">
                            <div class="form-group">
                                <label for="nom" class="ms-3">Nom</label>
                                <input type="text"
                                       v-model="formData.generalInfo.nom"
                                       class="form-control"
                                       id="nom"
                                       @blur="v.generalInfo.nom.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.generalInfo.nom.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                            <div class="form-group">
                                <label for="prenom" class="ms-3">Prénom</label>
                                <input type="text"
                                       v-model="formData.generalInfo.prenom"
                                       class="form-control"
                                       id="prenom"
                                       @blur="v.generalInfo.prenom.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.generalInfo.prenom.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="courriel" class="ms-3">Courriel</label>
                                <input type="email"
                                       v-model="formData.generalInfo.courriel"
                                       class="form-control"
                                       id="courriel"
                                       @blur="v.generalInfo.courriel.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.generalInfo.courriel.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                            <div class="form-group">
                                <label for="telephone" class="ms-3">Téléphone</label>
                                <input type="tel"
                                       v-model="formData.generalInfo.telephone"
                                       class="form-control"
                                       id="telephone"
                                       @blur="v.generalInfo.telephone.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.generalInfo.telephone.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="pseudonyme" class="ms-3">Pseudonyme</label>
                                <input type="text"
                                       v-model="formData.generalInfo.pseudo"
                                       class="form-control"
                                       id="pseudonyme"
                                       @blur="v.generalInfo.pseudo.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.generalInfo.pseudo.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>
                    </div>

                    <!-- Informations de carte de crédit -->
                    <div class="d-flex px-4 py-2 flex-column justify-content-start align-items-stretch .i">
                        <div class="d-flex flex-row justify-content-between mb-3 i">
                            <div class="form-group">
                                <label for="nomPropio" class="ms-3">Nom du propriétaire de la carte</label>
                                <input type="text"
                                       v-model="formData.carteCredit.nomProprio"
                                       class="form-control"
                                       id="nomPropio"
                                       @blur="v.carteCredit.nomProprio.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.carteCredit.nomProprio.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                            <div class="form-group">
                                <label for="numeroCarte" class="ms-3">Numéro de la carte</label>
                                <input type="text"
                                       v-model="formData.carteCredit.numeroCarte"
                                       class="form-control"
                                       id="numeroCarte"
                                       @blur="v.carteCredit.numeroCarte.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.carteCredit.numeroCarte.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="dateExpiration" class="ms-3">Date d'expiration</label>
                                <input type="text"
                                       v-model="formData.carteCredit.dateExpiration"
                                       class="form-control"
                                       id="dateExpiration"
                                       @blur="v.carteCredit.dateExpiration.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.carteCredit.dateExpiration.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>
                    </div>

                    <!-- Informations d'adresse -->
                    <div class="d-flex px-4 py-2 flex-column justify-content-start align-items-stretch .i">
                        <div class="d-flex flex-row justify-content-between mb-3 i">
                            <div class="form-group">
                                <label for="numeroCivique" class="ms-3">Numéro Civique</label>
                                <input type="text"
                                       v-model="formData.adresse.numeroCivique"
                                       class="form-control"
                                       id="numeroCivique"
                                       @blur="v.adresse.numeroCivique.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.adresse.numeroCivique.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                            <div class="form-group">
                                <label for="rue" class="ms-3">Rue</label>
                                <input type="text"
                                       v-model="formData.adresse.rue"
                                       class="form-control"
                                       id="rue"
                                       @blur="v.adresse.rue.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.adresse.rue.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="appartement" class="ms-3">Appartement/Bureau</label>
                                <input type="text"
                                       v-model="formData.adresse.appartement"
                                       class="form-control"
                                       id="appartement" />
                            </div>
                            <div class="form-group">
                                <label for="ville" class="ms-3">Ville</label>
                                <input type="text"
                                       v-model="formData.adresse.ville"
                                       class="form-control"
                                       id="ville"
                                       @blur="v.adresse.ville.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.adresse.ville.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="province" class="ms-3">Province</label>
                                <select id="province"
                                        v-model="formData.adresse.province"
                                        class="form-control"
                                        @blur="v.adresse.province.$touch()">
                                    <option value="" disabled selected>Sélectionner une province</option>
                                    <option value="AB">Alberta</option>
                                    <option value="BC">Colombie-Britannique</option>
                                    <option value="MB">Manitoba</option>
                                    <option value="NB">Nouveau-Brunswick</option>
                                    <option value="NL">Terre-Neuve-et-Labrador</option>
                                    <option value="NS">Nouvelle-Écosse</option>
                                    <option value="ON">Ontario</option>
                                    <option value="PE">Île-du-Prince-Édouard</option>
                                    <option value="QC">Québec</option>
                                    <option value="SK">Saskatchewan</option>
                                    <option value="NT">Territoires du Nord-Ouest</option>
                                    <option value="NU">Nunavut</option>
                                    <option value="YT">Yukon</option>
                                </select>
                                <span class="text-danger"
                                      v-for="error in v.adresse.province.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                            <div class="form-group">
                                <label for="pays" class="ms-3">Pays</label>
                                <input type="text"
                                       v-model="formData.adresse.pays"
                                       class="form-control"
                                       id="pays" />
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="postalCode" class="ms-3">Code Postal</label>
                                <input type="text"
                                       v-model="formData.adresse.codePostal"
                                       class="form-control"
                                       id="postalCode"
                                       @blur="v.adresse.codePostal.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.adresse.codePostal.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="d-flex justify-content-end gap-2 py-3">
                        <button type="submit"
                                :class="[
                  state1 ? 'bleuValide' : 'bleuNonValide',
                  'btn rounded-pill px-5 me-4',
                ]"
                                :disabled="state1">
                            Enregistrer
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, computed, reactive, onMounted } from "vue";
import { useStore } from "vuex";
import { useVuelidate } from "@vuelidate/core";
import { required, email, helpers } from "@vuelidate/validators";

const store = useStore();
const activeIndex = ref(1);
const messageRequis = helpers.withMessage("Ce champ est requis", required);
const messageCourriel = helpers.withMessage(
    "Veuillez entrer un courriel valide",
    email
);

// objet qui contient tous les champs remplis correctement
let formData = reactive({
    generalInfo: {
        nom: "",
        prenom: "",
        courriel: "",
        telephone: "",
        pseudo: "",
    },
    carteCredit: {
        nomProprio: "",
        numeroCarte: "",
        dateExpiration: "",
    },
    adresse: {
        numeroCivique: "",
        rue: "",
        appartement: "",
        ville: "",
        province: "",
        pays: "Canada",
        codePostal: "",
    }
});

// objet rules qui contient toutes les validations
let rules = computed(() => {
    return {
        generalInfo: {
            nom: { required: messageRequis },
            prenom: { required: messageRequis },
            courriel: { required: messageRequis, email: messageCourriel },
            telephone: { required: messageRequis },
            pseudo: { required: messageRequis },
        },
        carteCredit: {
            nomProprio: { required: messageRequis },
            numeroCarte: { required: messageRequis },
            dateExpiration: { required: messageRequis },
        },
        adresse: {
            numeroCivique: { required: messageRequis },
            rue: { required: messageRequis },
            appartement: {},
            ville: { required: messageRequis },
            province: { required: messageRequis },
            pays: { required: messageRequis },
            codePostal: { required: messageRequis },
        }
    };
});

const v = useVuelidate(rules, formData);
const info = useVuelidate(rules.value.generalInfo, formData.generalInfo);
const carte = useVuelidate(rules.value.carteCredit, formData.carteCredit);
const adresse = useVuelidate(rules.value.adresse, formData.adresse);

const state1 = computed(() => {
    return info.value.$invalid;
});

const state2 = computed(() => {
    return carte.value.$invalid;
});

const state3 = computed(() => {
    return adresse.value.$invalid;
});

const errorMessage = ref("");

onMounted(async () => {
    try {
        const response = await store.dispatch("fetchClientInfo");
        formData.generalInfo.nom = response.data.Name;
        formData.generalInfo.prenom = response.data.FirstName;
        formData.generalInfo.courriel = response.data.Email;
        formData.generalInfo.telephone = response.data.PhoneNumber;
        formData.generalInfo.pseudo = response.data.Pseudonym;
        formData.carteCredit.nomProprio = response.data.CardOwnerName;
        formData.carteCredit.numeroCarte = response.data.CardNumber;
        formData.carteCredit.dateExpiration = response.data.CardExpiryDate;
        formData.adresse.numeroCivique = response.data.CivicNumber;
        formData.adresse.rue = response.data.Street;
        formData.adresse.appartement = response.data.Apartment;
        formData.adresse.ville = response.data.City;
        formData.adresse.province = response.data.Province;
        formData.adresse.pays = response.data.Country;
        formData.adresse.codePostal = response.data.PostalCode;
    } catch (error) {
        errorMessage.value = "Erreur lors de la récupération des informations du client.";
    }
});

const submitForm = async () => {
    const result = await v.value.$validate();
    if (result) {
        try {
            await store.dispatch("updateClientInfo", {
                ...formData.generalInfo,
                ...formData.carteCredit,
                ...formData.adresse
            });
            alert("Informations mises à jour avec succès !");
        } catch (error) {
            errorMessage.value = "Erreur lors de la mise à jour des informations.";
        }
    } else {
        alert("Formulaire non valide !");
    }
};
</script>

<style scoped>
    .information .form-group {
        width: 45%;
    }

    label {
        font-weight: bolder;
    }

    .is-invalid,
    span {
        border-color: red;
        font-weight: 600;
    }

    .cadreBlanc {
        background-color: white;
        opacity: 0.4;
        border-radius: 15px;
        height: auto;
    }

    .bleuNonValide {
        background-color: #052445;
        color: white;
    }

    .bleuValide {
        background-color: #5a708a;
        color: white;
    }

    @media only screen and (max-width: 1000px) {
        .i div {
        display: flex;
        flex-basis: 100%;
        flex-direction: column;
        }
}
</style>

