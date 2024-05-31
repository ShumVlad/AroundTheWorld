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
                    {props.index && <span>{props.index}. </span>}
                    {props.data.name}
                    {props.onAddLocation && (
                            <button onClick={() => props.onAddLocation(props.data)}>Add Location</button>
                        )}
                        {props.onDeleteLocation && (
                            <button onClick={() => props.onDeleteLocation(props.data)}>Delete Location</button>
                        )}
                </div>
                <div className='aroundTheWorld__locationCard_container-box'>
                    <img src={props.data.imageUrl} alt="Location" style={{ maxWidth: '100%', maxHeight: '300px' }} />
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
