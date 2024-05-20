import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import Map from "./Components/Map/Map";
import reportWebVitals from './reportWebVitals';
import Login from './Components/login/Login';

const locationsData = [
  {
      id: "1",
      latitude: 51.5366865,
      longitude: 9.8995477,
  },
  {
      id: "2",
      latitude: 51.5266866,
      longitude: 9.8944579,
  }
];

const Home = () => {
    return (
  <div>
    <Login /> 
    <Map locations={locationsData}/>
  </div>
    )
}
export default Home;