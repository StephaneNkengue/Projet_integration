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

export default router
