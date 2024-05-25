
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import GoogleMapReact from 'google-map-react';
import MyLocationIcon from '@mui/icons-material/MyLocation';
import StarIcon from '@mui/icons-material/Star';

const CreateLocationPage = () => {
    const [formData, setFormData] = useState({
        name: '',
        description: '',
        address: '',
        type: '',
        latitude: '',
        longitude: '',
        imageUrl: ''
    });
    const [marker, setMarker] = useState(null);

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                setFormData(prevState => ({
                    ...prevState,
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude
                }));
            },
            (error) => {
                console.error("Error getting location: ", error);
            }
        );
    }, []);

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

    return (
        <div>
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
            <div style={{ height: '80vh' }}>
                <GoogleMapReact
                    bootstrapURLKeys={{ key: 'AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0' }}
                    defaultCenter={{
                        lat: formData.latitude,
                        lng: formData.longitude,
                    }}
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
                    <MyLocationIcon
                        lat={formData.latitude}
                        lng={formData.longitude}
                        color="primary"
                    />
                </GoogleMapReact>
            </div>
            {formData.imageUrl && (
                <div>
                    <h2>Preview Image:</h2>
                    <img src={formData.imageUrl} alt="Location" style={{ maxWidth: '100%', maxHeight: '300px' }} />
                </div>
            )}
        </div>
    );
};

export default CreateLocationPage;