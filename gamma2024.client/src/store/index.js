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
        console.log('Tentative de connexion avec:', userData); // Log des données envoyées
        const response = await api.post('/api/home/login', {
          email: userData.email,
          password: userData.password
        })
        console.log('Réponse du serveur:', response.data); // Log de la réponse du serveur
        if (response.data.message === "Connexion réussie") {
          commit('setLoggedIn', true)
          commit('setUser', {
            email: userData.email,
            id: response.data.userId
          })
          commit('setRoles', response.data.roles)
          return { success: true, roles: response.data.roles }
        }
      } catch (error) {
        console.error("Erreur de connexion détaillée:", error.response); // Log détaillé de l'erreur
        return { success: false, error: error.response?.data?.message || "Erreur de connexion" }
      }
    },
    logout({ commit }) {
      commit('setLoggedIn', false)
      commit('setUser', null)
      commit('setRoles', [])
    }
  },
  getters: {
    isAdmin: state => state.roles.includes('ADMINISTRATEUR'),
    isClient: state => state.roles.includes('CLIENT')
  }
})