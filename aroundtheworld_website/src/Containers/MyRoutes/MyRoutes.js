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
    const companyId = authState.companyId;
    const navigate = useNavigate();

    useEffect(() => {
        if (userRole === 'User') {
            GetUserRoutes(userId);
        } else if (userRole === 'Worker' || userRole === 'Guide') {
            getCompanyRoutes(companyId);
        }
    }, [userRole, userId, companyId]);

    const GetUserRoutes = (userId) => {
        axios.get('https://localhost:7160/api/Route/GetUserRoutes', { params: { userId } })
            .then((result) => {
                setData(result.data);
            })
            .catch((error) => {
                console.error("There was an error fetching the routes data!", error);
            });
    };

    const getCompanyRoutes = (companyId) => {
        axios.get('https://localhost:7160/api/Route/GetCompanyRoutes', { params: { companyId } })
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

    const handleDelete = (id) => {
        console.log(id)
        axios.delete('https://localhost:7160/api/Route/Delete', { params: { id } })
            .then(() => {
                setData(data.filter(route => route.id !== id));
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
                data.length > 0 ? (
                    data.map((route) => (
                        <RouteCard
                            key={route.id}
                            data={route}
                            onClick={() => handleNavigate(`/route-page/${route.id}`)}
                            onDelete={(userRole === 'Worker' || userRole === 'Guide') ? handleDelete : null}
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
