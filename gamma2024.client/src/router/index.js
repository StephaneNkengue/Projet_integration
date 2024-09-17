import { createRouter, createWebHistory } from 'vue-router'
import BarreDeNavigation from '@/components/BarreDeNavigation.vue'
import Inscription from '@/components/inscription.vue'
import Connexion from '@/components/Connexion.vue'

const routes = [
    //{
    //    path: '/',
    //    name: 'Accueil',
    //    component: Accueil
    //},
    {
        path: '/connexion',
        name: 'Connexion',
        component: Connexion
    },
    {
        path: '/inscription',
        name: 'inscription',
        component: Inscription
    }
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
