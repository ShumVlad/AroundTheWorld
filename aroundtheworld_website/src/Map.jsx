import React from 'react';
import GoogleMapReact from 'google-map-react';
import MyLocationIcon from '@mui/icons-material/MyLocation';
import StarIcon from '@mui/icons-material/Star';

class Map extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            latitude: 51.5266853,
            longitude: 9.8994478,
        };
    }

    componentDidMount() {
        navigator.geolocation.getCurrentPosition(
            (position) => {
                console.log(position.coords);
                this.setState({
                    latitude: position.coords.latitude,
                    longitude: position.coords.longitude,
                });
            },
            (error) => {
                console.error("Error getting location: ", error);
            }
        );
    }

    renderMap() {
        return (
            <div>
                <h1>ASDASDASDAS</h1>
                <div style={{ height: '80vh' }}>
                    <GoogleMapReact
                        bootstrapURLKeys={{ key: 'AIzaSyAderMV7HrObn9AQegVS6M3rENgMe5yLu0' }}
                        defaultCenter={{
                            lat: this.state.latitude,
                            lng: this.state.longitude,
                        }}
                        defaultZoom={14}
                    >
                        {
                            this.props.locations.map((location) => {
                                return (
                                    <StarIcon
                                        key={location.id}
                                        lat={location.latitude}
                                        lng={location.longitude}
                                        color="secondary"
                                    />
                                );
                            })
                        }
                        <MyLocationIcon
                            color="primary"
                            lat={this.state.latitude}
                            lng={this.state.longitude}
                        />
                    </GoogleMapReact>
                </div>
            </div>
        );
    }

    render() {
        return (
            <div>
                {this.renderMap()}
            </div>
        );
    }
}

export default Map;
