import React from "react";
import Home from './Home';
import Route from '././Containers/Route/Route'
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import MyRoutes from "./Containers/MyRoutes/MyRoutes";

const router = createBrowserRouter([
    {
        path: '/',
        element: <Home />,
    },
    {
        path: '/:routeId',
        element: <Route />,
    },
    {
        path: '/MyRoutes',
        element: <MyRoutes/>
    }
]);
   
   const RouterProviderWrapper = ({ children }) => (
    <RouterProvider router={router}>{children}</RouterProvider>
   );
   
export { RouterProviderWrapper };