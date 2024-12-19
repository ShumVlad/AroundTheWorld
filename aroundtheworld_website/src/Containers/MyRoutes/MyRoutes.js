import React, { useEffect, useState, useContext } from 'react';
import axios from 'axios';
import RouteCard from '../../Components/RouteCard/RouteCard';
import { AuthContext } from '../../context/AuthContext';
import Navbar from '../../Components/navbar/Navbar';
import { useNavigate } from 'react-router-dom';
import './myRoutes.css';

const MyRoutes = () => {
    const [userRoutes, setUserRoutes] = useState([]);
    const [otherRoutes, setOtherRoutes] = useState([]);
    const { authState } = useContext(AuthContext);
    const userId = authState.userId;
    const userRole = authState.userRole;
    const companyId = authState.companyId;
    const navigate = useNavigate();

    useEffect(() => {
        if (userRole === 'User') {
            getUserRoutes(userId);
            getNotUserRoutes(userId);
        } else if (userRole === 'Worker' || userRole === 'Guide') {
            getCompanyRoutes(companyId);
        }
        else{
            getAllRoutes();
        }
    }, [userRole, userId, companyId]);

    const getUserRoutes = (userId) => {
        axios.get('https://localhost:7160/api/Route/GetUserRoutes', { params: { userId } })
            .then((result) => {
                setUserRoutes(result.data.map(route => ({ ...route, isMyRoute: true })));
            })
            .catch((error) => {
                console.error("There was an error fetching the user routes data!", error);
            });
    };

    const getAllRoutes = () => {
        axios.get('https://localhost:7160/api/Route/GetAll')
            .then((result) => {
                setOtherRoutes(result.data.map(route => ({ ...route, isMyRoute: true })));
            })
            .catch((error) => {
                console.error("There was an error fetching the user routes data!", error);
            });
            console.log(otherRoutes)
    };


    const getNotUserRoutes = (userId) => {
        axios.get('https://localhost:7160/api/Route/GetNotUserRoutes', { params: { userId } })
            .then((result) => {
                setOtherRoutes(result.data.map(route => ({ ...route, isMyRoute: false })));
            })
            .catch((error) => {
                console.error("There was an error fetching the other routes data!", error);
            });
    };

    const getCompanyRoutes = (companyId) => {
        axios.get('https://localhost:7160/api/Route/GetCompanyRoutes', { params: { companyId } })
            .then((result) => {
                setUserRoutes(result.data.map(route => ({ ...route, isMyRoute: true })));
            })
            .catch((error) => {
                console.error("There was an error fetching the company routes data!", error);
            });
    };

    const handleNavigate = (route) => {
        navigate(route);
    };

    const handleDelete = (id) => {
        axios.delete('https://localhost:7160/api/Route/Delete', { params: { id } })
            .then(() => {
                setUserRoutes(userRoutes.filter(route => route.id !== id));
                setOtherRoutes(otherRoutes.filter(route => route.id !== id));
            })
            .catch((error) => {
                console.error("There was an error deleting the route!", error);
            });
    };

    return (
        <div className="my-routes">
            <Navbar />
            {(userRole === 'Worker' || userRole === 'Guide') && (
                <button onClick={() => handleNavigate("/create-route")}>
                    Add Route
                </button>
            )}
            {userId ? (
                <>
                    <h2>My Routes</h2>
                    {userRoutes.length > 0 ? (
                        userRoutes.map((route) => (
                            <RouteCard
                                key={route.id}
                                data={route}
                                isMyRoute = {true}
                                onClick={() => handleNavigate(`/route-page/${route.id}`)}
                                onDelete={(userRole === 'Worker' || userRole === 'Guide') ? handleDelete : null}
                            />
                        ))
                    ) : (
                        <p>No routes found.</p>
                    )}
                    <h2>Other Routes</h2>
                    {otherRoutes.length > 0 ? (
                        otherRoutes.map((route) => (
                            <RouteCard
                                key={route.id}
                                data={route}
                                isMyRoute = {false}
                                onClick={() => handleNavigate(`/route-page/${route.id}`)}
                            />
                        ))
                    ) : (
                        <p>No other routes found.</p>
                    )}
                </>
            ) : (
                <>
                <h2>Other Routes</h2>
                {otherRoutes.length > 0 ? (
                    otherRoutes.map((route) => (
                        <RouteCard
                            key={route.id}
                            data={route}
                            isMyRoute = {false}
                            onClick={() => handleNavigate(`/route-page/${route.id}`)}
                        />
                    ))
                ) : (
                    <p>No other routes found.</p>
                )}
                </>
            )}
        </div>
    );
};

export default MyRoutes;
