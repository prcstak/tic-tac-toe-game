import { useNavigate } from "react-router-dom";
import "./battlelist.css";

function BattleList() {
    const navigate = useNavigate();


    return (
        <div className="main">
            <div className="plate head">
                <img src="xo-game-logo.png" ></img>
                <div className="rate-create">
                    <button onClick={() => {}} className="but">
                        Profile
                    </button>
                    <button onClick={() => {}} className="but">
                        Create Room
                    </button>
                </div>
                <div />
            </div>
            <div className="plate list">
                    <div className="list-item">
                        <h3>username</h3>
                        <button className="join">
                            join
                        </button>
                    </div>
            </div>
        </div>
    );
}

export default BattleList;