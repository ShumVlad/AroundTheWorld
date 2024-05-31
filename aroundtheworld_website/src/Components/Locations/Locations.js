import React, { useEffect, useState, useRef, useCallback, useContext } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import Location from '../LocationCard/LocationCard';
import { AuthContext } from '../../context/AuthContext'; // Adjust the path as needed

const Locations = () => {
    const [locations, setLocations] = useState([]);
    const [page, setPage] = useState(1);
    const [hasMore, setHasMore] = useState(true);
    const observer = useRef();
    const { authState } = useContext(AuthContext);
    
    useEffect(() => {
        fetchLocations(page);
    }, [page]);

    const fetchLocations = async (page) => {
        try {
            const response = await axios.get('https://localhost:7160/api/Location/GetPaginated', { params: { page } });
            setLocations(prevLocations => [...prevLocations, ...response.data]);
            setHasMore(response.data.length > 0);
        } catch (error) {
            console.error('There was an error fetching the locations data!', error);
        }
    };

    const lastLocationElementRef = useCallback(node => {
        if (observer.current) observer.current.disconnect();
        observer.current = new IntersectionObserver(entries => {
            if (entries[0].isIntersecting && hasMore) {
                setPage(prevPage => prevPage + 1);
            }
        });
        if (node) observer.current.observe(node);
    }, [hasMore]);

    const navigate = useNavigate();

    const handleNavigate = (source) => {
        navigate(source);
    }
    
    return (
        <div>
            {(authState.userRole === 'Worker' || authState.userRole === 'Guide') && (
                <button onClick={() => {handleNavigate("/create-location")}}>
                    Add Location
                </button>
            )}
            {
            locations.map((location, index) => {
                if (locations.length === index + 1) {
                    return <Location ref={lastLocationElementRef} key={location.id} data={location} />;
                } else {
                    return <Location key={location.id} data={location} />;
                }
            })
            }
        </div>
    );
};

export default Locations;
