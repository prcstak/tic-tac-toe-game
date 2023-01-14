import httpClient from "./httpClient";
import authHeader from "./authHeader";


class playerService{
    static async getRating() {
        return httpClient.get("/Player/rating", {
            headers: authHeader()
        })
            .then(response => response.data)
    }
}

export default playerService;