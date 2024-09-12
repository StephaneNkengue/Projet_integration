<template>
    <div class="container d-flex flex-column justify-content-start align-items-stretch vh-100 container col-md-6">
        <h2 class="fs-1 text-center fw-bold mt-5">Inscription</h2>
        <p class="text-center">Obtenir un compte membre</p>

        <!-- Stepper Navigation -->
        <div class="mt-3">

            <Stepper v-model:activeIndex="activeIndex" :class="['mb-4']">

                <!-- Informations générales -->
                <StepItem value="1">
                    <Step class="border-top border-2 p-2 border-dark fs-2"> - Informations générales</Step>
                    <StepPanel v-slot="{ activateCallback }">

                        <div class="d-flex flex-column justify-content-start align-items-stretch">
                            <div class="px-4 py-2 mb-4">
                                <div class="d-flex flex-row justify-content-between mb-3">
                                    <div class="form-group" style="width: 45%;">
                                        <label for="nom" class="ms-3">Nom</label>
                                        <input type="text" class="form-control" id="nom" />
                                    </div>
                                    <div class="form-group" style="width: 45%;">
                                        <label for="prenom" class="ms-3">Prénom</label>
                                        <input type="text" class="form-control" id="prenom" />
                                    </div>
                                </div>

                                <div class="d-flex flex-row justify-content-between mb-3">
                                    <div class="form-group" style="width: 45%;">
                                        <label for="courriel" class="ms-3">Courriel</label>
                                        <input type="text" class="form-control" id="courriel" />
                                    </div>
                                    <div class="form-group" style="width: 45%;">
                                        <label for="telephone" class="ms-3">Téléphone</label>
                                        <input type="text" class="form-control" id="telephone" />
                                    </div>
                                </div>

                                <div class="d-flex flex-row justify-content-between mb-3">
                                    <div class="form-group" style="width: 45%;">
                                        <label for="pseudonyme" class="ms-3">Pseudonyme</label>
                                        <input type="text" class="form-control" id="pseudonyme" />
                                    </div>

                                </div>

                                <div class="d-flex flex-row justify-content-between mb-3">
                                    <div class="form-group" style="width: 45%;">
                                        <label for="motDePasse" class="ms-3">Mot de passe</label>
                                        <input type="text" class="form-control" id="motDePasse" />
                                    </div>
                                    <div class="form-group" style="width: 45%;">
                                        <label for="ConfirmMotDePasse" class="ms-3">Confirmation de mot de passe</label>
                                        <input type="text" class="form-control" id="ConfirmMotDePasse" />
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="d-flex justify-content-end gap-2 py-3">
                            <button type="button" class="btn btn-primary px-5 rounded-pill me-4" @click="activateCallback('2')">Suivant</button>
                        </div>
                    </StepPanel>
                </StepItem>

                <!-- Step 2: carte de crédit -->
                <StepItem value="2">
                    <Step class="border-top border-2 p-2 border-dark fs-2"> - Carte de crédit</Step>
                    <StepPanel v-slot="{ activateCallback }">

                        <div class="d-flex flex-column justify-content-start align-items-stretch">

                            <div class="px-4 py-2 mb-4">
                                <div class="form-group flex-fill mb-3">
                                    <label for="nomPropio" class="ms-3">Nom du propriétaire de la carte</label>
                                    <input type="text" class="form-control" id="nomPropio" />
                                </div>
                                <div class="d-flex flex-row justify-content-between mb-3">
                                    <div class="form-group col-md-6">
                                        <label for="numeroCarte" class="ms-3">Numéro de la carte</label>
                                        <input type="text" class="form-control" id="numeroCarte" />
                                    </div>
                                    <div class="form-group col-md-3">
                                        <label for="dateExpiration" class="ms-3">Date d'expiration</label>
                                        <input type="text" class="form-control" id="dateExpiration" />
                                    </div>
                                    <div class="form-group col-md-1">
                                        <label for="cvc" class="ms-3">CVC</label>
                                        <input type="text" class="form-control" id="cvc" />
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="d-flex justify-content-between gap-2 py-3">
                            <button type="button" class="btn btn-secondary px-5 rounded-pill ms-4" @click="activateCallback('1')">Précédent</button>
                            <button type="button" class="btn btn-primary px-5 rounded-pill me-4" @click="activateCallback('3')">Suivant</button>
                        </div>
                    </StepPanel>
                </StepItem>

                <!-- Step 3: Adresse -->
                <StepItem value="3">
                    <Step class="border-top border-2 p-2 border-dark fs-2"> - Adresse</Step>
                    <StepPanel v-slot="{ activateCallback }">


                        <div class="d-flex flex-column justify-content-start align-items-stretch">
                            <div class="px-4 py-2 mb-4">
                                <div class="d-flex flex-row justify-content-between mb-3">
                                    <div class="form-group" style="width: 45%;">
                                        <label for="numeroCivique" class="ms-3">Numéro Civique</label>
                                        <input type="text" class="form-control" id="numeroCivique" />
                                    </div>
                                    <div class="form-group" style="width: 45%;">
                                        <label for="rue" class="ms-3">Rue</label>
                                        <input type="text" class="form-control" id="rue" />
                                    </div>
                                </div>

                                <div class="d-flex flex-row justify-content-between mb-3">
                                    <div class="form-group" style="width: 45%;">
                                        <label for="appartement" class="ms-3">Appartement/Bureau</label>
                                        <input type="text" class="form-control" id="appartement" />
                                    </div>
                                    <div class="form-group" style="width: 45%;">
                                        <label for="ville" class="ms-3">Ville</label>
                                        <input type="text" class="form-control" id="ville" />
                                    </div>
                                </div>

                                <div class="d-flex flex-row justify-content-between mb-3">
                                    <div class="form-group" style="width: 45%;">
                                        <label for="province" class="ms-3">Province</label>
                                        <input type="text" class="form-control" id="province" />
                                    </div>
                                    <div class="form-group" style="width: 45%;">
                                        <label for="pays" class="ms-3">Pays</label>
                                        <input type="text" class="form-control" id="pays" />
                                    </div>

                                </div>

                                <div class="d-flex flex-row justify-content-between mb-3">

                                    <div class="form-group" style="width: 45%;">
                                        <label for="postalCode" class="ms-3">Code Postal</label>
                                        <input type="text" class="form-control" id="postalCode" />
                                    </div>
                                </div>

                            </div>
                        </div>


                        <div class="d-flex justify-content-between gap-2 py-3">
                            <button class="btn btn-secondary rounded-pill px-5 ms-4" @click="activateCallback('2')">Précédent</button>
                            <button class="btn btn-success rounded-pill px-5 me-4" @click="submitForm">Créer le compte</button>
                        </div>
                    </StepPanel>
                </StepItem>
            </Stepper>
        </div>
    </div>
</template>

<script setup>
    import { ref } from 'vue';
    import Stepper from 'primevue/stepper';
    import StepItem from 'primevue/stepitem';
    import Step from 'primevue/step';
    import StepPanel from 'primevue/steppanel';
    import Button from 'primevue/button';

    const activeIndex = ref(1);

    const submitForm = () => {
        alert('Form submitted successfully!');
    };
</script>

<style scoped>
    .card {
        max-width: 600px;
        width: 100%;
    }
</style>
