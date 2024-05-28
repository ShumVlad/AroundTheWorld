import React, { useEffect, useState } from 'react';
import GoogleMapReact from 'google-map-react';
import axios from 'axios';

const RentItemsMap = () => {
    const [locations, setLocations] = useState([]);

    useEffect(() => {
        const fetchLocations = async () => {
            try {
                const response = await axios.get('https://localhost:7160/api/Sensor/GetAll');
                setLocations(response.data);
            } catch (error) {
                console.error('Error fetching locations:', error);
            }
        };

        fetchLocations();
    }, []);

    return (
        <div style={{ height: '80vh' }}>
            <GoogleMapReact
                bootstrapURLKeys={{ key: 'AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0' }}
                defaultCenter={{ lat: 0, lng: 0 }}
                defaultZoom={2}
            >
                {locations.map(location => (
                    <Marker
                        key={location.id}
                        lat={location.latitude}
                        lng={location.longitude}
                    />
                ))}
            </GoogleMapReact>
        </div>
    );
};

const Marker = ({ lat, lng }) => (
    <div style={{ color: 'red', fontSize: '24px' }}>📍</div>
);

export default RentItemsMap;
