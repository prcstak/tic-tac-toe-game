import {HttpTransportType, HubConnection, HubConnectionBuilder} from "@microsoft/signalr";
import React, {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import authService from "../../api/authService";
import "./battlelist.css";
import playerService from "../../api/playerService";

interface props {
    connection: HubConnection | undefined,
    setConnection: any 
}

type PlayerInfo = {
    username : string;
    rating : number;
}


function BattleList(props: props) {
    const navigate = useNavigate();
    const options : Intl.DateTimeFormatOptions = { weekday: 'long', year: 'numeric', month: 'short', day: 'numeric', hour: "2-digit", minute: "2-digit" };

    const [showForm, setShowForm] = useState(false);
    const [showProfile, setShowProfile] = useState(false);
    const [playerInfo, setPlayerInfo] = useState<PlayerInfo>({username: "", rating : 0});
    const [rate, setRate] = useState(0);
    const [games, setGames] = useState([]);
   

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
                    onGamesListChange();
                    getGames();
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [props.connection]);

    const onRateChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setRate(Number(e.target.value));
    }
    
    const onGamesListChange = () => {
        props.connection?.on("Games", (l) => {
            setGames(l);
        });
    }
    
    
    
    const getGames = () => {
        props.connection?.send("GetGames");
    }
    
    const createGame = async () => {
        await props.connection?.send("CreateGame", rate);
        setShowForm(false)
    }
    
    const getMyRate = async () => {
        playerService.getRating().then(data => {
            setPlayerInfo(data);
            console.log(data)
        });
        setShowForm(false);
        setShowProfile(!showProfile);
    }

    return (
        <div className="main">
            <div className="plate head">
                <img src="xo-game-logo.png" ></img>
                <div className="rate-create">
                    <button onClick={() => getMyRate()} className="but">
                        Profile
                    </button>
                    {
                        showForm ? "" : <button onClick={() => {setShowProfile(false); setShowForm(true)}} className="but">
                            Create Room
                        </button>
                    }
                </div>
                <div />
                {
                    showProfile ? <div>
                        <h3>
                            {playerInfo.username}
                        </h3>
                        <h3>
                            {playerInfo.rating}
                        </h3>
                    </div> : ""
                }
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
                {
                    games.map((item : any, index : any) => {
                        return <div className="list-item" key={item["owner"]}>
                            <h3>{item["owner"]}</h3>
                            <h5>{new Date(Date.parse(item["dateCreated"])).toLocaleDateString('ru-ru', options)}</h5>
                            <button className="join" onClick={() => navigate(`/playground?room=${item["owner"]}`)}>
                                join
                            </button>
                        </div>
                    })
                }
                
            </div>
        </div>
    );
}

export default BattleList;