import React, { useContext, useState, useEffect } from 'react';
import axios from 'axios';
import DatePicker from "react-datepicker";
import { useParams } from "react-router-dom";
import LocationCard from '../../Components/LocationCard/LocationCard'; // Ensure correct import path
import GoogleMapReact from 'google-map-react';
import MyLocationIcon from '@mui/icons-material/MyLocation';
import CustomStarMarker from '../../Markers/CustomStarMarker';
import CustomHotelMarker from '../../Markers/CustomHotelMarker';
import Navbar from '../../Components/navbar/Navbar'
import './ChangeRoute.css';

import { AuthContext } from '../../context/AuthContext';

const ChangeRoute = () => {
    const [formData, setFormData] = useState({
        name: '',
        description: '',
        companyId: '',
        chosenLocations: [],
        groupName: '',
    });
    const [dateTime, setDateTime] = useState(new Date());
    const [locations, setLocations] = useState([]);
    const [selectedLocation, setSelectedLocation] = useState(null);
    const [userLocation, setUserLocation] = useState(null);
    const [isMapReady, setIsMapReady] = useState(false);
    const { authState } = useContext(AuthContext);
    const { routeId } = useParams();

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
                setIsMapReady(true);
            }
        );
        getGroup(routeId);
        getLocations();
        getSelectedLocations();
        getRoute(routeId);
    }, []);
    const getGroup = async (routeId) => {
        try {
            const response = await axios.get('https://localhost:7160/api/Group/GetByRouteId', { params: { routeId } });
            if (response.status === 200) {
                setFormData((prevFormData) => ({
                    ...prevFormData,
                    groupName: response.data.name
                }));
            }
        } catch (error) {
            console.error('Error getting group data:', error);
            alert('Error getting group data');
        }
    };
    const getRoute = async (id) => {
        try {
            const response = await axios.get('https://localhost:7160/api/Route/Get', { params: { id } });
            if (response.status === 200) {
                setFormData((prevFormData) => ({
                    ...prevFormData,
                    name: response.data.name,
                    description: response.data.description,
                    companyId: response.data.companyId
                }));
                setDateTime(new Date(response.data.startDateTime));
            }
        } catch (error) {
            console.error('Error fetching locations:', error);
            //alert('Error fetching locations');
        }
    };

    const getSelectedLocations = async () => {
        try {
            const response = await axios.get('https://localhost:7160/api/LocationRoute/GetLocationsFromRoute', { params: { routeId }});
            if (response.status === 200) {
                console.log("selected")
                console.log(response.data)
                setFormData((prevFormData) => ({
                    ...prevFormData,
                    chosenLocations: response.data
                }));
            }
        } catch (error) {
            console.error('Error fetching locations:', error);
            alert('Error fetching locations');
        }
    };

    function DateTimeChangeHandler(value){
        setDateTime(value);
    }

    const getLocations = async () => {
        try {
            const response = await axios.get('https://localhost:7160/api/Location/GetAll');
            if (response.status === 200) {
                setLocations(response.data);
                console.log("allLoca")
                console.log(response.data)
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
        if (!(dateTime instanceof Date)) {
            alert('Please select a valid date and time.');
            return;
        }
        const formattedDate = dateTime.toISOString();
        const routeData = {
            Id: routeId,
            Name: formData.name,
            Description: formData.description,
            CompanyId: authState.companyId,
            IsFinished: false,
            StartDateTime: formattedDate,
            Locations: formData.chosenLocations,
            groupName: formData.groupName
        };

        try {
            const response = await axios.put('https://localhost:7160/api/Route/Update', routeData);
            if (response.status === 200) {
                alert('Route updated successfully');
            }
        } catch (error) {
            console.error('Error updating route:', error);
            alert('Error updating route');
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
            <form className='aroundTheWorld__createRoute-form' onSubmit={handleSubmit}>
                <div className="aroundTheWorld__createRoute-routeContainer">
                    <div>
                        <label>Route Name</label>
                        <input type="text" name="name" value={formData.name} onChange={handleChange} />
                    </div>
                    <div>
                        <label>Route Description</label>
                        <input type="text" name="description" value={formData.description} onChange={handleChange} />
                    </div>
                    <div className="aroundTheWorld__createRoute-groupContainer-element">
                        <label>Choose start date and time</label>
                        <DatePicker
                            selected={dateTime}
                            onChange={DateTimeChangeHandler}
                            showTimeSelect
                            dateFormat="dd MMM yyyy h:mm aa"
                            timeFormat="HH:mm"
                            timeIntervals={15}
                            timeCaption="Time"
                        />
                    </div>
                    <div>
                        <label>Group Name</label>
                        <input type="text" name="groupName" value={formData.groupName} onChange={handleChange} />
                    </div> 
                </div>
                <button type="submit">Update Route</button>
            </form>
            <div style={{ height: '80vh' }}>
                {isMapReady && (
                    <GoogleMapReact
                        bootstrapURLKeys={{ key: 'AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0' }}
                        center={userLocation || { lat: 0, lng: 0 }}
                        defaultZoom={14}
                    >
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

export default ChangeRoute;
