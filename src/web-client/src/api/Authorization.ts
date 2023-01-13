import httpClient from "./httpClient";

class Authorization {
    static async SignUp() {
        return httpClient.post("" , { }, {

        }).then((response => {
            localStorage.setItem("token", response.data);
        }))
    }

    static async LogIn() {
        return httpClient.post("" , { }, {
            
        }).then((response => {
            localStorage.setItem("token", response.data);
        }))
    }

    static isLogged() {
        let token = localStorage.getItem("token");   
        if (token) {
            return true;
        }
        return false;
    }
}

export default Authorization;