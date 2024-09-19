import axios from 'axios'

const api = axios.create({
  baseURL: 'http://localhost:5122/api', // L'URL de votre API ASP.NET
  withCredentials: true // Important pour la gestion des cookies
})

export default api
