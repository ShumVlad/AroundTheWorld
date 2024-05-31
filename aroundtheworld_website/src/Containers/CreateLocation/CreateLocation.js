import React, { useState, useEffect } from 'react';
import axios from 'axios';
import GoogleMapReact from 'google-map-react';
import MyLocationIcon from '@mui/icons-material/MyLocation';
import StarIcon from '@mui/icons-material/Star';
import LocationCard from '../../Components/LocationCard/LocationCard';
import Navbar from '../../Components/navbar/Navbar'

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
                setIsMapReady(true); // Allow map to render with default center
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
            }
        } catch (error) {
            console.error('Error creating location:', error);
            alert('Error creating location');
        }
    };

    const addLocationToChosen = () => {
        if (selectedLocation && !formData.chosenLocations.some(loc => loc.id === selectedLocation.id)) {
            const updatedChosenLocations = [...formData.chosenLocations, selectedLocation];
            setFormData(prevState => ({
                ...prevState,
                chosenLocations: updatedChosenLocations
            }));
        }
    };

    return (
        <div>
            <Navbar/>
            <form onSubmit={handleSubmit}>
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
                <button type="submit">Create Location</button>
            </form>
            {formData.imageUrl && (
                <div>
                    <img src={formData.imageUrl} alt="Location" style={{ maxWidth: '100%', maxHeight: '300px' }} />
                </div>
            )}
            <div style={{ height: '80vh' }}>
                {isMapReady && (
                    <GoogleMapReact
                        bootstrapURLKeys={{ key: 'AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0' }}
                        center={userLocation || { lat: 0, lng: 0 }}
                        defaultZoom={14}
                        onClick={handleMapClick}
                    >
                        {marker && (
                            <StarIcon
                                lat={marker.lat}
                                lng={marker.lng}
                                color="secondary"
                            />
                        )}
                        {locations.map((location) => (
                            <StarIcon
                                key={location.id}
                                lat={location.latitude}
                                lng={location.longitude}
                                color={formData.chosenLocations.some(loc => loc.id === location.id) ? "primary" : "secondary"}
                                onClick={() => handleLocationClick(location)}
                            />
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
                <div>
                    <button onClick={addLocationToChosen}>Add Location</button>
                    <LocationCard data={selectedLocation} />
                </div>
            )}
        </div>
    );
};

export default CreateLocationPage;
