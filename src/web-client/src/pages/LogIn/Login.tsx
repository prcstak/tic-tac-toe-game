import { FunctionComponent, useState } from "react";
import { useNavigate } from "react-router-dom";
import authService from "../../api/authService";

interface LoginPageProps {
    
}

type loginForm = {
    username: string;
    password: string;
}
 
const LoginPage: FunctionComponent<LoginPageProps> = () => {
    const navigate = useNavigate();

    const [form, setForm] = useState<loginForm>({
        username: "",
        password: ""
    });

    const [message, setMessage] = useState("");

    async function tryLogIn(e : any): Promise<void> {
        e.preventDefault();
        authService.login(form.username, form.password).then(() => {
            navigate("/battlelist");
        }).catch((error) => {
            if (error.response) {
                setMessage("Incorrect username or password");
                console.log(error.message);
            }
            else {
                alert("Failed request");
                console.log(error);
            }
        });
        
    }

    const onChangeUsername = (e: React.ChangeEvent<HTMLInputElement>) => {
        form.username = e.target.value;
        setForm(form);
    }

    const onChangePassword = (e: React.ChangeEvent<HTMLInputElement>) => {
        form.password = e.target.value;
        setForm(form);
    }

    return ( 
        <div className="plate form">
            <div/>
            <h2>Log In</h2>
            <form onSubmit={tryLogIn}>
                <input type='text' onChange={onChangeUsername} />
                <input type='password' onChange={onChangePassword} />
                <span>{message}</span>
                <button type="submit"
                    className="account-button">
                    Log In
                </button>
                <div />
            </form>
        </div>
     );
}
 
export default LoginPage;