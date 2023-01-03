import { FunctionComponent } from "react";
import OPiece from "../Items/OPiece";
import XPiece from "../Items/XPiece";
import "./Points.css";

interface PointsProps {
    
}
 
const Points: FunctionComponent<PointsProps> = () => {
    return ( 
        <div className="points">
             <XPiece/>
             <h1>3</h1>
             <h1>2</h1>
             <OPiece/>
        </div>
     );
}
 
export default Points;