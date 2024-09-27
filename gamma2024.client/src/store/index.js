import { createStore } from 'vuex'
import api from '@/services/api'

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
                console.log("Données envoyées au serveur:", JSON.stringify(userData, null, 2));
                const response = await api.post('/utilisateurs/creer', userData);
                if (response.data.success) {
                    console.log("Compte créé avec succès !");
                    return { success: true };
                } else {
                    return { success: false, error: response.data.message || "Erreur lors de la création du compte" };
                }
            } catch (error) {
                console.error("Erreur détaillée lors de la création du compte:", error.response || error);
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

        async updateAvatar({ commit }, file) {
            try {
                const formData = new FormData();
                formData.append('avatar', file);
                const response = await api.put('/utilisateurs/avatar', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                });
                commit('setUser', { ...this.state.user, photo: response.data.avatarUrl });
                return response;
            } catch (error) {
                console.error("Erreur lors de la mise à jour de l'avatar:", error);
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
        }
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