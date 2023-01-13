import { FunctionComponent } from "react";
import GameBoard from "./components/GameBoard";
import "./Playground.css";
import Points from "./components/Point";

interface PlaygroundProps {

}

const Playground: FunctionComponent<PlaygroundProps> = () => {
    return (
        <div className="page">
            <div className="board plate">
                <Points />
                <GameBoard />
                <div/>
            </div>
        </div>
    );
}

export default Playground;