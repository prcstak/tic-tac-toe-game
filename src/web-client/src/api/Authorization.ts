import httpClient from "./httpClient";

class Authorization {
    static async SignUp() {
        return httpClient.post("" , { }, {

        })
    }

    static async LogIn() {
        return httpClient.post("" , { }, {
            
        })
    }
}

export default Authorization;