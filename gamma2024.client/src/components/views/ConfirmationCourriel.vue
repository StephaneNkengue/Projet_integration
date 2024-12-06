<template>
  <div class="confirmation_email">
    <h1>Confirmation réussie !</h1>
    <p>
      Votre adresse e-mail a été confirmée avec succès. Vous pouvez maintenant
      vous connecter.
    </p>
    <router-link :to="{ name: 'Connexion' }">
      Aller à la page de connexion
    </router-link>
  </div>
</template>

<script></script>

<style scoped>
.confirmation_email {
  text-align: center;
  margin-top: 50px;
}
</style>
