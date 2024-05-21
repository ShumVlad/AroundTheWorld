import React, { useEffect, useState, useRef, useCallback } from 'react';
import axios from 'axios';
import Location from '../LocationCard/LocationCard';

const Home = () => {
    const [locations, setLocations] = useState([]);
    const [page, setPage] = useState(1);
    const [hasMore, setHasMore] = useState(true);
    const observer = useRef();

    useEffect(() => {
        fetchLocations(page);
    }, [page]);

    const fetchLocations = async (page) => {
        try {
            const response = await axios.get('https://localhost:7160/api/LocationRoute/GetLocations', { params: { page } });
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

    return (
        <div>
            {locations.map((location, index) => {
                if (locations.length === index + 1) {
                    return <Location ref={lastLocationElementRef} key={location.id} data={location} />;
                } else {
                    return <Location key={location.id} data={location} />;
                }
            })}
        </div>
    );
};

export default Home;
