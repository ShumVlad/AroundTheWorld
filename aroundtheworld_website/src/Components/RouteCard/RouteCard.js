import React, { useState } from 'react';
import GroupDetails from '../GroupDetails/GroupDetails';
import './routeCard.css';

const RouteCard = ({ data, onClick, onDelete }) => {
    const [showGroupDetails, setShowGroupDetails] = useState(false);

    const handleToggleGroupDetails = (e) => {
        e.stopPropagation();
        setShowGroupDetails(!showGroupDetails);
    };

    return (
        <div className="route-card">
            <div onClick={onClick}>
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
            <div>
                <button
                    onClick={handleToggleGroupDetails}
                    className="toggle-group-details-button"
                >
                    {showGroupDetails ? 'Hide Group Details' : 'Show Group Details'}
                </button>
                {showGroupDetails && (
                    <GroupDetails routeId={data.id} />
                )}
            </div>
        </div>
    );
};

export default RouteCard;