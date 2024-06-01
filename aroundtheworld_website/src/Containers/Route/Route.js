import React, { useEffect, useState } from 'react';
import Map from '../../Components/Map/Map';
import axios from "axios";
import { useParams } from "react-router-dom";
import Location from '../../Components/LocationCard/LocationCard'
import Navbar from '../../Components/navbar/Navbar';

const Route = () => {
    const { routeId } = useParams();
    const [locationsData, setLocationsData] = useState([]);
    const [userLocations, setUserLocations] = useState([]);

    useEffect(() => {
        getLocations();
        getUserLocations();
    }, [routeId]);

    const getLocations = async () => {
        try {
            const result = await axios.get('https://localhost:7160/api/LocationRoute/GetLocationsFromRoute', { params: { routeId } });
            setLocationsData(result.data);
        } catch (error) {
            console.error("There was an error fetching the locations data!", error);
        }
    };

    const getUserLocations = async () => {
        try {
            const result = await axios.get('https://localhost:7160/api/Group/GetUserLocations', { params: { routeId } });
            setUserLocations(result.data);
        } catch (error) {
            console.error("There was an error fetching the user locations data!", error);
        }
    };

    return (
        <div>
            <Navbar />
            <Map routeLocations={locationsData} userLocations={userLocations} />
            <div>
                {locationsData.map((location) => (
                    <Location key={location.id} data={location} />
                ))}
                {userLocations.map((userLocation) => (
                    <div key={userLocation.userId}>
                        <h3>{userLocation.userName}</h3>
                        <p>Lat: {userLocation.latitude}, Lng: {userLocation.longitude}</p>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Route;
