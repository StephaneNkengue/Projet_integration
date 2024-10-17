import axios from "axios";

const apiUrls = [
    'https://localhost:7206/api',
    'http://localhost:5121/api',
    'http://localhost:5122/api',
    'https://sqlinfocg.cegepgranby.qc.ca/2135621/api',
];

let api = null;

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

export async function initApi(getToken) {
    if (api) return api;

    const baseURL = await findWorkingApi();
    api = axios.create({
        baseURL,
        withCredentials: true,
        headers: {
            'Accept': 'application/json'
        }
    });

    api.interceptors.request.use(config => {
        const token = getToken();
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    });

    return api;
}

export default { initApi };
