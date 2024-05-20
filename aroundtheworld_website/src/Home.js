import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import Map from "./Components/Map/Map";
import reportWebVitals from './reportWebVitals';
import Locations from './Components/Locations/Locations';
import Navbar from './Components/navbar/Navbar'

const Home = () => {
    return (
  <div>
    <Navbar/>
    <Locations/>
  </div>
    )
}
export default Home;