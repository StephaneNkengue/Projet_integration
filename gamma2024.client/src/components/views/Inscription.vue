<template>
    <div class="background-container">
        <div class="container d-flex flex-column justify-content-start align-items-stretch vh-100 container col-md-6">
            <h2 class="fs-1 text-center fw-bold mt-5">Inscription</h2>
            <p class="text-center">Obtenir un compte membre</p>

            <div v-if="errorMessage" class="alert alert-danger">
                {{ errorMessage }}
            </div>

            <div v-if="successMessage" class="alert alert-success">
                {{ successMessage }}
            </div>

            <!-- Stepper Navigation -->
            <div class="mt-3">
                <form @submit.prevent="submitForm">
                    <Stepper v-model:activeIndex="activeIndex" :class="['mb-4']">
                        <!-- Informations générales -->
                        <StepItem :disabled="activeIndex !== 1" class="information">
                            <Step value="1" class="border-top border-3 p-2 border-dark fs-2">
                                <span>1 - Informations générales</span>
                            </Step>

                            <StepPanel v-if="activeIndex === 1" value="1">
                                <div class="d-flex px-4 py-2 flex-column justify-content-start align-items-stretch .i">
                                    <div class="d-flex flex-row justify-content-between mb-3 i">
                                        <div class="form-group">
                                            <label for="nom" class="ms-3">Nom</label>
                                            <input type="text"
                                                   v-model="formData.generalInfo.nom"
                                                   class="form-control"
                                                   :class="{ 'is-invalid': v.generalInfo.nom.$error }"
                                                   id="nom"
                                                   @blur="v.generalInfo.nom.$touch()" />
                                            <div class="invalid-feedback" v-if="v.generalInfo.nom.$error">
                                                {{ v.generalInfo.nom.$errors[0].$message }}
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="prenom" class="ms-3">Prénom</label>
                                            <input type="text"
                                                   v-model="formData.generalInfo.prenom"
                                                   class="form-control"
                                                   :class="{ 'is-invalid': v.generalInfo.prenom.$error }"
                                                   id="prenom"
                                                   @blur="v.generalInfo.prenom.$touch()" />
                                            <div class="invalid-feedback" v-if="v.generalInfo.prenom.$error">
                                                {{ v.generalInfo.prenom.$errors[0].$message }}
                                            </div>
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row justify-content-between mb-3">
                                        <div class="form-group">
                                            <label for="courriel" class="ms-3">Courriel</label>
                                            <input type="email"
                                                   v-model="formData.generalInfo.courriel"
                                                   class="form-control"
                                                   :class="{ 'is-invalid': v.generalInfo.courriel.$error }"
                                                   id="courriel"
                                                   @blur="v.generalInfo.courriel.$touch()" />
                                            <div class="invalid-feedback" v-if="v.generalInfo.courriel.$error">
                                                {{ v.generalInfo.courriel.$errors[0].$message }}
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="telephone" class="ms-3">Téléphone</label>
                                            <input type="tel"
                                                   v-model="formData.generalInfo.telephone"
                                                   class="form-control"
                                                   :class="{ 'is-invalid': v.generalInfo.telephone.$error }"
                                                   id="telephone"
                                                   @blur="v.generalInfo.telephone.$touch()" />
                                            <div class="invalid-feedback" v-if="v.generalInfo.telephone.$error">
                                                {{ v.generalInfo.telephone.$errors[0].$message }}
                                            </div>
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row justify-content-between mb-3">
                                        <div class="form-group">
                                            <label for="pseudonyme" class="ms-3">Pseudonyme</label>
                                            <input type="text"
                                                   v-model="formData.generalInfo.pseudo"
                                                   class="form-control"
                                                   :class="{ 'is-invalid': v.generalInfo.pseudo.$error }"
                                                   id="pseudonyme"
                                                   @blur="v.generalInfo.pseudo.$touch()" />
                                            <div class="invalid-feedback" v-if="v.generalInfo.pseudo.$error">
                                                {{ v.generalInfo.pseudo.$errors[0].$message }}
                                            </div>
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row justify-content-between mb-3">
                                        <div class="form-group">
                                            <label for="motDePasse" class="ms-3">Mot de passe</label>
                                            <input type="password"
                                                   v-model="formData.generalInfo.motDePasse"
                                                   class="form-control"
                                                   :class="{ 'is-invalid': v.generalInfo.motDePasse.$error }"
                                                   id="motDePasse"
                                                   @blur="v.generalInfo.motDePasse.$touch()" />
                                            <div class="invalid-feedback" v-if="v.generalInfo.motDePasse.$error">
                                                {{ v.generalInfo.motDePasse.$errors[0].$message }}
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="confirmMotPasse" class="ms-3">Confirmation de mot de passe</label>
                                            <input type="password"
                                                   class="form-control"
                                                   v-model="formData.generalInfo.confirmMotPasse"
                                                   :class="{ 'is-invalid': v.generalInfo.confirmMotPasse.$error }"
                                                   id="confirmMotPasse"
                                                   @blur="v.generalInfo.confirmMotPasse.$touch()" />
                                            <div class="invalid-feedback" v-if="v.generalInfo.confirmMotPasse.$error">
                                                {{ v.generalInfo.confirmMotPasse.$errors[0].$message }}
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end gap-2 py-3">
                                    <button type="button"
                                            :class="[
                      state1 ? 'bleuValide' : 'bleuNonValide',
                      'btn rounded-pill px-5 me-4',
                    ]"
                                            :disabled="state1"
                                            @click="allerAuSuivantPrecedent(2)">
                                        Suivant
                                    </button>
                                </div>
                            </StepPanel>
                        </StepItem>

                        <!-- Step 2: Carte de crédit -->
                        <StepItem :disabled="activeIndex !== 2" class="carte">
                            <Step value="2"
                                  class="step2 border-top border-3 p-2 border-dark fs-2">
                                <span>2 - Carte de crédit</span>
                            </Step>

                            <StepPanel v-if="activeIndex === 2" value="2">
                                <div class="d-flex flex-column justify-content-start align-items-stretch c">
                                    <div class="form-group flex-fill mb-3">
                                        <label for="nomPropio" class="ms-3">Nom du propriétaire de la carte</label>
                                        <input type="text"
                                               v-model="formData.carteCredit.nomProprio"
                                               class="form-control"
                                               :class="{ 'is-invalid': v.carteCredit.nomProprio.$error }"
                                               id="nomPropio"
                                               @blur="v.carteCredit.nomProprio.$touch()" />
                                        <div class="invalid-feedback" v-if="v.carteCredit.nomProprio.$error">
                                            {{ v.carteCredit.nomProprio.$errors[0].$message }}
                                        </div>
                                    </div>
                                    <div class="d-flex flex-row justify-content-between mb-3">
                                        <div class="form-group col-md-6">
                                            <label for="numeroCarte" class="ms-3">Numéro de la carte</label>
                                            <input type="text"
                                                   class="form-control"
                                                   v-model="formData.carteCredit.numeroCarte"
                                                   :class="{ 'is-invalid': v.carteCredit.numeroCarte.$error }"
                                                   id="numeroCarte"
                                                   placeholder="1234567890123456"
                                                   @blur="v.carteCredit.numeroCarte.$touch()" />
                                            <div class="invalid-feedback" v-if="v.carteCredit.numeroCarte.$error">
                                                {{ v.carteCredit.numeroCarte.$errors[0].$message }}
                                            </div>
                                        </div>
                                        <div class="form-group col-md-5">
                                            <label for="dateExpiration" class="ms-3">Date d'expiration</label>
                                            <input type="text"
                                                   placeholder="MM/AA"
                                                   pattern="\d{2}/\d{2}"
                                                   v-model="formData.carteCredit.dateExpiration"
                                                   class="form-control"
                                                   :class="{ 'is-invalid': v.carteCredit.dateExpiration.$error }"
                                                   id="dateExpiration"
                                                   @blur="v.carteCredit.dateExpiration.$touch()" />
                                            <div class="invalid-feedback" v-if="v.carteCredit.dateExpiration.$error">
                                                {{ v.carteCredit.dateExpiration.$errors[0].$message }}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="d-flex justify-content-between gap-2 py-3">
                                    <button type="button"
                                            class="btn btn-secondary px-5 rounded-pill ms-4"
                                            @click="allerAuSuivantPrecedent(1)">
                                        Précédent
                                    </button>
                                    <button type="button"
                                            :class="[
                      state2 ? 'bleuValide' : 'bleuNonValide',
                      classeActiveBouton
                    ]"
                                            :disabled="state2"
                                            @click="allerAuSuivantPrecedent(3)">
                                        Suivant
                                    </button>
                                </div>
                            </StepPanel>
                        </StepItem>

                        <!-- Step 3: Adresse -->
                        <StepItem :disabled="activeIndex !== 3" class="adresse">
                            <Step value="3" class="border-top border-3 p-2 border-dark fs-2">
                                <span>3 - Adresse</span>
                            </Step>

                            <StepPanel v-if="activeIndex === 3">
                                <div class="d-flex flex-column justify-content-start align-items-stretch a">
                                    <div class="d-flex flex-row justify-content-between mb-3">
                                        <div class="form-group">
                                            <label for="numeroCivique" class="ms-3">Numéro Civique</label>
                                            <input type="text"
                                                   v-model="formData.adresse.numeroCivique"
                                                   class="form-control"
                                                   :class="{ 'is-invalid': v.adresse.numeroCivique.$error }"
                                                   id="numeroCivique"
                                                   @blur="v.adresse.numeroCivique.$touch()" />
                                            <div class="invalid-feedback" v-if="v.adresse.numeroCivique.$error">
                                                {{ v.adresse.numeroCivique.$errors[0].$message }}
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="rue" class="ms-3">Rue</label>
                                            <input type="text"
                                                   v-model="formData.adresse.rue"
                                                   class="form-control"
                                                   :class="{ 'is-invalid': v.adresse.rue.$error }"
                                                   id="rue"
                                                   @blur="v.adresse.rue.$touch()" />
                                            <div class="invalid-feedback" v-if="v.adresse.rue.$error">
                                                {{ v.adresse.rue.$errors[0].$message }}
                                            </div>
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
                                                   :class="{ 'is-invalid': v.adresse.ville.$error }"
                                                   id="ville"
                                                   @blur="v.adresse.ville.$touch()" />
                                            <div class="invalid-feedback" v-if="v.adresse.ville.$error">
                                                {{ v.adresse.ville.$errors[0].$message }}
                                            </div>
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row justify-content-between mb-3">
                                        <div class="form-group">
                                            <label for="province" class="ms-3">Province </label>
                                            <select id="province"
                                                    name="province"
                                                    v-model="formData.adresse.province"
                                                    class="form-control"
                                                    :class="{ 'is-invalid': v.adresse.province.$error }"
                                                    @blur="v.adresse.province.$touch()"
                                                    placeholder="Sélectionnez une province">
                                                <option value="" disabled selected>
                                                    Sélectionner une province
                                                </option>
                                                <option value="AB">Alberta</option>
                                                <option value="BC">Colombie-Britannique</option>
                                                <option value="MB">Manitoba</option>
                                                <option value="NB">Nouveau-Brunswick</option>
                                                <option value="NL">Terre-Neuve-et-Labrador</option>
                                                <option value="NS">Nouvelle-Écosse</option>
                                                <option value="ON">Ontario</option>
                                                <option value="PE">Île-du-Prince-Édouard</option>
                                                <option value="QC" selected>Québec</option>
                                                <option value="SK">Saskatchewan</option>
                                                <option value="NT">Territoires du Nord-Ouest</option>
                                                <option value="NU">Nunavut</option>
                                                <option value="YT">Yukon</option>
                                            </select>
                                            <div class="invalid-feedback" v-if="v.adresse.province.$error">
                                                {{ v.adresse.province.$errors[0].$message }}
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label for="pays" class="ms-3">Pays</label>
                                            <input type="text"
                                                   class="form-control"
                                                   v-model="formData.adresse.pays"
                                                   id="pays" />
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row justify-content-between mb-3">
                                        <div class="form-group">
                                            <label for="postalCode" class="ms-3">Code Postal</label>
                                            <input type="text"
                                                   class="form-control"
                                                   v-model="formData.adresse.codePostal"
                                                   :class="{ 'is-invalid': v.adresse.codePostal.$error }"
                                                   id="postalCode"
                                                   placeholder="A1A 2B2"
                                                   @blur="v.adresse.codePostal.$touch()" />
                                            <div class="invalid-feedback" v-if="v.adresse.codePostal.$error">
                                                {{ v.adresse.codePostal.$errors[0].$message }}
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="d-flex justify-content-between gap-2 py-3">
                                    <button class="btn btn-secondary rounded-pill px-5 ms-4"
                                            @click="allerAuSuivantPrecedent(2)">
                                        Précédent
                                    </button>
                                    <button type="submit"
                                            :disabled="stateFinal"
                                            :class="[
                      stateFinal ? 'bleuValide' : 'bleuNonValide',
                      classeActiveBouton
                    ]">
                                        Créer le compte
                                    </button>
                                </div>
                            </StepPanel>
                        </StepItem>
                    </Stepper>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
    import { ref, computed, reactive } from "vue";
    import Stepper from "primevue/stepper";
    import StepItem from "primevue/stepitem";
    import Step from "primevue/step";
    import StepPanel from "primevue/steppanel";
    import { useVuelidate } from "@vuelidate/core";
    import { required, email, helpers } from "@vuelidate/validators";
    import { useStore } from 'vuex';
    import { useRouter } from 'vue-router';

    const activeIndex = ref(1);
    const messageRequis = helpers.withMessage("Ce champ est requis", required);
    const messageCourriel = helpers.withMessage(
        "Veuillez entrer un courriel valide",
        email
    );

    //objet qui contient tous les champs remplis correctement
    let formData = reactive({
        generalInfo: {
            nom: "",
            prenom: "",
            courriel: "",
            telephone: "",
            pseudo: "",
            motDePasse: "",
            confirmMotPasse: "",
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
        },
    });

    //objet rules qui contient toutes les validations
    let rules = computed(() => {
        return {
            generalInfo: {
                nom: { required: messageRequis },
                prenom: { required: messageRequis },
                courriel: { required: messageRequis, email: messageCourriel },
                telephone: { required: messageRequis },
                pseudo: { required: messageRequis },
                motDePasse: { required: messageRequis },
                confirmMotPasse: {
                    required: messageRequis,
                },
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
            },
        };
    });

    const v = useVuelidate(rules, formData);
    const info = useVuelidate(rules.value.generalInfo, formData.generalInfo);
    const carte = useVuelidate(rules.value.carteCredit, formData.carteCredit);

    const classeActiveBouton = ref("btn rounded-pill px-5 me-4");

    const state1 = computed(() => {
        return info.value.$invalid;
    });

    const state2 = computed(() => {
        return carte.value.$invalid;
    });

    const stateFinal = computed(() => {
        return v.value.$invalid;
    });

    function allerAuSuivantPrecedent(stepIndex) {
        activeIndex.value = stepIndex;
    }

    const store = useStore();
    const router = useRouter();

    const errorMessage = ref('');
    const successMessage = ref('');

    const submitForm = async () => {
        const result = await v.value.$validate();
        if (result) {
            try {
                const response = await store.dispatch('creerCompteUtilisateur', formData);
                if (response.success) {
                    successMessage.value = "Compte créé avec succès !";
                    console.log('Nouveau compte créé:', response.user);
                    console.log('Rôles:', response.roles);
                    setTimeout(() => {
                        router.push('/');
                    }, 2000);
                } else {
                    errorMessage.value = response.error || "Une erreur est survenue lors de la création du compte.";
                }
            } catch (error) {
                errorMessage.value = "Une erreur est survenue lors de la création du compte.";
            }
        } else {
            errorMessage.value = "Veuillez corriger les erreurs dans le formulaire.";
        }
    };

</script>

<style scoped>
    .information .form-group,
    .adresse .form-group {
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

    .background-container {
        width: 100%;
        height: 100vh;
        background-image: url("/public/images/DessinGris.PNG");
        background-size: cover;
        background-position: center;
        background-repeat: no-repeat;
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
        .i div,
        .c div,
        .a div {
            display: flex;
            flex-basis: 100%;
            flex-direction: column;
        }
    }

    .alert {
        margin-top: 1rem;
        padding: 0.75rem 1.25rem;
        border: 1px solid transparent;
        border-radius: 0.25rem;
    }

    .alert-danger {
        color: #721c24;
        background-color: #f8d7da;
        border-color: #f5c6cb;
    }

    .alert-success {
        color: #155724;
        background-color: #d4edda;
        border-color: #c3e6cb;
    }
</style>


