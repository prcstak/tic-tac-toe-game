import { FunctionComponent } from "react";
import "./Piece.css";

interface OPieceProps {
    
}
 
const OPiece: FunctionComponent<OPieceProps> = () => {
    return ( 
        <div>
            <img
                    className="piece"
                    src="/opiece.png"
                    onContextMenu={(e) => e.preventDefault()}
                    alt="o" />
        </div>
     );
}
 
export default OPiece;