import React, { useEffect, useState, useContext } from 'react';
import axios from "axios";
import { useParams } from "react-router-dom";
import Map from '../../Components/Map/Map';
import Location from '../../Components/LocationCard/LocationCard';
import Navbar from '../../Components/navbar/Navbar';
import { AuthContext } from '../../context/AuthContext';

const Route = () => {
    const { routeId } = useParams();
    const [locationsData, setLocationsData] = useState([]);
    const [userLocations, setUserLocations] = useState([]);
    const { authState } = useContext(AuthContext);

    useEffect(() => {
        getLocations();
        sendUserPosition();
        getUserLocations();

        const intervalId = setInterval(() => {
            sendUserPosition();
            getUserLocations();
        }, 30000);

        return () => clearInterval(intervalId);
    }, [routeId]);

    const getLocations = async () => {
        try {
            const result = await axios.get('https://localhost:7160/api/LocationRoute/GetLocationsFromRoute', { params: { routeId } });
            setLocationsData(result.data);
            console.log(result.data)
        } catch (error) {
            console.error("There was an error fetching the locations data!", error);
        }
    };

    const sendUserPosition = async () => {
        try {
            navigator.geolocation.getCurrentPosition(async (position) => {
                const userPosition = {
                    Id: 'a',
                    Latitude: position.coords.latitude,
                    Longitude: position.coords.longitude,
                    UserId: authState.userId
                };
                await axios.put('https://localhost:7160/api/UserPosition/Update', userPosition);
            });
        } catch (error) {
            console.error("There was an error sending the user's position!", error);
        }
    };

    const getUserLocations = async () => {
        try {
            const result = await axios.get('https://localhost:7160/api/UserGroup/GetUserLocations', { params: { groupId: routeId } });
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
