<template lang="">
  <div class="container">
    <table class="table table-striped">
      <thead>
        <tr>
          <th scope="col">Nom</th>
          <th scope="col">Prénom</th>
          <th scope="col">Pseudonyme</th>
          <th scope="col">Courriel</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="membre in tousLesMembres" :key="membre.id">
          <td>{{ membre.name }}</td>
          <td>{{ membre.firstName }}</td>
          <td>{{ membre.userName }}</td>
          <td>{{ membre.email }}</td>
          <td>
            <span class="me-2">
              <button class="btn btn-info">
                <img
                  src="/public/images/ice.png"
                  class="img-fluid"
                  alt="..."
                /></button
            ></span>
            <span
              ><button class="btn btn-warning">
                <img
                  src="/public/images/blocked.png"
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

const store = useStore();
let tousLesMembres = ref([]);

onMounted(async () => {
  const response = await store.dispatch("ObtenirTousLesMembres");
  if (response) {
    tousLesMembres.value = response;
  }
});
</script>

<style scoped>
img {
  width: 25px;
  height: 20px;
}
</style>
