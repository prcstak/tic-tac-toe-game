import { FunctionComponent, useState } from "react";
import { useNavigate } from "react-router-dom";
import authService from "../../api/authService";

interface SignupPageProps {

}

type signupForm = {
    username: string;
    password: string;
    repeatPassword: string;
}

const SignupPage: FunctionComponent<SignupPageProps> = () => {
    const navigate = useNavigate();

    const [form, setForm] = useState<signupForm>({
        username: "",
        password: "",
        repeatPassword: ""
    });

    const [message, setMessage] = useState("");


    async function trySignUp(e: any): Promise<void> {
        e.preventDefault();
        if (form.password !== form.repeatPassword) {
            setMessage("Password didn't match");
            return;
        }

        setMessage("");
        authService.signup(form.username, form.password).then(() => {
            navigate("/login");
        }).catch((error) => {
            if (error.response) {
                    setMessage(error.response.data.error);
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

    const onChangePasswordRepeat = (e: React.ChangeEvent<HTMLInputElement>) => {
        form.repeatPassword = e.target.value;
        setForm(form)
    }

    return (
        <div className="plate">
            <div />
            <h2>Sign Up</h2>
            <form onSubmit={trySignUp}>
                <input type='text' onChange={onChangeUsername} />
                <input type='password' onChange={onChangePassword} />
                <input type='password' onChange={onChangePasswordRepeat} />
                <span>{message}</span>
                <button type="submit"
                    className="account-button">
                    Sign Up
                </button>
                <div />
            </form>
            <a onClick={() => navigate("/login")}>
                LogIn
            </a>
        </div>
    );
}

export default SignupPage;