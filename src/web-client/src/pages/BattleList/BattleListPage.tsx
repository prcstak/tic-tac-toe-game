import { HttpTransportType, HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import authService from "../../api/authService";
import "./battlelist.css";

interface props {
    connection: HubConnection | undefined,
    setConnection: any 
}

function BattleList(props: props) {
    const navigate = useNavigate();

    const [showForm, setShowForm] = useState(false);
    const [rate, setRate] = useState(0);

    useEffect(() => {
        let newConnection = new HubConnectionBuilder()
            .withUrl('http://localhost:5120/game', {
                accessTokenFactory: () => authService.getToken(),
                withCredentials: true,
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
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [props.connection]);

    const onRateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setRate(Number(e.target.value));
    }
    const createGame = async () => {
        await props.connection?.send("CreateGame", rate);
        setShowForm(false)
    }

    return (
        <div className="main">
            <div className="plate head">
                <img src="xo-game-logo.png" ></img>
                <div className="rate-create">
                    <button onClick={() => { }} className="but">
                        Profile
                    </button>
                    {
                        showForm ? "" : <button onClick={() => setShowForm(true)} className="but">
                            Create Room
                        </button>
                    }
                </div>
                <div />
                {
                    showForm ?
                        <form onSubmit={createGame}>
                            <input type='text' onChange={onRateChange} />
                            <button type="submit"
                                className="account-button">
                                Create Game
                            </button>
                            <div />
                        </form> : ""
                }
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