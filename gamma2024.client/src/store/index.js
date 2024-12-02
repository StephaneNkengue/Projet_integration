import { createStore } from "vuex";
import { initApi } from "@/services/api";
import { startSignalRConnection, stopSignalRConnection } from '@/services/signalR';

const store = createStore({
    state: {
        api: null,
        token: null,
        roles: [],
        isLoggedIn: false,
        user: null,
        connection: null, // Connexion SignalR
        notificationConnection: null,
        lots: {},
        userBids: [],
        userOutbidLots: [],
        notifications: [],
        userBidHistory: {}, // Format: { lotId: { userId: montant } }
        encanCourant: null,
        soireeCloture: null,
    },
    mutations: {
        ADD_NOTIFICATION(state, message) {
            state.userNotifications.push({ id: Date.now(), message, read: false });
        },

        MARK_ALL_AS_READ(state) {
            state.userNotifications.forEach((notification) => {
                notification.read = true;
            });
        },

        setLoggedIn(state, value) {
            state.isLoggedIn = value;
            sessionStorage.setItem("isLoggedIn", value);
            if (!value) {
                state.userBids = [];
                if (state.lots) {
                    Object.values(state.lots).forEach((lot) => {
                        lot.userHasBid = false;
                    });
                }
            }
        },
        setUser(state, user) {
            console.log("Données reçues dans setUser:", user);
            if (user) {
                state.user = {
                    id: user.id,
                    username: user.username,
                    name: user.name,
                    firstName: user.firstName,
                    email: user.email,
                    phoneNumber: user.phoneNumber,
                    pseudonym: user.pseudonym,
                    photo: user.photo,
                    roles: user.roles,
                    cardOwnerName: user.cardOwnerName,
                    cardNumber: user.cardNumber,
                    cardExpiryDate: user.cardExpiryDate,
                    civicNumber: user.civicNumber,
                    street: user.street,
                    apartment: user.apartment,
                    city: user.city,
                    province: user.province,
                    country: user.country,
                    postalCode: user.postalCode,
                };
            } else {
                state.user = null;
            }
            sessionStorage.setItem("user", JSON.stringify(state.user));
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
            sessionStorage.setItem("roles", JSON.stringify(state.roles));
        },
        setToken(state, token) {
            state.token = token;
            if (token) {
                sessionStorage.setItem("token", token);
                if (state.api) {
                    state.api.defaults.headers.common[
                        "Authorization"
                    ] = `Bearer ${token}`;
                }
            } else {
                sessionStorage.removeItem("token");
                if (state.api) {
                    delete state.api.defaults.headers.common["Authorization"];
                }
            }
        },
        SET_API(state, api) {
            state.api = api;
            // Configurer le token s'il existe
            const token = sessionStorage.getItem("token");
            if (token) {
                state.api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            }
        },
        SET_CLIENT_INFO(state, clientInfo) {
            state.clientInfo = clientInfo;
        },
        refreshUserData(state) {
            // Cette mutation ne fait rien, mais force la mise à jour des getters
            state.userDataVersion = (state.userDataVersion || 0) + 1;
        },
        updateLotMise(state, { idLot, montant, userId, userLastBid, nombreMises }) {
            console.log("Store - Mise à jour du lot:", {
                idLot,
                montant,
                userId,
                userActuel: state.user?.id
            });

            const newLots = { ...state.lots };
            if (!newLots[idLot]) {
                newLots[idLot] = {
                    id: idLot,
                    mise: montant,
                    idClientMise: userId,
                    nombreMises: nombreMises
                };
            } else {
                newLots[idLot] = {
                    ...newLots[idLot],
                    mise: montant,
                    idClientMise: userId,
                    nombreMises: nombreMises
                };
            }
            state.lots = newLots;

            // Mettre à jour userBids
            if (userLastBid && userLastBid.userId === state.user?.id) {
                if (!state.userBids.includes(idLot)) {
                    state.userBids.push(idLot);
                }
            }

            // Mettre à jour l'historique des mises
            if (userLastBid) {
                if (!state.userBidHistory[idLot]) {
                    state.userBidHistory[idLot] = {};
                }
                state.userBidHistory[idLot][userLastBid.userId] = userLastBid.montant;
            }
        },

        SET_CONNECTION(state, connection) {
            if (state.connection) {
                // Nettoyer les écouteurs de l'ancienne connexion
                state.connection.off("ReceiveNewBid");
            }
            state.connection = connection;
        },

        SET_NOTIFICATION_CONNECTION(state, notificationConnection) {
            state.notificationConnection = notificationConnection;
        },
        addUserBid(state, lotId) {
            if (!state.userBids.includes(lotId)) {
                state.userBids.push(lotId);
            }
        },
        setUserBids(state, bids) {
            // Stocke les IDs des lots sur lesquels l'utilisateur a misé
            state.userBids = bids.map(bid => bid.lotId);

            // Met à jour chaque lot avec son statut
            bids.forEach(bid => {
                if (state.lots[bid.lotId]) {
                    state.lots[bid.lotId] = {
                        ...state.lots[bid.lotId],
                        userHasBid: true,              // L'utilisateur a misé sur ce lot
                        isOutbid: !bid.isLastBidder    // L'utilisateur n'est plus le dernier enchérisseur
                    };
                }
            });
        },
        setLots(state, lots) {
            // Convertir le tableau en objet indexé par id
            const lotsObj = {};
            lots.forEach((lot) => {
                // Conserver l'état userHasBid s'il existe déjà
                const existingLot = state.lots[lot.id];
                lotsObj[lot.id] = {
                    ...lot,
                    userHasBid: existingLot ? existingLot.userHasBid : false,
                };
            });
            state.lots = lotsObj;
        },
        refreshLots(state) {
            // Forcer la réactivité en créant une nouvelle référence
            state.lots = [...state.lots];
        },
        updateLotsWithUserBids(state, userBids) {
            if (!state.lots || !userBids) return;

            // Mettre à jour les lots avec les informations de mise
            Object.keys(state.lots).forEach((lotId) => {
                const userBid = userBids.find((bid) => bid.lotId === parseInt(lotId));
                const lot = state.lots[lotId];

                if (userBid) {
                    // L'utilisateur a misé sur ce lot
                    const isHighestBidder = userBid.montant === lot.mise;
                    state.lots[lotId] = {
                        ...lot,
                        userHasBid: true,
                        isOutbid: !isHighestBidder && userBid.montant < lot.mise,
                    };
                }
            });
        },
        clearUserBids(state) {
            state.userBids = [];
            if (state.lots) {
                Object.values(state.lots).forEach((lot) => {
                    lot.userHasBid = false;
                });
            }
        },

        setLotOutbid(state, lotId) {
            state.outbidLots.push(lotId);
        },
        setSignalRConnection(state, connection) {  // Ajouter cette mutation manquante
            state.connection = connection;
        },
        updateUserBid(state, { lotId, userId, montant }) {
            if (!state.userBidHistory[lotId]) {
                state.userBidHistory[lotId] = {};
            }
            state.userBidHistory[lotId][userId] = montant;
        },
        UPDATE_USER_LAST_BID(state, { lotId, userId, montant }) {
            if (!state.userBidHistory) {
                state.userBidHistory = {};  // Initialiser l'objet s'il n'existe pas
            }
            if (!state.userBidHistory[lotId]) {
                state.userBidHistory[lotId] = {};
            }
            state.userBidHistory[lotId][userId] = montant;
        },
        SET_ENCAN_COURANT(state, encan) {
            state.encanCourant = encan
        },
        SET_SOIREE_CLOTURE(state, soiree) {
            state.soireeCloture = soiree
        },
        UPDATE_LOT_TEMPS(state, { lotId, nouveauTemps }) {
            const lot = state.lots[lotId]
            if (lot) {
                lot.dateFinDecompteLot = nouveauTemps
            }
        },
        SET_LOT_VENDU(state, lotId) {
            const lot = state.lots[lotId]
            if (lot) {
                lot.estVendu = true
                this.commit('REORGANISER_LOTS')
            }
        },
        REORGANISER_LOTS(state) {
            // Trier les lots non vendus par temps restant
            const lotsNonVendus = Object.values(state.lots)
              .filter(lot => !lot.estVendu)
              .sort((a, b) => {
                const tempsRestantA = new Date(a.dateFinDecompteLot) - new Date();
                const tempsRestantB = new Date(b.dateFinDecompteLot) - new Date();
                
                if (tempsRestantA === tempsRestantB) {
                  // En cas d'égalité, utiliser DateDebutDecompteLot
                  return new Date(a.dateDebutDecompteLot) - new Date(b.dateDebutDecompteLot);
                }
                
                return tempsRestantA - tempsRestantB;
              });

            // Mettre à jour l'ordre des lots
            state.lots = lotsNonVendus.reduce((acc, lot) => {
              acc[lot.id] = lot;
              return acc;
            }, {});
        }
    },
    actions: {
        async login({ commit, state, dispatch }, userData) {
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
                        sessionStorage.setItem("token", response.data.token);
                        state.api.defaults.headers.common[
                            "Authorization"
                        ] = `Bearer ${response.data.token}`;
                        await dispatch("initializeSignalR");
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
            const token = state.token || sessionStorage.getItem("token");
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
                    await dispatch("fetchUserBids");
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
                    sessionStorage.removeItem("token");
                }
            }
        },
        async logout({ commit, state }) {
            try {
                await stopSignalRConnection();
                commit("SET_CONNECTION", null);
                // Appel à l'API pour invalider le token côté serveur
                const response = await state.api.post("/home/logout");
                console.log("Réponse de la déconnexion:", response);

            } catch (error) {
                console.error("Erreur lors de la déconnexion:", error);
            } finally {
                // Nettoyage des données côté client
                commit("setLoggedIn", false);
                commit("setUser", null);
                commit("setRoles", []);
                commit("setToken", null);
                commit("SET_CONNECTION", null); // Nettoyer la référence SignalR

                // Nettoyage du stockage local
                sessionStorage.removeItem("token");
                sessionStorage.removeItem("user");
                sessionStorage.removeItem("roles");
                sessionStorage.removeItem("isLoggedIn");

                // Réinitialiser les données liées aux mises
                commit("updateLotMise", {
                    idLot: null,
                    montant: null,
                    userId: null
                });
                state.userBids = [];
                state.lots = {};
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

        async initializeStore({ commit, dispatch }) {
            console.log("=== Initialisation du Store ===");

            // Récupérer les données de session
            const token = sessionStorage.getItem("token");
            const savedIsLoggedIn = sessionStorage.getItem("isLoggedIn") === "true";
            const savedUser = JSON.parse(sessionStorage.getItem("user"));
            const savedRoles = JSON.parse(sessionStorage.getItem("roles")) || [];

            // Initialiser l'API avec le token
            const api = initApi(() => token);
            if (token) {
                api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            }
            commit("SET_API", api);

            // Initialiser l'état
            commit("setToken", token);
            commit("setLoggedIn", savedIsLoggedIn);
            commit("setUser", savedUser);
            commit("setRoles", savedRoles);

            if (savedIsLoggedIn) {
                await dispatch("fetchUserBids");
                // Initialiser SignalR après la connexion
                await dispatch("initializeSignalR");
            }
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

        async chercherTousLotsRecherche({ state }) {
            try {
                const response = await state.api.get(`/lots/cherchertouslotsrecherche`);

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

        async placerMise({ state, commit, dispatch }, miseData) {
            try {
                console.log('Store - Début placerMise:', miseData);

                const response = await state.api.post("/lots/placerMise", {
                    LotId: miseData.lotId,
                    Montant: parseFloat(miseData.montant),
                    UserId: state.user?.id,
                    MontantMaximal: miseData.montantMaximal
                });

                console.log('Store - Réponse placerMise:', response.data);
                return response.data;
            } catch (error) {
                console.error("Store - Erreur lors de la mise:", error);
                console.log('Store - Détails de l\'erreur:', {
                    status: error.response?.status,
                    data: error.response?.data,
                    message: error.message
                });
                return {
                    success: false,
                    message: error.response?.data?.message || "Erreur lors de la mise"
                };
            }
        },

        async fetchUserBids({ state, commit }) {
            if (!state.user?.id || !state.token) return;

            try {
                const response = await state.api.get(`/lots/userBids/${state.user.id}`, {
                    headers: {
                        'Authorization': `Bearer ${state.token}`
                    }
                });
                commit("setUserBids", response.data);
            } catch (error) {
                console.error("Erreur lors du chargement des mises:", error);
                // Si erreur d'authentification, nettoyer les données
                if (error.response?.status === 401) {
                    commit("setUserBids", []);
                }
            }
        },

        async initializeSignalR({ commit, state }) {
            // Vérifier si une connexion existe déjà
            if (state.connection) {
                console.log("SignalR connection already exists");
                return;
            }

            if (!state.isLoggedIn) return;

            try {
                const baseUrl = state.api.defaults.baseURL.replace('/api', '');
                const connection = await startSignalRConnection(baseUrl, state.token);

                if (connection) {
                    connection.on("ReceiveNewBid", (data) => {
                        commit("updateLotMise", {
                            idLot: data.idLot,
                            montant: data.montant,
                            userId: data.userId
                        });

                        if (data.userLastBid?.userId === state.user?.id) {
                            commit("UPDATE_USER_LAST_BID", {
                                lotId: data.idLot,
                                userId: data.userLastBid.userId,
                                montant: data.userLastBid.montant
                            });
                        }
                    });

                    commit("SET_CONNECTION", connection);
                }
            } catch (error) {
                console.error("Erreur lors de l'initialisation de SignalR:", error);
            }
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
        async creerCompteUtilisateur({ commit, state }, formData) {
            try {
                const response = await state.api.post("/utilisateurs/creer", formData);

                if (response.data.success) {
                    return {
                        success: true,
                        message: response.data.message,
                    };
                } else {
                    return {
                        success: false,
                        message: response.data.message,
                    };
                }
            } catch (error) {
                console.error("Erreur lors de la création du compte:", error);
                return {
                    success: false,
                    message:
                        error.response?.data?.message ||
                        "Une erreur est survenue lors de la création du compte",
                };
            }
        },
        async fetchFactureInfoMembre({ commit, state }) {
            try {
                const response = await state.api.get("/factures/chercherFacturesMembre");
                console.log("Données reçues de l'API:", response.data);
                return response.data;
            } catch (error) {
                console.error("Erreur détaillée:", error.response || error);
                throw error;
            }
        },

        async obtenirArtistes({ state }) {
            const response = await state.api.get("/lots/artistes");
            return response.data;
        },

        async creerSetupIntent({ state }) {
            try {
                const response = await state.api.post("/paiement/creerSetupIntent");
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },

        async chercherCartesUser({ state }) {
            try {
                const response = await state.api.get("/paiement/chercherCartes");
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },
        async fetchFactureInfo({ commit, state }) {
            try {
                const response = await state.api.get("/factures/chercherFactures");
                console.log("Données reçues de l'API:", response.data); // Pour le débogage

                return response.data;
            } catch (error) {
                console.error("Erreur détaillée:", error.response || error);
                throw error;
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
        },
        async chercherPrevisualisationLivraison({ state }, idFacture) {
            try {
                const response = await state.api.get("/facturesLivraison/GenererFactureLivraison/" + idFacture);
                return response;
            } catch (error) {
                console.error("Erreur détaillée:", error.response || error);
                throw error;
            }
        },

        async chercherAdressesClient({ state }) {
            try {
                const response = await state.api.get(
                    "/utilisateurs/chercheradressesclient"
                );
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },

        async enregistrerChoixLivraison({ state }, choixLivraison) {
            try {
                const response = await state.api.post(
                    "/facturesLivraison/enregistrerChoixLivraison", choixLivraison
                );
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },

        async supprimerCarte({ state }, pmId) {
            try {
                const response = await state.api.post(
                    "/paiement/supprimerCarte/" + pmId
                );
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },

        async getUserBidForLot({ state, commit }, lotId) {
            // Ne faire l'appel que si l'utilisateur est connecté
            if (!state.isLoggedIn) return 0;
            
            try {
                const response = await state.api.get(`/lots/userLastBid/${lotId}`);
                if (response.data > 0) {
                    commit('UPDATE_USER_LAST_BID', {
                        lotId,
                        userId: state.user?.id,
                        montant: response.data
                    });
                }
                return response.data;
            } catch (error) {
                console.error("Erreur lors de la récupération de la dernière mise:", error);
                return 0;
            }
        },
        async chargerClientsFinEncan({ state }, numeroEncan) {
            try {
                const response = await state.api.post("/factures/CreerFacturesParEncan/" + 232);
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },
        async chercherFacturesParEncan({ state }, numeroEncan) {
            try {
                const response = await state.api.get("/factures/chercherFacturesParEncan/" + 232);
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },
        async chercherFacturesChoixAFaire({ state }) {
            try {
                const response = await state.api.get("/factures/chercherFacturesChoixAFaire");
                return response;
            } catch (error) {
                return "Erreur, veuillez réessayer";
            }
        },
        async verifierEtatEncan({ state, commit }) {
            const response = await state.api.get('/encans/etat-courant')
            const { type, encan } = response.data
            
            commit('SET_ENCAN_COURANT', encan)
            
            if (encan?.id) {
                // Charger les lots séparément pour avoir toutes les informations
                const lotsResponse = await state.api.get(`/lots/cherchertouslotsparencan/${encan.id}`)
                if (lotsResponse.data) {
                    commit('setLots', lotsResponse.data)
                }
            }
            
            return type // 'courant' ou 'soireeCloture' ou null
        },
        async surveillerTransitionEncan({ dispatch, commit }) {
            const verifierEtat = async () => {
                const ancienType = this.state.typeEncanCourant
                const nouveauType = await dispatch('verifierEtatEncan')
                
                if (ancienType !== nouveauType) {
                    if (ancienType === 'courant' && nouveauType === 'soireeCloture') {
                        await dispatch('initialiserSoireeCloture')
                    } else if (nouveauType === 'aucun') {
                        commit('RESET_ENCAN_STATE')
                    }
                }
            }
            setInterval(verifierEtat, 5000)
        },

        async initialiserSoireeCloture({ commit, dispatch }) {
            try {
                // Synchroniser les temps avec le serveur
                if (this.state.connection) {
                    await this.state.connection.invoke("DemanderSynchronisation");
                }

                // Démarrer la surveillance des transitions
                await dispatch('surveillerTransitionEncan');
            } catch (error) {
                console.error("Erreur lors de l'initialisation de la soirée de clôture:", error);
            }
        },

        async synchroniserTempsLots({ commit }, tempsLots) {
            Object.entries(tempsLots).forEach(([lotId, temps]) => {
                commit('UPDATE_LOT_TEMPS', { lotId: parseInt(lotId), nouveauTemps: temps });
            });
            commit('REORGANISER_LOTS');
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
                    const baseUrl = state.api.defaults.avatarURL;
                    const fullUrl = baseUrl + "/" + state.user.photo;
                    console.log("URL complète de l'avatar:", fullUrl);
                    return fullUrl;
                }
            }
            return "/gamma2024.client/public/icons/Avatar.png";
        },
        hasUserBidOnLot: (state) => (lotId) => {
            return state.userBids.includes(lotId);
        },
        getLot: (state) => (id) => {
            return state.lots[id] || null;
        },
        getAllLots: (state) => {
            return Object.values(state.lots);
        },
        getUniqueOffersCount: (state) => (lotId) => {
            const lot = state.lots[lotId];
            return lot?.nombreMises || 0;
        }
    },
});

// Initialiser le store immédiatement
store.dispatch("initializeStore");

export default store;
