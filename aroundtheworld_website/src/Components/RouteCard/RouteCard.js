import React from 'react';

const RouteCard = React.forwardRef((props, ref) => {
    return (
        <div ref={ref}>
            <div className="aroundTheWorld__routeCard_container-element">
                {props.data.name}
            </div> 
            <div className="aroundTheWorld__routeCard_container-element">
                {props.data.description}
            </div>
            <div className="aroundTheWorld__routeCard_container-element">
                {props.data.companyName}
            </div>
            <div className="aroundTheWorld__routeCard_container-element">
                {props.data.isFinished}
            </div>
        </div>
    );
});

export default RouteCard;
