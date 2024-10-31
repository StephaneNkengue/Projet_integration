<template>
    <div class="d-flex flex-column justify-content-between">
        <h2 class="d-flex justify-content-center">Ajout d'un encan</h2>

        <transition name="fade" class="transit">
            <div v-if="errorMessage" class="alert alert-danger">
                {{ errorMessage }}
            </div>
            <div v-else>
                <div v-if="successMessage" class="alert alert-success">
                    {{ successMessage }}
                </div>
            </div>
        </transition>
        <div class="d-flex justify-content-center mt-4">
            <form @submit.prevent="creerEncan" class="text-center">
                <div class="mb-3">
                    <label for="dateDebut" class="form-label">Date de début:</label>
                    <VueDatePicker type="date"
                                   v-model="formData.dateDebut"
                                   :class="['form-control', { 'is-invalid': v.dateDebut.$error }]"
                                   @blur="v.dateDebut.$touch()"
                                   :min-date="new Date()"
                                   :max-date="desacDateDebutEntre"
                                   :enable-time-picker="false"
                                   select-text="Choisir"
                                   cancel-text="Annuler"
                                   now-button-label="Aujourd'hui"
                                   :action-row="{ showNow: true }"
                                   :format-locale="fr"
                                   :year-range="[new Date().getFullYear(), new Date().getFullYear() + 1000]" />
                    <div class="invalid-feedback" v-if="v.dateDebut.$error">
                        {{ v.dateDebut.$errors[0].$message }}
                    </div>
                </div>
                <div class="mb-3">
                    <label for="dateFin" class="form-label">Date de fin:</label>
                    <VueDatePicker type="date"
                                   v-model="formData.dateFin"
                                   :class="['form-control', { 'is-invalid': v.dateFin.$error }]"
                                   @blur="v.dateFin.$touch()"
                                   :min-date="desacDateFinEntre"
                                   :enable-time-picker="true"
                                   select-text="Choisir"
                                   cancel-text="Annuler"
                                   :format-locale="fr"
                                   :year-range="[new Date().getFullYear(), new Date().getFullYear() + 1000]"
                                   :disabled="formData.dateDebut != '' ? disabled : ''" />
                    <div class="invalid-feedback" v-if="v.dateFin.$error">
                        {{ v.dateFin.$errors[0].$message }}
                    </div>
                </div>
                <router-link to="TableauDeBordEncans" class="text-decoration-none me-2">
                    <button type="button"
                            class="btn bleuMarinSecondaireFond btnSurvolerBleuMoyenFond btnClick text-white">
                        Annuler
                    </button>
                </router-link>
                <button :disabled="stateFinal"
                        :class="[
            stateFinal ? 'bleuValide' : 'bleuNonValide',
            classeActiveBouton,
            'btn',
          ]"
                        class="bleuValideSurvoler"
                        type="submit">
                    Ajouter
                </button>
            </form>
        </div>
    </div>
</template>

<script setup>
    import { ref, reactive, computed } from "vue";
    import useVuelidate from "@vuelidate/core";
    import { required, helpers } from "@vuelidate/validators";
    import { useStore } from "vuex";

    import VueDatePicker from "@vuepic/vue-datepicker";
    import "@vuepic/vue-datepicker/dist/main.css";
    import { fr } from "date-fns/locale";

    import { useRouter } from "vue-router";
    const router = useRouter();

    const store = useStore();
    const messageRequis = helpers.withMessage(
        "Ce champ est obligatoire.",
        required
    );
    const errorMessage = ref("");
    const successMessage = ref("");

    let formData = reactive({
        dateDebut: "",
        dateFin: "",
    });

    let rules = computed(() => {
        return {
            dateDebut: { required: messageRequis },
            dateFin: { required: messageRequis },
        };
    });

    const v = useVuelidate(rules, formData);

    const stateFinal = computed(() => {
        return v.value.$invalid;
    });

    const creerEncan = async () => {
        const result = await v.value.$validate();

        if (!result) {
            errorMessage.value = "Il y a des champs qui sont incorrectes.";
            return;
        }

        try {
            const response = await store.dispatch("creerEncan", formData);
            if (response.success) {
                successMessage.value = "Encan créé avec succès!";
                errorMessage.value = "";

                setTimeout(() => {
                    router.push({ name: "TableauDeBordEncans" });
                }, 3500);
            } else {
                errorMessage.value = response.error;
                successMessage.value = "";
            }
        } catch (error) {
            errorMessage.value =
                "Une erreur est survenue lors de la création d'un encan.";
        }
    };

    const desacDateFinEntre = computed(() => {
        const debutDate = formData.dateDebut;

        if (debutDate != "") {
            const dateDebutDesac = new Date(debutDate);
            dateDebutDesac.setDate(dateDebutDesac.getDate() + 1);

            return [dateDebutDesac];
        }
        return [new Date()];
    });

    const desacDateDebutEntre = computed(() => {
        const finDate = formData.dateFin;

        if (finDate != "") {
            const dateFinDesac = new Date(finDate);
            dateFinDesac.setDate(dateFinDesac.getDate() - 1);

            return [dateFinDesac];
        }
        return null;
    });
</script>

<style scoped>
    .fade-enter,
    .fade-leave-to {
        opacity: 0;
    }

    .bleuNonValide {
        background-color: #1c3755;
        color: white;
    }

    .bleuValide {
        background-color: #83a0ba;
        color: white;
    }

    .bleuValideSurvoler:hover {
        background-color: #83a0ba;
        color: white;
    }

    .transit {
        margin: auto;
        width: 600px;
    }
</style>
