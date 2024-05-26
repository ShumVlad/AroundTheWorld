import React from 'react';
import './rentItemCard.css'

const RentItemCard = React.forwardRef((props, ref) => {
    if (!props.data) {
        return null;
    }

    return (
        <div ref={ref}>
            <div className="aroundTheWorld__rentItemCard_container">
                <div className='aroundTheWorld__locationCard_container-header'>
                    {props.data.name}
                </div>
                <div className='aroundTheWorld__rentItemCard_container-box'>
                    <img src={props.data.imageUrl} alt="RentItem" style={{ maxWidth: '100%', maxHeight: '300px' }} />
                    <div className="aroundTheWorld__rentItemCard_container-textElements">
                        <div className="aroundTheWorld__rentItemCard_container-element">
                            {props.data.description}
                        </div>
                        <div className="aroundTheWorld__rentItemCard_container-element">
                            {props.data.address}
                        </div>
                        <div className="aroundTheWorld__rentItemCard_container-element">
                            {props.data.type}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
});

export default RentItemCard;
