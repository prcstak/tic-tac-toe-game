import { FunctionComponent } from "react";
import { Link, useNavigate } from "react-router-dom";
import "./StartPage.css";

interface StartPageProps {

}

const StartPage: FunctionComponent<StartPageProps> = () => {
    const navigate = useNavigate();
    return (
        <div className="board plate">
            <div className="start">
                <img
                    src="/xo-game-logo.png"
                    onContextMenu={(e) => e.preventDefault()}
                    className="logo"
                    alt="logo" />
                <h2>Tic Tac Toe Game</h2>
                <div className="card">
                    <button onClick={() => navigate("/login")} className="account-button">
                        Log In
                    </button>
                    <button onClick={() => navigate("/signup")} className="account-button">
                        Sign Up
                    </button>
                </div>
                <h1/>
            </div>
        </div>
    );
}

export default StartPage;