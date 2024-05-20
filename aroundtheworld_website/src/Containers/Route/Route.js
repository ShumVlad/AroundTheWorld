import React from 'react';
import ReactDOM from 'react-dom/client';
import Map from '../../Components/Map/Map';

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

class Route extends React.Component {
    render(){
        return (
            <div>
                <Map locations={locationsData}/>
            </div>
        )
}
}
export default Route;