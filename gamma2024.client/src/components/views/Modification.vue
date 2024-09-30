<template>
    <div class="bg-image pt-5 imageDeFondEsquise">
        <div class="container d-flex flex-column justify-content-start align-items-stretch container col-md-6">
            <!-- Ajout de la section avatar -->
            <div class="avatar-section mb-4 d-flex flex-column align-items-center">
                <div class="avatar-container">
                <img :src="avatarUrl" alt="Avatar" class="avatar-image mb-2" @click="triggerFileInput" @error="handleImageError" />                </div>
                <button @click="triggerFileInput" class="btn btn-primary mt-2">Modifier l'avatar</button>
                <input type="file" ref="fileInput" @change="handleFileChange" accept="image/*" style="display: none;" />
            </div>

            <h2 class="fs-1 text-center fw-bold mt-5">Modification des informations</h2>
            <p class="text-center">Modifier vos informations personnelles</p>

            <div v-if="message" :class="['alert', message.type === 'success' ? 'alert-success' : 'alert-danger']">
                {{ message.text }}
            </div>

            <div class="mt-3">
                <form @submit.prevent="submitForm">
                    <!-- Informations générales -->
                    <div class="d-flex px-4 py-2 flex-column justify-content-start align-items-stretch .i">
                        <div class="d-flex flex-row justify-content-between mb-3 i">
                            <div class="form-group">
                                <label for="nom" class="ms-3">Nom</label>
                                <input type="text"
                                       v-model="formData.generalInfo.nom"
                                       class="form-control"
                                       id="nom"
                                       @blur="v.generalInfo.nom.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.generalInfo.nom.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                            <div class="form-group">
                                <label for="prenom" class="ms-3">Prénom</label>
                                <input type="text"
                                       v-model="formData.generalInfo.prenom"
                                       class="form-control"
                                       id="prenom"
                                       @blur="v.generalInfo.prenom.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.generalInfo.prenom.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="courriel" class="ms-3">Courriel</label>
                                <input type="email"
                                       v-model="formData.generalInfo.courriel"
                                       class="form-control"
                                       id="courriel"
                                       @blur="v.generalInfo.courriel.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.generalInfo.courriel.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                            <div class="form-group">
                                <label for="telephone" class="ms-3">Téléphone</label>
                                <input type="tel"
                                       v-model="formData.generalInfo.telephone"
                                       class="form-control"
                                       id="telephone"
                                       @blur="v.generalInfo.telephone.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.generalInfo.telephone.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="pseudonyme" class="ms-3">Pseudonyme</label>
                                <input type="text"
                                       v-model="formData.generalInfo.pseudo"
                                       class="form-control"
                                       id="pseudonyme"
                                       @blur="v.generalInfo.pseudo.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.generalInfo.pseudo.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>
                    </div>

                    <!-- Informations de carte de crédit -->
                    <div class="d-flex px-4 py-2 flex-column justify-content-start align-items-stretch .i">
                        <div class="d-flex flex-row justify-content-between mb-3 i">
                            <div class="form-group">
                                <label for="nomPropio" class="ms-3">Nom du propriétaire de la carte</label>
                                <input type="text"
                                       v-model="formData.carteCredit.nomProprio"
                                       class="form-control"
                                       id="nomPropio"
                                       @blur="v.carteCredit.nomProprio.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.carteCredit.nomProprio.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                            <div class="form-group">
                                <label for="numeroCarte" class="ms-3">Numéro de la carte</label>
                                <input type="text"
                                       v-model="formData.carteCredit.numeroCarte"
                                       class="form-control"
                                       id="numeroCarte"
                                       @blur="v.carteCredit.numeroCarte.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.carteCredit.numeroCarte.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="dateExpiration" class="ms-3">Date d'expiration</label>
                                <input type="text"
                                       v-model="formData.carteCredit.dateExpiration"
                                       class="form-control"
                                       id="dateExpiration"
                                       @blur="v.carteCredit.dateExpiration.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.carteCredit.dateExpiration.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>
                    </div>

                    <!-- Informations d'adresse -->
                    <div class="d-flex px-4 py-2 flex-column justify-content-start align-items-stretch .i">
                        <div class="d-flex flex-row justify-content-between mb-3 i">
                            <div class="form-group">
                                <label for="numeroCivique" class="ms-3">Numéro Civique</label>
                                <input type="text"
                                       v-model="formData.adresse.numeroCivique"
                                       class="form-control"
                                       id="numeroCivique"
                                       @blur="v.adresse.numeroCivique.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.adresse.numeroCivique.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                            <div class="form-group">
                                <label for="rue" class="ms-3">Rue</label>
                                <input type="text"
                                       v-model="formData.adresse.rue"
                                       class="form-control"
                                       id="rue"
                                       @blur="v.adresse.rue.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.adresse.rue.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="appartement" class="ms-3">Appartement/Bureau</label>
                                <input type="text"
                                       v-model="formData.adresse.appartement"
                                       class="form-control"
                                       id="appartement" />
                            </div>
                            <div class="form-group">
                                <label for="ville" class="ms-3">Ville</label>
                                <input type="text"
                                       v-model="formData.adresse.ville"
                                       class="form-control"
                                       id="ville"
                                       @blur="v.adresse.ville.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.adresse.ville.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="province" class="ms-3">Province</label>
                                <select id="province"
                                        v-model="formData.adresse.province"
                                        class="form-control"
                                        @blur="v.adresse.province.$touch()">
                                    <option value="" disabled selected>Sélectionner une province</option>
                                    <option value="AB">Alberta</option>
                                    <option value="BC">Colombie-Britannique</option>
                                    <option value="MB">Manitoba</option>
                                    <option value="NB">Nouveau-Brunswick</option>
                                    <option value="NL">Terre-Neuve-et-Labrador</option>
                                    <option value="NS">Nouvelle-Écosse</option>
                                    <option value="ON">Ontario</option>
                                    <option value="PE">Île-du-Prince-Édouard</option>
                                    <option value="QC">Québec</option>
                                    <option value="SK">Saskatchewan</option>
                                    <option value="NT">Territoires du Nord-Ouest</option>
                                    <option value="NU">Nunavut</option>
                                    <option value="YT">Yukon</option>
                                </select>
                                <span class="text-danger"
                                      v-for="error in v.adresse.province.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                            <div class="form-group">
                                <label for="pays" class="ms-3">Pays</label>
                                <input type="text"
                                       v-model="formData.adresse.pays"
                                       class="form-control"
                                       id="pays" />
                            </div>
                        </div>

                        <div class="d-flex flex-row justify-content-between mb-3">
                            <div class="form-group">
                                <label for="postalCode" class="ms-3">Code Postal</label>
                                <input type="text"
                                       v-model="formData.adresse.codePostal"
                                       class="form-control"
                                       id="postalCode"
                                       @blur="v.adresse.codePostal.$touch()" />
                                <span class="text-danger"
                                      v-for="error in v.adresse.codePostal.$errors"
                                      :key="error.$uid">
                                    {{ error.$message }}
                                </span>
                            </div>
                        </div>
                    </div>

                    <div class="d-flex justify-content-end gap-2 py-3">
                        <button type="submit"
                                :class="[
                                !isFormValid ? 'bleuNonValide' : 'bleuValide',
                                'btn rounded-pill px-5 me-4',
                                ]"
                                :disabled="!isFormValid">
                        Enregistrer
                        </button>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script setup>
import { ref, computed, reactive, onMounted, watch } from "vue";
import { useStore } from "vuex";
import { useVuelidate } from "@vuelidate/core";
import { required, email, helpers } from "@vuelidate/validators";
import api from '@/services/api';
import { useRouter } from 'vue-router'; 


const store = useStore();
const activeIndex = ref(1);
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
        if (store.state.user.photo.startsWith('http')) {
            return store.state.user.photo;
        } else {
            return `${api.defaults.baseURL.replace('/api', '')}${store.state.user.photo}`;
        }
    }
    return '/icons/Avatar.png';
});

// objet qui contient tous les champs remplis correctement
let formData = reactive({
    generalInfo: {
        nom: "",
        prenom: "",
        courriel: "",
        telephone: "",
        pseudo: "",
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
    }
});

// objet rules qui contient toutes les validations
let rules = computed(() => {
    return {
        generalInfo: {
            nom: { required: messageRequis },
            prenom: { required: messageRequis },
            courriel: { required: messageRequis, email: messageCourriel },
            telephone: { required: messageRequis },
            pseudo: { required: messageRequis },
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
        }
    };
});

const v = useVuelidate(rules, formData);
const info = useVuelidate(rules.value.generalInfo, formData.generalInfo);
const carte = useVuelidate(rules.value.carteCredit, formData.carteCredit);
const adresse = useVuelidate(rules.value.adresse, formData.adresse);

const state1 = computed(() => {
    return info.value.$invalid;
});

const state2 = computed(() => {
    return carte.value.$invalid;
});

const state3 = computed(() => {
    return adresse.value.$invalid;
});

const errorMessage = ref("");

const isLoading = ref(true);

onMounted(async () => {
  try {
    isLoading.value = true;
    const response = await store.dispatch("fetchClientInfo");
    
    // Mise à jour des données du formulaire
    formData.generalInfo = {
      nom: response.data.name || '',
      prenom: response.data.firstName || '',
      courriel: response.data.email || '',
      telephone: response.data.phoneNumber || '',
      pseudo: response.data.pseudonym || '',
    };

    formData.carteCredit = {
      nomProprio: response.data.cardOwnerName || '',
      numeroCarte: response.data.cardNumber || '',
      dateExpiration: response.data.cardExpiryDate || '',
    };

    formData.adresse = {
      numeroCivique: response.data.civicNumber || '',
      rue: response.data.street || '',
      appartement: response.data.apartment || '',
      ville: response.data.city || '',
      province: mapProvince(response.data.province) || '',
      pays: response.data.country || 'Canada',
      codePostal: response.data.postalCode || '',
    };

    // Utiliser l'URL de base de l'API pour construire l'URL de l'avatar
    avatarUrl.value = response.data.photo ? `${api.defaults.baseURL.replace('/api', '')}${response.data.photo}` : '/icons/Avatar.png';
    console("objet crée pour test" + response.data);
  } catch (error) {
    console.error("Erreur lors de la récupération des données:", error);
    errorMessage.value = "Erreur lors de la récupération des informations du client.";
    avatarUrl.value = '/icons/Avatar.png';
  } finally {
    isLoading.value = false;
  }
});


const mapProvince = (province) => {
  const provinceMap = {
    'Québec': 'QC',
    'Ontario': 'ON',
    'Alberta': 'AB',
    'Colombie-Britannique': 'BC',
    'Manitoba': 'MB',
    'Nouveau-Brunswick': 'NB',
    'Terre-Neuve-et-Labrador': 'NL',
    'Nouvelle-Écosse': 'NS',
    'Île-du-Prince-Édouard': 'PE',
    'Saskatchewan': 'SK',
    'Territoires du Nord-Ouest': 'NT',
    'Nunavut': 'NU',
    'Yukon': 'YT'
  };
  return provinceMap[province] || province;
};
// Observateur pour vérifier les changements dans formData
watch(formData, (newVal) => {
  console.log("formData mis à jour:", newVal);
}, { deep: true });

const triggerFileInput = () => {
  fileInput.value.click();
};

const handleImageError = () => {
  console.error("Erreur de chargement de l'image:", avatarUrl.value);
  setTimeout(() => {
    if (!avatarUrl.value.includes('Avatar.png')) {
      avatarUrl.value = '/icons/Avatar.png';
    }
  }, 1000); // Attendre 1 seconde avant de remplacer par l'image par défaut
};

    const handleFileChange = async (event) => {
    const file = event.target.files[0];
    if (file) {
        try {
            const formData = new FormData();
            formData.append('avatar', file);
            const response = await store.dispatch("updateAvatar", formData);
            console.log("Réponse du serveur pour l'avatar:", response);
            
            // Utiliser l'URL complète retournée par l'action du store
            avatarUrl.value = response.data.avatarUrl;
            
            console.log("Nouvelle URL de l'avatar:", avatarUrl.value);
            alert("Avatar mis à jour avec succès !");
        } catch (error) {
            console.error("Erreur lors de la mise à jour de l'avatar:", error);
            errorMessage.value = "Erreur lors de la mise à jour de l'avatar: " + (error.response?.data?.message || error.message);
        }
    }
};


    const isFormValid = computed(() => {
    return !v.value.$invalid && formDataChanged.value;
    });

    const formDataChanged = ref(false);

    watch(formData, () => {
    formDataChanged.value = true;
    }, { deep: true });

    watch(() => store.state.user?.photo, (newPhotoUrl) => {
        if (newPhotoUrl) {
            avatarUrl.value = newPhotoUrl;
        }
    });

    const submitForm = async () => {
    const result = await v.value.$validate();
    if (result) {
        try {
            const mappedData = {
                Name: formData.generalInfo.nom,
                FirstName: formData.generalInfo.prenom,
                Email: formData.generalInfo.courriel,
                PhoneNumber: formData.generalInfo.telephone,
                Pseudonym: formData.generalInfo.pseudo,
                CardOwnerName: formData.carteCredit.nomProprio,
                CardNumber: formData.carteCredit.numeroCarte,
                CardExpiryDate: formData.carteCredit.dateExpiration,
                CivicNumber: formData.adresse.numeroCivique,
                Street: formData.adresse.rue,
                Apartment: formData.adresse.appartement,
                City: formData.adresse.ville,
                Province: formData.adresse.province,
                Country: formData.adresse.pays,
                PostalCode: formData.adresse.codePostal,
                Photo: avatarUrl.value
            };

            await store.dispatch("updateClientInfo", mappedData);
            message.value = { type: 'success', text: "Informations mises à jour avec succès !" };

            await store.dispatch("fetchClientInfo");

            setTimeout(() => {
                router.push('/'); 
            }, 2000);
        } catch (error) {
            console.error("Erreur lors de la mise à jour des informations:", error);
            message.value = { type: 'error', text: "Erreur lors de la mise à jour des informations: " + (error.response?.data?.message || error.message) };
        }
    } else {
        message.value = { type: 'error', text: "Formulaire non valide. Veuillez vérifier les champs." };
    }
};
</script>

<style scoped>
.information .form-group {
    width: 45%;
}

label {
    font-weight: bolder;
}

.is-invalid,
span {
    border-color: red;
    font-weight: 600;
}

.cadreBlanc {
    background-color: white;
    opacity: 0.4;
    border-radius: 15px;
    height: auto;
}

.bleuNonValide {
    background-color: #052445;
    color: white;
}

.bleuValide {
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

.imageDeFondEsquise {
    position: relative;
}

.imageDeFondEsquise::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-size: cover;
    background-position: center;
    opacity: 0.5;
    z-index: 1;
}

.container {
    position: relative;
    z-index: 2;
}

@media only screen and (max-width: 1000px) {
    .i div {
        display: flex;
        flex-basis: 100%;
        flex-direction: column;
    }
}
</style>