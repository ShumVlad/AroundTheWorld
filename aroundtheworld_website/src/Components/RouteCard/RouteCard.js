import React from 'react';
import './routeCard.css';

const RouteCard = ({ data, onClick, onDelete }) => {
    return (
        <div className="route-card" onClick={onClick}>
            <h2>{data.name}</h2>
            <p>{data.companyName}</p>
            <p>{data.isFinished ? 'Finished' : 'Not Finished'}</p>
            {onDelete && (
                <button
                    onClick={(e) => {
                        e.stopPropagation();
                        onDelete(data.id);
                    }}
                    className="delete-button"
                >
                    Delete
                </button>
            )}
        </div>
    );
};

export default RouteCard;
