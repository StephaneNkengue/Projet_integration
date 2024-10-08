<template lang="">
  <div>
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
        <tr v-for="membre in tousLesMembres" :key="membre.pseudonym">
          <td>{{ membre.name }}</td>
          <td>{{ membre.firstName }}</td>
          <td>{{ membre.pseudonym }}</td>
          <td>{{ membre.email }}</td>
          <td>
            <span></span>
            <span></span>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script setup>
import { ref } from "vue";
import { useStore } from "vuex";

const store = useStore();

let tousLesMembres = ref([]);
const response = await store.dispatch("ObtenirtousLesMembres");
tousLesMembres.value = response.data;
</script>

<style scoped></style>
