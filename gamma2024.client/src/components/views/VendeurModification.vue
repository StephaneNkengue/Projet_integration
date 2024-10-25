<template>
  <section class="h-100 bg-dark">
    <div class="container py-5 h-100">
      <div class="row d-flex justify-content-center align-items-center h-100">
        <div class="col">
          <div class="card card-registration my-4">
            <div class="row g-0">
              <div class="col-xl-6 d-none d-xl-block">
                <img
                  src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-registration/img4.webp"
                  alt="Sample photo"
                  class="img-fluid"
                  style="
                    border-top-left-radius: 0.25rem;
                    border-bottom-left-radius: 0.25rem;
                  "
                />
              </div>
              <div class="col-xl-6">
                <div class="card-body p-md-3 text-black">
                  <h1 class="mb-5">Modification d'un vendeur</h1>

                  <div
                    v-if="message"
                    :class="[
                      'alert',
                      message.type === 'success'
                        ? 'alert-success'
                        : 'alert-danger',
                    ]"
                  >
                    {{ message.text }}
                  </div>

                  <form @submit.prevent="submitForm">
                    <div class="row">
                      <div class="col-md-6 mb-4">
                        <div class="form-outline">
                          <input
                            v-model="vendeur.prenom"
                            type="text"
                            id="prenom"
                            class="form-control form-control-lg"
                            required
                          />
                          <label class="form-label" for="prenom">Prénom</label>
                        </div>
                      </div>
                      <div class="col-md-6 mb-4">
                        <div class="form-outline">
                          <input
                            v-model="vendeur.nom"
                            type="text"
                            id="nom"
                            class="form-control form-control-lg"
                            required
                          />
                          <label class="form-label" for="nom">Nom</label>
                        </div>
                      </div>
                    </div>

                    <div class="form-outline mb-4">
                      <input
                        v-model="vendeur.courriel"
                        type="email"
                        id="courriel"
                        class="form-control form-control-lg"
                        required
                      />
                      <label class="form-label" for="courriel">Courriel</label>
                    </div>

                    <div class="form-outline mb-4">
                      <input
                        v-model="vendeur.telephone"
                        type="tel"
                        id="telephone"
                        class="form-control form-control-lg"
                        required
                      />
                      <label class="form-label" for="telephone"
                        >Téléphone</label
                      >
                    </div>

                    <div class="form-outline mb-4">
                      <input
                        v-model="vendeur.adresse.numeroCivique"
                        type="text"
                        id="numeroCivique"
                        class="form-control form-control-lg"
                        required
                      />
                      <label class="form-label" for="numeroCivique"
                        >Numéro civique</label
                      >
                    </div>

                    <div class="form-outline mb-4">
                      <input
                        v-model="vendeur.adresse.rue"
                        type="text"
                        id="rue"
                        class="form-control form-control-lg"
                        required
                      />
                      <label class="form-label" for="rue">Rue</label>
                    </div>

                    <div class="row">
                      <div class="col-md-6 mb-4">
                        <div class="form-outline">
                          <input
                            v-model="vendeur.adresse.ville"
                            type="text"
                            id="ville"
                            class="form-control form-control-lg"
                            required
                          />
                          <label class="form-label" for="ville">Ville</label>
                        </div>
                      </div>
                      <div class="col-md-6 mb-4">
                        <div class="form-outline">
                          <select
                            v-model="vendeur.adresse.province"
                            id="province"
                            class="form-select form-select-lg"
                            required
                          >
                            <option value="" disabled>Province</option>
                            <option
                              v-for="province in provinces"
                              :key="province"
                              :value="province"
                            >
                              {{ province }}
                            </option>
                          </select>
                          <label class="form-label" for="province"
                            >Province</label
                          >
                        </div>
                      </div>
                    </div>

                    <div class="form-outline mb-4">
                      <input
                        v-model="vendeur.adresse.codePostal"
                        type="text"
                        id="codePostal"
                        class="form-control form-control-lg"
                        required
                      />
                      <label class="form-label" for="codePostal"
                        >Code postal</label
                      >
                    </div>

                    <div class="form-outline mb-4">
                      <input
                        v-model="vendeur.adresse.pays"
                        type="text"
                        id="pays"
                        class="form-control form-control-lg"
                        required
                      />
                      <label class="form-label" for="pays">Pays</label>
                    </div>

                    <div class="d-flex justify-content-end pt-3">
                      <button
                        type="button"
                        class="btn btn-light btn-lg"
                        @click="resetForm"
                      >
                        Réinitialiser
                      </button>
                      <button type="submit" class="btn btn-custom btn-lg ms-2">
                        Mettre à jour le vendeur
                      </button>
                    </div>
                  </form>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import { useStore } from "vuex";
import { useRouter, useRoute } from "vue-router";

const store = useStore();
const router = useRouter();
const route = useRoute();

const provinces = [
  "Alberta",
  "Colombie-Britannique",
  "Île-du-Prince-Édouard",
  "Manitoba",
  "Nouveau-Brunswick",
  "Nouvelle-Écosse",
  "Ontario",
  "Québec",
  "Saskatchewan",
  "Terre-Neuve-et-Labrador",
  "Territoires du Nord-Ouest",
  "Nunavut",
  "Yukon",
];

const normalizeString = (str) =>
  str
    .toLowerCase()
    .normalize("NFD")
    .replace(/[\u0300-\u036f]/g, "");

const normalizedProvinces = computed(() =>
  provinces.map((province) => ({
    original: province,
    normalized: normalizeString(province),
  }))
);

const vendeur = ref({
  id: "",
  nom: "",
  prenom: "",
  courriel: "",
  telephone: "",
  adresse: {
    numeroCivique: "",
    rue: "",
    ville: "",
    province: "",
    pays: "",
    codePostal: "",
  },
});

onMounted(async () => {
  try {
    const vendeurData = await store.dispatch("obtenirVendeur", route.params.id);
    vendeur.value = {
      id: vendeurData.id,
      nom: vendeurData.nom,
      prenom: vendeurData.prenom,
      courriel: vendeurData.courriel,
      telephone: vendeurData.telephone,
      adresse: {
        numeroCivique: vendeurData.adresse.numeroCivique,
        rue: vendeurData.adresse.rue,
        ville: vendeurData.adresse.ville,
        province: vendeurData.adresse.province,
        pays: vendeurData.adresse.pays,
        codePostal: vendeurData.adresse.codePostal,
      },
    };

    // Normaliser et trouver la correspondance
    const normalizedLoadedProvince = normalizeString(
      vendeur.value.adresse.province
    );
    const matchedProvince = normalizedProvinces.value.find(
      (p) => p.normalized === normalizedLoadedProvince
    );
    if (matchedProvince) {
      vendeur.value.adresse.province = matchedProvince.original;
    }
  } catch (error) {
    console.error(
      "Erreur lors de la récupération des données du vendeur:",
      error
    );
  }
});

const message = ref(null);

const submitForm = async () => {
  try {
    const result = await store.dispatch("modifierVendeur", vendeur.value);
    if (result.success) {
      message.value = { type: "success", text: result.message };
      setTimeout(() => {
        router.push("/affichagevendeurs");
      }, 2000);
    } else {
      message.value = { type: "danger", text: result.error };
    }
  } catch (error) {
    console.error("Erreur lors de la modification du vendeur:", error);
    message.value = {
      type: "danger",
      text: "Une erreur est survenue lors de la modification du vendeur.",
    };
  }
};

const resetForm = () => {
  // Réinitialiser le formulaire aux valeurs originales du vendeur
  onMounted();
};
</script>

<style scoped>
.card-registration .select-input.form-control[readonly]:not([disabled]) {
  font-size: 1rem;
  line-height: 2.15;
  padding-left: 0.75em;
  padding-right: 0.75em;
}
.card-registration .select-arrow {
  top: 13px;
}

.form-select {
  appearance: none;
  background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16'%3e%3cpath fill='none' stroke='%23343a40' stroke-linecap='round' stroke-linejoin='round' stroke-width='2' d='M2 5l6 6 6-6'/%3e%3c/svg%3e");
  background-repeat: no-repeat;
  background-position: right 0.75rem center;
  background-size: 16px 12px;
  border: 1px solid #ced4da;
  border-radius: 0.25rem;
  padding: 0.375rem 2.25rem 0.375rem 0.75rem;
}

.form-select:focus {
  border-color: #86b7fe;
  outline: 0;
  box-shadow: 0 0 0 0.25rem rgba(13, 110, 253, 0.25);
}

.btn-custom {
  background-color: #1e3a8a;
  color: white;
}

.btn-custom:hover {
  background-color: #152c69;
  color: white;
}
</style>
