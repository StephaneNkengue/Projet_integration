<template>
  <div class="bg-image pt-5 imageDeFondEsquise">
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
                class="form-control"
                id="email"
                v-model="emailUser"
                @blur="v.emailUser.$touch()"
              />
            </div>
          </div>
          <div class="d-flex flex-row justify-content-center mb-3">
            <div class="form-group w-90">
              <label for="confirmMotdepasse" class="fw-bold ms-3"
                >Mot de passe</label
              >
              <input
                type="password"
                class="form-control"
                id="confirmMotdepasse"
                v-model="motDePasse"
                @blur="v.motDePasse.$touch()"
              />
              <div class="invalid-feedback" v-if="v.motDePasse.$error">
                {{ v.motDePasse.$errors[0].$message }}
              </div>
            </div>
          </div>
          <div class="d-flex flex-row justify-content-center mb-3">
            <div class="form-group w-90">
              <label for="motDePasse" class="fw-bold ms-3"
                >Confirmer le mot de passe</label
              >
              <input
                type="password"
                class="form-control"
                id="motDePasse"
                v-model="confirmMotDePasse"
                @blur="v.confirmMotDePasse.$touch()"
              />
              <div class="invalid-feedback" v-if="v.confirmMotDePasse.$error">
                {{ v.confirmMotDePasse.$errors[0].$message }}
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
let errorMessage = ref("");
let successMessage = ref("");
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
  emailUser: "",
  motDePasse: "",
  confirmMotDePasse: "",
});

const messageSameAsPassword = helpers.withMessage(
  "Les mots de passe ne correspondent pas.",
  sameAs(computed(() => resetPasswordData.generalInfo.motDePasse))
);

let rules = computed(() => {
  return {
    emailUser: {
      required: messageRequis,
      email: messageCourriel,
    },
    motDePasse: {
      required: messageRequis,
      minLength: messageMinLengthPassword,
    },
    confirmMotDePasse: {
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
    errorMessage.value =
      "Certaines informations du formulaire sont incorrectes";
    return;
  }

  try {
    const response = await store.dispatch(
      "reinitialisePassword",
      resetPasswordData
    );
    if (response.success) {
      successMessage.value = "Mot de passe changé avec succès !";

      setTimeout(() => {
        router.push("/connexion");
      }, 3500);
    } else {
      errorMessage.value =
        response.message ||
        "Une erreur est survenue lors de la création du compte.";
    }
  } catch (error) {
    errorMessage.value =
      "Une erreur est survenue lors de la création du compte.";
  } finally {
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

.fade-enter-active,
.fade-leave-active {
  transition: opacity 1s ease;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}
</style>
