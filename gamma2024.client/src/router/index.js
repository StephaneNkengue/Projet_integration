import { createRouter, createWebHistory } from 'vue-router'
import store from '@/store'
import api from '@/services/api'

import Inscription from '@/components/views/Inscription.vue'
import Connexion from '@/components/views/Connexion.vue'
import Accueil from '@/components/views/Accueil.vue'
import Contact from '@/components/views/Contact.vue'
import APropos from '@/components/views/APropos.vue'
import TableauDeBordInventaire from '@/components/views/TableauDeBordInventaire.vue'
import EncanPresent from '@/components/views/EncanPresent.vue'
import TousLesEncans from '@/components/views/TousLesEncans.vue'
import AffichageDetailsLot from '@/components/views/AffichageDetailsLot.vue'
import Modification from '@/components/views/Modification.vue'
import AffichageVendeur from '@/components/views/AffichageVendeur.vue'
import VendeurCreation from '@/components/views/VendeurCreation.vue'
import VendeurModification from '@/components/views/VendeurModification.vue'
import AccesNonAutorise from '@/components/views/AccesNonAutorise.vue'

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
        path: '/encanpresent',
        name: 'EncanPresent',
        component: EncanPresent
    },
    {
        path: '/touslesencans',
        name: "TousLesEncans",
        component: TousLesEncans
    },
    {
        path: '/affichagedetailslot',
        name: 'DetailsLot',
        component: AffichageDetailsLot
    },
    {
        path: '/inventaire',
        name: 'TableauDeBordInventaire',
        component: TableauDeBordInventaire
    },
    {
        path: '/modification',
        name: 'Modification',
        component: Modification,
        meta: { requiresAuth: true, requiredRole: 'Client' }
    },
    {
        path: '/affichagevendeurs',
        name: 'AffichageVendeurs',
        component: AffichageVendeur,
        meta: { requiresAuth: true, requiredRole: 'Administrateur' }
    },
    {
        path: '/vendeurcreation',
        name: 'VendeurCreation',
        component: VendeurCreation,
        meta: { requiresAuth: true, requiredRole: 'Administrateur' }
    },
    {
        path: '/vendeurmodification/:id',
        name: 'vendeurModification', 
        component: VendeurModification,
        props: true, // Ceci permet de passer l'ID comme prop au composant
        meta: { requiresAuth: true, requiredRole: 'Administrateur' }
    },
    {
        path: '/accesnonautorise',
        name: 'AccesNonAutorise',
        component: AccesNonAutorise
    }
]

const router = createRouter({
    history: createWebHistory(),
    routes,
    scrollBehavior(to, from, savedPosition) {
        return { top: 0 }
    }
})

// Attendez que l'URL de base de l'API soit déterminée avant de permettre la navigation
router.beforeEach(async (to, from, next) => {
    if (!api.defaults.baseURL) {
        await new Promise(resolve => {
            const checkBaseURL = setInterval(() => {
                if (api.defaults.baseURL) {
                    clearInterval(checkBaseURL)
                    resolve()
                }
            }, 100)
        })
    }

    const isLoggedIn = store.state.isLoggedIn
    const userRoles = store.state.roles

    if (to.meta.requiresAuth && !isLoggedIn) {
        next('/connexion') 
    } else if (to.meta.requiredRole && !userRoles.includes(to.meta.requiredRole)) {
        next({ name: 'AccesNonAutorise' })  
    } else {
        next()
    }
})

export default router
