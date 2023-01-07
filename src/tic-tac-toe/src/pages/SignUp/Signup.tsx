import { FunctionComponent } from "react";
import { useNavigate } from "react-router-dom";

interface SignupPageProps {
    
}
 
const SignupPage: FunctionComponent<SignupPageProps> = () => {
    const navigate = useNavigate();
    return (  
        <div className="board">
            <button onClick={() => navigate("/playground")} className="account-button">
                    Play
                </button>
        </div>
    );
}
 
export default SignupPage;