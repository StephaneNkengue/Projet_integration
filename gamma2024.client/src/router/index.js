import { createRouter, createWebHistory } from 'vue-router'

import store from '@/store'
import Inscription from '@/components/views/Inscription.vue'
import Connexion from '@/components/views/Connexion.vue'
import Accueil from '@/components/views/Accueil.vue'
import Contact from '@/components/views/Contact.vue'
import APropos from '@/components/views/APropos.vue'


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
        path: '/inscription',
        name: 'Inscription',
        component: Inscription
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes,
    scrollBehavior(to, from, savedPosition) {
        return { top: 0 }
    }
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
