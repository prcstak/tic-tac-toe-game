import { FunctionComponent } from "react";
import { Link, useNavigate } from "react-router-dom";
import "./StartPage.css";

interface StartPageProps {

}

const StartPage: FunctionComponent<StartPageProps> = () => {
    const navigate = useNavigate();
    return (
        <div className="board">
            <img
                src="/xo-game-logo.png"
                onContextMenu={(e) => e.preventDefault()}
                className="logo"
                alt="logo" />
            <h1>XO</h1>
            <h2>Tic Tac Toe Game</h2>
            <br />
            <div className="card">
                <button onClick={() => navigate("/login")} className="account-button">
                    Log In
                </button>
                <button onClick={() => navigate("/signup")} className="account-button">
                    Sign Up
                </button>
            </div></div>
    );
}

export default StartPage;