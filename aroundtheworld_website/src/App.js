import React from 'react';
import { Outlet } from 'react-router-dom';
import Navbar from './Components/navbar/Navbar';
import Route from './Containers/Route/Route';
import Login from './Components/login/Login';
import { AuthProvider } from './context/AuthContext';
import MyRoutes from './Containers/MyRoutes/MyRoutes';

function App() {
    return (
        <AuthProvider>
            <div>
                <Navbar/>
                <Login />
                <Route />
                <Outlet />
                <MyRoutes />
            </div>
        </AuthProvider>
    );
}

export default App;
