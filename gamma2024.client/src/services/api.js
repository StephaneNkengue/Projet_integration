import axios from "axios";

export const initApi = (getToken) => {
  const api = axios.create({
    baseURL: `${import.meta.env.VITE_API_URL}${import.meta.env.VITE_BASE_URL}`,
    withCredentials: true,
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
  });

  // Intercepteur pour les requêtes
  api.interceptors.request.use(
    (config) => {
      const token = getToken();
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    (error) => {
      return Promise.reject(error);
    }
  );

  return api;
};

export default { initApi };
