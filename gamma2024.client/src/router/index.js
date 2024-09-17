import { createRouter, createWebHistory } from 'vue-router'
import BarreDeNavigation from '@/components/BarreDeNavigation.vue'
import Connexion from '@/components/Connexion.vue'

const routes = [
    //{
    //    path: '/',
    //    name: 'Accueil',
    //    component: Accueil
    //},
    //{
    //    path: '/accueil',
    //    name: 'AccueilPage',
    //    component: Accueil
    //},
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
