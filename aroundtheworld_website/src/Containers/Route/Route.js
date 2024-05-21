import React, { useEffect, useState } from 'react';
import Map from '../../Components/Map/Map';
import axios from "axios";
import { useParams } from "react-router-dom";

const Route = () => {
    const { routeId } = useParams();
    const [locationsData, setLocationsData] = useState([]);
    useEffect(() => {
        getLocations();
    }, [routeId]);

    const getLocations = () => {
        axios.get('https://localhost:7160/api/LocationRoute/GetLocationsFromRoute', { params: { routeId } })
            .then((result) => {
                setLocationsData(result.data);
            })
            .catch((error) => {
                console.error("There was an error fetching the locations data!", error);
            });
    };

    return (
        <div>
            <Map locations={locationsData} />
        </div>
    );
};

export default Route;
