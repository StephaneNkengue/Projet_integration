import { createStore } from 'vuex'

export default createStore({
  state() {
    return {
      isLoggedIn: false,
      user: null
    }
  },
  mutations: {
    setLoggedIn(state, value) {
      state.isLoggedIn = value
    },
    setUser(state, user) {
      state.user = user
    }
  },
  actions: {
    login({ commit }, user) {
      // Ici, vous feriez normalement un appel API
      commit('setLoggedIn', true)
      commit('setUser', user)
    },
    logout({ commit }) {
      commit('setLoggedIn', false)
      commit('setUser', null)
    }
  }
})