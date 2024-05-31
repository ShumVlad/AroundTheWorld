import React, { useEffect, useState } from 'react';
import Map from '../../Components/Map/Map';
import axios from "axios";
import { useParams } from "react-router-dom";
import Location from '../../Components/LocationCard/LocationCard'
import Navbar from '../../Components/navbar/Navbar'

const Route = () => {
    const { routeId } = useParams();
    const [locationsData, setLocationsData] = useState([]);
    
    useEffect(() => {
        getLocations();
    }, [routeId]);

    const getLocations = () => {
        console.log("1111")
        axios.get('https://localhost:7160/api/LocationRoute/GetLocationsFromRoute', { params: { routeId } })
            .then((result) => {
                setLocationsData(result.data);
                console.log(routeId)
            })
            .catch((error) => {
                console.error("There was an error fetching the locations data!", error);
            });
    };

    return (
        <div>
            <Navbar />
            <Map locations={locationsData} />
            <div>
            {locationsData.map((location, index) => {
                return <Location key={location.id} data={location} />;
            })}
            </div>
        </div>
    );
};

export default Route;
