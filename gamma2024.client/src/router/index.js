import { createRouter, createWebHistory } from 'vue-router'
import BarreDeNavigation from '@/components/BarreDeNavigation.vue'
import Connexion from '@/components/Connexion.vue'

const routes = [
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

export default router
