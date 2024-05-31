import React, { useEffect, useState, useContext } from 'react';
import axios from 'axios';
import RouteCard from '../../Components/RouteCard/RouteCard';
import { AuthContext } from '../../context/AuthContext';
import Navbar from '../../Components/navbar/Navbar';
import { useNavigate } from 'react-router-dom';

const MyRoutes = () => {
    const [data, setData] = useState([]);
    const { authState } = useContext(AuthContext);
    const userId = authState.userId;
    const userRole = authState.userRole;
    const navigate = useNavigate();

    useEffect(() => {
        if (userRole == "User") {
            GetUserRoutes(userId);
        }
        else if(userRole == "Worker" || userRole == "Guide"){
            getCompanyRoutes(authState.companyId);
        }
    }, [userRole], [userId]);

    const GetUserRoutes = () => {
        axios.get('https://localhost:7160/api/Route/GetUserRoutes', { params: { userId } })
            .then((result) => {
                setData(result.data);
            })
            .catch((error) => {
                console.error("There was an error fetching the routes data!", error);
            });
    };

    const getCompanyRoutes = () => {
        axios.get('https://localhost:7160/api/Route/GetCompanyRoutes', { params: { userId } })
            .then((result) => {
                setData(result.data);
            })
            .catch((error) => {
                console.error("There was an error fetching the routes data!", error);
            });
    };

    const handleNavigate = (route) => {
        navigate(route);
    };

    return (
        <div className="my-routes">
            <Navbar />
            {(authState.userRole === 'Worker' || authState.userRole === 'Guide') && (
                <button onClick={() => {handleNavigate("/create-route")}}>
                    Add Route
                </button>
            )}
            {userId ? (
                data.length > 0 ? (
                    data.map((route) => (
                        <RouteCard
                            key={route.id}
                            data={route}
                            onClick={() => handleNavigate(`/route-page/${route.id}`)}
                        />
                    ))
                ) : (
                    <p>No routes found.</p>
                )
            ) : (
                <p>Please log in to view your routes.</p>
            )}
        </div>
    );
};

export default MyRoutes;
