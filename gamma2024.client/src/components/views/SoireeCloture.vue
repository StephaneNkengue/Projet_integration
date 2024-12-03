<template>
  <div class="soiree-cloture" v-if="encan && lotsTriesParTemps.length > 0">
    <h2 class="text-center w-100">Soirée de clôture - Encan #{{ encan.numeroEncan }}</h2>
    
    <div v-if="lotsRestants.length > 0">
      <!-- Les 15 premiers lots en tuiles -->
      <div class="row row-cols-lg-5 row-cols-md-3 row-cols-sm-2 row-cols-1 w-100 px-3">
        <div v-for="lot in premierQuinzeLots" 
             :key="lot.id" 
             class="col p-2 d-flex">
          <LotTuile 
            :lotRecu="lot" 
            :showDecompte="true" 
          />
        </div>
      </div>

      <!-- Le reste en liste -->
      <div class="d-flex flex-column px-5 w-100">
        <div v-for="lot in autresLots" 
             :key="lot.id" 
             class="p-2">
          <LotListe 
            :lotRecu="lot" 
            :showDecompte="true" 
          />
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
import LotTuile from '@/components/views/LotTuile.vue'
import LotListe from '@/components/views/LotListe.vue'

const store = useStore()
const router = useRouter()
const encan = computed(() => store.state.encanCourant)

const lots = computed(() => {
  console.log("État actuel des lots dans le store:", store.state.lots)
  return store.state.lots || {}
})

const lotsTriesParTemps = computed(() => {
  console.log("Calcul des lots triés");
  return Object.values(lots.value)
    .filter(lot => {
      console.log(`Vérification lot ${lot.id}:`, {
        estVendu: lot.estVendu,
        tempsRestant: lot.dateFinDecompteLot ? 
          new Date(lot.dateFinDecompteLot) - new Date() : 'pas de temps'
      });
      return !lot.estVendu && 
             lot.dateFinDecompteLot && 
             new Date(lot.dateFinDecompteLot) > new Date();
    })
    .sort((a, b) => {
      return new Date(a.dateFinDecompteLot) - new Date(b.dateFinDecompteLot);
    });
})

const premierQuinzeLots = computed(() => lotsTriesParTemps.value.slice(0, 15))
const autresLots = computed(() => lotsTriesParTemps.value.slice(15))

const lotsRestants = computed(() => 
  Object.values(lots.value).filter(lot => !lot.estVendu)
)

// Si tous les lots sont vendus, rediriger vers EncanPresent
watch(lotsRestants, async (newLots) => {
  if (newLots.length === 0) {
    await router.push({ name: 'EncanPresent' })
  }
})

onMounted(async () => {
  console.log("SoireeCloture monté")
  const type = await store.dispatch('verifierEtatEncan')
  console.log("Type d'encan reçu:", type)
  console.log("Encan courant:", encan.value)
  
  if (!type || type !== 'soireeCloture') {
    console.log("Redirection vers EncanPresent car type incorrect")
    router.push({ name: 'EncanPresent' })
  }
})

// Ajouter un watch pour s'assurer que les lots sont chargés
watch(() => encan.value, async (newEncan) => {
  console.log("Nouvel encan détecté:", newEncan)
  if (newEncan && newEncan.id) {
    if (Object.keys(lots.value).length === 0) {
      console.log("Chargement des lots car aucun lot trouvé")
      await store.dispatch('verifierEtatEncan')
    }
  }
}, { immediate: true })

// Forcer la réactivité
watch(() => store.state.lots, () => {
  console.log("Changement détecté dans les lots");
}, { deep: true });
</script> 