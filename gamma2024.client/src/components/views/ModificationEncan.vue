<template>
  <div class="d-flex flex-column justify-content-between">
    <h2 class="d-flex justify-content-center">Modification de l'encan</h2>

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
      <form @submit.prevent="updateEncan" class="text-center">
        <div class="mb-3">
          <label for="numeroEncan" class="form-label">Numéro de l'encan:</label>
          <input
            type="number"
            disabled
            aria-label="numeroEncan"
            v-model="encanData.numeroEncan"
            :class="['form-control', { 'is-invalid': v.numeroEncan.$error }]"
            @blur="v.numeroEncan.$touch()"
          />
          <div class="invalid-feedback" v-if="v.numeroEncan.$error">
            {{ v.numeroEncan.$errors[0].$message }}
          </div>
        </div>
        <div class="mb-3">
          <label for="dateDebut" class="form-label">Date de début:</label>
          <VueDatePicker
            type="date"
            v-model="encanData.dateDebut"
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
          />
          <div class="invalid-feedback" v-if="v.dateDebut.$error">
            {{ v.dateDebut.$errors[0].$message }}
          </div>
        </div>
        <div class="mb-3">
          <label for="dateFin" class="form-label">Date de fin:</label>
          <VueDatePicker
            type="date"
            v-model="encanData.dateFin"
            :class="['form-control', { 'is-invalid': v.dateFin.$error }]"
            @blur="v.dateFin.$touch()"
            :min-date="desacDateFinEntre"
            :enable-time-picker="true"
            select-text="Choisir"
            cancel-text="Annuler"
            :format-locale="fr"
          />
          <div class="invalid-feedback" v-if="v.dateFin.$error">
            {{ v.dateFin.$errors[0].$message }}
          </div>
        </div>

        <button
          type="button"
          @click="resetForm"
          class="btn btn-secondary me-3"
          :disabled="!formDataChanged"
        >
          Réinitialiser
        </button>
        <button
          type="submit"
          :disabled="!formDataChanged"
          :class="[formDataChanged ? 'bleuValide' : 'bleuNonValide', 'btn ']"
        >
          Enregistrer
        </button>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, reactive, computed, onMounted, watch } from "vue";
import useVuelidate from "@vuelidate/core";
import { required, helpers } from "@vuelidate/validators";
import { useStore } from "vuex";
import { useRoute, useRouter } from "vue-router";
import VueDatePicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import { fr } from "date-fns/locale";

const route = useRoute();
const router = useRouter();
const store = useStore();

const messageRequis = helpers.withMessage(
  "Ce champ est obligatoire.",
  required
);
let errorMessage = ref("");
let successMessage = ref("");
let formDataChanged = ref(false);
let initialFormData = ref({});

let encanData = reactive({
  id: "",
  numeroEncan: "",
  dateDebut: "",
  dateFin: "",
});

onMounted(async () => {
  const response = await store.dispatch("obtenirUnEncanParId", route.params.id);
  if (response) {
    encanData.id = route.params.id;
    encanData.numeroEncan = response.numeroEncan;
    encanData.dateDebut = response.dateDebut;
    encanData.dateFin = response.dateFin;
  }

  initialFormData.value = JSON.parse(JSON.stringify(encanData));
  formDataChanged.value = false;
});

let rules = computed(() => {
  return {
    numeroEncan: { required: messageRequis },
    dateDebut: { required: messageRequis },
    dateFin: { required: messageRequis },
  };
});

const v = useVuelidate(rules, encanData);

const desacDateFinEntre = computed(() => {
  const debutDate = encanData.dateDebut;

  if (debutDate != "") {
    const dateDebutDesac = new Date(debutDate);
    dateDebutDesac.setDate(dateDebutDesac.getDate() + 1);

    return [dateDebutDesac];
  }
  return null;
});

const desacDateDebutEntre = computed(() => {
  const finDate = encanData.dateFin;

  if (finDate != "") {
    const dateFinDesac = new Date(finDate);
    dateFinDesac.setDate(dateFinDesac.getDate() - 1);

    return [dateFinDesac];
  }
  return null;
});

watch(
  () => JSON.parse(JSON.stringify(encanData)), // Deep clone copy to avoid same ref type.
  () => {
    // Comparaison entre l'état initial et l'état actuel
    if (JSON.stringify(encanData) !== JSON.stringify(initialFormData.value)) {
      formDataChanged.value = true;
    } else {
      formDataChanged.value = false;
    }
  },
  { deep: true }
);

const resetForm = function () {
  Object.assign(encanData, JSON.parse(JSON.stringify(initialFormData.value)));
  v.value.$reset();
  formDataChanged.value = false;
};

const updateEncan = async function () {
  try {
    const result = await store.dispatch("modifierEncan", encanData);
    console.log("resultat dans modifEncan", result);
    if (result.success) {
      successMessage.value = result.message;
      setTimeout(() => {
        router.push({ name: "TableauDeBordEncans" });
      }, 3000);
    } else {
      errorMessage.value = result.error;
      setTimeout(() => {
        errorMessage.value = "";
      }, 4000);
    }
  } catch (error) {
    console.error("Erreur lors de la modification de l'encan:", error);
  }
};
</script>

<style scoped>
.bleuValide {
  background-color: #052445;
  color: white;
}

.bleuNonValide {
  background-color: #5a708a;
  color: white;
}

.transit {
  margin: auto;
  width: 600px;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 3s ease;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
