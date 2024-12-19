import React, { useState, useEffect } from 'react';
import axios from 'axios';
import GoogleMapReact from 'google-map-react';
import MyLocationIcon from '@mui/icons-material/MyLocation';
import LocationCard from '../../Components/LocationCard/LocationCard';
import Navbar from '../../Components/navbar/Navbar';
import CustomHotelMarker from '../../Markers/CustomHotelMarker';
import CustomStarMarker from '../../Markers/CustomStarMarker';
import './createLocation.css';

const CreateLocationPage = () => {
    const [formData, setFormData] = useState({
        name: '',
        description: '',
        address: '',
        type: '',
        latitude: '',
        longitude: '',
        imageUrl: '',
        chosenLocations: []
    });
    const [marker, setMarker] = useState(null);
    const [locations, setLocations] = useState([]);
    const [selectedLocation, setSelectedLocation] = useState(null);
    const [userLocation, setUserLocation] = useState(null);
    const [isMapReady, setIsMapReady] = useState(false);
    const [searchQuery, setSearchQuery] = useState('');
    const [center, setCenter] = useState({
        latitude: 51.5266853,
        longitude: 9.8994478
    });

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                setFormData(prevState => ({
                    ...prevState,
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude
                }));
                setUserLocation({
                    lat: position.coords.latitude,
                    lng: position.coords.longitude
                });
                setIsMapReady(true);
            },
            (error) => {
                console.error("Error getting location: ", error);
                setIsMapReady(true);
            }
        );
        getLocations();
    }, []);

    const getLocations = async () => {
        try {
            const response = await axios.get('https://localhost:7160/api/Location/GetAll');
            if (response.status === 200) {
                setLocations(response.data);
            }
        } catch (error) {
            console.error('Error fetching locations:', error);
            alert('Error fetching locations');
        }
    };

    const handleLocationClick = (location) => {
        setSelectedLocation(location);
    };

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleMapClick = ({ lat, lng }) => {
        setMarker({ lat, lng });
        setFormData({ ...formData, latitude: lat, longitude: lng });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        try {
            const response = await axios.post('https://localhost:7160/api/Location/Create', formData);
            if (response.status === 200) {
                alert('Location created successfully');
                getLocations();
            }
        } catch (error) {
            console.error('Error creating location:', error);
            alert('Error creating location');
        }
    };

    const handleSearch = async () => {
        if (!window.google || !window.google.maps) {
            alert('Google Maps JavaScript API not loaded properly.');
            return;
        }
        
        const geocoder = new window.google.maps.Geocoder();
        geocoder.geocode({ address: searchQuery }, (results, status) => {
            if (status === 'OK') {
                const location = results[0].geometry.location;
                setCenter({ latitude: location.lat(), longitude: location.lng() });
                setFormData({
                    ...formData,
                    latitude: location.lat(),
                    longitude: location.lng()
                });
            } else {
                alert('Geocode was not successful for the following reason: ' + status);
            }
        });
    };

    return (
        <div className='createLocation'>
            <Navbar />
            <form onSubmit={handleSubmit} className="form-container">
                <div>
                    <label>Name</label>
                    <input type="text" name="name" value={formData.name} onChange={handleChange} />
                </div>
                <div>
                    <label>Description</label>
                    <input type="text" name="description" value={formData.description} onChange={handleChange} />
                </div>
                <div>
                    <label>Address</label>
                    <input type="text" name="address" value={formData.address} onChange={handleChange} />
                </div>
                <div>
                    <label>Type</label>
                    <input type="text" name="type" value={formData.type} onChange={handleChange} />
                </div>
                <div>
                    <label>Image URL</label>
                    <input type="text" name="imageUrl" value={formData.imageUrl} onChange={handleChange} />
                </div>
                <div>
                    <label>Longitude</label>
                    <input type="text" name="longitude" value={formData.longitude} readOnly />
                    <label>Latitude</label>
                    <input type="text" name="latitude" value={formData.latitude} readOnly />
                </div>
                <button type="submit">Create Location</button>
            </form>
            {formData.imageUrl && (
                <div>
                    <img src={formData.imageUrl} alt="Location" className="image-preview" />
                </div>
            )}
            <div className="search-container">
                <input
                    type="text"
                    placeholder="Enter address"
                    value={searchQuery}
                    onChange={(e) => setSearchQuery(e.target.value)}
                />
                <button onClick={handleSearch} type="button">Search</button>
            </div>
            <div style={{ height: '80vh' }}>
                {isMapReady && (
                    <GoogleMapReact
                        bootstrapURLKeys={{ key: 'AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0' }}
                        center={{ lat: center.latitude, lng: center.longitude }}
                        zoom={14}
                        onClick={handleMapClick}
                    >
                        {marker && (
                            <CustomStarMarker
                                lat={marker.lat}
                                lng={marker.lng}
                            />
                        )}
                        {locations.map((location) => (
                            location.type === 'Hotel' ? (
                                <CustomHotelMarker
                                    key={location.id}
                                    lat={location.latitude}
                                    lng={location.longitude}
                                    onClick={() => handleLocationClick(location)}
                                />
                            ) : (
                                <CustomStarMarker
                                    key={location.id}
                                    lat={location.latitude}
                                    lng={location.longitude}
                                    onClick={() => handleLocationClick(location)}
                                />
                            )
                        ))}
                        {userLocation && (
                            <MyLocationIcon
                                lat={userLocation.lat}
                                lng={userLocation.lng}
                                color="primary"
                            />
                        )}
                    </GoogleMapReact>
                )}
            </div>
            {selectedLocation && (
                <div className='locationCard'>
                    <LocationCard data={selectedLocation} />
                </div>
            )}
        </div>
    );
};

export default CreateLocationPage;
