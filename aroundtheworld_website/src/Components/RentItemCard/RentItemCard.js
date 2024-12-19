import React, { useContext, useState } from 'react';
import './rentItemCard.css';
import axios from 'axios';
import { AuthContext } from '../../context/AuthContext';

const RentItemCard = React.forwardRef((props, ref) => {
    const { authState } = useContext(AuthContext);
    const [showEmailInput, setShowEmailInput] = useState(false);
    const [userName, setUserName] = useState('');
    const [loading, setLoading] = useState(false);

    if (!props.data) {
        return null;
    }

    const handleRentButtonClick = () => {
        setShowEmailInput(true);
    };

    const handleStopRentingButtonClick = async () => {
        setLoading(true);

        try {
            props.data.userId = authState.userId;
            const response = await axios.put('https://localhost:7160/api/RentItem/StopRenting', props.data);

            if (response.status === 200) {
                alert('Renting stopped successfully');
                props.data.isRented = false;
                props.data.userName = '';

                setShowEmailInput(false);
                setUserName('');
            }
        } catch (error) {
            console.error('Error stopping renting:', error);
            alert('Error stopping renting');
        } finally {
            setLoading(false);
        }
    };

    const handleUserNameChange = (e) => {
        setUserName(e.target.value);
    };

    const handleSubmit = async () => {
        if (!userName) {
            alert('Please enter the traveler\'s name.');
            return;
        }

        props.data.userName = userName;

        setLoading(true);

        try {
            const response = await axios.put('https://localhost:7160/api/RentItem/RentItem', props.data);

            if (response.status === 200) {
                alert('Item rented successfully');
                props.data.isRented = true;
            }
        } catch (error) {
            console.error('Error renting item:', error);
            alert('Error renting item');
        } finally {
            setLoading(false);
            setShowEmailInput(false);
        }
    };

    return (
        <div ref={ref} className={`rent-item-card ${props.isUserItem ? 'user-item' : ''}`}>
            <div className="aroundTheWorld__rentItemCard_container">
                <div className="aroundTheWorld__rentItemCard_container-header">
                    {props.data.name}
                    {authState.userRole === "Worker" && (
                        <>
                            {props.data.isRented ? (
                                <button onClick={handleStopRentingButtonClick} disabled={loading}>
                                    {loading ? 'Processing...' : 'Stop Renting'}
                                </button>
                            ) : (
                                <>
                                    <button onClick={handleRentButtonClick}>Rent</button>
                                    {showEmailInput && (
                                        <div>
                                            <input
                                                type="text"
                                                placeholder="Traveler's Name"
                                                value={userName}
                                                onChange={handleUserNameChange}
                                            />
                                            <button onClick={handleSubmit} disabled={loading}>
                                                {loading ? 'Submitting...' : 'Submit'}
                                            </button>
                                        </div>
                                    )}
                                </>
                            )}
                        </>
                    )}
                </div>
                <div className="aroundTheWorld__rentItemCard_container-box">
                    <img
                        src={props.data.imageLink}
                        alt="Rent Item"
                        className="aroundTheWorld__rentItemCard_container-img"
                    />
                    <div className="aroundTheWorld__rentItemCard_container-textElements">
                        <div className="aroundTheWorld__rentItemCard_container-element">
                            {props.data.description}
                        </div>
                        {authState.userRole === "Worker" ? (
                            <div className="aroundTheWorld__rentItemCard_container-element">
                                {props.data.isRented
                                    ? `Rented by: ${props.data.userName}`
                                    : 'Is not rented'}
                            </div>
                        ) : (
                            <div />
                        )}
                    </div>
                </div>
            </div>
        </div>
    );
});

export default RentItemCard;
