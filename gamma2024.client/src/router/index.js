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
import DetailsLot from '@/components/views/DetailsLot.vue'
import AffichageVendeur from '@/components/views/AffichageVendeur.vue'
import VendeurCreation from '@/components/views/VendeurCreation.vue'
import VendeurModification from '@/components/views/VendeurModification.vue'
import AccesNonAutorise from '@/components/views/AccesNonAutorise.vue'
import ModificationProfilUtilisateur from '@/components/views/ModificationProfilUtilisateur.vue'
import AffichageLots from '@/components/views/AffichageLots.vue'
import CreationLot from '@/components/views/CreationLot.vue'
import ModificationLot from '@/components/views/ModificationLot.vue'

const routes = [
    {
        path: '/',
        name: 'Accueil',
        component: Accueil,
        meta: { requiresAuth: false }
    },
    {
        path: '/accueil',
        name: 'AccueilPage',
        component: Accueil,
        meta: { requiresAuth: false }
    },
    {
        path: '/contact',
        name: 'Contact',
        component: Contact,
        meta: { requiresAuth: false }
    },
    {
        path: '/apropos',
        name: 'APropos',
        component: APropos,
        meta: { requiresAuth: false }
    },
    {
        path: '/connexion',
        name: 'Connexion',
        component: Connexion,
        meta: { requiresAuth: false }
    },
    {
        path: '/inscription',
        name: 'Inscription',
        component: Inscription,
        meta: { requiresAuth: false }
    },
    {
        path: '/encanpresent',
        name: 'EncanPresent',
        component: EncanPresent,
        meta: { requiresAuth: false }
    },
    {
        path: '/touslesencans',
        name: "TousLesEncans",
        component: TousLesEncans,
        meta: { requiresAuth: false }
    },
    {
        path: '/detailslot',
        name: 'DetailsLot',
        component: DetailsLot,
        meta: { requiresAuth: false }

    },
    {
        path: '/inventaire',
        name: 'TableauDeBordInventaire',
        component: TableauDeBordInventaire,
        meta: { requiresAuth: true, requiredRole: 'Administrateur' }
    },
    {
        path: '/modificationprofilutilisateur',
        name: 'ModificationProfilUtilisateur',
        component: () => ModificationProfilUtilisateur,
        meta: { requiresAuth: true, requiredRole: 'Client' },
        beforeEnter: async (to, from, next) => {
            if (store.state.isLoggedIn) {
            try {
                await store.dispatch('fetchClientInfo');
                next();
            } catch (error) {
                console.error("Erreur lors du chargement des informations client:", error);
                next('/error');
            }
            } else {
            next('/login');
            }
        }
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
        component: AccesNonAutorise,
        meta: { requiresAuth: false }
    },
    {
        path: '/affichagelots',
        name: 'AffichageLots',
        component: AffichageLots,
        meta: { requiresAuth: true, requiredRole: 'Administrateur' }
    },
    {
        path: '/lots/creation',
        name: 'CreationLot',
        component: CreationLot,
        meta: { requiresAuth: true, requiredRole: 'Administrateur' }
    },
    {
        path: '/lots/modification/:id',
        name: 'ModificationLot',
        component: ModificationLot,
        props: true,
        meta: { requiresAuth: true, requiredRole: 'Administrateur' }
    },
]

const router = createRouter({
    history: createWebHistory(),
    routes,
    scrollBehavior(to, from, savedPosition) {
        return { top: 0 }
    }
})

// Attendez que l'URL de base de l'API soit dtermine avant de permettre la navigation
router.beforeEach(async (to, from, next) => {
    if (!store.state.api) {
        await store.dispatch('initializeStore');
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

export default router;
