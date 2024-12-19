import React, { useState, useEffect } from 'react';
import GoogleMapReact from 'google-map-react';
import MyLocationIcon from '@mui/icons-material/MyLocation';
import LocationCard from '../LocationCard/LocationCard';
import './Map.css';
import CustomStarMarker from '../../Markers/CustomStarMarker';
import CustomHotelMarker from '../../Markers/CustomHotelMarker';

const Marker = ({ children }) => children;

const Map = ({ routeLocations, userLocations}) => {
    const [center, setCenter] = useState({
        latitude: 51.5266853,
        longitude: 9.8994478
    });
    const [userPosition, setUserPosition] = useState({
        latitude: 51.5266854,
        longitude: 9.8994478
    });
    const [selectedLocation, setSelectedLocation] = useState(null);
    const [searchQuery, setSearchQuery] = useState('');
    const [isMapReady, setIsMapReady] = useState(false);
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
                setIsMapReady(true);
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

    const handleSearch = async () => {
        const geocoder = new window.google.maps.Geocoder();
        geocoder.geocode({ address: searchQuery }, (results, status) => {
            if (status === 'OK') {
                const location = results[0].geometry.location;
                setCenter({ latitude: location.lat(), longitude: location.lng() });
            } else {
                alert('Geocode was not successful for the following reason: ' + status);
            }
        });
    };

    return (
        <div>
            <div className="search-container">
                <input
                    type="text"
                    placeholder="Enter address"
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                />
                <button onClick={handleSearch}>Search</button>
            </div>
            <div style={{ height: '60vh' }}>
                {isMapReady && (<GoogleMapReact
                    bootstrapURLKeys={{ key: 'AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0' }}
                    center={{ lat: center.latitude, lng: center.longitude }}
                    defaultZoom={14}
                >
                    {routeLocations.map((location, index) => (
                        <Marker
                            key={location.id}
                            lat={location.latitude}
                            lng={location.longitude}
                        >
                            {location.type === 'Hotel' ? (
                                <CustomHotelMarker 
                                    onClick={() => handleLocationClick(location)} 
                                />
                            ) : (
                                <CustomStarMarker 
                                    key={location.id}
                                    order={location.order}
                                    color="primary"
                                    onClick={() => handleLocationClick(location)} 
                                />
                            )}
                        </Marker>
                    ))}
                    {userLocations.map((userLocation) => (
                        <Marker
                            key={userLocation.userId}
                            lat={userLocation.latitude}
                            lng={userLocation.longitude}
                        >
                            <div className={userLocation.userRole === "Guide" ? 'orange-dot' : 'blue-dot'} />
                        </Marker>
                    ))}
                    <Marker
                        lat={userPosition.latitude}
                        lng={userPosition.longitude}
                    >
                        <MyLocationIcon color="primary" />
                    </Marker>
                </GoogleMapReact>
                )}
                {selectedLocation && <LocationCard location={selectedLocation} />}
            </div>
        </div>
    );
};

export default Map;
