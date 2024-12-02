<template>
  <div class="soiree-cloture" v-if="encan">
    <h2>Soirée de clôture - Encan #{{ encan.numeroEncan }}</h2>
    
    <div v-if="lotsRestants.length > 0">
      <!-- Les 15 premiers lots en tuiles -->
      <div class="row row-cols-lg-5 row-cols-md-3 row-cols-sm-2 row-cols-1 w-100 px-3">
        <div v-for="lot in premierQuinzeLots" 
             :key="lot.id" 
             class="col p-2 d-flex">
          <LotTuile :lotRecu="lot" :showDecompte="true" />
        </div>
      </div>

      <!-- Le reste en liste -->
      <div class="d-flex flex-column px-5 w-100">
        <div v-for="lot in autresLots" 
             :key="lot.id" 
             class="p-2">
          <LotListe :lotRecu="lot" :showDecompte="true" />
        </div>
      </div>
    </div>
    <div v-else>
      <h5 class="text-center">
        Tous les lots ont été vendus
      </h5>
    </div>
  </div>
</template>

<script setup>
import { ref, computed, onMounted, watch } from 'vue'
import { useStore } from 'vuex'
import { useRouter } from 'vue-router'
import LotTuile from '@/components/LotTuile.vue'
import LotListe from '@/components/LotListe.vue'

const store = useStore()
const router = useRouter()
const encan = computed(() => store.state.encanCourant)
const lots = computed(() => store.state.lots)

// Trier les lots par DateFinDecompteLot
const lotsTriesParTemps = computed(() => {
  return [...lots.value]
    .filter(lot => !lot.estVendu && new Date(lot.dateFinDecompteLot) > new Date())
    .sort((a, b) => {
      // D'abord par temps restant
      const tempsRestantA = new Date(a.dateFinDecompteLot) - new Date()
      const tempsRestantB = new Date(b.dateFinDecompteLot) - new Date()
      
      if (tempsRestantA === tempsRestantB) {
        // En cas d'égalité, trier par DateDebutDecompteLot
        return new Date(a.dateDebutDecompteLot) - new Date(b.dateDebutDecompteLot)
      }
      
      return tempsRestantA - tempsRestantB
    })
})

const premierQuinzeLots = computed(() => lotsTriesParTemps.value.slice(0, 15))
const autresLots = computed(() => lotsTriesParTemps.value.slice(15))

const lotsRestants = computed(() => 
  Object.values(lots.value).filter(lot => !lot.estVendu)
)

// Surveiller s'il reste des lots
watch(lotsRestants, async (newLots) => {
  if (newLots.length === 0) {
    // Rediriger vers EncanPresent qui affichera le message approprié
    await router.push({ name: 'EncanPresent' });
  }
})

onMounted(async () => {
  const type = await store.dispatch('verifierEtatEncan')
  if (!type || type !== 'soireeCloture') {
    router.push({ name: 'EncanPresent' })
  }

  // Écouter les événements SignalR pour les lots vendus
  if (store.state.connection) {
    store.state.connection.on("LotVendu", (lotId) => {
      store.commit('SET_LOT_VENDU', lotId)
      store.commit('REORGANISER_LOTS')
    })
  }
})
</script> 