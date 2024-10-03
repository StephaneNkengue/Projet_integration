<template>
  <div>
    <div
      class="modal fade"
      :id="props.h.idModal"
      tabindex="-1"
      data-bs-backdrop="static"
      data-bs-keyboard="false"
      aria-labelledby="changePasswordModalLabel"
      aria-hidden="true"
    >
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title" id="changePasswordModalLabel">
              Changer le mot de passe
            </h5>
            <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
              aria-label="Close"
            ></button>
          </div>

          <div class="modal-body">
            <form @submit.prevent="handleSubmit">
              <div class="mb-3">
                <label for="currentPassword" class="form-label"
                  >Mot de passe actuel:</label
                >
                <input
                  type="password"
                  class="form-control"
                  id="currentPassword"
                  v-model="form.currentPassword"
                />
              </div>
              <div class="mb-3">
                <label for="newPassword" class="form-label"
                  >Nouveau mot de passe:</label
                >
                <input
                  type="password"
                  :class="[
                    'form-control',
                    { 'is-invalid': v.form.newPassword.$error },
                  ]"
                  id="newPassword"
                  v-model="form.newPassword"
                  placeholder="Nouveau mot de passe"
                  @blur="v.form.newPassword.$touch()"
                />
                <div class="invalid-feedback" v-if="v.form.newPassword.$error">
                  {{ v.form.newPassword.$errors[0].$message }}
                </div>
              </div>
              <div class="mb-3">
                <label for="confirmPassword" class="form-label"
                  >Confirmation du mot de passe:</label
                >
                <input
                  type="password"
                  id="confirmPassword"
                  v-model="form.confirmPassword"
                  placeholder="Confirmer le mot de passe"
                  :class="[
                    'form-control',
                    { 'is-invalid': v.form.confirmPassword.$error },
                  ]"
                  @blur="v.form.confirmPassword.$touch()"
                />
                <div
                  class="invalid-feedback"
                  v-if="v.form.confirmPassword.$error"
                >
                  {{ v.form.confirmPassword.$errors[0].$message }}
                </div>
              </div>
              <div class="d-flex justify-content-between">
                <button
                  type="button"
                  class="btn btn-secondary"
                  data-bs-dismiss="modal"
                >
                  Annuler
                </button>
                <button type="submit" class="btn btn-primary">
                  Sauvegarder
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { reactive, computed } from "vue";
import { required, helpers, minLength, sameAs } from "@vuelidate/validators";
import { useVuelidate } from "@vuelidate/core";

const messageRequis = helpers.withMessage("Ce champ est requis", required);
const passwordMinLength = helpers.withMessage(
  "Le mot de passe doit contenir au moins 8 caractères, minimum 1 majuscule, 1 minuscule, 1 chiffre et 1 caractère spécial.",
  minLength(8)
);

let form = reactive({
  currentPassword: "",
  newPassword: "",
  confirmPassword: "",
});

const messageSameAsPassword = helpers.withMessage(
  "Les mots de passe ne correspondent pas.",
  sameAs(computed(() => form.newPassword))
);

let rules = computed(() => {
  return {
    form: {
      currentPassword: {},
      newPassword: {
        required: messageRequis,
        minLength: passwordMinLength,
      },
      confirmPassword: {
        required: messageRequis,
        sameAsPassword: messageSameAsPassword,
      },
    },
  };
});

const v = useVuelidate(rules, form);

const handleSubmit = async () => {
  const result = await v.value.$validate();
  if (result) {
    form.currentPassword = "";
    form.newPassword = "";
    form.confirmPassword = "";

    // modal.hide();
  } else {
    return;
  }
};

const props = defineProps({
  h: {
    type: Object,
    default: () => ({}),
  },
});
</script>

<style scoped>
.is-invalid,
span {
  border-color: red;
  font-weight: 600;
}
</style>
