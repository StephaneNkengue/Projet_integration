import axios from 'axios';

const apiUrls = [
    'https://localhost:7206/api',
    'http://localhost:5121/api',
    'http://localhost:5122/api'
];

let workingBaseURL = null;

async function findWorkingApi() {
    for (const baseURL of apiUrls) {
        try {
            const response = await axios.get(`${baseURL}/health`, { timeout: 5000 });
            if (response.status === 200) {
                console.log(`API accessible sur : ${baseURL}`);
                workingBaseURL = baseURL;
                return baseURL;
            }
        } catch (error) {
            console.log(`Échec de connexion à ${baseURL}:`, error.message);
        }
    }
    throw new Error("Impossible de se connecter à l'API sur toutes les URLs testées");
}

const api = axios.create({
    baseURL: 'http://localhost:5122/api', // URL par défaut
    withCredentials: true
});

findWorkingApi()
    .then(baseURL => {
        api.defaults.baseURL = baseURL;
    })
    .catch(error => {
        console.error(error);
        // Fallback sur une URL par défaut si aucune ne fonctionne
        api.defaults.baseURL = 'http://localhost:5122/api';
    });

export default {
    get: (...args) => api.get(...args),
    post: (...args) => api.post(...args),
    put: (...args) => api.put(...args),
    delete: (...args) => api.delete(...args)
};
