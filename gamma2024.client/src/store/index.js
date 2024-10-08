import { createStore } from 'vuex'
import api from '@/services/api'
import { toast } from 'vue3-toastify';

const store = createStore({
    state() {
        return {
            isLoggedIn: false,
            user: null,
            roles: [],
            token: null
        }
    },
    mutations: {
        setLoggedIn(state, value) {
            state.isLoggedIn = value
        },
        setUser(state, user) {
            state.user = user
        },
        setRoles(state, roles) {
            state.roles = roles
        },
        setToken(state, token) {
            state.token = token
        }
    },
    actions: {
        async login({ commit }, userData) {
            try {
                console.log("Tentative de connexion avec:", userData);
                const response = await api.post('/home/login', userData);
                console.log("Réponse de l'API:", response);
                if (response.data && response.data.message === "Connexion réussie") {
                    commit('setLoggedIn', true);
                    commit('setUser', {
                        id: response.data.userId,
                        username: response.data.username,
                        name: response.data.name,
                        firstName: response.data.firstName,
                        roles: response.data.roles,
                        photo: response.data.photo
                    });
                    commit('setRoles', response.data.roles);
                    if (response.data.token) {
                        commit('setToken', response.data.token);
                        api.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;
                    }
                    return { success: true, roles: response.data.roles };
                } else {
                    return { success: false, error: response.data?.message || "Réponse inattendue du serveur" };
                }
            } catch (error) {
                console.error("Erreur détaillée de connexion:", error);
                return { success: false, error: error.response?.data?.message || error.message || "Erreur de connexion" };
            }
        },


        logout({ commit }) {
            commit('setLoggedIn', false)
            commit('setUser', null)
            commit('setRoles', [])
            commit('setToken', null)
            delete api.defaults.headers.common['Authorization'];
        },


        async creerCompteUtilisateur({ commit }, userData) {
            try {
                const response = await api.post('/utilisateurs/creer', userData);
                if (response.data.success) {

                    return { success: true };
                } else {
                    return { success: false, error: response.data.message || "Erreur lors de la création du compte" };
                }
            } catch (error) {
                return {
                    success: false,
                    error: error.response?.data?.message || error.message || "Erreur lors de la création du compte"
                };
            }
        },


        async fetchClientInfo({ commit }) {
            try {
                const response = await api.get('/utilisateurs/ObtentionInfoClient');
                console.log("Données reçues de l'API:", response.data); // Pour le débogage

                // Construire l'URL complète de l'avatar si nécessaire
                if (response.data.photo && !response.data.photo.startsWith('http')) {
                    response.data.photo = `${api.defaults.baseURL.replace('/api', '')}${response.data.photo}`;
                }

                commit('setUser', response.data);
                return response;
            } catch (error) {
                console.error("Erreur détaillée:", error.response || error);
                throw error;
            }
        },


        async updateClientInfo({ commit }, userData) {
            try {
                const response = await api.put('/utilisateurs/miseajourinfoclient', userData);
                console.log("Réponse de mise à jour:", response.data); // Pour le débogage
                commit('setUser', response.data);
                return response;
            } catch (error) {
                console.error("Erreur lors de la mise à jour des informations du client:", error.response || error);
                throw error;
            }
        },

        async updateAvatar({ commit, state }, formData) {
            try {
                console.log("FormData reçu dans updateAvatar:", formData);
                const response = await api.put('/utilisateurs/avatar', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                });

                // Construire l'URL complète de l'avatar
                const fullAvatarUrl = `${api.defaults.baseURL.replace('/api', '')}${response.data.avatarUrl}`;

                // Mettre à jour l'utilisateur avec la nouvelle URL de l'avatar
                const updatedUser = { ...state.user, photo: fullAvatarUrl };
                commit('setUser', updatedUser);

                console.log("Avatar mis à jour dans le store:", fullAvatarUrl);

                return { ...response, data: { ...response.data, avatarUrl: fullAvatarUrl } };
            } catch (error) {
                console.error("Erreur détaillée lors de la mise à jour de l'avatar:", error.response || error);
                throw error;
            }
        },

        async verifierPseudonyme({ commit }, pseudo) {
            try {
                const response = await api.get(`/utilisateurs/verifier-pseudonyme?pseudo=${encodeURIComponent(pseudo)}`);
                return response.data.disponible;
            } catch (error) {
                console.error("Erreur lors de la vérification du pseudonyme:", error);
                throw error;
            }
        },

        async verifierEmail({ commit }, email) {
            try {
                const response = await api.get(`/utilisateurs/verifier-email?email=${encodeURIComponent(email)}`);
                return response.data.disponible;
            } catch (error) {
                console.error("Erreur lors de la vérification de l'email:", error);
                throw error;
            }
        },

        async fetchEncanInfo({ commit }) {
            try {
                const response = await api.get('/encans/cherchertouslesencans');
                console.log("Données reçues de l'API:", response.data); // Pour le débogage

                return response.data;
            } catch (error) {
                console.error("Erreur détaillée:", error.response || error);
                throw error;
            }
        },
    },
    getters: {
        isAdmin: state => state.roles.includes('ADMINISTRATEUR'),
        isClient: state => state.roles.includes('CLIENT')
    }
});

// Ajoutez l'intercepteur ici
api.interceptors.request.use(config => {
    const token = store.state.token;
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});

export default store;