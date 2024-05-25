import React from 'react';
import './routeCard.css';

const RouteCard = React.forwardRef((props, ref) => {
    return (
        <div ref={ref} className="route-card">
            <div className="route-card-element">
                <strong>Name:</strong> {props.data.name}
            </div>
            <div className="route-card-element">
                <strong>Description:</strong> {props.data.description}
            </div>
            <div className="route-card-element">
                <strong>Company:</strong> {props.data.companyName}
            </div>
            <div className="route-card-element">
                <strong>Status:</strong> {props.data.isFinished ? "Finished" : "Ongoing"}
            </div>
        </div>
    );
});

export default RouteCard;
