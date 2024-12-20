import React from "react";
import Home from './Home';
import Route from './Containers/Route/Route';
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import MyRoutes from "./Containers/MyRoutes/MyRoutes";
import CreateLocationPage from "./Containers/CreateLocation/CreateLocation";
import CreateRoute from './Containers/CreateRoute/CreateRoute';
import RentItems from './Containers/RentItems/RentItems';
import RegisterCompany from "./Containers/RegisterCompany/RegisterCompany";
import RentItemsMap from './Containers/RentItemsMap/RentItemsMap';
import CompanyProfile from './Containers/CompanyProfile/CompanyProfile';
import ChangeRoute from "./Containers/ChangeRoute/ChangeRoute";

const router = createBrowserRouter([
    {
        path: '/',
        element: <Home />,
    },
    {
        path: '/change-route/:routeId',
        element: <ChangeRoute/>
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
        element: <CreateLocationPage />
    },
    {
        path: '/create-route',
        element: <CreateRoute />
    },
    {
        path: '/rent-items',
        element: <RentItems />
    },
    {
        path: '/register-company',
        element: <RegisterCompany />
    },
    {
        path: '/rent-items-map',
        element: <RentItemsMap />
    }, 
    {
        path: '/company/:companyId',
        element: <CompanyProfile/>
    },    
]);

const RouterProviderWrapper = ({ children }) => (
    <RouterProvider router={router}>{children}</RouterProvider>
);

export { RouterProviderWrapper };
