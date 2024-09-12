import { createRouter, createWebHistory } from 'vue-router'
import Accueil from '@/views/Accueil.vue'
import Inscription from '@/components/Inscription.vue'
import Connexion from '@/views/Connexion.vue' // Assurez-vous d'avoir créé ce composant

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
        path: '/connexion',
        name: 'Connexion',
        component: Connexion
    },
    {
        path: '/inscription',
        name: 'inscription',
        component: Inscription
    }
    // ... autres routes
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
