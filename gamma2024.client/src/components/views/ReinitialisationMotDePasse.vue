<template>
  <div class="bg-image pt-5 imageDeFondEsquise">
    <transition name="fade" class="transit">
      <div
        v-if="message"
        :class="[
          'alert',
          message.type === 'success' ? 'alert-success' : 'alert-danger',
        ]"
      >
        {{ message.text }}
      </div>
    </transition>
    <div
      class="container d-flex flex-column justify-content-start align-items-stretch bg-white bg-opacity-75 cadreBlanc"
    >
      <h2 class="fs-1 text-center fw-bold pt-4">
        Réinitialiser le mot de passe
      </h2>

      <form
        @submit.prevent="resetPassword"
        class="d-flex flex-column justify-content-start align-items-stretch"
      >
        <div class="px-4 py-2 mb-4">
          <div class="d-flex flex-row justify-content-center mb-3">
            <div class="form-group w-90">
              <label for="email" class="fw-bold ms-3">Adresse courriel</label>
              <input
                type="text"
                :class="['form-control', { 'is-invalid': v.email.$error }]"
                id="email"
                v-model="resetPasswordData.email"
                @blur="v.email.$touch()"
              />
              <div class="invalid-feedback" v-if="v.email.$error">
                {{ v.email.$errors[0].$message }}
              </div>
            </div>
          </div>
          <div class="d-flex flex-row justify-content-center mb-3">
            <div class="form-group w-90">
              <label for="confirmPassword" class="fw-bold ms-3"
                >Nouveau mot de passe</label
              >
              <input
                type="password"
                :class="['form-control', { 'is-invalid': v.password.$error }]"
                id="confirmPassword"
                v-model="resetPasswordData.password"
                @blur="v.password.$touch()"
              />
              <div class="invalid-feedback" v-if="v.password.$error">
                {{ v.password.$errors[0].$message }}
              </div>
            </div>
          </div>
          <div class="d-flex flex-row justify-content-center mb-3">
            <div class="form-group w-90">
              <label for="password" class="fw-bold ms-3"
                >Confirmer le nouveau mot de passe</label
              >
              <input
                type="password"
                :class="[
                  'form-control',
                  { 'is-invalid': v.confirmPassword.$error },
                ]"
                id="password"
                v-model="resetPasswordData.confirmPassword"
                @blur="v.confirmPassword.$touch()"
              />
              <div class="invalid-feedback" v-if="v.confirmPassword.$error">
                {{ v.confirmPassword.$errors[0].$message }}
              </div>
            </div>
          </div>
          <div class="d-flex flex-row justify-content-center mb-2">
            <button
              :disabled="isSubmitting"
              class="btn bleuNonValide rounded-pill px-5 text-white"
              type="submit"
            >
              <span
                v-if="isSubmitting"
                class="spinner-grow spinner-grow-sm"
                role="status"
                aria-hidden="true"
              ></span>
              Changer le mot de passe
            </button>
          </div>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, reactive } from "vue";
import { useVuelidate } from "@vuelidate/core";
import {
  required,
  email,
  helpers,
  sameAs,
  minLength,
} from "@vuelidate/validators";
import { useStore } from "vuex";
import { useRouter } from "vue-router";

const store = useStore();
const router = useRouter();
let isSubmitting = ref(false);
const message = ref(null);
const messageRequis = helpers.withMessage("Ce champ est requis.", required);
const messageCourriel = helpers.withMessage(
  "Veuillez entrer un courriel valide.",
  email
);

const messageMinLengthPassword = helpers.withMessage(
  "Le mot de passe doit contenir au moins 8 caractères, minimum 1 majuscule, 1 minuscule, 1 chiffre et 1 caractère spécial.",
  minLength(8)
);

let resetPasswordData = reactive({
  email: "",
  password: "",
  confirmPassword: "",
});

const messageSameAsPassword = helpers.withMessage(
  "Les mots de passe ne correspondent pas.",
  sameAs(computed(() => resetPasswordData.password))
);

let rules = computed(() => {
  return {
    email: {
      required: messageRequis,
      email: messageCourriel,
    },
    password: {
      required: messageRequis,
      minLength: messageMinLengthPassword,
    },
    confirmPassword: {
      required: messageRequis,
      sameAsPassword: messageSameAsPassword,
    },
  };
});

const v = useVuelidate(rules, resetPasswordData);

const resetPassword = async function () {
  const result = await v.value.$validate();
  isSubmitting.value = true;

  if (!result) {
    isSubmitting.value = false;
    message.value = {
      type: "danger",
      text: "Certaines informations du formulaire sont incorrectes",
    };
    setTimeout(() => {
      message.value = null;
    }, 3500);
    return;
  }

  try {
    const response = await store.dispatch(
      "reinitialisePassword",
      resetPasswordData
    );
    if (response.success) {
      message.value = { type: "success", text: response.message };

      setTimeout(() => {
        router.push("/connexion");
      }, 4000);
    } else {
      message.value = { type: "danger", text: response.message };
    }
  } catch (error) {
    message.value = {
      type: "danger",
      text: "Une erreur est survenue lors de la création du compte.",
    };
  } finally {
    setTimeout(() => {
      message.value = null;
    }, 3500);
    isSubmitting.value = false;
  }
};
</script>

<style scped>
.cadreBlanc {
  border-radius: 15px;
  min-height: 450px;
  width: 550px;
}

.imageDeFondEsquise {
  background-image: url("/public/images/DessinGris.PNG");
  background-size: cover;
  background-position: 0px -100px;
  background-attachment: fixed;
  height: 100vh;
  width: 100%;
}

.w-80 {
  width: 80%;
}

.w-90 {
  width: 90%;
}

.bleuNonValide {
  background-color: #052445;
  color: white;
}

.bleuNonValide:hover {
  background-color: #5a708a;
}

.text-danger {
  font-size: 0.875rem;
}

.invalid-feedback {
  display: block;
  color: #dc3545;
  font-size: 0.875rem;
}

.lockMessage {
  margin: auto;
  width: 35%;
  text-align: center;
  margin-bottom: 10px;
}

.transit {
  margin: auto;
  width: 600px;
  margin-bottom: 15px;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 2s ease;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
