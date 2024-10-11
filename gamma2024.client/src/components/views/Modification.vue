<template>
  <div class="pt-5 container">
    <div
      class="container d-flex flex-column justify-content-start align-items-stretch card p-5 mb-5"
    >
      <div class="card-header">
        <!-- Ajout de la section avatar -->
        <div class="avatar-section mb-4 d-flex flex-column align-items-center">
          <div class="avatar-container">
            <img
              :src="avatarUrl"
              alt="Avatar"
              class="avatar-image mb-2"
              @click="triggerFileInput"
              @error="handleImageError"
            />
          </div>
          <button @click="triggerFileInput" class="btn btn-primary mt-3">
            Modifier l'avatar
          </button>
          <input
            type="file"
            ref="fileInput"
            @change="handleFileChange"
            accept="image/*"
            style="display: none"
          />
        </div>

        <h2 class="fs-1 text-center fw-bold mt-5">Profil d'utilisateur</h2>

        <div
          v-if="message"
          :class="[
            'alert',
            message.type === 'success' ? 'alert-success' : 'alert-danger',
          ]"
        >
          {{ message.text }}
        </div>
      </div>

      <div class="mt-3 card-body">
        <form @submit.prevent="submitForm">
          <!-- Informations générales -->
          <div class="card mb-5">
            <div class="card-header">Informations générales</div>
            <div class="card-body">
              <div class="row mt-3">
                <div class="col-md-6">
                  <label for="nom">Nom</label>
                  <input
                    type="text"
                    id="nom"
                    :class="[
                      'form-control',
                      { 'is-invalid': v.generalInfo.nom.$error },
                    ]"
                    v-model="formData.generalInfo.nom"
                    @blur="v.generalInfo.nom.$touch()"
                  />
                  <div class="invalid-feedback" v-if="v.generalInfo.nom.$error">
                    {{ v.generalInfo.nom.$errors[0].$message }}
                  </div>
                </div>
                <div class="col-md-6">
                  <label for="prenom">Prénom</label>
                  <input
                    type="text"
                    id="prenom"
                    :class="[
                      'form-control',
                      { 'is-invalid': v.generalInfo.prenom.$error },
                    ]"
                    v-model="formData.generalInfo.prenom"
                    @blur="v.generalInfo.prenom.$touch()"
                  />
                  <div
                    class="invalid-feedback"
                    v-if="v.generalInfo.prenom.$error"
                  >
                    {{ v.generalInfo.prenom.$errors[0].$message }}
                  </div>
                </div>
              </div>
              <div class="row mt-3">
                <div class="col-md-6">
                  <label for="pseudonyme">Pseudonyme</label>
                  <input
                    type="text"
                    id="pseudonyme"
                    :class="[
                      'form-control',
                      { 'is-invalid': v.generalInfo.pseudo.$error },
                    ]"
                    v-model="formData.generalInfo.pseudo"
                    @blur="v.generalInfo.pseudo.$touch()"
                  />
                </div>
                <div class="col-md-6">
                  <label for="telephone">Téléphone</label>
                  <div class="input-wrapper">
                    <InputMask
                      type="tel"
                      mask="(999) 999-9999"
                      :class="[
                        'form-control',
                        { 'is-invalid': v.generalInfo.telephone.$error },
                      ]"
                      id="telephone"
                      v-model="formData.generalInfo.telephone"
                      @blur="v.generalInfo.telephone.$touch()"
                    />
                    <div
                      class="invalid-feedback"
                      v-if="v.generalInfo.telephone.$error"
                    >
                      {{ v.generalInfo.telephone.$errors[0].$message }}
                    </div>
                  </div>
                </div>
              </div>
              <div class="row mt-3">
                <div class="col-md-6">
                  <label class="labels">Courriel</label>
                  <input
                    type="text"
                    class="form-control"
                    v-model="formData.generalInfo.courriel"
                  />
                </div>
                <div class="d-flex flex-column col-md-6">
                  <label class="labels">Mot de passe</label>
                  <button
                    type="button"
                    class="btn border border-1 btn_change_password"
                    @click="changerLaVisibilite"
                  >
                    <img
                      src="/icons/icon_password.png"
                      class="img-fluid img_password"
                      alt="icon_password"
                    />
                    <span class="ms-2">Changer le mot de passe</span>
                  </button>
                </div>
              </div>
              <div
                class="d-flex flex-column col-md-12 mt-3"
                v-if="changeVisible"
              >
              <div class="form-group">
      <label for="currentPassword">Mot de passe actuel</label>
      <input
        type="password"
        v-model="formData.generalInfo.currentPassword"
        id="currentPassword"
        class="form-control"
        @input="handleCurrentPasswordInput"
      />
                  </div>
                  <div v-if="showNewPasswordFields" class="form-group mt-3">
                    <label for="newPassword">Nouveau mot de passe</label>
                    <input
                      type="password"
                      v-model="formData.generalInfo.newPassword"
                      id="newPassword"
                      class="form-control"
                    />
                  </div>
                  <div v-if="showNewPasswordFields" class="form-group mt-3">
                    <label for="confirmNewPassword">Confirmer le nouveau mot de passe</label>
                    <input
                      type="password"
                      v-model="formData.generalInfo.confirmNewPassword"
                      id="confirmNewPassword"
                      class="form-control"
                    />
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Informations de carte de crédit -->
          <div class="card my-5">
            <div class="card-header">Carte de crédit</div>
            <div class="card-body">
              <div class="row mt-3">
                <div class="col-md-12">
                  <label for="nomPropio">Nom du propriétaire de la carte</label>
                  <input
                    type="text"
                    v-model="formData.carteCredit.nomProprio"
                    id="nomPropio"
                    @blur="v.carteCredit.nomProprio.$touch()"
                    :class="[
                      'form-control',
                      { 'is-invalid': v.carteCredit.nomProprio.$error },
                    ]"
                  />
                  <div
                    class="invalid-feedback"
                    v-if="v.carteCredit.nomProprio.$error"
                  >
                    {{ v.carteCredit.nomProprio.$errors[0].$message }}
                  </div>
                </div>
              </div>
              <div class="row mt-3">
                <div class="col-md-6">
                  <label for="numeroCarte">Numéro de la carte</label>
                  <div class="input-wrapper">
                    <InputMask
                      type="text"
                      mask="9999 9999 9999 9999"
                      :class="[
                        'form-control',
                        { 'is-invalid': v.carteCredit.numeroCarte.$error },
                      ]"
                      id="numeroCarte"
                      v-model="formData.carteCredit.numeroCarte"
                      @blur="v.carteCredit.numeroCarte.$touch()"
                    />
                  </div>
                  <div
                    class="invalid-feedback"
                    v-if="v.carteCredit.numeroCarte.$error"
                  >
                    {{ v.carteCredit.numeroCarte.$errors[0].$message }}
                  </div>
                </div>
                <div class="col-md-6">
                  <label for="dateExpiration">Date d'expiration</label>
                  <div class="input-wrapper">
                    <InputMask
                      type="text"
                      mask="99/99"
                      :class="[
                        'form-control',
                        {
                          'is-invalid': v.carteCredit.dateExpiration.$error,
                        },
                      ]"
                      placeholder="MM/YY"
                      id="dateExpiration"
                      @blur="v.carteCredit.dateExpiration.$touch()"
                      v-model="formData.carteCredit.dateExpiration"
                    />
                    <div
                      class="invalid-feedback"
                      v-if="v.carteCredit.dateExpiration.$error"
                    >
                      {{ v.carteCredit.dateExpiration.$errors[0].$message }}
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- Informations d'adresse -->
          <div class="card my-5">
            <div class="card-header">Adresse</div>
            <div class="card-body">
              <div class="row mt-3">
                <div class="col-md-6">
                  <label for="numeroCivique">Numéro civique</label>
                  <input
                    type="text"
                    v-model="formData.adresse.numeroCivique"
                    :class="[
                      'form-control',
                      { 'is-invalid': v.adresse.numeroCivique.$error },
                    ]"
                    id="numeroCivique"
                    @blur="v.adresse.numeroCivique.$touch()"
                  />
                  <div
                    class="invalid-feedback"
                    v-if="v.adresse.numeroCivique.$error"
                  >
                    {{ v.adresse.numeroCivique.$errors[0].$message }}
                  </div>
                </div>
                <div class="col-md-6">
                  <label for="rue">Rue</label>
                  <input
                    type="text"
                    v-model="formData.adresse.rue"
                    :class="[
                      'form-control',
                      { 'is-invalid': v.adresse.rue.$error },
                    ]"
                    id="rue"
                    @blur="v.adresse.rue.$touch()"
                  />
                  <div class="invalid-feedback" v-if="v.adresse.rue.$error">
                    {{ v.adresse.rue.$errors[0].$message }}
                  </div>
                </div>
              </div>
              <div class="row mt-3">
                <div class="col-md-6">
                  <label for="appartement">Appartement/bureau</label>
                  <input
                    type="text"
                    class="form-control"
                    id="appartement"
                    v-model="formData.adresse.appartement"
                  />
                </div>
                <div class="col-md-6">
                  <label for="ville">Ville</label>
                  <input
                    type="text"
                    class="form-control"
                    v-model="formData.adresse.ville"
                    :class="[
                      'form-control',
                      { 'is-invalid': v.adresse.ville.$error },
                    ]"
                    id="ville"
                    @blur="v.adresse.ville.$touch()"
                  />
                  <div class="invalid-feedback" v-if="v.adresse.ville.$error">
                    {{ v.adresse.ville.$errors[0].$message }}
                  </div>
                </div>
              </div>
              <div class="row mt-3">
                <div class="col-md-6">
                  <label for="province">Province</label>
                  <select
                    id="province"
                    name="province"
                    v-model="formData.adresse.province"
                    :class="[
                      'form-control',
                      { 'is-invalid': v.adresse.province.$error },
                    ]"
                    @blur="v.adresse.province.$touch()"
                  >
                    <option value="" disabled selected>
                      Sélectionner une province
                    </option>
                    <option
                      v-for="province in provinces"
                      :key="province.value"
                      :value="province.value"
                    >
                      {{ province.text }}
                    </option>
                  </select>
                </div>
                <div class="col-md-6">
                  <label class="labels">Pays</label>
                  <input
                    type="text"
                    class="form-control"
                    v-model="formData.adresse.pays"
                  />
                </div>
              </div>
              <div class="row mt-3">
                <div class="col-md-12">
                  <label class="labels">Code postal</label>
                  <div class="input-wrapper">
                    <inputMask
                      type="text"
                      id="postalCode"
                      :class="[
                        'form-control',
                        { 'is-invalid': v.adresse.codePostal.$error },
                      ]"
                      mask="a9a 9a9"
                      v-model="formData.adresse.codePostal"
                      @blur="v.adresse.codePostal.$touch()"
                    />
                  </div>
                  <div
                    class="invalid-feedback"
                    v-if="v.adresse.codePostal.$error"
                  >
                    {{ v.adresse.codePostal.$errors[0].$message }}
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="d-flex justify-content-end gap-2 py-3 card-footer">
            <button
              class="btn btn-secondary rounded-pill px-4 me-4"
              :disabled="formDataChanged"
              type="button"
            >
              {{ formDataChanged }}
              Annuler
            </button>
            <button
              type="submit"
              :class="[
                isFormValid && formDataChanged ? 'bleuValide' : 'bleuNonValide',
                'btn rounded-pill px-4 me-4',
              ]"
              :disabled="!isFormValid || !isFormEdited"
            >
              Enregistrer
            </button>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch } from "vue";
import { useStore } from "vuex";
import { useVuelidate } from "@vuelidate/core";
import InputMask from "primevue/inputmask";
import { required, email, helpers } from "@vuelidate/validators";
import api from "@/services/api";
import { useRouter } from "vue-router";

const store = useStore();
const message = ref(null);
const fileInput = ref(null);
const messageRequis = helpers.withMessage("Ce champ est requis", required);
const messageCourriel = helpers.withMessage(
  "Veuillez entrer un courriel valide",
  email
);
const router = useRouter();

const avatarUrl = computed(() => {
  if (store.state.user && store.state.user.photo) {
    if (store.state.user.photo.startsWith("http")) {
      return store.state.user.photo;
    } else {
      return `${api.defaults.baseURL.replace("/api", "")}${
        store.state.user.photo
      }`;
    }
  }
  return "/icons/Avatar.png";
});

const provinces = ref([
  { text: "Alberta", value: "AB" },
  { text: "Colombie-Britannique", value: "BC" },
  { text: "Manitoba", value: "MB" },
  { text: "Nouveau-Brunswick", value: "NB" },
  { text: "Terre-Neuve-et-Labrador", value: "NL" },
  { text: "Nouvelle-Écosse", value: "NS" },
  { text: "Ontario", value: "ON" },
  { text: "Île-du-Prince-Édouard", value: "PE" },
  { text: "Québec", value: "QC" },
  { text: "Saskatchewan", value: "SK" },
  { text: "Territoires du Nord-Ouest", value: "NT" },
  { text: "Nunavut", value: "NU" },
  { text: "Yukon", value: "YT" },
]);

let changeVisible = ref(false);
let isFormEdited = computed(() => {
  return formDataChanged.value;
});
let changerLaVisibilite = () => {
  changeVisible.value = !changeVisible.value;
};

let formDataChanged = ref(true);
// objet qui contient tous les champs remplis correctement
let formData = ref({
  generalInfo: {
    nom: "",
    prenom: "",
    courriel: "",
    telephone: "",
    pseudo: "",
    currentPassword: "",
    newPassword: "",
    confirmNewPassword: "",
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

const initialFormData = ref(null);
initialFormData.value = JSON.parse(JSON.stringify(formData.value));

// objet rules qui contient toutes les validations
let rules = computed(() => {
  return {
    generalInfo: {
      nom: { required: messageRequis },
      prenom: { required: messageRequis },
      courriel: { required: messageRequis, email: messageCourriel },
      telephone: { required: messageRequis },
      pseudo: { required: messageRequis },
      currentPassword: {
        required: helpers.withMessage(
          "Le mot de passe actuel est requis si vous souhaitez le changer",
          (value) => !formData.value.generalInfo.newPassword || !!value
        ),
      },
      newPassword: {
        minLength: helpers.withMessage(
          "Le mot de passe doit contenir au moins 8 caractères",
          (value) => !value || value.length >= 8
        ),
      },
      confirmNewPassword: {
        sameAsPassword: helpers.withMessage(
          "Les mots de passe ne correspondent pas",
          (value) =>
            !formData.value.generalInfo.newPassword ||
            value === formData.value.generalInfo.newPassword
        ),
      },
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

const errorMessage = ref("");
const isLoading = ref(true);

onMounted(async () => {
  try {
    isLoading.value = true;
    const response = await store.dispatch("fetchClientInfo");

    // Mise à jour des données du formulaire
    formData.value.generalInfo = {
      nom: response.data.name || "",
      prenom: response.data.firstName || "",
      courriel: response.data.email || "",
      telephone: response.data.phoneNumber || "",
      pseudo: response.data.pseudonym || "",
    };

    formData.value.carteCredit = {
      nomProprio: response.data.cardOwnerName || "",
      numeroCarte: response.data.cardNumber || "",
      dateExpiration: response.data.cardExpiryDate || "",
    };

    formData.value.adresse = {
      numeroCivique: response.data.civicNumber || "",
      rue: response.data.street || "",
      appartement: response.data.apartment || "",
      ville: response.data.city || "",
      province: mapProvince(response.data.province) || "",
      pays: response.data.country || "Canada",
      codePostal: response.data.postalCode || "",
    };

    // Utiliser l'URL de base de l'API pour construire l'URL de l'avatar
    avatarUrl.value = response.data.photo
      ? `${api.defaults.baseURL.replace("/api", "")}${response.data.photo}`
      : "/icons/Avatar.png";
    console.log("objet crée pour test" + response.data);
  } catch (error) {
    console.error("Erreur lors de la récupération des données:", error);
    errorMessage.value =
      "Erreur lors de la récupération des informations du client.";
    avatarUrl.value = "/icons/Avatar.png";
  } finally {
    isLoading.value = false;
  }
});

const mapProvince = (province) => {
  const provinceMap = {
    Québec: "QC",
    Ontario: "ON",
    Alberta: "AB",
    "Colombie-Britannique": "BC",
    Manitoba: "MB",
    "Nouveau-Brunswick": "NB",
    "Terre-Neuve-et-Labrador": "NL",
    "Nouvelle-Écosse": "NS",
    "Île-du-Prince-Édouard": "PE",
    Saskatchewan: "SK",
    "Territoires du Nord-Ouest": "NT",
    Nunavut: "NU",
    Yukon: "YT",
  };
  return provinceMap[province] || province;
};

watch(
  formData,
  (newVal) => {
    // Comparaison avec l'état initial
    console.log("Initial:", JSON.stringify(initialFormData.value)); // Affiche l'état initial du formulaire
    console.log("newVal:", JSON.stringify(newVal)); // Affiche la nouvelle valeur

    // Comparaison entre l'état initial et l'état actuel
    if (JSON.stringify(initialFormData.value) !== JSON.stringify(newVal)) {
      formDataChanged.value = true; // Indique que le formulaire a changé
    } else {
      formDataChanged.value = false; // Pas de changement
    }

    console.log("Formulaire changé:", formDataChanged.value);
  },
  { deep: true }
);

const triggerFileInput = () => {
  fileInput.value.click();
};

const handleImageError = () => {
  console.error("Erreur de chargement de l'image:", avatarUrl.value);
  setTimeout(() => {
    if (!avatarUrl.value.includes("Avatar.png")) {
      avatarUrl.value = "/icons/Avatar.png";
    }
  }, 1000); // Attendre 1 seconde avant de remplacer par l'image par défaut
};

const handleFileChange = async (event) => {
  const file = event.target.files[0];
  if (file) {
    try {
      const formData = new FormData();
      formData.append("avatar", file);
      const response = await store.dispatch("updateAvatar", formData);
      console.log("Réponse du serveur pour l'avatar:", response);

      // Utiliser l'URL complète retournée par l'action du store
      avatarUrl.value = response.data.avatarUrl;

      console.log("Nouvelle URL de l'avatar:", avatarUrl.value);
    } catch (error) {
      console.error("Erreur lors de la mise à jour de l'avatar:", error);
      errorMessage.value =
        "Erreur lors de la mise à jour de l'avatar: " +
        (error.response?.data?.message || error.message);
    }
  }
};

const isFormValid = computed(() => {
  const passwordFieldsEmpty =
    !formData.value.generalInfo.currentPassword &&
    !formData.value.generalInfo.newPassword &&
    !formData.value.generalInfo.confirmNewPassword;

  const otherFieldsValid =
    !v.value.generalInfo.nom.$invalid &&
    !v.value.generalInfo.prenom.$invalid &&
    !v.value.generalInfo.courriel.$invalid &&
    !v.value.generalInfo.telephone.$invalid &&
    !v.value.generalInfo.pseudo.$invalid &&
    !v.value.carteCredit.$invalid &&
    !v.value.adresse.$invalid;

  const passwordFieldsValid =
    passwordFieldsEmpty ||
    (!v.value.generalInfo.currentPassword.$invalid &&
      !v.value.generalInfo.newPassword.$invalid &&
      !v.value.generalInfo.confirmNewPassword.$invalid);

  return otherFieldsValid && passwordFieldsValid && formDataChanged.value;
});

// watch(
//   () => store.state.user?.photo,
//   (newPhotoUrl) => {
//     if (newPhotoUrl) {
//       avatarUrl.value = newPhotoUrl;
//       formDataChanged.value = false;
//     }
//   }
// );
const showNewPasswordFields = ref(false);

const handleCurrentPasswordInput = () => {
  showNewPasswordFields.value = formData.value.generalInfo.currentPassword.length > 0;
};

const submitForm = async () => {
  const result = await v.value.$validate();
  if (result) {
    try {
      const mappedData = {
        Name: formData.value.generalInfo.nom,
        FirstName: formData.value.generalInfo.prenom,
        Email: formData.value.generalInfo.courriel,
        PhoneNumber: formData.value.generalInfo.telephone,
        Pseudonym: formData.value.generalInfo.pseudo,
        CurrentPassword:
          formData.value.generalInfo.currentPassword || undefined,
        NewPassword: formData.value.generalInfo.newPassword || undefined,
        ConfirmNewPassword:
          formData.value.generalInfo.confirmNewPassword || undefined,
        CardOwnerName: formData.value.carteCredit.nomProprio,
        CardNumber: formData.value.carteCredit.numeroCarte,
        CardExpiryDate: formData.value.carteCredit.dateExpiration,
        CivicNumber: formData.value.adresse.numeroCivique,
        Street: formData.value.adresse.rue,
        Apartment: formData.value.adresse.appartement,
        City: formData.value.adresse.ville,
        Province: formData.value.adresse.province,
        Country: formData.value.adresse.pays,
        PostalCode: formData.value.adresse.codePostal,
        Photo: avatarUrl.value,
      };

      // Ne pas envoyer les champs de mot de passe s'ils sont tous vides
      if (
        !mappedData.CurrentPassword &&
        !mappedData.NewPassword &&
        !mappedData.ConfirmNewPassword
      ) {
        delete mappedData.CurrentPassword;
        delete mappedData.NewPassword;
        delete mappedData.ConfirmNewPassword;
      }

      await store.dispatch("updateClientInfo", mappedData);
      message.value = {
        type: "success",
        text: "Informations mises à jour avec succès !",
      };

      // Réinitialiser les champs de mot de passe
      formData.value.generalInfo.currentPassword = "";
      formData.value.generalInfo.newPassword = "";
      formData.value.generalInfo.confirmNewPassword = "";

      await store.dispatch("fetchClientInfo");

      setTimeout(() => {
        router.push("/");
      }, 2000);
    } catch (error) {
      console.error("Erreur lors de la mise à jour des informations:", error);
      message.value = {
        type: "error",
        text:
          "Erreur lors de la mise à jour des informations: " +
          (error.response?.data?.message || error.message),
      };
    }
  } else {
    message.value = {
      type: "error",
      text: "Formulaire non valide. Veuillez vérifier les champs.",
    };
  }
};

watch(
  () => formData.value.generalInfo.currentPassword,
  (currentPassword) => {
    if (!currentPassword) {
      // Réinitialiser tous les champs de mot de passe et leurs validations
      formData.value.generalInfo.newPassword = '';
      formData.value.generalInfo.confirmNewPassword = '';
      v.value.generalInfo.currentPassword.$reset();
      v.value.generalInfo.newPassword.$reset();
      v.value.generalInfo.confirmNewPassword.$reset();
    } else {
      // Toucher seulement la validation du mot de passe actuel
      v.value.generalInfo.currentPassword.$touch();
    }
  }
);

// Ajouter un nouveau watch pour les nouveaux champs de mot de passe
watch(
  [
    () => formData.value.generalInfo.newPassword,
    () => formData.value.generalInfo.confirmNewPassword
  ],
  ([newPassword, confirmNewPassword]) => {
    if (newPassword || confirmNewPassword) {
      v.value.generalInfo.newPassword.$touch();
      v.value.generalInfo.confirmNewPassword.$touch();
    }
  }
);
</script>

<style scoped>
.is-invalid {
  border-color: red;
  font-weight: 600;
}

.cadreBlanc {
  background-color: white;
  opacity: 0.4;
  border-radius: 15px;
  height: auto;
}

.bleuValide {
  background-color: #052445;
  color: white;
}

.bleuNonValide {
  background-color: #5a708a;
  color: white;
}

.avatar-section {
  text-align: center;
  margin-bottom: 20px;
  position: relative;
  z-index: 10;
}

.avatar-container {
  width: 100px;
  height: 100px;
  overflow: hidden;
  border-radius: 50%;
  margin: 0 auto 10px;
}

.avatar-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
  cursor: pointer;
}

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

.img_password {
  width: 15px;
  height: 17px;
}

.btn_change_password:hover {
  background-color: rgba(217, 217, 217, 0.273);
}
</style>
