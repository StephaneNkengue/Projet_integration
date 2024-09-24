<template>
    <div class="bg-image pt-5 imageDeFondEsquise">
        <div class="container d-flex flex-column justify-content-start align-items-stretch bg-white bg-opacity-75 cadreBlanc">
            <h2 class="fs-1 text-center fw-bold pt-4">Connexion</h2>
            <p class="text-center">Se connecter pour continuer</p>

            <form @submit.prevent="connexion" class="d-flex flex-column justify-content-start align-items-stretch">
                <div class="px-4 py-2 mb-4">
                    <div class="d-flex flex-row justify-content-center mb-3">
                        <div class="form-group w-90">
                            <label for="emailOuPseudo" class="fw-bold ms-3">Email ou pseudonyme</label>
                            <input type="text"
                                   class="form-control"
                                   id="emailOuPseudo"
                                   v-model="emailOuPseudo"
                                   @input="validateEmailOuPseudo"
                                   :class="{ 'is-invalid': emailOuPseudoError }" />
                            <div v-if="emailOuPseudoError" class="invalid-feedback">{{ emailOuPseudoError }}</div>
                        </div>
                    </div>
                    <div class="d-flex flex-row justify-content-center mb-3">
                        <div class="form-group w-90">
                            <label for="motdepasse" class="fw-bold ms-3">Mot de passe</label>
                            <input type="password"
                                   class="form-control"
                                   id="motDePasse"
                                   v-model="password"
                                   @input="validatePassword"
                                   :class="{ 'is-invalid': passwordError }" />
                            <div v-if="passwordError" class="invalid-feedback">{{ passwordError }}</div>
                        </div>
                    </div>
                    <div class="d-flex flex-row justify-content-center mt-4 mb-1">
                        <div class="form-group w-80">
                            <router-link to="/" class="text-decoration-none">
                                <p class="text-center lienEnTexteNoir">J'ai oublié mon Mot de passe</p>
                            </router-link>
                        </div>
                    </div>
                    <div class="d-flex flex-row justify-content-center mb-2">
                        <router-link to="Accueil">
                            <button class="btn btnSurvolerBleuMoyenFond rounded-pill px-5 bleuMoyenFond text-white" @click="submitForm">Connexion</button>
                        </router-link>
                    </div>
                    <div class="d-flex flex-row justify-content-center mb-3">
                        <div class="form-group w-80">
                            <router-link to="Inscription" class="text-decoration-none">
                                <p class="fs-5 text-center fw-bold lienEnTexteNoir">M'inscrire</p>
                            </router-link>
                        </div>
                    </div>

                </div>
            </form>

        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                emailOuPseudo: '',
                password: '',
                emailOuPseudoError: '',
                passwordError: '',
                messageErreur: '',
                messageSucces: ''
            }
        },
        methods: {
            validateEmailOuPseudo() {
                if (!this.emailOuPseudo) {
                    this.emailOuPseudoError = "L'email ou le pseudonyme est obligatoire";
                } else {
                    this.emailOuPseudoError = "";
                }
            },
            validatePassword() {
                if (!this.password) {
                    this.passwordError = "Le mot de passe est obligatoire";
                } else {
                    this.passwordError = "";
                }
            },
            connexion() {
                this.validateEmailOuPseudo();
                this.validatePassword();

                if (this.emailOuPseudoError || this.passwordError) {
                    return; // Empêche la soumission si des erreurs sont présentes
                }

                this.tenterConnexion();
            },
            async tenterConnexion() {
                try {
                    console.log('Tentative de connexion avec:', { emailOuPseudo: this.emailOuPseudo, password: this.password });
                    const result = await this.$store.dispatch('login', {
                        emailOuPseudo: this.emailOuPseudo,
                        password: this.password
                    });
                    if (result.success) {
                        this.messageSucces = `Connexion réussie en tant que ${result.roles.join(', ')}`;
                        console.log('Utilisateur connecté:', this.$store.state.user);
                        console.log('Rôles:', this.$store.state.roles);
                        // Redirection après un court délai
                        setTimeout(() => {
                            this.$router.push('/');
                        }, 2000);
                    } else {
                        this.messageErreur = 'Échec de la connexion: ' + result.error;
                    }
                } catch (error) {
                    this.messageErreur = "Erreur lors de la connexion: " + error;
                }
            }
        }
    }
</script>

<style scoped>
    .cadreBlanc {
        border-radius: 15px;
        height: 450px;
        width: 410px;
    }

    .imageDeFondEsquise {
        background-image: url('/public/images/DessinGris.PNG');
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

    .text-danger {
        font-size: 0.875rem;
    }

    .invalid-feedback {
        display: block;
        color: #dc3545;
        font-size: 0.875rem;
    }
</style>
