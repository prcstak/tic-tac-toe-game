import axios from "axios";

export const BASE_URL = "http://localhost:5120"

const httpClient = axios.create({
    baseURL: BASE_URL
});

export default httpClient;