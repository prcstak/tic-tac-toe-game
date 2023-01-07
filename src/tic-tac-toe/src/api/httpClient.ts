import axios from "axios";

export const BASE_URL = process.env.API_URL ?? "http://localhost:xxxx/"

const httpClient = axios.create({
    baseURL: BASE_URL
});

export default httpClient;