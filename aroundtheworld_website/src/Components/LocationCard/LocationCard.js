import React from 'react';
import './locationCard.css';

const LocationCard = React.forwardRef((props, ref) => {
    if (!props.data) {
        return null;
    }

    return (
        <div ref={ref}>
            <div className="aroundTheWorld__locationCard_container">
                <div className='aroundTheWorld__locationCard_container-header'>
                    {props.data.order != 0 && <span>{props.data.order}. </span>}
                    <div className="name">{props.data.name}</div>
                    <div className="buttons">
                        {props.onAddLocation && (
                            <button onClick={() => props.onAddLocation(props.data)}>Add Location</button>
                        )}
                        {props.onDeleteLocation && (
                            <button onClick={() => props.onDeleteLocation(props.data)}>Delete Location</button>
                        )}
                    </div>
                </div>
                <div className='aroundTheWorld__locationCard_container-box'>
                    <img
                        src={props.data.imageUrl}
                        alt="Location"
                        className="aroundTheWorld__locationCard_container-img"
                    />
                    <div className="aroundTheWorld__locationCard_container-textElements">
                        <div className="aroundTheWorld__locationCard_container-element">
                            {props.data.description}
                        </div>
                        <div className="aroundTheWorld__locationCard_container-element">
                            {props.data.address}
                        </div>
                        <div className="aroundTheWorld__locationCard_container-element">
                            {props.data.type}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
});

export default LocationCard;
