import React from 'react';
import StarIcon from '@mui/icons-material/Star';

const CustomStarMarker = ({ color = "secondary", onClick, order }) => {
    return (
        <div style={{ position: 'relative', display: 'inline-block' }}>
            <StarIcon 
                color={color} 
                onClick={onClick} 
                style={{ cursor: 'pointer' }} 
            />
            <span style={{
                position: 'absolute',
                top: '50%',
                left: '50%',
                transform: 'translate(-50%, -50%)',
                color: 'white',
                fontWeight: 'bold'
            }}>
                {order}
            </span>
        </div>
    );
};

export default CustomStarMarker;
