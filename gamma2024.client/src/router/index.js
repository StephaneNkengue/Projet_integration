import { createRouter, createWebHistory } from "vue-router";

import store from "@/store";
import Inscription from "@/components/views/Inscription.vue";
import Connexion from "@/components/views/Connexion.vue";
import Accueil from "@/components/views/Accueil.vue";
import Contact from "@/components/views/Contact.vue";
import APropos from "@/components/views/APropos.vue";
import TableauDeBordInventaire from "@/components/views/TableauDeBordInventaire.vue";
import EncanPresent from "@/components/views/EncanPresent.vue";
import TousLesEncans from "@/components/views/TousLesEncans.vue";
import DetailsLot from "@/components/views/DetailsLot.vue";
import Modification from "@/components/views/Modification.vue";
import GestionMembreParAdmin from "@/components/views/GestionMembreParAdmin.vue";
import DetailsMembreParAdmin from "@/components/views/DetailsMembreParAdmin.vue";

const routes = [
  {
    path: "/",
    name: "Accueil",
    component: Accueil,
  },
  {
    path: "/accueil",
    name: "AccueilPage",
    component: Accueil,
  },
  {
    path: "/contact",
    name: "Contact",
    component: Contact,
  },
  {
    path: "/apropos",
    name: "APropos",
    component: APropos,
  },
  {
    path: "/connexion",
    name: "Connexion",
    component: Connexion,
  },
  {
    path: "/inscription",
    name: "Inscription",
    component: Inscription,
  },
  {
    path: "/encanpresent",
    name: "EncanPresent",
    component: EncanPresent,
  },
  {
    path: "/touslesencans",
    name: "TousLesEncans",
    component: TousLesEncans,
  },
  {
    path: "/detailslot",
    name: "DetailsLot",
    component: DetailsLot,
  },
  {
    path: "/inventaire",
    name: "Inventaire",
    component: TableauDeBordInventaire,
  },
  {
    path: "/modification",
    name: "Modification",
    component: Modification,
    meta: { requiresAuth: true, requiredRole: "Client" },
  },
  {
    path: "/gestionMembre",
    name: "GestionMembre",
    component: GestionMembreParAdmin,
    meta: { requiresAuth: true, requiredRole: "Administrateur" },
  },
  {
    path: "/detailsMembre/:id",
    name: "detailsMembre",
    component: DetailsMembreParAdmin,
    props: true,
    meta: { requiresAuth: true, requiredRole: "Administrateur" },
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
  scrollBehavior(to, from, savedPosition) {
    return { top: 0 };
  },
});

router.beforeEach((to, from, next) => {
  const isLoggedIn = store.state.isLoggedIn;
  const userRoles = store.state.roles;

  if (to.meta.requiresAuth && !isLoggedIn) {
    next("/connexion");
  } else if (
    to.meta.requiredRole &&
    !userRoles.includes(to.meta.requiredRole)
  ) {
    next("/unauthorized");
  } else {
    next();
  }
});

export default router;
