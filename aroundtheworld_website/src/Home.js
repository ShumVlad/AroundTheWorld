import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import Map from "./Map";
import reportWebVitals from './reportWebVitals';
import Login from './Components/login/Login';

const Home = () => {
    return (
  <div>
    <Login /> 
    <Map />
  </div>
    )
}
export default Home;