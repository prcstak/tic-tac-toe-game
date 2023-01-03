import { FunctionComponent, useState } from "react";
import "./GameBoard.css";
import OPiece from "./OPiece";
import XPiece from "./XPiece";

interface GameBoardProps {

}

const GameBoard: FunctionComponent<GameBoardProps> = () => {
    const [board, setBoard] = useState([[-1, -1, -1], [-1, -1, -1], [-1, -1, -1]]);
    const [turn, setTurn] = useState(true);

    const check = (e: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
        let a: number = +e.currentTarget.id.split(".")[0];
        let b: number = +e.currentTarget.id.split(".")[1];
        board[a][b] = turn ? 1 : 0;
        setBoard(board);
        setTurn(!turn);
    }

    const buildBoard = () => {
        return board.map((row, i) => {
            return <tr key={`${i}`}>
                {row.map((piece, j) => {
                    if (piece === 1) {
                        return <td key={`${j}`}><XPiece /></td>;
                    }
                    if (piece === 0) {
                        return <td key={`${j}`}><OPiece /></td>;
                    }
                    return <td key={`${j}`}>
                        <div key={`${j}.${i}`}
                            className="xo"
                            id={`${i}.${j}`}
                            onClick={(e) => check(e)}>
                            {
                                turn ? "X" : "O"
                            }
                        </div>
                    </td>;
                })}
            </tr>;
        });
    }

    return (
        <div className="game">
            <table>
                <tbody>
                    {buildBoard()}
                </tbody>
            </table>
        </div>
    );
}

export default GameBoard;