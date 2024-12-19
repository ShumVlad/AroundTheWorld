import React from 'react';
import HomeIcon from '@mui/icons-material/Home';
import './customMarker.css';

const CustomHotelMarker = ({ onClick }) => {
    return (
        <div className="custom-marker" onClick={onClick}>
            <HomeIcon color="secondary" />
        </div>
    );
};

export default CustomHotelMarker;
