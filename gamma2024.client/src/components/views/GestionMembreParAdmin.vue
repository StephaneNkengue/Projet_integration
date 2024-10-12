<template lang="">
  <div class="container">
    <table class="table table-striped mt-5">
      <thead class="fs-3 fw-bold">
        <tr>
          <th scope="col">Nom</th>
          <th scope="col">Prénom</th>
          <th scope="col">Pseudonyme</th>
          <th scope="col">Courriel</th>
          <th scope="col">Voir</th>
          <th scope="col">État</th>
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
          </td>
          <td>
            <span v-if="membre.estBloque"
              ><button class="btn btn-danger">
                <img
                  src="/images/Locked.png"
                  class="img-fluid"
                  alt="..."
                  @click="debloquerUnMembre(membre)"
                /></button
            ></span>
            <span v-else
              ><button class="btn btn-success">
                <img
                  src="/images/Unlocked.png"
                  class="img-fluid"
                  alt="..."
                  @click="bloquerUnMembre(membre)"
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
    updateData();
  } catch (error) {
    console.error("Erreur lors de la récupération des membres:", error);
  }
});

function detailsDuMembre(idMembre) {
  router.push({ name: "detailsMembre", params: { id: idMembre } });
}

const bloquerUnMembre = async (membre) => {
  const response = await store.dispatch("bloquerUnMembre", membre.id);
  updateData();
};

const debloquerUnMembre = async (membre) => {
  const response = await store.dispatch("debloquerUnMembre", membre.id);
  updateData();
};

const updateData = async function () {
  const response = await store.dispatch("ObtenirTousLesMembres");
  tousLesMembres.value = response.map((membre) => ({
    ...membre,
  }));
};
</script>

<style scoped>
img {
  width: 25px;
  height: 30px;
}
</style>
