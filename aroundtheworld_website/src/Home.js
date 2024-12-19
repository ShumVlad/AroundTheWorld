import React from 'react';
import Navbar from './Components/navbar/Navbar';
import Locations from './Components/Locations/Locations';
import './home.css';

const Home = () => {
    return (
        <div className="home">
            <Navbar />
            <Locations />
        </div>
    );
};

export default Home;
