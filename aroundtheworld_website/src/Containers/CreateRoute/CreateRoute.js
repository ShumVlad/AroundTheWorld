import React, { useState, useEffect } from 'react';
import axios from 'axios';
import LocationCard from '../../Components/LocationCard/LocationCard'; // Ensure correct import path
import GoogleMapReact from 'google-map-react';
import MyLocationIcon from '@mui/icons-material/MyLocation';
import StarIcon from '@mui/icons-material/Star';
import Navbar from '../../Components/navbar/Navbar'

const CreateRoute = () => {
    const [formData, setFormData] = useState({
        name: '',
        description: '',
        companyId: '',
        chosenLocations: []
    });

    const [locations, setLocations] = useState([]);
    const [selectedLocation, setSelectedLocation] = useState(null);
    const [userLocation, setUserLocation] = useState(null);
    const [isMapReady, setIsMapReady] = useState(false);

    useEffect(() => {
        navigator.geolocation.getCurrentPosition(
            (position) => {
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

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        const routeData = {
            Name: formData.name,
            Description: formData.description,
            CompanyId: formData.companyId,
            IsFinished: false, // Assuming this should always be false for a new route
            Locations: formData.chosenLocations
        };
    
        try {
            const response = await axios.post('https://localhost:7160/api/Route/Create', routeData);
            if (response.status === 200) {
                alert('Route created successfully');
                getLocations();
            }
        } catch (error) {
            console.error('Error creating route:', error);
            alert('Error creating route');
        }
    };    
    
    const addLocationToChosen = (location) => {
        if (location && !formData.chosenLocations.some(loc => loc.id === location.id)) {
            const updatedChosenLocations = [...formData.chosenLocations, location];
            setFormData(prevState => ({
                ...prevState,
                chosenLocations: updatedChosenLocations
            }));
        }
    };
    
    const deleteLocationFromChosen = (location) => {
        const updatedChosenLocations = formData.chosenLocations.filter(loc => loc.id !== location.id);
        setFormData(prevState => ({
            ...prevState,
            chosenLocations: updatedChosenLocations
        }));
    };
    
    return (
        <div>
            <Navbar/>
            <form onSubmit={handleSubmit}>
                <div>
                    <div>
                        <label>Name</label>
                        <input type="text" name="name" value={formData.name} onChange={handleChange} />
                    </div>
                    <div>
                        <label>Description</label>
                        <input type="text" name="description" value={formData.description} onChange={handleChange} />
                    </div>
                    <div>
                        <label>Company key</label>
                        <input type="text" name="companyId" value={formData.companyId} onChange={handleChange} />
                    </div>
                </div>
                <div>
                    <div>
                        <label>Name</label>
                        <input type="text" name="name" value={formData.name} onChange={handleChange} />
                    </div>
                    
                    <div>
                        <label>Company key</label>
                        <input type="text" name="companyId" value={formData.companyId} onChange={handleChange} />
                    </div>
                </div>
                <button type="submit">Create Route</button>
            </form>
            <div style={{ height: '80vh' }}>
                {isMapReady && (
                    <GoogleMapReact
                        bootstrapURLKeys={{ key: 'AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0' }}
                        center={userLocation || { lat: 0, lng: 0 }}
                        defaultZoom={14}
                    >
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
                    <LocationCard data={selectedLocation} onAddLocation={addLocationToChosen} />
                </div>
            )}
            {formData.chosenLocations.length > 0 && (
                <div>
                    <h2>Chosen Locations</h2>
                    {formData.chosenLocations.map((location, index) => (
                        <LocationCard
                            key={location.id}
                            data={location}
                            index={index + 1}
                            onDeleteLocation={deleteLocationFromChosen}
                        />
                    ))}
                </div>
            )}
        </div>
    );
};

export default CreateRoute;
