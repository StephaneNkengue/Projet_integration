<template>
  <template>
    <div class="bg-image pt-5 imageDeFondEsquise">
      <transition name="fade">
        <div
          v-if="messageLockout"
          class="container lockMessage alert alert-warning"
          role="alert"
        >
          {{ messageLockout }}
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
                <label for="emailOuPseudo" class="fw-bold ms-3"
                  >Email ou pseudonyme</label
                >
                <input
                  type="text"
                  class="form-control"
                  id="emailOuPseudo"
                  v-model="emailOuPseudo"
                  @input="validateEmailOuPseudo"
                  :class="{ 'is-invalid': emailOuPseudoError }"
                />
                <div v-if="emailOuPseudoError" class="invalid-feedback">
                  {{ emailOuPseudoError }}
                </div>
              </div>
            </div>
            <div class="d-flex flex-row justify-content-center mb-3">
              <div class="form-group w-90">
                <label for="motdepasse" class="fw-bold ms-3"
                  >Mot de passe</label
                >
                <input
                  type="password"
                  class="form-control"
                  id="motDePasse"
                  v-model="password"
                  @input="validatePassword"
                  :class="{ 'is-invalid': passwordError }"
                />
                <div v-if="passwordError" class="invalid-feedback">
                  {{ passwordError }}
                </div>
              </div>
            </div>
            <div class="d-flex flex-row justify-content-center mt-4 mb-1">
              <div class="form-group w-80">
                <router-link to="/" class="text-decoration-none">
                  <p class="text-center lienEnTexteNoir">
                    J'ai oublié mon Mot de passe
                  </p>
                </router-link>
              </div>
            </div>
            <div class="d-flex flex-row justify-content-center mb-2">
              <button
                :disabled="isSubmitting"
                class="btn bleuNonValide rounded-pill px-5 text-white"
                @click="connexion"
              >
                <span
                  v-if="isSubmitting"
                  class="spinner-grow spinner-grow-sm"
                  role="status"
                  aria-hidden="true"
                ></span>
                Connexion
              </button>
            </div>
            <div class="d-flex flex-row justify-content-center mb-3">
              <div class="form-group w-80">
                <router-link to="Inscription" class="text-decoration-none">
                  <p class="fs-5 text-center fw-bold lienEnTexteNoir">
                    M'inscrire
                  </p>
                </router-link>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
  </template>
</template>
<script setup>
import Password from "primevue/password";
</script>

<style scped>
.cadreBlanc {
  border-radius: 15px;
  min-height: 450px;
  width: 410px;
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
