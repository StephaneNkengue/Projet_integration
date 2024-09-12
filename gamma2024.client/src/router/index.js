import { createRouter, createWebHistory } from 'vue-router'
import BarreDeNavigation from '@/components/views/BarreDeNavigation.vue'
import Accueil from '@/components/views/Accueil.vue'

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
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
