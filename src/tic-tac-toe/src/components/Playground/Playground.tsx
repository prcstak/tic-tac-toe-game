import { FunctionComponent } from "react";
import GameBoard from "../Items/GameBoard";
import "./Playground.css";
import Points from "./Point";

interface PlaygroundProps {

}

const Playground: FunctionComponent<PlaygroundProps> = () => {
    return (
        <div className="page">
            <div className="board">
                <Points />
                <GameBoard />
            </div>
            <div className="board">

            </div>
        </div>
    );
}

export default Playground;