import { FunctionComponent } from "react";
import "./Piece.css";
import "./OPiece.css";

interface OPieceProps {
    
}
 
const OPiece: FunctionComponent<OPieceProps> = () => {
    return ( 
        <img
                    className="opiece"
                    src="/opiece.png"
                    onContextMenu={(e) => e.preventDefault()}
                    alt="o" />
     );
}
 
export default OPiece;