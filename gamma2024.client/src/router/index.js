import { createRouter, createWebHistory } from 'vue-router'
import HomePage from '@/views/Home.vue'
import LoginPage from '@/views/Login.vue' // Assurez-vous d'avoir créé ce composant

const routes = [
  {
    path: '/',
    name: 'Home',
    component: HomePage
  },
  {
    path: '/home',
    name: 'HomePage',
    component: HomePage
  },
  {
    path: '/login',
    name: 'Login',
    component: LoginPage
  },
  {
    path: '/login',
    name: 'LoginPage',
    component: LoginPage
  }
  // ... autres routes
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
