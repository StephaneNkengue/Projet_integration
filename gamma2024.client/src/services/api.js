import axios from 'axios';

const apiUrls = [
    //'https://sqlinfocg.cegepgranby.qc.ca/2135621/api',
    'https://localhost:7206/api',
    'http://localhost:5121/api',
    'http://localhost:5122/api',
];

let workingBaseURL = null;

async function findWorkingApi() {
    for (const baseURL of apiUrls) {
        try {
            const response = await axios.get(`${baseURL}/health`, {
                timeout: 5000,
                withCredentials: true
            });
            if (response.status === 200) {
                console.log(`API accessible sur : ${baseURL}`);
                return baseURL;
            }
        } catch (error) {
            console.log(`Échec de connexion à ${baseURL}:`, error.message);
        }
    }
    throw new Error("Impossible de se connecter à l'API sur toutes les URLs testées");
}

const api = axios.create({
    baseURL: 'https://sqlinfocg.cegepgranby.qc.ca/2135621/api', // URL par défaut
    withCredentials: true
});

// Supprimez l'intercepteur qui utilise store ici

findWorkingApi()
    .then(baseURL => {
        api.defaults.baseURL = baseURL;
    })
    .catch(error => {
        console.error(error);
        // Fallback sur une URL par défaut si aucune ne fonctionne
        api.defaults.baseURL = 'https://sqlinfocg.cegepgranby.qc.ca/2135621/api';
    });

export default api;
