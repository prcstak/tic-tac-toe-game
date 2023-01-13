import httpClient from "./httpClient";

class authService {
    static async signup(username : string, password : string) {
        return httpClient.post("/Authentication/signup", {Username: username, Password: password}, {
            headers: {
                "Content-Type": "multipart/form-data",
              },
        }).then((response => {
            return response.data;
        }))
    }

    static async login(username : string, password : string) {
        return httpClient.post("/Authentication/login", {Username: username, Password: password}, {
            headers: {
                "Content-Type": "multipart/form-data",
              },
        }).then((response => {
            if (response.data.accessToken) {
                localStorage.setItem("token", JSON.stringify(response.data));
            }
            return response.data;
        }))
    }

    static async logout() {
        localStorage.removeItem("token");
    }

    static isLogged() {
        let token = localStorage.getItem("token");
        if (token) {
            return true;
        }
        return false;
    }

    static async getToken() {
        let token = localStorage.getItem('user');
        if (token) {
            return JSON.parse(token);
        }
        return null;
    }
}

export default authService;