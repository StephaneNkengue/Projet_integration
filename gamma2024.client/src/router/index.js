import { createRouter, createWebHistory } from 'vue-router'
import store from '@/store'

import BarreDeNavigation from '@/components/BarreDeNavigation.vue'
import Connexion from '@/components/Connexion.vue'
import HomePage from '@/views/Home.vue'
import LoginPage from '@/views/Login.vue'
import Connexion from '@/components/Connexion.vue'
import BarreDeNavigation from '@/components/BarreDeNavigation.vue'

const routes = [
    {
        path: '/',
        name: 'Accueil',
        component: Accueil
    },
    {
        path: '/accueil',
        name: 'AccueilPage',
        component: Accueil
    },
    {
        path: '/contact',
        name: 'Contact',
        component: Contact
    },
    {
        path: '/apropos',
        name: 'APropos',
        component: APropos
    },
    {
        path: '/connexion',
        name: 'Connexion',
        component: Connexion
    },
    {
        //    path: '/',
        //    name: 'Accueil',
        //    component: BarreDeNavigation
        //},
        //{
        //    path: '/accueil',
        //    name: 'AccueilPage',
        //    component: BarreDeNavigation
        //},
        path: '/connexion',
        name: 'Connexion',
        component: Connexion
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
            next('/unauthorized') // Cr�ez une vue pour les acc�s non autoris�s
        } else {
            next()
        }
    } else {
        next()
    }
})

export default router
