import { FunctionComponent } from "react";
import { useNavigate } from "react-router-dom";
import Authorization from "../../api/Authorization";

interface SignupPageProps {
    
}
 
const SignupPage: FunctionComponent<SignupPageProps> = () => {
    const navigate = useNavigate();



    async function trySignUp(): Promise<void> {

        Authorization.SignUp().then(() => {
            //...
            navigate("/battlelist");
        }).catch(() => {
            //...
        });
        
    }

    return (  
        <div className="plate form">
            <div/>
            <h2>Sign Up</h2>
            <input type='text' />
            <input type='password'/> 
            <input type='password'/>
            <button onClick={() => trySignUp()} className="account-button">
                    Sign Up
                </button>
                <div/>
        </div>
    );
}
 
export default SignupPage;