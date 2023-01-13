import axios from "axios";

export const BASE_URL = "http://localhost:3000/"

const httpClient = axios.create({
    baseURL: BASE_URL
});

export default httpClient;