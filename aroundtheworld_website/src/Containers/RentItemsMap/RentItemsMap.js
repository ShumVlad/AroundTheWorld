import React, { useEffect, useState } from 'react';
import GoogleMapReact from 'google-map-react';
import axios from 'axios';
import RentItemCard from '../../Components/RentItemCard/RentItemCard';
import Navbar from '../../Components/navbar/Navbar'

const RentItemsMap = () => {
    const [locations, setLocations] = useState([]);
    const [selectedRentItem, setSelectedRentItem] = useState(null);
    const [searchQuery, setSearchQuery] = useState('');
    const [center, setCenter] = useState({
        latitude: 51.5266853,
        longitude: 9.8994478
    });
    
    useEffect(() => {
        const fetchRentItems = async () => {
            try {
                const response = await axios.get('https://localhost:7160/api/RentItem/GetAll');
                setLocations(response.data);
            } catch (error) {
                console.error('Error fetching Rent Items:', error);
            }
        };

        fetchRentItems();
    }, []);

    const handleMarkerClick = (location) => {
        setSelectedRentItem(location);
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
            <Navbar/>
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
                <GoogleMapReact
                    bootstrapURLKeys={{ key: 'AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0' }}
                    defaultCenter={{ lat: center.latitude, lng: center.longitude}}
                    defaultZoom={14}
                >
                    {locations.map(location => (
                        <Marker
                            key={location.id}
                            lat={location.latitude}
                            lng={location.longitude}
                            onClick={() => handleMarkerClick(location)}
                        />
                    ))}
                </GoogleMapReact>
            </div>
            {selectedRentItem && (
                <div style={{ marginTop: '20px' }}>
                    <RentItemCard data={selectedRentItem} />
                </div>
            )}
        </div>
    );
};

const Marker = ({ lat, lng, onClick }) => (
    <div onClick={onClick} style={{ color: 'red', fontSize: '24px', cursor: 'pointer' }}>📍</div>
);

export default RentItemsMap;
