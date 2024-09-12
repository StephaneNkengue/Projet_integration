import { createRouter, createWebHistory } from 'vue-router'
import BarreDeNavigation from '@/components/BarreDeNavigation.vue'

const routes = [
    //{
    //    path: '/',
    //    name: 'Accueil',
    //    component: BarreDeNavigation
    //},
    //{
    //    path: '/accueil',
    //    name: 'AccueilPage',
    //    component: BarreDeNavigation
    //}
]

const router = createRouter({
    history: createWebHistory(),
    routes
})

export default router
