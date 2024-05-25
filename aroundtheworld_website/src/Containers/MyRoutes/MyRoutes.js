import React, { useEffect, useState, useContext } from 'react';
import axios from 'axios';
import RouteCard from '../../Components/RouteCard/RouteCard';
import { AuthContext } from '../../context/AuthContext';
import Navbar from '../../Components/navbar/Navbar';

const MyRoutes = () => {
    const [data, setData] = useState([]);
    const { authState } = useContext(AuthContext);
    const userId = authState.userId;

    useEffect(() => {
        if (userId) {
            getData();
        }
    }, [userId]);

    const getData = () => {
        axios.get('https://localhost:7160/api/Route/GetMyRoutes', { params: { userId } })
            .then((result) => {
                setData(result.data);
            })
            .catch((error) => {
                console.error("There was an error fetching the routes data!", error);
            });
    };

    return (
        <div className="my-routes">
            <Navbar />
            {userId ? (
                data.length > 0 ? (
                    data.map((route) => (
                        <RouteCard key={route.id} data={route} />
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
