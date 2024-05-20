import React from 'react';

const Location = ({ location }) => {
    if (!location) return null;

    return (
        <div className="location-details">
            <h2>{location.name}</h2>
            <p>{location.type}</p>
            <p>{location.description}</p>
            <p>{location.address}</p>
            <p>Latitude: {location.latitude}</p>
            <p>Longitude: {location.longitude}</p>
        </div>
    );
};

export default Location;
