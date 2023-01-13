import { FunctionComponent } from "react";
import { useNavigate } from "react-router-dom";
import Authorization from "../../api/Authorization";

interface LoginPageProps {
    
}
 
const LoginPage: FunctionComponent<LoginPageProps> = () => {
    const navigate = useNavigate();

    async function tryLogIn(): Promise<void> {

        Authorization.LogIn().then(() => {
            //...
            navigate("/battlelist");
        }).catch(() => {
            //...
        });
        
    }

    return ( 
        <div className="plate form">
            <div/>
            <h2>Log In</h2>
            <input type='text' />
            <input type='password'/>
            <button onClick={() => tryLogIn()} className="account-button">
                    Log In
                </button>
                <div/>
        </div>
     );
}
 
export default LoginPage;