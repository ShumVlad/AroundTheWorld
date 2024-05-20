import React from 'react';
import ReactDOM from 'react-dom/client';
import Map from '../../Components/Map/Map';
import axios from "axios";

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
const [routeId, setrouteId] = useState('');

const getLocations = () => {
    axios.get('https://localhost:7172/api/Trip/GetAll', { params: { userId: userId } })
    .then((result) => {
        const dt = result.data;
        setData(result.data)
      })
}

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