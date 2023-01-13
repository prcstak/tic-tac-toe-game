import { useNavigate } from "react-router-dom";
import "./battlelist.css";

function BattleList() {
    const navigate = useNavigate();
    return (
        <div>
            <div className="plate head">
                <img src="xo-game-logo.png" ></img>
                <div className="rate-create">
                    <button onClick={() => navigate("/login")} className="but">
                        Log In
                    </button>
                    <button onClick={() => navigate("/signup")} className="but">
                        Sign Up
                    </button>
                </div>
                <div />
            </div>
            <div className="plate list">
aa
            </div>
        </div>
    );
}

export default BattleList;