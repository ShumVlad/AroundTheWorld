import React from 'react';

const Location = React.forwardRef((props, ref) => {
    return (
        <div ref={ref}>
            <div className="aroundTheWorld__location_container-element">
                {props.data.name}
            </div> 
            <div className="aroundTheWorld__location_container-element">
                {props.data.description}
            </div>
            <div className="aroundTheWorld__location_container-element">
                {props.data.address}
            </div>
            <div className="aroundTheWorld__location_container-element">
                {props.data.type}
            </div>
        </div>
    );
});

export default Location;
