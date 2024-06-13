import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import GroupDetails from '../GroupDetails/GroupDetails';
import './routeCard.css';
import { AuthContext } from '../../context/AuthContext';

const RouteCard = ({ data, onClick, onDelete }) => {
    const [showGroupDetails, setShowGroupDetails] = useState(false);
    const { authState } = useContext(AuthContext);

    const handleToggleGroupDetails = (e) => {
        e.stopPropagation();
        setShowGroupDetails(!showGroupDetails);
    };

    const navigate = useNavigate();

    const handleChangeClick = (e) => {
        e.stopPropagation();
        navigate(`/change-route/${data.id}`);
    };

    const startDateTime = new Date(data.startDateTime);
    const formattedDate = startDateTime.toISOString();

    return (
        <div className="route-card">
            <div onClick={onClick}>
                <h2>{data.name}</h2>
                <p>{data.companyName}</p>
                <p>{formattedDate}</p>
                <p>{data.isFinished ? 'Finished' : 'Not Finished'}</p>
            </div>
            {
                authState.userRole =='Worker'  ? <button onClick={handleChangeClick}>
                Change
            </button> : ''
            }
            
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
