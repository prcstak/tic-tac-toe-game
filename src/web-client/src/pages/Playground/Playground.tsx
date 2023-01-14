import {FunctionComponent, useEffect, useState} from "react";
import GameBoard from "./components/GameBoard";
import "./Playground.css";
import Points from "./components/Point";
import {HttpTransportType, HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import XPiece from "../../components/Piece/XPiece";
import OPiece from "../../components/Piece/OPiece";
import authService from "../../api/authService";

interface props {
    connection: HubConnection | undefined,
    setConnection: any
}

enum cell {
    Empty,
    X,
    O
}

enum player {
    X,
    O
}

const Playground = (props: props) => {
    const urlParams = new URLSearchParams(window.location.search);
    
    const [board, setBoard] = useState<cell[]>([0, 0, 0, 0, 0, 0, 0, 0, 0]);
    const [turn, setTurn] = useState(player.X);
    const [usename, setUsername] = useState("");

    useEffect(() => {
        let newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5120/game', {
                accessTokenFactory: () => authService.getToken(),
                withCredentials: false,
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets
            })
            .withAutomaticReconnect()
            .build();

        props.setConnection(newConnection);
    }, []); 
    
    useEffect(() => {
        if (props.connection) {
            props.connection.start()
                .then(_ => {
                    console.log('Connected!');
                    props.connection?.send("JoinGame", urlParams.get("room"));
                    console.log("joined to " + urlParams.get("room"))
                    onGameChange();
                    onGameEnded();
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [props.connection]);

    const onGameChange = () => {
        props.connection?.on("OnTurn", (b, t) => {
            console.log(b)
            console.log(t)
            setBoard(b);
            setTurn(t);
        });
    }

    const onGameEnded = () => {
        props.connection?.on("GameEnded", (s, type) => {
            alert(cell[type] + "  win");
        });
    }
    
    const makeTurn = (id : number) => {
        console.log(id)
        props.connection?.send("MakeTurn", turn+1, id, urlParams.get("room"));
    }
    

    const check = (id : number) => {
        if (board[id] == cell.Empty) {
            makeTurn(id);
            //setBoard(board);
            //setTurn((turn + 1) % 2);
        }
    }

    const choosePiece = (id: number) => {
        if (board[id] == cell.Empty) {
            return turn == player.X ? 
                <div style={{opacity: 0.3}}><XPiece/></div> :
                <div style={{opacity: 0.3}}><OPiece/></div>
        }
        return board[id]==cell.X ?
            <XPiece/> :
            <OPiece/>
    }

    const buildBoard = () => {
       return <tbody>
       <tr>
           <td>
               <div onClick={() => check(0)}>
                   {choosePiece(0)}
               </div>
           </td>
           <td>
               <div onClick={() => check(1)}>
                   {choosePiece(1)}
               </div>
           </td>
           <td>
               <div onClick={() => check(2)}>
                   {choosePiece(2)}
               </div>
           </td>
       </tr>
       <tr>
           <td>
               <div onClick={() => check(3)}>
                   {choosePiece(3)}
               </div>
           </td>
           <td>
               <div onClick={() => check(4)}>
                   {choosePiece(4)}
               </div>
           </td>
           <td>
               <div onClick={() => check(5)}>
                   {choosePiece(5)}
               </div>
           </td>
       </tr>
       <tr>
           <td>
               <div onClick={() => check(6)}>
                   {choosePiece(6)}
               </div>
           </td>
           <td>
               <div onClick={() => check(7)}>
                   {choosePiece(7)}
               </div>
           </td>
           <td>
               <div onClick={() => check(8)}>
                   {choosePiece(8)}
               </div>
           </td>
       </tr>
       </tbody>
    }

    /*return board.map((row, i) => {
        return <tr key={`${i}`}>
            {row.map((piece, j) => {
                if (piece === 1) {
                    return <td key={`${j}`}><XPiece/></td>;
                }
                if (piece === 0) {
                    return <td key={`${j}`}><OPiece/></td>;
                }
                return <td key={`${j}`}>
                    <div key={`${j}.${i}`}
                         id={`${i}.${j}`}
                         onClick={(e) => check(e)}>
                        {
                            turn ? <div style={{opacity: 0.3}}><XPiece/></div> :
                                <div style={{opacity: 0.3}}><OPiece/></div>
                        }
                    </div>
                </td>;
            })}
        </tr>;
    });*/

    return (
        <div className="page">
            <div className="board plate">

                <div className="game">
                    <table>
                        {buildBoard()}
                    </table>
                </div>
            </div>
        </div>
    );
}

export default Playground;