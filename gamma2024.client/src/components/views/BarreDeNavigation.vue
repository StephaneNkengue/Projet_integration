<template>
  <header class="sticky-top">
    <div>
      <nav class="navbar navbar-expand-lg bleuMarinSecondaireFond py-0" data-bs-theme="dark">
        <div class="container-fluid justify-content-between">
          <router-link :to="{ name: 'Accueil' }" class="text-decoration-none">
            <a class="navbar-brand d-flex align-items-center fs-6">
              <img src="/images/Logo.png" alt="Les Encans de Nantes" height="40" />
              Les Encans de Nantes <br />au Québec
            </a>
          </router-link>

          <button
            class="navbar-toggler"
            data-bs-theme="dark"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarSupportedContent"
            aria-controls="navbarSupportedContent"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span class="navbar-toggler-icon"></span>
          </button>

          <div class="collapse navbar-collapse justify-content-between" id="navbarSupportedContent">
            <ul class="navbar-nav text-center">
              <li class="nav-item">
                <router-link :to="{ name: 'Accueil' }" class="text-decoration-none">
                  <a class="nav-link" :class="{ active: $route.name === 'Accueil' }"> Accueil </a>
                </router-link>
              </li>

              <li class="nav-item">
                <router-link :to="{ name: 'EncanPresent' }" class="text-decoration-none">
                  <a class="nav-link" :class="{ active: $route.name === 'EncanPresent' }"> Encan courant </a>
                </router-link>
              </li>

              <li class="nav-item">
                <router-link :to="{ name: 'EncansPasses' }" class="text-decoration-none">
                  <a class="nav-link" :class="{ active: $route.name === 'EncansPasses' }"> Encans passés </a>
                </router-link>
              </li>

              <li class="nav-item">
                <router-link :to="{ name: 'EncansFuturs' }" class="text-decoration-none">
                  <a class="nav-link" :class="{ active: $route.name === 'EncansFuturs' }"> Encans futurs </a>
                </router-link>
              </li>

              <li class="nav-item" v-if="estConnecte && estClient">
                <router-link :to="{ name: 'HistoriqueAchatsParMembre' }" class="text-decoration-none">
                  <a class="nav-link" :class="{ active: $route.name === 'HistoriqueAchatsParMembre' }"> Historique des achats </a>
                </router-link>
              </li>
            </ul>

            <div class="d-flex justify-content-center gap-3 mb-2 mb-lg-0 flex-row align-items-center justify-content-center">
              <!-- Boutons Inscription/Connexion -->
              <router-link :to="{ name: 'Inscription' }" v-if="!estConnecte">
                <button class="btn btn-outline bleuMoyenFond text-white py-0 butttonNavBar btnSurvolerBleuMoyenFond">
                  Inscription
                </button>
              </router-link>
              <router-link :to="{ name: 'Connexion' }" v-if="!estConnecte">
                <button class="btn btn-outline bleuMoyenFond text-white py-0 butttonNavBar btnSurvolerBleuMoyenFond">
                  Connexion
                </button>
              </router-link>

              <!-- Dropdown Tableau de bord (Admin) -->
              <div 
                class="dropdown text-white align-self-stretch justify-content-center align-items-center d-flex"
                v-if="estAdmin"
              >
                <a 
                  class="nav-link d-flex align-items-center gap-1"
                  role="button"
                  @click.stop="isTableauBordDropdownOpen = !isTableauBordDropdownOpen"
                >
                  Tableau de bord
                  <i class="bi" :class="isTableauBordDropdownOpen ? 'bi-chevron-up' : 'bi-chevron-down'"></i>
                </a>
                <ul
                  v-show="isTableauBordDropdownOpen"
                  class="dropdown-menu bleuMarinSecondaireFond text-center"
                  :class="{ 'show': isTableauBordDropdownOpen }"
                  style="position: absolute; top: 100%;"
                >
                  <li>
                    <router-link 
                      :to="{ name: 'TableauDeBordInventaire' }"
                      class="text-decoration-none"
                      @click="isTableauBordDropdownOpen = false"
                    >
                      <a class="dropdown-item contenuListeDropdown">Inventaire</a>
                    </router-link>
                  </li>
                  <li>
                    <router-link 
                      :to="{ name: 'TableauDeBordEncans' }"
                      class="text-decoration-none"
                      @click="isTableauBordDropdownOpen = false"
                    >
                      <a class="dropdown-item contenuListeDropdown">Encans</a>
                    </router-link>
                  </li>
                  <li>
                    <router-link 
                      :to="{ name: 'AffichageVendeurs' }"
                      class="text-decoration-none"
                      @click="isTableauBordDropdownOpen = false"
                    >
                      <a class="dropdown-item contenuListeDropdown">Vendeurs</a>
                    </router-link>
                  </li>
                  <li>
                    <router-link 
                      :to="{ name: 'TableauDeBordVentes' }"
                      class="text-decoration-none"
                      @click="isTableauBordDropdownOpen = false"
                    >
                      <a class="dropdown-item contenuListeDropdown">Ventes</a>
                    </router-link>
                  </li>
                  <li>
                    <router-link 
                      :to="{ name: 'GestionMembre' }"
                      class="text-decoration-none"
                      @click="isTableauBordDropdownOpen = false"
                    >
                      <a class="dropdown-item contenuListeDropdown">Profils de membre</a>
                    </router-link>
                  </li>
                </ul>
              </div>
              <!-- Dropdown Notifications -->
              <div 
                class="dropdown text-white align-self-stretch justify-content-center align-items-center d-flex"
                v-if="estConnecte && !estAdmin"
              >
                <button
                  class="nav-link d-flex align-items-center justify-content-center position-relative"
                  role="button"
                  @click.stop="toggleNotifications"
                >
                  <img src="/icons/IconeCloche.png" alt="Icon cloche" height="25" />
                  <span
                    v-if="unreadCount > 0"
                    class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger"
                  >
                    {{ unreadCount }}
                    <span class="visually-hidden">unread messages</span>
                  </span>
                </button>

                <ul
                  v-show="isNotificationDropdownOpen"
                  class="dropdown-menu bleuMarinSecondaireFond text-center end-0"
                  :class="{ 'show': isNotificationDropdownOpen }"
                  style="position: absolute; top: 100%;"
                >
                  <li
                    v-for="notification in notifications"
                    :key="notification.id"
                    class="list-group-item bleuMarinSecondaireFond text-white border-bottom"
                  >
                    <a 
                      class="dropdown-item text-white btnSurvolerBleuMoyenFond"
                      @click="isNotificationDropdownOpen = false"
                    >
                      {{ notification.message }}
                    </a>
                  </li>
                  <li v-if="notifications.length === 0" class="text-white p-2">
                    Aucune notification
                  </li>
                </ul>
              </div>

              <!-- Dropdown Profil -->
              <div class="dropdown text-white position-relative" v-if="estConnecte">
                <a
                  class="nav-link d-flex align-items-center"
                  role="button"
                  @click.stop="isProfileDropdownOpen = !isProfileDropdownOpen"
                >
                  <p class="m-0 me-1">{{ username }}</p>
                  <img
                    :src="avatarUrl"
                    alt="Avatar"
                    class="imgProfile rounded-circle"
                  />
                </a>
                
                <ul
                  v-show="isProfileDropdownOpen"
                  class="dropdown-menu bleuMarinSecondaireFond text-center end-0"
                  :class="{ 'show': isProfileDropdownOpen }"
                  style="position: absolute; top: 100%;"
                >
                  <li>
                    <router-link
                      v-if="estClient"
                      :to="{ name: 'ModificationProfilUtilisateur' }"
                      class="text-decoration-none text-white d-flex align-items-center gap-3"
                      @click="isProfileDropdownOpen = false"
                    >
                      <a class="dropdown-item text-white btnSurvolerBleuMoyenFond">
                        Profil
                      </a>
                    </router-link>
                  </li>
                  <li>
                    <a
                      class="dropdown-item text-danger btnSurvolerBleuMoyenFond fw-bold"
                      href="#"
                      @click.prevent="deconnecter"
                    >
                      Déconnexion
                    </a>
                  </li>
                </ul>
              </div>
            </div>
          </div>
        </div>
      </nav>

      <!-- Barre de recherche et recherche avancée -->
      <nav class="navbar bg-white navbar-expand-md py-0" data-bs-theme="dark">
        <div class="container-fluid justify-content-center justify-content-md-between d-flex flex-row-reverse">
          <form class="d-flex align-items-center">
            <div class="d-flex input-group mt-1" v-if="activationRecherche">
              <!-- Dropdown des critères -->
              <button
                class="btn btn-outline bleuMoyenFond text-white butttonNavBar py-0 btnSurvolerBleuMoyenFond dropdown-toggle"
                type="button"
                @click="toggleCriteresDropdown"
                v-if="showCriteresButton"
              >
                {{ getCriteresButtonText }}
              </button>
              <!-- Section recherche avancée -->
              <div v-if="$route.name == 'EncanPresent' || $route.name == 'Encan' || $route.name == 'ResultatRechercheLots'">
                <nav class="navbar bg-white">
                  <div class="container-fluid d-flex justify-content-end">
                    <button
                      class="navbar-toggler"
                      type="button"
                      data-bs-toggle="collapse"
                      data-bs-target="#navbarToggleRechercheAvancee"
                      aria-controls="navbarToggleRechercheAvancee"
                      aria-expanded="false"
                      aria-label="Toggle navigation"
                    >
                      <span class="navbar-toggler-icon"></span>
                    </button>
                  </div>
                </nav>
                <div class="bg-white">
                  <div
                    class="container collapse card mb-5 bg-white aucunPaddingPourCarte"
                    id="navbarToggleRechercheAvancee"
                  >
                    <div class="card-header d-flex justify-content-center">
                      <h2>Recherche avancée de lots</h2>
                    </div>
                    <div class="card-body">
                      <!-- Numéro d'encan -->
                      <div class="col mt-2">
                        <label class="text-nowrap recherchelabel fw-bold" for="rechercheLotsNumeroEncan">
                          Numéro d'encan
                        </label>
                        <div class="row">
                          <div class="col-sm-3">
                            <select
                              class="form-select py-0"
                              id="selectNumeroEncan"
                              @change="affichageInputNumeroEncan"
                              aria-label="Default select example"
                            >
                              <option class="py-0" value="0" selected>Égal à</option>
                              <option class="py-0" id="selectNumeroEncanEntre" value="1">Entre</option>
                            </select>
                          </div>
                          <div class="col-sm">
                            <input
                              type="number"
                              maxlength="10"
                              class="form-control rechercheinput align-self-end"
                              id="rechercheLotsNumeroEncan"
                            />
                          </div>
                          <div
                            class="col-sm-auto inputAAfficher align-items-center"
                            id="inputAAfficherNumeroEncan"
                          >
                            <label class="fs-6">et</label>
                          </div>
                          <div class="col-sm inputAAfficher" id="inputAAfficherNumeroEncan">
                            <input
                              type="number"
                              maxlength="10"
                              class="form-control rechercheinput align-self-end"
                              id="rechercheLotsNumeroEncan2"
                            />
                          </div>
                        </div>
                      </div>

                      <!-- Valeur estimée -->
                      <div class="col mt-2">
                        <label class="text-nowrap recherchelabel fw-bold" for="rechercheLotsValeurEstimee">
                          Valeur estimée
                        </label>
                        <div class="row">
                          <div class="col-sm-3">
                            <select
                              class="form-select py-0"
                              id="selectValeurEstimee"
                              @change="affichageInputValeurEstimee"
                              aria-label="Default select example"
                            >
                              <option class="py-0" value="0" selected>Égale à</option>
                              <option class="py-0" value="1">Inférieure à</option>
                              <option class="py-0" value="2">Supérieure à</option>
                              <option class="py-0" value="3" id="selectValeurEstimeeEntre">Entre</option>
                            </select>
                          </div>
                          <div class="col-sm">
                            <input
                              type="number"
                              maxlength="10"
                              class="form-control rechercheinput align-self-end"
                              id="rechercheLotsValeurEstimee"
                            />
                          </div>
                          <div
                            class="col-sm-auto inputAAfficher align-items-center"
                            id="inputAAfficherValeurEstimee"
                          >
                            <label class="fs-6">et</label>
                          </div>
                          <div class="col-sm inputAAfficher" id="inputAAfficherValeurEstimee">
                            <input
                              type="number"
                              maxlength="10"
                              class="form-control rechercheinput align-self-end"
                              id="rechercheLotsValeurEstimee2"
                            />
                          </div>
                        </div>
                      </div>

                      <!-- Sélecteurs (Artiste, Catégorie, Medium) -->
                      <div class="row mt-2">
                        <div class="col-md-4">
                          <label class="text-nowrap recherchelabel fw-bold" for="rechercheLotsArtiste">
                            Artiste
                          </label>
                          <select
                            class="form-select py-0 align-self-center"
                            id="selectArtiste"
                            aria-label="Default select example"
                            required
                          >
                            <option class="py-0 text-muted" value="" selected>Pas de choix</option>
                            <option
                              v-for="artiste in listeDesArtistes"
                              :key="artiste.nomArtiste"
                              class="py-0"
                              :value="artiste.nomArtiste"
                            >
                              {{artiste.nomArtiste}}
                            </option>
                          </select>
                        </div>
                        <div class="col-md-4">
                          <label class="text-nowrap recherchelabel fw-bold" for="rechercheLotsCategorie">
                            Catégorie
                          </label>
                          <select
                            class="form-select py-0 align-self-center"
                            id="selectCategorie"
                            aria-label="Default select example"
                            required
                          >
                            <option class="py-0 text-muted" value="" selected>Pas de choix</option>
                            <option
                              v-for="categorie in listeDesCategories"
                              :key="categorie.id"
                              class="py-0"
                              :value="categorie.id"
                            >
                              {{categorie.nom}}
                            </option>
                          </select>
                        </div>
                        <div class="col-md-4">
                          <label class="text-nowrap recherchelabel fw-bold" for="rechercheLotsMedium">
                            Medium
                          </label>
                          <select
                            class="form-select py-0 align-self-center"
                            id="selectMedium"
                            aria-label="Default select example"
                            required
                          >
                            <option class="py-0 text-muted" value="" selected>Pas de choix</option>
                            <option
                              v-for="medium in listeDesMediums"
                              :key="medium.id"
                              class="py-0"
                              :value="medium.id"
                            >
                              {{medium.type}}
                            </option>
                          </select>
                        </div>
                      </div>
                      <!-- Dimensions (Hauteur et Largeur) -->
                      <div class="row">
                        <div class="col-6">
                          <div class="col mt-2">
                            <label class="text-nowrap recherchelabel fw-bold" for="rechercheLotsHauteur">
                              Hauteur
                            </label>
                            <div class="row">
                              <div class="col-sm-4">
                                <select
                                  class="form-select py-0"
                                  id="selectHauteur"
                                  @change="affichageInputHauteur"
                                  aria-label="Default select example"
                                >
                                  <option class="py-0" value="0" selected>Égale à</option>
                                  <option class="py-0" value="1">Inférieure à</option>
                                  <option class="py-0" value="2">Supérieure à</option>
                                  <option class="py-0" value="3" id="selectHauteurEntre">Entre</option>
                                </select>
                              </div>
                              <div class="col-sm">
                                <input
                                  type="number"
                                  maxlength="10"
                                  class="form-control rechercheinput align-self-end"
                                  id="rechercheLotsHauteur"
                                />
                              </div>
                              <div
                                class="col-sm-auto inputAAfficher align-items-center"
                                id="inputAAfficherHauteur"
                              >
                                <label class="fs-6">et</label>
                              </div>
                              <div class="col-sm inputAAfficher" id="inputAAfficherHauteur">
                                <input
                                  type="number"
                                  maxlength="10"
                                  class="form-control rechercheinput align-self-end"
                                  id="rechercheLotsHauteur2"
                                />
                              </div>
                            </div>
                          </div>
                        </div>
                        <div class="col-6">
                          <div class="col mt-2">
                            <label class="text-nowrap recherchelabel fw-bold" for="rechercheLotsLargeur">
                              Largeur
                            </label>
                            <div class="row">
                              <div class="col-sm-4">
                                <select
                                  class="form-select py-0"
                                  id="selectLargeur"
                                  @change="affichageInputLargeur"
                                  aria-label="Default select example"
                                >
                                  <option class="py-0" value="0" selected>Égale à</option>
                                  <option class="py-0" value="1">Inférieure à</option>
                                  <option class="py-0" value="2">Supérieure à</option>
                                  <option class="py-0" value="3" id="selectLargeurEntre">Entre</option>
                                </select>
                              </div>
                              <div class="col-sm">
                                <input
                                  type="number"
                                  maxlength="10"
                                  class="form-control rechercheinput align-self-end"
                                  id="rechercheLotsLargeur"
                                />
                              </div>
                              <div
                                class="col-sm-auto inputAAfficher align-items-center"
                                id="inputAAfficherLargeur"
                              >
                                <label class="fs-6">et</label>
                              </div>
                              <div class="col-sm inputAAfficher" id="inputAAfficherLargeur">
                                <input
                                  type="number"
                                  maxlength="10"
                                  class="form-control rechercheinput align-self-end"
                                  id="rechercheLotsLargeur2"
                                />
                              </div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <!-- Bouton de recherche -->
                    <div class="col mt-2 mb-2 d-flex justify-content-center">
                      <button
                        class="d-flex align-items-center text-center rounded btn bleuMoyenFond text-white btnSurvolerBleuMoyenFond btnDesactiverBleuMoyenFond"
                        type="button"
                        @click="rechercheAvanceeLots"
                      >
                        Lancer la recherche
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- Bouton d'activation de la recherche -->
            <a @click="activationRecherche = !activationRecherche">
              <img
                src="/icons/IconeRechercheAvanceeBleu.png"
                alt="Icon recherche avancée"
                height="30"
                class="my-1"
              />
            </a>
          </form>
        </div>
      </nav>
    </div>
  </header>
</template>

<style scoped>
/* Styles généraux */
.ms-7 {
  margin-left: 7.9rem;
}

.start-79 {
  left: 79%;
}

/* Styles du profil et avatar */
.imgProfile {
  width: 40px;
  height: 40px;
  object-fit: cover;
  border: 2px solid white;
}

/* Styles des dropdowns */
.dropdown {
  position: relative;
}

.dropdown-menu {
  z-index: 1000;
  min-width: 10rem;
  margin: 0;
  border: 1px solid rgba(0,0,0,.15);
  position: absolute;
  transition: opacity 0.2s ease-in-out, transform 0.2s ease-in-out;
  opacity: 0;
  transform: translateY(-10px);
  background-color: var(--bleu-marin);
  box-shadow: 0 2px 5px rgba(0,0,0,0.2);
}

.dropdown-menu.show {
  display: block;
  opacity: 1;
  transform: translateY(0);
}

.dropdown-item {
  padding: 0.5rem 1rem;
  color: white;
  transition: background-color 0.2s ease;
}

.dropdown-item:hover {
  background-color: var(--bleu-moyen);
}

.list-group-item {
  background-color: transparent;
  border-bottom: 1px solid rgba(255,255,255,0.1) !important;
}

.list-group-item:last-child {
  border-bottom: none !important;
}

/* Styles de la recherche avancée */
input.form-control {
  height: 25px !important;
  font-size: 15px !important;
  border-radius: 4px;
}

select {
  width: 100% !important;
}

select.form-select {
  height: 25px !important;
  margin-right: 10px !important;
  font-size: 15px !important;
  padding: 0 0.5rem;
}

select option {
  height: 25px !important;
  font-size: 15px !important;
  padding: 4px;
}

.recherchelabel {
  margin-left: 10px !important;
  margin-right: 10px !important;
  font-weight: bold !important;
  color: #333;
}

.rechercheinput {
  margin-right: 10px !important;
}

.margesPourTable {
  padding-left: 15px;
  padding-right: 15px;
}

.aucunPaddingPourCarte {
  padding-left: 0px !important;
  padding-right: 0px !important;
  width: 1000px;
  margin: auto;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.selectwidth {
  width: 160px;
}

.inputAAfficher {
  display: none;
}

.card-body {
  padding-top: 0px !important;
  padding-bottom: 10px !important;
}

/* Styles des selects */
select,
select option {
  color: #000000;
}

select:invalid,
select option[value=""] {
  color: #999999;
}

/* Styles des notifications */
.notification-badge {
  position: absolute;
  top: -8px;
  right: -8px;
  background-color: red;
  color: white;
  border-radius: 50%;
  padding: 2px 6px;
  font-size: 12px;
  min-width: 20px;
  text-align: center;
}

/* Styles des boutons */
.butttonNavBar {
  height: 30px;
  display: flex;
  align-items: center;
  padding: 0 1rem;
  border-radius: 4px;
  transition: all 0.3s ease;
}

.btnSurvolerBleuMoyenFond {
  transition: background-color 0.3s ease, transform 0.2s ease;
}

.btnSurvolerBleuMoyenFond:hover {
  background-color: var(--bleu-moyen-hover) !important;
  border-color: var(--bleu-moyen-hover) !important;
  transform: translateY(-1px);
}

.btnSurvolerBleuMoyenFond:active {
  transform: translateY(0);
}

.btnDesactiverBleuMoyenFond:disabled {
  background-color: var(--bleu-moyen-disabled) !important;
  border-color: var(--bleu-moyen-disabled) !important;
  cursor: not-allowed;
  opacity: 0.7;
}

/* Animation des dropdowns */
@keyframes fadeInDown {
  from {
    opacity: 0;
    transform: translateY(-10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

.dropdown-menu.show {
  animation: fadeInDown 0.2s ease-out forwards;
}

/* Responsive Design */
@media (max-width: 768px) {
  .dropdown-menu {
    position: static;
    float: none;
    width: 100%;
    margin-top: 0.5rem;
    background-color: var(--bleu-marin);
    box-shadow: none;
    border: none;
  }

  .aucunPaddingPourCarte {
    width: 100%;
    margin: 0;
    border-radius: 0;
  }

  .rechercheinput,
  select.form-select {
    margin-right: 0 !important;
    margin-bottom: 0.5rem;
  }

  .col-sm-3,
  .col-sm-4 {
    width: 100%;
    margin-bottom: 0.5rem;
  }

  .row {
    margin: 0;
  }
}

/* Styles spécifiques pour les écrans très petits */
@media (max-width: 576px) {
  .imgProfile {
    width: 32px;
    height: 32px;
  }

  .butttonNavBar {
    padding: 0 0.5rem;
  }

  .navbar-brand {
    font-size: 0.9rem;
  }
}

/* Styles d'accessibilité */
.btn:focus,
.form-control:focus,
.form-select:focus {
  box-shadow: 0 0 0 0.2rem rgba(var(--bleu-moyen-rgb), 0.25);
  outline: none;
}

/* Styles pour le mode sombre si nécessaire */
@media (prefers-color-scheme: dark) {
  .bg-white {
    background-color: #1a1a1a !important;
  }

  .text-dark {
    color: #ffffff !important;
  }

  .card {
    background-color: #2d2d2d;
    border-color: #404040;
  }
}

/* Ajoutez ces styles spécifiques pour les dropdowns */
.dropdown-menu {
  background-color: var(--bs-dark) !important; /* Forcer la couleur de fond */
}

.dropdown-menu.bleuMarinSecondaireFond {
  background-color: #1C3755 !important; /* Forcer la couleur bleuMarinSecondaire */
  border-color: #1C3755 !important;
}

/* Pour s'assurer que les items du dropdown gardent leur style au survol */
.dropdown-menu.bleuMarinSecondaireFond .dropdown-item:hover {
  background-color: #5A708A !important;
}
</style>

<script setup>
import { computed, watch, ref, onMounted, onUnmounted } from "vue";
import { useStore } from "vuex";
import { useRouter, useRoute } from "vue-router";

const store = useStore();
const router = useRouter();
const route = useRoute();

// Computed properties
const estConnecte = computed(() => store.state.isLoggedIn);
const estAdmin = computed(() => store.getters.isAdmin);
const estClient = computed(() => store.getters.isClient);
const username = computed(() => store.getters.username);
const avatarUrl = computed(() => store.getters.avatarUrl);
const notifications = computed(() => store.state.notifications);
const unreadCount = computed(() => notifications.value.filter(n => !n.read).length);

// Refs pour les données
const currentUser = ref(null);
const listeDesArtistes = ref([]);
const listeDesCategories = ref([]);
const listeDesMediums = ref([]);
const stringDeRecherche = ref("");

// Refs pour les états UI
const activationRecherche = ref(false);
const isProfileDropdownOpen = ref(false);
const isTableauBordDropdownOpen = ref(false);
const isNotificationDropdownOpen = ref(false);

// Refs pour les critères de recherche
const colonnesDeCriteresLots = ref({
  encan: true,
  numero: true,
  estimationMin: true,
  estimationMax: true,
  categorie: true,
  artiste: true,
  dimension: true,
  description: true,
  medium: true,
});

const colonnesDeCriteresEncans = ref({
  numero: true,
  date: true,
});

// Computed pour les conditions d'affichage
const showCriteresButton = computed(() => {
  return ['EncanPresent', 'Encan', 'TousLesLots', 'ResultatRechercheLots', 
          'TousLesEncans', 'EncansPasses', 'EncansFuturs'].includes(route.name);
});

const getCriteresButtonText = computed(() => {
  if (['EncanPresent', 'Encan', 'TousLesLots', 'ResultatRechercheLots'].includes(route.name)) {
    return 'Critères des lots';
  }
  return 'Critères d\'encans';
});

// Watchers
watch(
  () => store.state.user,
  (newUser) => {
    currentUser.value = newUser;
  },
  { deep: true, immediate: true }
);

watch(
  () => store.state.isLoggedIn,
  (newValue) => {
    if (newValue) {
      refreshUserInfo();
    }
  }
);

// Méthodes de gestion des utilisateurs
const refreshUserInfo = async () => {
  if (store.state.isLoggedIn) {
    try {
      await store.dispatch("fetchClientInfo");
    } catch (error) {
      console.error("Erreur lors de la récupération des informations client:", error);
    }
  }
};

const deconnecter = async () => {
  try {
    await store.dispatch("logout");
    router.push("/");
    isProfileDropdownOpen.value = false;
  } catch (error) {
    console.error("Erreur lors de la déconnexion:", error);
  }
};

// Méthodes de gestion des dropdowns
const closeDropdowns = (e) => {
  if (!e.target.closest('.dropdown')) {
    isProfileDropdownOpen.value = false;
    isTableauBordDropdownOpen.value = false;
    isNotificationDropdownOpen.value = false;
  }
};

const toggleNotifications = () => {
  isNotificationDropdownOpen.value = !isNotificationDropdownOpen.value;
  if (isNotificationDropdownOpen.value) {
    store.commit("MARK_ALL_AS_READ");
  }
};

// Méthodes de recherche
const rechercheSimple = (e) => {
  e.preventDefault();
  const texte = stringDeRecherche.value;
  window.find(texte, false, false, true);
};

const rechercheAvanceeLots = async () => {
  const stringquery = {};
  const selectNumeroEncan = document.querySelector("#selectNumeroEncan").value;
  const stringNumeroEncan = document.querySelector("#rechercheLotsNumeroEncan").value;
  const stringNumeroEncan2 = document.querySelector("#rechercheLotsNumeroEncan2").value;
  const selectValeurEstimee = document.querySelector("#selectValeurEstimee").value;
  const stringValeurEstimee = document.querySelector("#rechercheLotsValeurEstimee").value;
  const stringValeurEstimee2 = document.querySelector("#rechercheLotsValeurEstimee2").value;
  const selectArtiste = document.querySelector("#selectArtiste").value;
  const selectCategorie = document.querySelector("#selectCategorie").value;
  const selectMedium = document.querySelector("#selectMedium").value;
  const selectHauteur = document.querySelector("#selectHauteur").value;
  const stringHauteur = document.querySelector("#rechercheLotsHauteur").value;
  const stringHauteur2 = document.querySelector("#rechercheLotsHauteur2").value;
  const selectLargeur = document.querySelector("#selectLargeur").value;
  const stringLargeur = document.querySelector("#rechercheLotsLargeur").value;
  const stringLargeur2 = document.querySelector("#rechercheLotsLargeur2").value;

  // Construction de la requête
  if (stringNumeroEncan) {
    stringquery.selectNumeroEncan = selectNumeroEncan;
    stringquery.stringNumeroEncan = stringNumeroEncan;
    if (selectNumeroEncan === "1") {
      stringquery.stringNumeroEncan2 = stringNumeroEncan2;
    }
  }

  if (stringValeurEstimee) {
    stringquery.selectValeurEstimee = selectValeurEstimee;
    stringquery.stringValeurEstimee = stringValeurEstimee;
    if (selectValeurEstimee === "3") {
      stringquery.stringValeurEstimee2 = stringValeurEstimee2;
    }
  }

  if (selectArtiste) stringquery.selectArtiste = selectArtiste;
  if (selectCategorie) stringquery.selectCategorie = selectCategorie;
  if (selectMedium) stringquery.selectMedium = selectMedium;

  if (stringHauteur) {
    stringquery.selectHauteur = selectHauteur;
    stringquery.stringHauteur = stringHauteur;
    if (selectHauteur === "3") {
      stringquery.stringHauteur2 = stringHauteur2;
    }
  }

  if (stringLargeur) {
    stringquery.selectLargeur = selectLargeur;
    stringquery.stringLargeur = stringLargeur;
    if (selectLargeur === "3") {
      stringquery.stringLargeur2 = stringLargeur2;
    }
  }

  router.push({
    path: "/resultatrecherchelots",
    query: {
      data: JSON.stringify(stringquery),
    },
  });
};

// Méthodes d'affichage des inputs
const affichageInputNumeroEncan = () => {
  const inputAAfficher = document.querySelectorAll("#inputAAfficherNumeroEncan");
  const selectNumeroEncanEntre = document.querySelector("#selectNumeroEncanEntre");
  
  inputAAfficher.forEach(el => {
    el.style.display = selectNumeroEncanEntre.selected ? "inline-block" : "none";
  });
};

const affichageInputValeurEstimee = () => {
  const inputAAfficher = document.querySelectorAll("#inputAAfficherValeurEstimee");
  const selectValeurEstimeeEntre = document.querySelector("#selectValeurEstimeeEntre");
  
  inputAAfficher.forEach(el => {
    el.style.display = selectValeurEstimeeEntre.selected ? "inline-block" : "none";
  });
};

const affichageInputHauteur = () => {
  const inputAAfficher = document.querySelectorAll("#inputAAfficherHauteur");
  const selectHauteurEntre = document.querySelector("#selectHauteurEntre");
  
  inputAAfficher.forEach(el => {
    el.style.display = selectHauteurEntre.selected ? "inline-block" : "none";
  });
};

const affichageInputLargeur = () => {
  const inputAAfficher = document.querySelectorAll("#inputAAfficherLargeur");
  const selectLargeurEntre = document.querySelector("#selectLargeurEntre");
  
  inputAAfficher.forEach(el => {
    el.style.display = selectLargeurEntre.selected ? "inline-block" : "none";
  });
};

// Gestion des checkboxes
const comportementTousSelectionnerRecherche = () => {
  const checkboxToutSelectionner = document.querySelector(".checkboxTousRecherche");
  const checkboxesSeules = document.querySelectorAll(".checkboxSeulRecherche");
  
  checkboxesSeules.forEach(el => {
    el.checked = checkboxToutSelectionner.checked;
    el.disabled = checkboxToutSelectionner.checked;
  });
};

// Lifecycle hooks
onMounted(async () => {
  document.addEventListener('click', closeDropdowns);
  
  try {
    // Chargement des données pour la recherche avancée
    listeDesArtistes.value = await store.dispatch('obtenirArtistes');
    listeDesMediums.value = await store.dispatch('obtenirMediums');
    listeDesCategories.value = await store.dispatch('obtenirCategories');
  } catch (error) {
    console.error("Erreur lors du chargement des données:", error);
  }
});

onUnmounted(() => {
  document.removeEventListener('click', closeDropdowns);
});
</script>