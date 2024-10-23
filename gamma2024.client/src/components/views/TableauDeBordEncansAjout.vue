<template>
    <div class="d-flex flex-column justify-content-between">
        <h2 class="d-flex justify-content-center">Ajout d'un encan</h2>

        <div v-if="errorMessage" class="alert alert-danger">
            {{ errorMessage }}
        </div>
        <div v-else>
            <div v-if="successMessage" class="alert alert-success">
                {{ successMessage }}
            </div>
        </div>

        <div class="d-flex justify-content-center mt-4">
            <form @submit.prevent="creerEncan" class="text-center">
                <div class="mb-3">
                    <label for="numeroEncan" class="form-label">Numéro de l'encan:</label>
                    <input type="number" aria-label="numeroEncan" v-model="formData.numeroEncan"
                           :class="['form-control', { 'is-invalid': v.numeroEncan.$error }]"
                           @blur="v.numeroEncan.$touch()">
                    <div class="invalid-feedback" v-if="v.numeroEncan.$error">
                        {{v.numeroEncan.$errors[0].$message}}
                    </div>
                </div>
                <div class="mb-3">
                    <label for="dateDebut" class="form-label">Date de début:</label>
                    <VueDatePicker type="date" v-model="formData.dateDebut"
                                   :class="['form-control', { 'is-invalid': v.dateDebut.$error }]"
                                   @blur="v.dateDebut.$touch()"
                                   :min-date="new Date()"
                                   :max-date="desacDateDebutEntre"
                                   :enable-time-picker="false"
                                   select-text="Choisir"
                                   cancel-text="Annuler"
                                   now-button-label="Aujourd'hui"
                                   :action-row="{ showNow: true }"
                                   :format-locale="fr" />
                    <div class="invalid-feedback" v-if="v.dateDebut.$error">
                        {{v.dateDebut.$errors[0].$message}}
                    </div>
                </div>
                <div class="mb-3">
                    <label for="dateFin" class="form-label">Date de fin:</label>
                    <VueDatePicker type="date" v-model="formData.dateFin"
                                   :class="['form-control', { 'is-invalid': v.dateFin.$error }]"
                                   @blur="v.dateFin.$touch()"
                                   :min-date="desacDateFinEntre"
                                   :enable-time-picker="true"
                                   select-text="Choisir"
                                   cancel-text="Annuler"
                                   :format-locale="fr" />
                    <div class="invalid-feedback" v-if="v.dateFin.$error">
                        {{v.dateFin.$errors[0].$message}}
                    </div>
                </div>
                <div class="mb-3">
                    <label for="dateSoireeCloture" class="form-label">Soirée de clôture:</label>
                    <VueDatePicker type="time" v-model="formData.dateDebutSoireeCloture"
                                   :class="['form-control', { 'is-invalid': v.dateDebutSoireeCloture.$error }]"
                                   @blur="v.dateDebutSoireeCloture.$touch()"
                                   select-text="Choisir"
                                   cancel-text="Annuler"
                                   :min-time="{ hours: new Date(formData.dateFin).getHours(), minutes: new Date(formData.dateFin).getMinutes() }"
                                   :start-time="{ hours: new Date(formData.dateFin).getHours(), minutes: new Date(formData.dateFin).getMinutes() }"
                                   time-picker />
                    <div class="invalid-feedback" v-if="v.dateDebutSoireeCloture.$error">
                        {{v.dateDebutSoireeCloture.$errors[0].$message}}
                    </div>
                </div>
                <router-link to="TableauDeBordEncans" class="text-decoration-none">
                    <button type="button" class="btn bleuMarinSecondaireFond btnSurvolerBleuMoyenFond btnClick text-white">Annuler</button>
                </router-link>
                <button type="submit" class="btn bleuMarinSecondaireFond btnSurvolerBleuMoyenFond btnClick text-white">Ajouter</button>
            </form>
        </div>
    </div>
</template>

<script setup>
    import { ref, reactive, computed } from "vue";
    import useVuelidate from "@vuelidate/core";
    import { required, helpers } from "@vuelidate/validators";
    import { useStore } from 'vuex';

    import VueDatePicker from '@vuepic/vue-datepicker';
    import '@vuepic/vue-datepicker/dist/main.css';
    import { fr } from 'date-fns/locale';

    const store = useStore();
    const messageRequis = helpers.withMessage("Ce champ est obligatoire.", required);
    const errorMessage = ref('');
    const successMessage = ref('');

    let formData = reactive({
        numeroEncan: "",
        dateDebut: "",
        dateFin: "",
        dateDebutSoireeCloture : "",
    });

    let rules = computed(() => {
        return {
            numeroEncan: { required: messageRequis },
            dateDebut: { required: messageRequis },
            dateFin: { required: messageRequis },
            dateDebutSoireeCloture : {required: messageRequis}
        }
    });

    const v = useVuelidate(rules, formData);

    const creerEncan = async () => {
        const result = await v.value.$validate();

        if (!result) {
            errorMessage.value = "Il y a des champs qui sont incorrectes."
            return;
        }

        try {
            const response = await store.dispatch('creerEncan', formData);
            if (response.success) {
                successMessage.value = "Encan créé avec succès!";
            }
            else {
                errorMessage.value = response.message;
            }
        }
        catch (error) {
            errorMessage.value = "Une erreur est survenue lors de la création d'un encan.";
        }
    };

    const desacDateFinEntre = computed(() => {
        const debutDate = formData.dateDebut;

        if (debutDate != "") {
            const dateDebutDesac = new Date(debutDate)
            dateDebutDesac.setDate(dateDebutDesac.getDate() + 1)

            return [dateDebutDesac]
        }
        return null;
    });

    const desacDateDebutEntre = computed(() => {
        const finDate = formData.dateFin;

        if (finDate != "") {

            const dateFinDesac = new Date(finDate)
            dateFinDesac.setDate(dateFinDesac.getDate() - 1)

            return [dateFinDesac]
        }
        return null;
    });
</script>

<style scoped>
</style>