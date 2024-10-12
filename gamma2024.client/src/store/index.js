import { createStore } from "vuex";
import api from "@/services/api";

const store = createStore({
  state() {
    return {
      isLoggedIn: localStorage.getItem("isLoggedIn") === "true",
      user: JSON.parse(localStorage.getItem("user")) || null,
      roles: JSON.parse(localStorage.getItem("roles")) || [],
      token: localStorage.getItem("token") || null,
    };
  },
  mutations: {
    setLoggedIn(state, value) {
      state.isLoggedIn = value;
      localStorage.setItem("isLoggedIn", value);
    },
    setUser(state, user) {
      state.user = user;
      localStorage.setItem("user", JSON.stringify(user));
    },
    setRoles(state, roles) {
      state.roles = roles;
      localStorage.setItem("roles", JSON.stringify(roles));
    },
    setToken(state, token) {
      state.token = token;
      localStorage.setItem("token", token);
    },
  },

  actions: {
    async login({ commit }, userData) {
      try {
        console.log("Tentative de connexion avec:", userData);
        const response = await api.post("/home/login", userData);
        console.log("Réponse de l'API:", response);
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
            api.defaults.headers.common[
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
        console.error("Erreur détaillée de connexion:", error);
        return {
          success: false,
          error:
            error.response?.data?.message ||
            error.message ||
            "Erreur de connexion",
        };
      }
    },

    logout({ commit }) {
      commit("setLoggedIn", false);
      commit("setUser", null);
      commit("setRoles", []);
      commit("setToken", null);
      delete api.defaults.headers.common["Authorization"];
    },

    async creerCompteUtilisateur({ commit }, userData) {
      try {
        const response = await api.post("/utilisateurs/creer", userData);
        if (response.data.success) {
          return { success: true };
        } else {
          return {
            success: false,
            error:
              response.data.message || "Erreur lors de la création du compte",
          };
        }
      } catch (error) {
        return {
          success: false,
          error:
            error.response?.data?.message ||
            error.message ||
            "Erreur lors de la création du compte",
        };
      }
    },
    async fetchClientInfo({ commit }) {
      try {
        const response = await api.get("/utilisateurs/ObtentionInfoClient");
        console.log("Données reçues de l'API:", response.data);

        // Corriger l'URL de l'avatar
        if (response.data.photo) {
          // Supprimer '/api' de l'URL de base si présent
          const baseUrl = api.defaults.baseURL.replace("/api", "");
          response.data.photo = `${baseUrl}/Avatars/${response.data.photo
            .split("/")
            .pop()}`;
        }

        commit("setUser", response.data);
        return response;
      } catch (error) {
        console.error("Erreur détaillée:", error.response || error);
        throw error;
      }
    },

    async updateClientInfo({ commit }, userData) {
      try {
        const response = await api.put(
          "/utilisateurs/miseajourinfoclient",
          userData
        );
        console.log("Réponse de mise à jour:", response.data); // Pour le débogage
        commit("setUser", response.data);
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
        const response = await api.put("/utilisateurs/avatar", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        });

        // Construire l'URL complète de l'avatar
        const fullAvatarUrl = `${api.defaults.baseURL.replace("/api", "")}${
          response.data.avatarUrl
        }`;

        // Mettre à jour l'utilisateur avec la nouvelle URL de l'avatar
        const updatedUser = { ...state.user, photo: fullAvatarUrl };
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

    async verifierPseudonyme({ commit }, pseudo) {
      try {
        const response = await api.get(
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

    async verifierEmail({ commit }, email) {
      try {
        const response = await api.get(
          `/utilisateurs/verifier-email?email=${encodeURIComponent(email)}`
        );
        return response.data.disponible;
      } catch (error) {
        console.error("Erreur lors de la vérification de l'email:", error);
        throw error;
      }
    },

    async obtenirTousVendeurs({ commit }) {
      try {
        const response = await api.get("/vendeurs/tous");
        return response.data;
      } catch (error) {
        console.error(
          "Erreur lors de la récupération de tous les vendeurs:",
          error
        );
        throw error;
      }
    },
    async creerVendeur({ commit }, vendeurData) {
      try {
        console.log("Données envoyées au serveur:", vendeurData); // Ajoutez cette ligne
        const response = await api.post("/vendeurs/creer", vendeurData);
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
    async modifierVendeur({ commit }, vendeurData) {
      try {
        const response = await api.put(
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
    async obtenirVendeur({ commit }, id) {
      try {
        const response = await api.get(`/vendeurs/${id}`);
        return response.data;
      } catch (error) {
        console.error("Erreur lors de la récupération du vendeur:", error);
        throw error;
      }
    },
    async checkAuthStatus({ commit }) {
      try {
        const response = await api.get("/utilisateurs/check-auth");
        if (response.data.isAuthenticated) {
          commit("setLoggedIn", true);
          commit("setUser", response.data.user);
          commit("setRoles", response.data.roles);
        } else {
          // L'utilisateur n'est pas authentifié, réinitialiser l'état
          commit("setLoggedIn", false);
          commit("setUser", null);
          commit("setRoles", []);
          commit("setToken", null);
        }
      } catch (error) {
        console.error(
          "Erreur lors de la vérification de l'authentification:",
          error
        );
        // En cas d'erreur, considérer l'utilisateur comme déconnecté
        commit("setLoggedIn", false);
        commit("setUser", null);
        commit("setRoles", []);
        commit("setToken", null);
      }
    },
    async ObtenirTousLesMembres({ commit }) {
      try {
        const response = await api.get(`/administrateur/ObtenirTousLesUsers`);
        console.log("Données reçues:", response.data); // Pour le débogage
        return response.data;
      } catch (error) {
        console.error(
          "Erreur détaillée:",
          error.response?.data || error.message
        );
        throw error;
      }
    },

    async obtenirUnMembre({ commit }, membreId) {
      try {
        const response = await api.get(
          `/administrateur/obtenirTousLesMembres/${membreId}`
        );
        console.log(response.data);
        return response.data;
      } catch (error) {
        console.error("Erreur lors de la récupération du membre:", error);
        throw error;
      }
    },

    async bloquerUnMembre({ commit }, membreId) {
      try {
        const response = await api.get(
          `/administrateur/bloquerMembre/${membreId}`
        );
        console.log(response.data);
        return response.data;
      } catch (error) {
        console.error("Erreur lors du blocage du membre:", error);
        throw error;
      }
    },

    async debloquerUnMembre({ commit }, membreId) {
      try {
        const response = await api.get(
          `/administrateur/debloquerMembre/${membreId}`
        );
        console.log(response.data);
        return response.data;
      } catch (error) {
        console.error("Erreur lors du déblocage du membre:", error);
        throw error;
      }
    },
  },
  getters: {
    isAdmin: (state) => state.roles.includes("Administrateur"),
    isClient: (state) => state.roles.includes("Client"),
    currentUser: (state) => state.user,
    username: (state) =>
      state.user ? state.user.pseudonym || state.user.username : "USERNAME",
    avatarUrl: (state) => {
      console.log("État de l'utilisateur:", state.user);
      if (state.user && state.user.photo) {
        console.log("Photo de l'utilisateur:", state.user.photo);
        if (state.user.photo.startsWith("http")) {
          return state.user.photo;
        } else {
          const fullUrl = `${api.defaults.baseURL.replace("/api", "")}${
            state.user.photo
          }`;
          console.log("URL complète de l'avatar:", fullUrl);
          return fullUrl;
        }
      }
      console.log("Utilisation de l'avatar par défaut");
      return "/icons/Avatar.png";
    },
  },
});

// Ajout de l'intercepteur pour ajouter le token à l'en-tête de la requête
api.interceptors.request.use((config) => {
  const token = store.state.token;
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export default store;
