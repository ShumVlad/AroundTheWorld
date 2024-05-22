import React, { useEffect, useState } from 'react';
import axios from 'axios';
import RouteCard from '../../Components/RouteCard/RouteCard';

const MyRoutes = () => {
    const [data, setData] = useState([]);
    const userId = '32a4f073-df07-4deb-b524-75312c4c9d14'; // Replace this with the actual user ID

    useEffect(() => {
        getData();
    }, []);

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
            {data.map((route) => (
                <RouteCard key={route.id} data={route} />
            ))}
        </div>
    );
};

export default MyRoutes;
