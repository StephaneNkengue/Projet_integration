<template>
    <div class="bg-image py-5 imageDeFondEsquise">
        <div class="container d-flex flex-column justify-content-start align-items-stretch container col-md-6 bg-white bg-opacity-75 cadreBlanc px-5">
            <h2 class="fs-1 text-center fw-bold mt-5">Inscription</h2>
            <p class="text-center">Obtenir un compte membre</p>

            <div v-if="errorMessage" class="alert alert-danger">
                {{ errorMessage }}
            </div>
            <div v-else>
                <div v-if="successMessage" class="alert alert-success">
                    {{ successMessage }}
                </div>
            </div>

            <!-- Stepper Navigation -->
            <div class="mt-3">
                <form @submit.prevent="submitForm">
                    <Stepper v-model:activeIndex="activeIndex" :class="['mb-4']">
                        <!-- Informations générales -->
                        <StepItem :disabled="activeIndex !== 1" id="itemStep1">
                            <Step value="1" class="border-top border-3 p-2 border-dark fs-2">
                                <span>1 - Informations générales</span>
                            </Step>

                            <StepPanel v-if="activeIndex === 1" value="1">
                                <div class="container px-4 py-2">
                                    <div class="row mb-3 justify-content-around">
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="nom">Nom</label>
                                                <input type="text"
                                                       v-model="formData.generalInfo.nom"
                                                       :class="['form-control', { 'is-invalid': v.generalInfo.nom.$error }]"
                                                       id="nom"
                                                       placeholder="John"
                                                       @blur="v.generalInfo.nom.$touch()" />
                                                <div class="invalid-feedback" v-if="v.generalInfo.nom.$error">
                                                    {{ v.generalInfo.nom.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="prenom">Prénom</label>
                                                <input type="text"
                                                       v-model="formData.generalInfo.prenom"
                                                       :class="['form-control', { 'is-invalid': v.generalInfo.prenom.$error }]"
                                                       id="prenom"
                                                       placeholder="Doe"
                                                       @blur="v.generalInfo.prenom.$touch()" />
                                                <div class="invalid-feedback" v-if="v.generalInfo.prenom.$error">
                                                    {{ v.generalInfo.prenom.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3 justify-content-around">
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="courriel">Courriel</label>
                                                <input type="email"
                                                       placeholder="johndoe@gmail.com"
                                                       v-model="formData.generalInfo.courriel"
                                                       :class="['form-control', { 'is-invalid': v.generalInfo.courriel.$error }]"
                                                       id="courriel"
                                                       @blur="v.generalInfo.courriel.$touch()" />
                                                <div class="invalid-feedback" v-if="v.generalInfo.courriel.$error">
                                                    {{ v.generalInfo.courriel.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="telephone">Téléphone</label>
                                                <div class="input-wrapper">
                                                    <InputMask type="tel"
                                                               placeholder="(999) 999-9999"
                                                               mask="(999) 999-9999"
                                                               v-model="formData.generalInfo.telephone"
                                                               :class="['form-control', { 'is-invalid': v.generalInfo.telephone.$error }]"
                                                               id="telephone"
                                                               @blur="v.generalInfo.telephone.$touch()" />
                                                    <div class="invalid-feedback" v-if="v.generalInfo.telephone.$error">
                                                        {{ v.generalInfo.telephone.$errors[0].$message }}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3 justify-content-around">
                                        <div class="col col-md-12">
                                            <div class="form-group">
                                                <label for="pseudonyme">Pseudonyme</label>
                                                <input type="text"
                                                       v-model="formData.generalInfo.pseudo"
                                                       :class="['form-control', { 'is-invalid': v.generalInfo.pseudo.$error }]"
                                                       id="pseudonyme"
                                                       placeholder="johnDoe45"
                                                       @blur="v.generalInfo.pseudo.$touch()" />
                                                <div class="invalid-feedback" v-if="v.generalInfo.pseudo.$error">
                                                    {{ v.generalInfo.pseudo.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3 justify-content-around">
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="motDePasse">Mot de passe</label>
                                                <input type="password"
                                                       v-model="formData.generalInfo.motDePasse"
                                                       :class="['form-control', { 'is-invalid': v.generalInfo.motDePasse.$error }]"
                                                       id="motDePasse"
                                                       @blur="v.generalInfo.motDePasse.$touch()" />
                                                <div class="invalid-feedback" v-if="v.generalInfo.motDePasse.$error">
                                                    {{ v.generalInfo.motDePasse.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="confirmMotPasse">Confirmation de mot de passe</label>
                                                <input type="password"
                                                       v-model="formData.generalInfo.confirmMotPasse"
                                                       :class="['form-control', { 'is-invalid': v.generalInfo.confirmMotPasse.$error }]"
                                                       id="confirmMotPasse"
                                                       @blur="v.generalInfo.confirmMotPasse.$touch()" />
                                                <div class="invalid-feedback" v-if="v.generalInfo.confirmMotPasse.$error">
                                                    {{ v.generalInfo.confirmMotPasse.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end gap-2 py-3">
                                    <button type="button"
                                            :class="[
                      state1 ? 'bleuValide' : 'bleuNonValide',
                      'btn rounded-pill px-5 me-4'
                    ]"
                                            :disabled="state1"
                                            @click="allerAuSuivantPrecedent(2)">
                                        Suivant
                                    </button>
                                </div>
                            </StepPanel>
                        </StepItem>

                        <!-- Step 2: Carte de crédit -->
                        <StepItem :disabled="activeIndex !== 2">
                            <Step value="2"
                                  class="step2 border-top border-3 p-2 border-dark fs-2">
                                <span>2 - Carte de crédit</span>
                            </Step>

                            <StepPanel v-if="activeIndex === 2" value="2">
                                <div class="container px-4 py-2">

                                    <div class="row mb-3 justify-content-around">
                                        <div class="col col-md-12">
                                            <div class="form-group">
                                                <label for="nomPropio">Nom du propriétaire de la carte</label>
                                                <input type="text"
                                                       v-model="formData.carteCredit.nomProprio"
                                                       placeholder="John Doe"
                                                       :class="['form-control', { 'is-invalid': v.carteCredit.nomProprio.$error }]"
                                                       id="nomPropio"
                                                       @blur="v.carteCredit.nomProprio.$touch()" />
                                                <div class="invalid-feedback" v-if="v.carteCredit.nomProprio.$error">
                                                    {{ v.carteCredit.nomProprio.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3 justify-content-around">
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="numeroCarte">Numéro de la carte</label>
                                                <div class="input-wrapper">
                                                    <InputMask type="text"
                                                               v-model="formData.carteCredit.numeroCarte"
                                                               mask="9999 9999 9999 9999"
                                                               placeholder="4444 4444 4444 4444"
                                                               :class="['form-control', { 'is-invalid': v.carteCredit.numeroCarte.$error }]"
                                                               id="numeroCarte"
                                                               @blur="v.carteCredit.numeroCarte.$touch()" />
                                                    <div class="invalid-feedback" v-if="v.carteCredit.numeroCarte.$error">
                                                        {{ v.carteCredit.numeroCarte.$errors[0].$message }}
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="dateExpiration">Date d'expiration</label>

                                                <div class="input-wrapper">
                                                    <InputMask type="text"
                                                               mask="99/99"
                                                               placeholder="MM/YY"
                                                               v-model="formData.carteCredit.dateExpiration"
                                                               :class="['form-control', { 'is-invalid': v.carteCredit.dateExpiration.$error }]"
                                                               id="dateExpiration"
                                                               @blur="v.carteCredit.dateExpiration.$touch()" />
                                                    <div class="invalid-feedback" v-if="v.carteCredit.dateExpiration.$error">
                                                        {{ v.carteCredit.dateExpiration.$errors[0].$message }}
                                                    </div>
                                                </div>
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
                      classeActiveBouton, 'btn'
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
                                <div class="container px-4 py-2">

                                    <div class="row mb-3 justify-content-around">
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="numeroCivique">Numéro Civique</label>
                                                <input type="text"
                                                       v-model="formData.adresse.numeroCivique"
                                                       placeholder="1"
                                                       :class="['form-control', { 'is-invalid': v.adresse.numeroCivique.$error }]"
                                                       id="numeroCivique"
                                                       @blur="v.adresse.numeroCivique.$touch()" />
                                                <div class="invalid-feedback" v-if="v.adresse.numeroCivique.$error">
                                                    {{ v.adresse.numeroCivique.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="rue">Rue</label>
                                                <input type="text"
                                                       v-model="formData.adresse.rue"
                                                       placeholder="Saint Jacques"
                                                       :class="['form-control', { 'is-invalid': v.adresse.rue.$error }]"
                                                       id="rue"
                                                       @blur="v.adresse.rue.$touch()" />
                                                <div class="invalid-feedback" v-if="v.adresse.rue.$error">
                                                    {{ v.adresse.rue.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3 justify-content-around">
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="appartement">Appartement/Bureau</label>
                                                <input type="text"
                                                       placeholder="12"
                                                       v-model="formData.adresse.appartement"
                                                       class="form-control"
                                                       id="appartement" />
                                            </div>
                                        </div>
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="ville">Ville</label>
                                                <input type="text"
                                                       placeholder="Granby"
                                                       v-model="formData.adresse.ville"
                                                       :class="['form-control', { 'is-invalid': v.adresse.ville.$error }]"
                                                       id="ville"
                                                       @blur="v.adresse.ville.$touch()" />
                                                <div class="invalid-feedback" v-if="v.adresse.ville.$error">
                                                    {{ v.adresse.ville.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row mb-3 justify-content-around">
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="province">Province </label>
                                                <select id="province"
                                                        name="province"
                                                        v-model="formData.adresse.province"
                                                        :class="['form-control', { 'is-invalid': v.adresse.province.$error }]"
                                                        @blur="v.adresse.province.$touch()"
                                                        placeholder="Sélectionnez une province">
                                                    <option value="" disabled selected>
                                                        Sélectionner une province
                                                    </option>
                                                    <option v-for="province in provinces" :value="province.value">
                                                        {{ province.text }}
                                                    </option>
                                                </select>
                                                <div class="invalid-feedback" v-if="v.adresse.province.$error">
                                                    {{ v.adresse.province.$errors[0].$message }}
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col col-md-6 col-12">
                                            <div class="form-group">
                                                <label for="pays">Pays</label>
                                                <input type="text"
                                                       class="form-control"
                                                       v-model="formData.adresse.pays"
                                                       id="pays" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row justify-content-between mb-3">
                                        <div class="col col-md-12">
                                            <div class="form-group">
                                                <label for="postalCode">Code Postal</label>
                                                <div class="input-wrapper">
                                                    <inputMask type="text"
                                                               v-model="formData.adresse.codePostal"
                                                               :class="['form-control', { 'is-invalid': v.adresse.codePostal.$error }]"
                                                               id="postalCode"
                                                               mask="a9a 9a9"
                                                               placeholder="A1A 2B2"
                                                               @blur="v.adresse.codePostal.$touch()" />
                                                    <div class="invalid-feedback" v-if="v.adresse.codePostal.$error">
                                                        {{ v.adresse.codePostal.$errors[0].$message }}
                                                    </div>
                                                </div>
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
                      classeActiveBouton, 'btn'
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
    import InputMask from 'primevue/inputmask';
    import StepPanel from "primevue/steppanel";
    import { h } from 'vue';
    import { useVuelidate } from "@vuelidate/core";
    import ToastContent from '../Toast/toastConfirm.vue'
    import { required, email, helpers, sameAs, minLength, maxLength } from "@vuelidate/validators";
    import { useStore } from 'vuex';
    import { toast } from 'vue3-toastify';
    import { useRouter } from 'vue-router';


    const store = useStore();
    const router = useRouter();

    const errorMessage = ref('');
    const successMessage = ref('');

    const activeIndex = ref(1);
    const messageRequis = helpers.withMessage("Ce champ est requis.", required);
    const messageCourriel = helpers.withMessage(
        "Veuillez entrer un courriel valide.",
        email
    );

    const messageMinLength = helpers.withMessage(
        "Le mot de passe doit contenir au moins 8 caractères, minimum 1 majuscule, 1 minuscule, 1 chiffre et 1 caractère spécial.",
        minLength(8)
    );

    const messageMinLengthPseudo = helpers.withMessage(
        "Le pseudo doit contenir au moins 3 caractères.",
        minLength(3)
    );

    const messageMaxLengthPseudo = helpers.withMessage(
        "Le pseudo doit contenir au maximum 20 caractères.",
        maxLength(20)
    );


    const provinces = ref([
        { text: 'Alberta', value: 'AB' },
        { text: 'Colombie-Britannique', value: 'BC' },
        { text: 'Manitoba', value: 'MB' },
        { text: 'Nouveau-Brunswick', value: 'NB' },
        { text: 'Terre-Neuve-et-Labrador', value: 'NL' },
        { text: 'Nouvelle-Écosse', value: 'NS' },
        { text: 'Ontario', value: 'ON' },
        { text: 'Île-du-Prince-Édouard', value: 'PE' },
        { text: 'Québec', value: 'QC' },
        { text: 'Saskatchewan', value: 'SK' },
        { text: 'Territoires du Nord-Ouest', value: 'NT' },
        { text: 'Nunavut', value: 'NU' },
        { text: 'Yukon', value: 'YT' }
    ])

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



    const messageSameAsPassword = helpers.withMessage(
        "Les mots de passe ne correspondent pas.",
        sameAs(computed(() => formData.generalInfo.motDePasse))
    );


    //objet rules qui contient toutes les validations
    let rules = computed(() => {
        return {
            generalInfo: {
                nom: { required: messageRequis },
                prenom: { required: messageRequis },
                courriel: {
                    required: messageRequis,
                    email: messageCourriel
                },
                telephone: { required: messageRequis },
                pseudo: {
                    required: messageRequis,
                    minLengthValue: messageMinLengthPseudo,
                    maxLengthValue: messageMaxLengthPseudo
                },
                motDePasse: {
                    required: messageRequis,
                    minLength: messageMinLength,
                },
                confirmMotPasse: {
                    required: messageRequis,
                    sameAsPassword: messageSameAsPassword
                }
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



    const submitForm = async () => {
        const result = await v.value.$validate();

        if (!result) {
            errorMessage.value = "Certaines informations du formulaire sont incorrectes";
            return;
        }

        try {
            const response = await store.dispatch('creerCompteUtilisateur', formData);
            if (response.success) {
                successMessage.value = "Compte crée avec succès !"
                toast.success(
                    h(ToastContent, {
                        title: 'Compte créé avec succès !',
                        description: "Veuillez confirmer votre courriel afin d'activer votre compte",
                    }),
                    {
                        position: toast.POSITION.TOP_CENTER,
                        autoClose: 3000,
                        pauseOnFocusLoss: false,
                        theme: 'dark',
                        toastStyle: {
                            backgroundColor: '#052445',
                            color: '#fff',
                        },
                    }
                );

                setTimeout(() => {
                    router.push('/connexion');
                }, 4000);
            } else {
                errorMessage.value = response.error || "Une erreur est survenue lors de la création du compte.";
            }
        } catch (error) {
            errorMessage.value = "Une erreur est survenue lors de la création du compte.";
        }
    };

</script>

<style scoped>

    label {
        font-weight: bolder;
        margin-left: 15px;
    }

    .is-invalid,
    span {
        border-color: red;
        font-weight: 600;
    }

    .cadreBlanc {
        border-radius: 50px;
        height: auto;
        width: auto;
    }

    .bleuNonValide {
        background-color: #052445;
        color: white;
    }

    .bleuValide {
        background-color: #5a708a;
        color: white;
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



    .input-wrapper .p-inputmask {
        width: 100%;
        border-radius: 0.25rem;
        padding: 0.375rem 0.75rem;
        background-color: white;
        height: calc(1.5em + .75rem + 2px);
        box-shadow: inset 0 1px 2px rgba(0,0,0,.075);
        transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
    }


    input::placeholder {
        color: #6c757d;
        opacity: 1;
    }


    .p-inputmask input::placeholder {
        color: #6c757d;
        opacity: 1;
        font-weight: 400;
    }

    .input-wrapper .p-inputmask.is-invalid {
        border-color: #dc3545;
    }

    .input-wrapper .p-inputmask:focus {
        border-color: #80bdff;
        outline: 0;
        box-shadow: 0 0 0 0.2rem rgba(0,123,255,.25);
    }
</style>


