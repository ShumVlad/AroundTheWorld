import React, { useEffect, useState, useContext } from 'react';
import axios from 'axios';
import RentItemCard from '../../Components/RentItemCard/RentItemCard';
import { AuthContext } from '../../context/AuthContext';
import Navbar from '../../Components/navbar/Navbar';
import { useNavigate } from 'react-router-dom';

const RentItems = () => {
    const [data, setData] = useState([]);
    const { authState } = useContext(AuthContext);
    const userId = authState.userId;
    const navigate = useNavigate();

    useEffect(() => {
        getData();
    });

    const getData = () => {
        axios.get('https://localhost:7160/api/RentItem/GetAll')
            .then((result) => {
                setData(result.data);
            })
            .catch((error) => {
                console.error("There was an error fetching the RentItems data!", error);
            });
    };

    return (
        <div className="rent-items">
            <Navbar />
            {
                data.map((rentItem) => (
                    <RentItemCard
                        key={rentItem.id}
                        data={rentItem}
                    />
                    ))
            }
        </div>
    );
};

export default RentItems;
