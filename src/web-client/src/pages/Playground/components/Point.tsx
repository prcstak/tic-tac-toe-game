import { FunctionComponent } from "react";
import OPiece from "../../../components/Piece/OPiece";
import XPiece from "../../../components/Piece/XPiece";
import "./Points.css";

interface PointsProps {
    
}
 
const Points: FunctionComponent<PointsProps> = () => {
    return ( 
        <div className="points">
             <div style={{width: "6em"}}>
             <XPiece/>
             </div>
             <h1>3</h1>
             <h1>2</h1>
             <div style={{width: "6em"}}>
             <OPiece/>
             </div>
        </div>
     );
}
 
export default Points;