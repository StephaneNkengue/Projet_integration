import { createRouter, createWebHistory } from 'vue-router'

import store from '@/store'
import Inscription from '@/components/views/Inscription.vue'
import Connexion from '@/components/views/Connexion.vue'
import Accueil from '@/components/views/Accueil.vue'
import Contact from '@/components/views/Contact.vue'
import APropos from '@/components/views/APropos.vue'
import Modification from '@/components/views/Modification.vue'

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
    },
    {
        path: '/modification',
        name: 'Modification',
        component: Modification,
        meta: { requiresAuth: true, requiredRole: 'CLIENT' }
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
            next('/unauthorized') // Crez une vue pour les accs non autoriss
        } else {
            next()
        }
    } else {
        next()
    }
})

export default router
