import React from 'react';
import GoogleMapReact from "google-map-react";

const Map = () => { {
     return (
    <div>
        <h1>ASDASDASDAS</h1>
        <div style={{height: "80vh"}}>
            <GoogleMapReact
                boostrapURLKeys={{ key: "AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0"}}
                defaultCenter={{
                    lat: 10.99835602,
                    lng: 77.01502627
                }}
                defaultZoom={14}
                ></GoogleMapReact>
        </div>
        </div>
     )
    }
}

export default Map