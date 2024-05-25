import React from 'react';
import './routeCard.css'; // Adjust the import path if necessary

const RouteCard = ({ data, onClick }) => {
    return (
        <div className="route-card" onClick={onClick}>
            <h2>{data.name}</h2>
            <p>{data.companyName}</p>
            <p>{data.isFinished}</p>
        </div>
    );
};

export default RouteCard;
