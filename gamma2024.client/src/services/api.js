    import axios from 'axios';
import store from '@/store';

const apiUrls = [
    'https://localhost:7206/api',
    'http://localhost:5121/api',
    'http://localhost:5122/api',
    'https://sqlinfocg.cegepgranby.qc.ca/2135621/api',
];

const api = axios.create({
    withCredentials: true,
    headers: {
        'Accept': 'application/json'
    }
});

async function findWorkingApi() {
    const savedBaseURL = localStorage.getItem('apiBaseURL');
    if (savedBaseURL) {
        try {
            const response = await axios.get(`${savedBaseURL}/health`, { 
                timeout: 5000,
                withCredentials: true
            });
            if (response.status === 200) {
                console.log(`API accessible sur : ${savedBaseURL}`);
                return savedBaseURL;
            }
        } catch (error) {
            console.log(`Échec de connexion à l'URL sauvegardée ${savedBaseURL}:`, error.message);
        }
    }

    for (const baseURL of apiUrls) {
        try {
            const response = await axios.get(`${baseURL}/health`, { 
                timeout: 5000,
                withCredentials: true
            });
            if (response.status === 200) {
                console.log(`API accessible sur : ${baseURL}`);
                localStorage.setItem('apiBaseURL', baseURL);
                return baseURL;
            }
        } catch (error) {
            console.log(`Échec de connexion à ${baseURL}:`, error.message);
        }
    }
    throw new Error("Impossible de se connecter à l'API sur toutes les URLs testées");
}

findWorkingApi()
    .then(baseURL => {
        api.defaults.baseURL = baseURL;
        console.log("Base URL de l'API définie :", api.defaults.baseURL);
    })
    .catch(error => {
        console.error(error);
        api.defaults.baseURL = 'http://localhost:5121/api';
    });

api.interceptors.request.use(config => {
    const token = store.state.token || localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
        console.log("Token ajouté à la requête:", token);
    } else {
        console.log("Aucun token trouvé pour la requête");
    }
    return config;
});

export default api;
