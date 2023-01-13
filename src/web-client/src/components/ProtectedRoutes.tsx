import { Outlet } from "react-router-dom";
import authService from "../api/authService";
import SignupPage from "../pages/SignUp/Signup";



const ProtectedRoutes = () => {
    return (
        authService.isLogged() ? <Outlet /> : <SignupPage />
    );
};

export default ProtectedRoutes;