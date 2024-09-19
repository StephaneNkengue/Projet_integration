import { createStore } from 'vuex'
import api from '@/services/api'

export default createStore({
  state() {
    return {
      isLoggedIn: false,
      user: null,
      roles: []
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
    }
  },
  actions: {
    async login({ commit }, userData) {
      try {
        console.log('Tentative de connexion avec:', userData);
        const response = await api.post('/home/login', {
          emailOuPseudo: userData.emailOuPseudo,  // Modifié ici
          password: userData.password
        });
        console.log('Réponse du serveur:', response.data);
        if (response.data.message === "Connexion réussie") {
          commit('setLoggedIn', true);
          commit('setUser', {
            email: userData.emailOuPseudo,  // Modifié ici
            id: response.data.userId
          });
          commit('setRoles', response.data.roles);
          return { success: true, roles: response.data.roles };
        }
      } catch (error) {
        console.error("Erreur de connexion détaillée:", error.response);
        return { success: false, error: error.response?.data?.message || "Erreur de connexion" };
      }
    },
    logout({ commit }) {
      commit('setLoggedIn', false)
      commit('setUser', null)
      commit('setRoles', [])
    },
    async creerCompteUtilisateur({ commit }, userData) {
      try {
        console.log("Données envoyées au serveur:", userData); // Ajoutez cette ligne
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
    }
  },
  getters: {
    isAdmin: state => state.roles.includes('ADMINISTRATEUR'),
    isClient: state => state.roles.includes('CLIENT')
  }
})