import React from 'react';
import StarIcon from '@mui/icons-material/Star';
import './customMarker.css';

const CustomStarMarker = ({ order, onClick }) => {
    return (
        <div className="custom-marker" onClick={onClick}>
            <StarIcon color="secondary" />
            <div className="marker-order">{order}</div>
        </div>
    );
};

export default CustomStarMarker;
