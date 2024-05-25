import React from 'react';
import './locationCard.css';

const Location = React.forwardRef((props, ref) => {
    return (
        <div ref={ref}>
            <div className="aroundTheWorld__locationCard_container">
                <div className='aroundTheWorld__locationCard_container-header'>
                    {props.data.name}
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

export default Location;
