<template>
  <div class="login">
    <h2>Connexion</h2>
    <form @submit.prevent="login">
      <div>
        <label for="email">Email :</label>
        <input type="email" id="email" v-model="email" required>
      </div>
      <div>
        <label for="password">Mot de passe :</label>
        <input type="password" id="password" v-model="password" required>
      </div>
      <button type="submit">Se connecter</button>
    </form>
  </div>
</template>

<script>
export default {
  data() {
    return {
      email: '',
      password: ''
    }
  },
  methods: {
    login() {
      // Utilisateur fictif pour le test
      const fakeUser = {
        email: 'test@example.com',
        password: 'password123'
      }

      if (this.email === fakeUser.email && this.password === fakeUser.password) {
        // Connexion réussie
        this.$store.dispatch('login', { email: this.email })
        alert('Connexion réussie !')
        this.$router.push('/') // Redirection vers la page d'accueil
      } else {
        // Échec de la connexion
        alert('Email ou mot de passe incorrect')
      }
    }
  }
}
</script>

<style scoped>
.login {
  max-width: 400px;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

h1 {
  text-align: center;
  margin-bottom: 20px;
}

.form-group {
  margin-bottom: 15px;
}

label {
  display: block;
  margin-bottom: 5px;
}

input {
  width: 100%;
  padding: 8px;
  box-sizing: border-box;
}

button {
  width: 100%;
  padding: 10px;
  background-color: #42b983;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

button:hover {
  background-color: #369f6b;
}
</style>
