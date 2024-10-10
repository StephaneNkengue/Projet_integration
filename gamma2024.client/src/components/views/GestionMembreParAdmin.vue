<template lang="">
  <div class="container">
    <table class="table table-striped">
      <thead class="fs-3 fw-bold">
        <tr>
          <th scope="col">Nom</th>
          <th scope="col">Prénom</th>
          <th scope="col">Pseudonyme</th>
          <th scope="col">Courriel</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody class="fs-6">
        <tr v-for="membre in tousLesMembres" :key="membre.id">
          <td>{{ membre.name }}</td>
          <td>{{ membre.firstName }}</td>
          <td>{{ membre.userName }}</td>
          <td>{{ membre.email }}</td>
          <td>
            <span class="me-3">
              <button class="btn btn-info">
                <img
                  src="/images/ice.png"
                  class="img-fluid"
                  alt="..."
                  @click="detailsDuMembre(membre.id)"
                /></button
            ></span>
            <span class="me-3"
              ><button class="btn btn-danger">
                <img
                  src="/images/blocked.png"
                  class="img-fluid"
                  alt="..."
                /></button
            ></span>
            <span
              ><button class="btn btn-success">
                <img
                  src="/images/blocked.png"
                  class="img-fluid"
                  alt="..."
                /></button
            ></span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import { useStore } from "vuex";
import { useRouter } from "vue-router";

const store = useStore();
const router = useRouter();
let tousLesMembres = ref([]);

onMounted(async () => {
  try {
    const response = await store.dispatch("ObtenirTousLesMembres");
    tousLesMembres.value = response;
  } catch (error) {
    console.error("Erreur lors de la récupération des memebres.");
  }
});

function detailsDuMembre(idMembre) {
  router.push({ name: "detailsMembre", params: { id: idMembre } });
}
</script>

<style scoped>
img {
  width: 25px;
  height: 20px;
}
</style>
