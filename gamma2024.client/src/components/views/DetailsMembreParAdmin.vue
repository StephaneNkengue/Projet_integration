<template>
  <div class="pt-5">
    <div
      class="container d-flex flex-column justify-content-start align-items-stretch container col-md-6"
    >
      <!-- Ajout de la section avatar -->
      <div class="avatar-section mb-4 d-flex flex-column align-items-center">
        <div class="avatar-container">
          <img src="" alt="Avatar" class="avatar-image mb-2" />
        </div>
      </div>

      <h2 class="fs-1 text-center fw-bold mt-5">
        Modification des informations
      </h2>

      <div class="mt-3">
        <form>
          <!-- Informations générales -->
          <div class="card mb-5">
            <div class="card-header">Informations générales</div>
            <div class="card-body">
              <div class="row mt-2">
                <div class="col-md-6">
                  <label for="nom">Nom</label>
                  <input
                    type="text"
                    id="nom"
                    class="form-control"
                    v-model="membre.name"
                  />
                </div>
                <div class="col-md-6">
                  <label for="prenom">Prénom</label>
                  <input
                    type="text"
                    id="prenom"
                    class="form-control"
                    v-model="membre.firstName"
                  />
                </div>
              </div>
              <div class="row mt-2">
                <div class="col-md-6">
                  <label for="pseudonyme">Pseudonyme</label>
                  <input
                    type="text"
                    id="pseudonyme"
                    class="form-control"
                    v-model="membre.userName"
                  />
                </div>
                <div class="col-md-6">
                  <label for="telephone">Téléphone</label>
                  <div class="input-wrapper">
                    <InputMask
                      type="tel"
                      mask="(999) 999-9999"
                      id="telephone"
                      v-model="membre.phoneNumber"
                    />
                  </div>
                </div>
              </div>
              <div class="row mt-2">
                <div class="col-md-6">
                  <label class="labels">Courriel</label>
                  <input
                    type="text"
                    class="form-control"
                    v-model="membre.email"
                  />
                </div>
              </div>
            </div>
          </div>

          <!-- Informations de carte de crédit -->
          <div class="card my-5">
            <div class="card-header">Carte de crédit</div>
            <div class="card-body">
              <div class="row mt-2">
                <div class="col-md-12">
                  <label for="nomPropio">Nom du propriétaire de la carte</label>
                  <input type="text" id="nomPropio" class="form-control" />
                </div>
              </div>
              <div class="row mt-2">
                <div class="col-md-6">
                  <label for="numeroCarte">Numéro de la carte</label>
                  <div class="input-wrapper">
                    <InputMask
                      type="text"
                      mask="9999 9999 9999 9999"
                      id="numeroCarte"
                      class="form-control"
                    />
                  </div>
                </div>
                <div class="col-md-6">
                  <label for="dateExpiration">Date d'expiration</label>
                  <div class="input-wrapper">
                    <InputMask
                      type="text"
                      mask="99/99"
                      class="form-control"
                      placeholder="MM/YY"
                      id="dateExpiration"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Informations d'adresse -->
          <div class="card my-5">
            <div class="card-header">Adresse</div>
            <div class="card-body">
              <div class="row mt-2">
                <div class="col-md-6">
                  <label for="numeroCivique">Numéro civique</label>
                  <input type="text" id="numeroCivique" />
                </div>
                <div class="col-md-6">
                  <label for="rue">Rue</label>
                  <input type="text" class="form-control" id="rue" />
                </div>
              </div>
              <div class="row mt-2">
                <div class="col-md-6">
                  <label for="appartement">Appartement/bureau</label>
                  <input type="text" class="form-control" id="appartement" />
                </div>
                <div class="col-md-6">
                  <label for="ville">Ville</label>
                  <input type="text" class="form-control" id="ville" />
                </div>
              </div>
              <div class="row mt-2">
                <div class="col-md-6">
                  <label for="province">Province</label>
                  <input type="text" class="form-control" id="province" />
                </div>
                <div class="col-md-6">
                  <label class="labels">Pays</label>
                  <input type="text" class="form-control" />
                </div>
              </div>
              <div class="row mt-2">
                <div class="col-md-12">
                  <label for="postalCode">Code postal</label>
                  <div class="input-wrapper">
                    <inputMask
                      type="text"
                      id="postalCode"
                      class="form-control"
                      mask="a9a 9a9"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>
<script setup>
import { ref, onMounted } from "vue";
import { useStore } from "vuex";
import InputMask from "primevue/inputmask";
import { useRoute } from "vue-router";

const route = useRoute();
const store = useStore();
let membre = ref({});

onMounted(async () => {
  try {
    const membreId = route.params.id;
    const response = await store.dispatch("obtenirUnMembre", membreId);
    membre.value = response;
  } catch (error) {
    console.error("Erreur lors de la récupération du membre");
  }
});
</script>

<style scoped>
label {
  font-weight: 600;
  margin-left: 15px;
}

.input-wrapper .p-inputmask {
  width: 100%;
  border-radius: 0.35rem;
  border: 1px solid rgba(0, 0, 0, 0.12);
  padding: 0.375rem 0.75rem;
  background-color: white;
  height: calc(1.5em + 0.75rem + 2px);
  transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
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
  box-shadow: 0 0 0 0.2rem #80bdff;
}

.input-wrapper .p-inputmask:hover {
  border: 1px solid rgba(0, 0, 0, 0.12);
}
</style>
