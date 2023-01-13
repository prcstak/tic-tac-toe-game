import { Outlet } from "react-router-dom";
import Authorization from "../api/Authorization";
import SignupPage from "../pages/SignUp/Signup";



const ProtectedRoutes = () => {
    return (
        Authorization.isLogged() ? <Outlet /> : <SignupPage />
    );
};

export default ProtectedRoutes;