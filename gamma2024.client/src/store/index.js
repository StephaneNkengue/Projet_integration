import { createStore } from "vuex";
import { initApi } from "@/services/api";

const store = createStore({
    state: {
        api: null,
        token: null,
        roles: [],
        isLoggedIn: false,
        user: null,
    },
    mutations: {
        setLoggedIn(state, value) {
            state.isLoggedIn = value;
            localStorage.setItem("isLoggedIn", value);
        },
        setUser(state, user) {
            state.user = user;
            if (user) {
                localStorage.setItem("user", JSON.stringify(user));
            } else {
                localStorage.removeItem("user");
            }
        },
        setRoles(state, roles) {
            console.log("Roles reçus dans setRoles:", roles);
            if (roles && roles) {
                state.roles = roles;
            } else if (Array.isArray(roles)) {
                state.roles = roles;
            } else {
                state.roles = [roles];
            }
            console.log("Roles après traitement:", state.roles);
            localStorage.setItem("roles", JSON.stringify(state.roles));
        },
        setToken(state, token) {
            state.token = token;
            if (token) {
                localStorage.setItem("token", token);
            } else {
                localStorage.removeItem("token");
            }
        },
        SET_API(state, api) {
            state.api = api;
        },
        SET_CLIENT_INFO(state, clientInfo) {
            state.clientInfo = clientInfo;
        },
        refreshUserData(state) {
            // Cette mutation ne fait rien, mais force la mise à jour des getters
            state.userDataVersion = (state.userDataVersion || 0) + 1;
        },
    },
    actions: {
        async login({ commit, state }, userData) {
            try {
                if (!state.api) {
                    throw new Error("API non initialisée");
                }
                const response = await state.api.post("/home/login", userData);
                if (response.data && response.data.message === "Connexion réussie") {
                    commit("setLoggedIn", true);
                    commit("setUser", {
                        id: response.data.userId,
                        username: response.data.username,
                        name: response.data.name,
                        firstName: response.data.firstName,
                        roles: response.data.roles,
                        photo: response.data.photo,
                    });
                    commit("setRoles", response.data.roles);
                    if (response.data.token) {
                        commit("setToken", response.data.token);
                        localStorage.setItem("token", response.data.token);
                        state.api.defaults.headers.common[
                            "Authorization"
                        ] = `Bearer ${response.data.token}`;
                    }
                    return { success: true, roles: response.data.roles };
                } else {
                    return {
                        success: false,
                        error: response.data?.message || "Réponse inattendue du serveur",
                    };
                }
            } catch (error) {
                console.log("catch");
                return {
                    success: false,
                    element: error.response.data?.element,
                    error:
                        error.response?.data?.message ||
                        error.message ||
                        "Erreur lors de la création du compte",
                };
            }
        },

        async fetchClientInfo({ state, commit }) {
            try {
                const response = await state.api.get(
                    "/Utilisateurs/ObtentionInfoClient"
                );
                commit("SET_CLIENT_INFO", response.data);
                // Assurez-vous également de mettre à jour l'utilisateur
                commit("setUser", response.data);
                return response.data;
            } catch (error) {
                console.error(
                    "Erreur lors de la récupération des informations du client:",
                    error
                );
                throw error;
            }
        },

        async updateClientInfo({ state, commit }, userData) {
            try {
                const response = await state.api.put(
                    "/utilisateurs/miseajourinfoclient",
                    userData
                );
                const updatedUser = { ...state.user, ...response.data };
                commit("setUser", updatedUser);

                // Forcer la mise à jour des getters
                commit("refreshUserData");

                return response;
            } catch (error) {
                console.error(
                    "Erreur lors de la mise à jour des informations du client:",
                    error.response || error
                );
                throw error;
            }
        },

        async updateAvatar({ commit, state }, formData) {
            try {
                console.log("FormData reçu dans updateAvatar:", formData);
                const response = await state.api.put("/utilisateurs/avatar", formData, {
                    headers: {
                        "Content-Type": "multipart/form-data",
                    },
                });

                // Construire l'URL complète de l'avatar
                const avatarPath = response.data.avatarUrl.startsWith("/")
                    ? response.data.avatarUrl
                    : `/Avatars/${response.data.avatarUrl}`;
                const fullAvatarUrl = `${state.api.defaults.baseURL.replace(
                    "/api",
                    ""
                )}${avatarPath}`;

                // Mettre à jour l'utilisateur avec la nouvelle URL de l'avatar
                const updatedUser = { ...state.user, photo: avatarPath };
                commit("setUser", updatedUser);

                console.log("Avatar mis à jour dans le store:", fullAvatarUrl);

                return {
                    ...response,
                    data: { ...response.data, avatarUrl: fullAvatarUrl },
                };
            } catch (error) {
                console.error(
                    "Erreur détaillée lors de la mise à jour de l'avatar:",
                    error.response || error
                );
                throw error;
            }
        },

        async verifierPseudonyme({ commit, state }, pseudo) {
            try {
                const response = await state.api.get(
                    `/utilisateurs/verifier-pseudonyme?pseudo=${encodeURIComponent(
                        pseudo
                    )}`
                );
                return response.data.disponible;
            } catch (error) {
                console.error("Erreur lors de la vérification du pseudonyme:", error);
                throw error;
            }
        },

        async verifierEmail({ commit, state }, email) {
            try {
                const response = await state.api.get(
                    `/utilisateurs/verifier-email?email=${encodeURIComponent(email)}`
                );
                return response.data.disponible;
            } catch (error) {
                console.error("Erreur lors de la vérification de l'email:", error);
                throw error;
            }
        },

        async obtenirTousVendeurs({ commit, state }) {
            try {
                const response = await state.api.get("/vendeurs/tous");
                return response.data;
            } catch (error) {
                console.error(
                    "Erreur lors de la récupération de tous les vendeurs:",
                    error
                );
                throw error;
            }
        },
        async creerVendeur({ commit, state }, vendeurData) {
            try {
                const response = await state.api.post("/vendeurs/creer", vendeurData);
                if (response.data.success) {
                    return { success: true, message: response.data.message };
                } else {
                    return { success: false, error: response.data.message };
                }
            } catch (error) {
                console.error(
                    "Erreur détaillée lors de la création du vendeur:",
                    error.response || error
                );
                return {
                    success: false,
                    error:
                        error.response?.data?.message ||
                        error.message ||
                        "Erreur lors de la création du vendeur",
                    details: error.response?.data, // Ajoutez cette ligne pour obtenir plus de détails
                };
            }
        },
        async modifierVendeur({ commit, state }, vendeurData) {
            try {
                const response = await state.api.put(
                    `/vendeurs/modifier/${vendeurData.id}`,
                    vendeurData
                );
                if (response.data.success) {
                    return { success: true, message: response.data.message };
                } else {
                    return { success: false, error: response.data.message };
                }
            } catch (error) {
                console.error("Erreur lors de la modification du vendeur:", error);
                return {
                    success: false,
                    error:
                        error.response?.data?.message ||
                        "Erreur lors de la modification du vendeur",
                };
            }
        },
        async obtenirVendeur({ commit, state }, id) {
            try {
                const response = await state.api.get(`/vendeurs/${id}`);
                return response.data;
            } catch (error) {
                console.error("Erreur lors de la récupération du vendeur:", error);
                throw error;
            }
        },
        async creerLot({ state }, formData) {
            try {
                const response = await state.api.post("/lots/creer", formData, {
                    headers: {
                        "Content-Type": "multipart/form-data",
                    },
                });
                return response.data;
            } catch (error) {
                console.error("Erreur lors de la création du lot:", error);
                throw error;
            }
        },

        async modifierLot({ state }, { id, lotData }) {
            try {
                const response = await state.api.put(`/lots/modifier/${id}`, lotData, {
                    headers: {
                        "Content-Type": "multipart/form-data",
                    },
                });
                return response.data;
            } catch (error) {
                console.error("Erreur lors de la modification du lot:", error);
                throw error;
            }
        },

        async obtenirTousLots({ state }) {
            try {
                const response = await state.api.get("/lots/tous");
                console.log("Données brutes:", response.data);
                return response.data || [];
            } catch (error) {
                console.error(
                    "Erreur lors de la récupération de tous les lots:",
                    error
                );
                throw error;
            }
        },

        async obtenirLot({ state }, id) {
            try {
                const response = await state.api.get(`/lots/${id}`);
                console.log("Lot reçu:", response.data);
                return response.data;
            } catch (error) {
                console.error("Erreur lors de la récupération du lot:", error);
                throw error;
            }
        },

        async supprimerLot({ state }, id) {
            try {
                const response = await state.api.delete(`/lots/supprimer/${id}`);
                return response.data;
            } catch (error) {
                console.error("Erreur lors de la suppression du lot:", error);
                throw error;
            }
        },
        async obtenirCategories({ state }) {
            const response = await state.api.get("/lots/categories");
            return response.data;
        },
        async obtenirVendeurs({ state }) {
            const response = await state.api.get("/lots/vendeurs");
            return response.data;
        },
        async obtenirMediums({ state }) {
            const response = await state.api.get("/lots/mediums");
            return response.data;
        },
        async obtenirEncans({ state }) {
            const response = await state.api.get("/lots/encans");
            return response.data;
        },
        async ObtenirTousLesMembres({ commit, state }) {
            try {
                const response = await state.api.get(
                    `/administrateur/ObtenirTousLesUsers`
                );
                return response.data;
            } catch (error) {
                console.error(
                    "Erreur détaillée:",
                    error.response?.data || error.message
                );
                throw error;
            }
        },

        async obtenirUnMembre({ commit, state }, membreId) {
            try {
                const response = await state.api.get(
                    `/administrateur/obtenirTousLesMembres/${membreId}`
                );
                return response.data;
            } catch (error) {
                console.error("Erreur lors de la récupération du membre:", error);
                throw error;
            }
        },

        async bloquerUnMembre({ commit, state }, membreId) {
            try {
                const response = await state.api.get(
                    `/administrateur/bloquerMembre/${membreId}`
                );
                return response.data;
            } catch (error) {
                console.error("Erreur lors du blocage du membre:", error);
                throw error;
            }
        },

        async debloquerUnMembre({ commit, state }, membreId) {
            try {
                const response = await state.api.get(
                    `/administrateur/debloquerMembre/${membreId}`
                );
                return response.data;
            } catch (error) {
                console.error("Erreur lors du déblocage du membre:", error);
                throw error;
            }
        },
        async checkAuthStatus({ commit, state, dispatch }) {
            const token = state.token || localStorage.getItem("token");
            console.log("Token trouvé :", token ? "Oui" : "Non");
            if (!token) {
                console.log("Aucun token trouvé, déconnexion de l'utilisateur");
                commit("setLoggedIn", false);
                commit("setUser", null);
                commit("setRoles", []);
                return;
            }

            // Attendre que la base URL soit définie
            while (!state.api.defaults.baseURL) {
                await new Promise((resolve) => setTimeout(resolve, 100));
            }

            try {
                console.log(
                    "URL de la requête :",
                    `${state.api.defaults.baseURL}/home/check-auth`
                );
                const response = await state.api.get("/home/check-auth");
                console.log("Réponse complète de check-auth:", response.data);
                if (response.data.isAuthenticated) {
                    commit("setLoggedIn", true);
                    commit("setUser", response.data.user);
                    commit("setRoles", response.data.roles);
                    dispatch("forceUpdate");
                } else {
                    throw new Error("Non authentifié");
                }
            } catch (error) {
                console.error(
                    "Erreur détaillée lors de la vérification de l'authentification:",
                    error.response || error
                );
                if (error.response && error.response.status === 401) {
                    commit("setLoggedIn", false);
                    commit("setUser", null);
                    commit("setRoles", []);
                    commit("setToken", null);
                    localStorage.removeItem("token");
                }
            }
        },
        async logout({ commit, state }) {
            try {
                // Appel à l'API pour invalider le token côté serveur (optionnel mais recommandé)
                const response = await state.api.post("/home/logout");
                console.log("Réponse de la déconnexion:", response);
            } catch (error) {
                console.error("Erreur lors de la déconnexion côté serveur:", error);
            } finally {
                // Nettoyage des données côté client
                commit("setLoggedIn", false);
                commit("setUser", null);
                commit("setRoles", []);
                commit("setToken", null);
                localStorage.removeItem("token");
                localStorage.removeItem("user");
                localStorage.removeItem("roles");
                localStorage.removeItem("isLoggedIn");
            }
        },

        async fetchListeDeLotsPourAdministrateur({ commit, state }) {
            try {
                const response = await state.api.get("/lots/chercherTousLots");
                console.log("Données reçues de l'API:", response.data); // Pour le débogage
                // let dataResponse = await response.json();
                return response.data;
            } catch (error) {
                console.error("Erreur détaillée:", error.response || error);
                throw error;
            }
        },

        async initializeStore({ commit, state }) {
            console.log("=== Initialisation du Store ===");

            // Récupérer l'état depuis localStorage
            const savedIsLoggedIn = localStorage.getItem("isLoggedIn") === "true";
            const savedToken = localStorage.getItem("token");
            const savedUser = JSON.parse(localStorage.getItem("user"));
            const savedRoles = JSON.parse(localStorage.getItem("roles")) || [];

            // Initialiser l'état
            commit("setLoggedIn", savedIsLoggedIn);
            commit("setToken", savedToken);
            commit("setUser", savedUser);
            commit("setRoles", savedRoles);

            const api = initApi(() => state.token);
            commit("SET_API", api);
        },

        async chercherTousEncansVisibles({ commit, state }) {
            try {
                const response = await state.api.get(
                    "/encans/cherchertousencansvisibles"
                );
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },
        async chercherEncanParNumero({ commit, state }, numeroEncan) {
            try {
                const response = await state.api.get(
                    "/encans/chercherencanparnumero/" + numeroEncan
                );
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },
        async chercherEncanEnCours({ commit, state }) {
            try {
                const response = await state.api.get("/encans/chercherencanencours");
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },

        async chercherNumeroEncanEnCours({ commit, state }) {
            try {
                const response = await state.api.get(
                    "/encans/ChercherNumeroEncanEnCours"
                );
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },

        async fetchEncanInfo({ commit, state }) {
            try {
                const response = await state.api.get("/encans/cherchertousencans");
                console.log("Données reçues de l'API:", response.data); // Pour le débogage

                return response.data;
            } catch (error) {
                console.error("Erreur détaillée:", error.response || error);
                throw error;
            }
        },
        async supprimerUnEncan({ commit, state }, numeroEncan) {
            try {
                const response = await state.api.delete(
                    `/encans/supprimerEncan/${numeroEncan}`
                );
                return response.data;
            } catch (error) {
                console.error(
                    "Erreur détaillée lors de la suppression de l'encan",
                    error.response || error
                );
                throw error;
            }
        },
        async obtenirUnEncanParId({ commit, state }, idEncan) {
            try {
                const response = await state.api.get(
                    `/encans/obtenirUnEncan/${idEncan}`
                );
                return response.data;
            } catch (error) {
                console.error(
                    "Erreur détaillée lors de la suppression de l'encan",
                    error.response || error
                );
                throw error;
            }
        },

        async modifierEncan({ commit, state }, encanData) {
            try {
                const response = await state.api.put(
                    `/encans/modifierEncan/${encanData.id}`,
                    encanData
                );
                if (response.data.success) {
                    return { success: true, message: response.data.message };
                } else {
                    return { success: false, error: response.data.message };
                }
            } catch (error) {
                return {
                    success: false,
                    error:
                        error.response?.data?.message ||
                        "Erreur lors de la modification de l'encan",
                };
            }
        },

        async creerEncan({ commit, state }, encanData) {
            try {
                const response = await state.api.post("/encans/creerEncan", encanData);
                if (response.data.sucess) {
                    return { success: true, message: response.data.message };
                } else {
                    return { success: false, error: response.data.message };
                }
            } catch (error) {
                return {
                    success: false,
                    error: error.response.data.message,
                    details: error.response?.data,
                };
            }
        },

        async mettreAJourEncanPublie({ commit, state }, encanData) {
            try {
                const response = await state.api.put(
                    "/encans/mettreAJourEncanPublie",
                    encanData
                );
                if (response.data.sucess) {
                    return { success: true, message: response.data.message };
                } else {
                    return { success: false, error: response.data.message };
                }
            } catch (error) {
                return {
                    success: false,
                    error: error.response.data.message,
                    details: error.response?.data,
                };
            }
        },

        async chercherTousLotsParEncan({ state }, idEncan) {
            try {
                const response = await state.api.get(
                    `/lots/cherchertouslotsparencan/${idEncan}`
                );

                console.log("Réponse reçue:", response);
                return response;
            } catch (error) {
                console.error("Erreur détaillée:", {
                    message: error.message,
                    status: error.response?.status,
                    data: error.response?.data,
                    config: error.config,
                });
                throw error;
            }
        },
        async chercherDetailsLotParId({ commit, state }, idLot) {
            try {
                const response = await state.api.get(
                    "/lots/chercherDetailsLotParId/" + idLot
                );
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },

        async chercherEncansFuturs({ commit, state }) {
            try {
                const response = await state.api.get("/encans/chercherencansfuturs");
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },

        async chercherEncansPasses({ commit, state }) {
            try {
                const response = await state.api.get("/encans/chercherencanspasses");
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },
        forceUpdate({ commit }) {
            commit("refreshUserData");
        },

        async reinitialisePassword({ commit, state }, resetPasswordData) {
            try {
                const response = await state.api.post(
                    "/utilisateurs/reinitialiserMotDePasse",
                    resetPasswordData
                );
                return {
                    success: true,
                    message: response.data,
                };
            } catch (error) {
                return {
                    success: false,
                    message:
                        error.response?.data ||
                        "Erreur lors de la modification du mot de passe.",
                };
            }
        },

        async creerPaymentIntent({ state }, idFacture) {
            try {
                const response = await state.api.post(
                    "/paiement/creerPaymentIntent/" + idFacture
                );
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        }
    },
    getters: {
        isAdmin: (state) => {
            // console.log("Rôles dans le getter isAdmin:", state.roles);
            const result =
                Array.isArray(state.roles) && state.roles.includes("Administrateur");
            // console.log("L'utilisateur est-il admin ?", result);
            return result;
        },
        isClient: (state) =>
            Array.isArray(state.roles) && state.roles.includes("Client"),
        currentUser: (state) => state.user,
        username: (state) =>
            state.user ? state.user.pseudonym || state.user.username : "USERNAME",
        avatarUrl: (state) => {
            console.log("Photo de l'utilisateur brute:", state.user.photo);
            if (state.user && state.user.photo) {
                if (state.user.photo.startsWith("http")) {
                    return state.user.photo;
                } else {
                    const baseUrl = state.api.defaults.baseURL.replace("/api", "");
                    const fullUrl = new URL(state.user.photo, baseUrl).href;
                    console.log("URL complète de l'avatar:", fullUrl);
                    return fullUrl;
                }
            }
            return "/gamma2024.client/public/icons/Avatar.png";
        },
    },
});

// Initialiser le store immédiatement
store.dispatch("initializeStore");

export default store;
