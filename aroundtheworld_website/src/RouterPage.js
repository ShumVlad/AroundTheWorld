import React from "react";
import Home from './Home';
import Route from './Containers/Route/Route';
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import MyRoutes from "./Containers/MyRoutes/MyRoutes";
import CreateLocation from "./Containers/CreateLocation/CreateLocation";
import CreateRoute from './Containers/CreateRoute/CreateRoute';
import RentItems from './Containers/RentItems/RentItems'

const router = createBrowserRouter([
    {
        path: '/',
        element: <Home />,
    },
    {
        path: '/route-page/:routeId',
        element: <Route />,
    },
    {
        path: '/my-routes',
        element: <MyRoutes />
    },
    {
        path: '/create-location',
        element: <CreateLocation />
    },
    {
        path: '/create-route',
        element: <CreateRoute />
    },
    {
        path: '/rent-items',
        element: <RentItems />
    },
]);

const RouterProviderWrapper = ({ children }) => (
    <RouterProvider router={router}>{children}</RouterProvider>
);

export { RouterProviderWrapper };
