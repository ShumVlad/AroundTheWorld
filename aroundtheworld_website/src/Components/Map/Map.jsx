import React, { useState, useEffect } from 'react';
import GoogleMapReact from 'google-map-react';
import MyLocationIcon from '@mui/icons-material/MyLocation';
import StarIcon from '@mui/icons-material/Star';
import LocationCard from '../LocationCard/LocationCard';
import './Map.css';

// Custom Marker Component
const Marker = ({ children }) => children;

const Map = ({ routeLocations, userLocations }) => {
    const [center, setCenter] = useState({
        latitude: 51.5266853,
        longitude: 9.8994478
    });
    const [userPosition, setUserPosition] = useState({
        latitude: 51.5266854,
        longitude: 9.8994478
    });
    const [selectedLocation, setSelectedLocation] = useState(null);

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                setCenter({
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude,
                });
                setUserPosition({
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude,
                });
            },
            (error) => {
                console.error("Error getting location: ", error);
            }
        );

        const watchId = navigator.geolocation.watchPosition(
            (position) => {
                setUserPosition({
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude,
                });
            },
            (error) => {
                console.error("Error watching location: ", error);
            },
            { enableHighAccuracy: true, maximumAge: 10000, timeout: 5000 }
        );

        return () => navigator.geolocation.clearWatch(watchId);
    }, []);

    const handleLocationClick = (location) => {
        setSelectedLocation(location);
    };

    return (
        <div style={{ height: '80vh' }}>
            <GoogleMapReact
                bootstrapURLKeys={{ key: 'AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0' }}
                center={{ lat: center.latitude, lng: center.longitude }}
                defaultZoom={14}
            >
                {routeLocations.map((location) => (
                    <Marker
                        key={location.id}
                        lat={location.latitude}
                        lng={location.longitude}
                    >
                        <StarIcon
                            color="secondary"
                            onClick={() => handleLocationClick(location)}
                        />
                    </Marker>
                ))}
                {userLocations.map((userLocation) => (
                    <Marker
                        key={userLocation.userId}
                        lat={userLocation.latitude}
                        lng={userLocation.longitude}
                    >
                        <div className="green-dot" />
                    </Marker>
                ))}
                <Marker
                    lat={userPosition.latitude}
                    lng={userPosition.longitude}
                >
                    <MyLocationIcon color="primary" />
                </Marker>
            </GoogleMapReact>
            {selectedLocation && <LocationCard location={selectedLocation} />}
        </div>
    );
};

export default Map;
