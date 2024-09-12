import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '@/views/Home.vue'
import LoginPage from '@/views/Login.vue' // Assurez-vous d'avoir créé ce composant
import store from '@/store'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HomePage
  },
  {
    path: '/home',
    name: 'HomePage',
    component: HomePage
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginPage
  },
  {
    path: '/login',
    name: 'LoginPage',
    component: LoginPage
  }
  // ... autres routes
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

router.beforeEach((to, from, next) => {
  const isLoggedIn = store.state.isLoggedIn
  const requiredRole = to.meta.requiredRole

  if (to.matched.some(record => record.meta.requiresAuth)) {
    if (!isLoggedIn) {
      next('/login')
    } else if (requiredRole && !store.state.roles.includes(requiredRole)) {
      next('/unauthorized') // Créez une vue pour les accès non autorisés
    } else {
      next()
    }
  } else {
    next()
  }
})

export default router
