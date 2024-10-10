import { createStore } from 'vuex'
import { initApi } from '@/services/api'

const store = createStore({
    state: {
        api: null,
        // ... autres états
    },
    mutations: {
        setLoggedIn(state, value) {
            state.isLoggedIn = value
            localStorage.setItem('isLoggedIn', value)
        },
        setUser(state, user) {
            state.user = user
            if (user) {
                localStorage.setItem('user', JSON.stringify(user))
            } else {
                localStorage.removeItem('user')
            }
        },
        setRoles(state, roles) {
            state.roles = roles
            localStorage.setItem('roles', JSON.stringify(roles))
        },
        setToken(state, token) {
            state.token = token
            localStorage.setItem('token', token)
        },
        SET_API(state, api) {
            state.api = api;
        }
    },
    actions: {
        async login({ commit, state }, userData) {
            try {
                console.log("Tentative de connexion avec:", userData);
                if (!state.api) {
                    throw new Error("API non initialisée");
                }
                const response = await state.api.post('/home/login', userData);
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
                        localStorage.setItem('token', response.data.token);
                        state.api.defaults.headers.common['Authorization'] = `Bearer ${response.data.token}`;
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
            delete state.api.defaults.headers.common['Authorization'];
        },


        async creerCompteUtilisateur({ commit }, userData) {
            try {
                const response = await state.api.post('/utilisateurs/creer', userData);
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
                const response = await state.api.get('/utilisateurs/ObtentionInfoClient');
                console.log("Données reçues de l'API:", response.data);

                // Corriger l'URL de l'avatar
                if (response.data.photo) {
                    // Supprimer '/api' de l'URL de base si présent
                    const baseUrl = state.api.defaults.baseURL.replace('/api', '');
                    response.data.photo = `${baseUrl}/Avatars/${response.data.photo.split('/').pop()}`;
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
                const response = await state.api.put('/utilisateurs/miseajourinfoclient', userData);
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
                const response = await state.api.put('/utilisateurs/avatar', formData, {
                    headers: {
                        'Content-Type': 'multipart/form-data'
                    }
                });
                
                // Construire l'URL complète de l'avatar
                const fullAvatarUrl = `${state.api.defaults.baseURL.replace('/api', '')}${response.data.avatarUrl}`;
                
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
                const response = await state.api.get(`/utilisateurs/verifier-pseudonyme?pseudo=${encodeURIComponent(pseudo)}`);
                return response.data.disponible;
            } catch (error) {
                console.error("Erreur lors de la vérification du pseudonyme:", error);
                throw error;
            }
        },

        async verifierEmail({ commit }, email) {
            try {
                const response = await state.api.get(`/utilisateurs/verifier-email?email=${encodeURIComponent(email)}`);
                return response.data.disponible;
            } catch (error) {
                console.error("Erreur lors de la vérification de l'email:", error);
                throw error;
            }
        },

        async obtenirTousVendeurs({ commit }) {
            try {
                const response = await state.api.get('/vendeurs/tous');
                return response.data;
            } catch (error) {
                console.error("Erreur lors de la récupération de tous les vendeurs:", error);
                throw error;
            }
        },
        async creerVendeur({ commit }, vendeurData) {
            try {
                console.log("Données envoyées au serveur:", vendeurData); // Ajoutez cette ligne
                const response = await state.api.post('/vendeurs/creer', vendeurData);
                if (response.data.success) {
                    return { success: true, message: response.data.message };
                } else {
                    return { success: false, error: response.data.message };
                }
            } catch (error) {
                console.error("Erreur détaillée lors de la création du vendeur:", error.response || error);
                return { 
                    success: false, 
                    error: error.response?.data?.message || error.message || "Erreur lors de la création du vendeur",
                    details: error.response?.data // Ajoutez cette ligne pour obtenir plus de détails
                };
            }
        },
        async modifierVendeur({ commit }, vendeurData) {
            try {
                const response = await state.api.put(`/vendeurs/modifier/${vendeurData.id}`, vendeurData);
                if (response.data.success) {
                    return { success: true, message: response.data.message };
                } else {
                    return { success: false, error: response.data.message };
                }
            } catch (error) {
                console.error("Erreur lors de la modification du vendeur:", error);
                return { success: false, error: error.response?.data?.message || "Erreur lors de la modification du vendeur" };
            }
        },
        async obtenirVendeur({ commit }, id) {
            try {
                const response = await state.api.get(`/vendeurs/${id}`);
                return response.data;
            } catch (error) {
                console.error("Erreur lors de la récupération du vendeur:", error);
                throw error;
            }
        },
        async checkAuthStatus({ commit, state }) {
            const token = state.token || localStorage.getItem('token');
            console.log("Token trouvé :", token ? "Oui" : "Non");
            if (!token) {
                console.log("Aucun token trouvé, déconnexion de l'utilisateur");
                commit('setLoggedIn', false);
                commit('setUser', null);
                commit('setRoles', []);
                return;
            }

            // Attendre que la base URL soit définie
            while (!state.api.defaults.baseURL) {
                await new Promise(resolve => setTimeout(resolve, 100));
            }

            try {
                console.log("URL de la requête :", `${state.api.defaults.baseURL}/home/check-auth`);
                const response = await state.api.get('/home/check-auth');
                console.log("Réponse de check-auth:", response.data);
                if (response.data.isAuthenticated) {
                    commit('setLoggedIn', true);
                    commit('setUser', response.data.user);
                    commit('setRoles', response.data.roles);
                } else {
                    throw new Error('Non authentifié');
                }
            } catch (error) {
                console.error("Erreur détaillée lors de la vérification de l'authentification:", error.response || error);
                if (error.response && error.response.status === 401) {
                    commit('setLoggedIn', false);
                    commit('setUser', null);
                    commit('setRoles', []);
                    commit('setToken', null);
                    localStorage.removeItem('token');
                }
            }
        },
        async logout({ commit }) {
            try {
                // Appel à l'API pour invalider le token côté serveur (optionnel mais recommandé)
                await state.api.post('/api/home/logout');
            } catch (error) {
                console.error("Erreur lors de la déconnexion côté serveur:", error);
            } finally {
                // Nettoyage des données côté client
                commit('setLoggedIn', false);
                commit('setUser', null);
                commit('setRoles', []);
                commit('setToken', null);
                localStorage.removeItem('token');
                localStorage.removeItem('user');
                localStorage.removeItem('roles');
                localStorage.removeItem('isLoggedIn');
            }
        },
        async initializeStore({ commit, dispatch }) {
            const api = await initApi(() => store.state.token || localStorage.getItem('token'));
            commit('SET_API', api);
            await dispatch('checkAuthStatus');
        }
    },
    getters: {
        isAdmin: state => state.roles.includes('Administrateur'),
        isClient: state => state.roles.includes('Client'),
        currentUser: state => state.user,
        username: state => state.user ? state.user.pseudonym || state.user.username : 'USERNAME',
        avatarUrl: state => {
            console.log("État de l'utilisateur:", state.user);
            if (state.user && state.user.photo) {
                console.log("Photo de l'utilisateur:", state.user.photo);
                if (state.user.photo.startsWith('http')) {
                    return state.user.photo;
                } else {
                    const fullUrl = `${state.api.defaults.baseURL.replace('/api', '')}${state.user.photo}`;
                    console.log("URL complète de l'avatar:", fullUrl);
                    return fullUrl;
                }
            }
            console.log("Utilisation de l'avatar par défaut");
            return '/icons/Avatar.png';
        }
    }
});

// Initialiser le store immédiatement
store.dispatch('initializeStore');

export default store;