import React from "react";
import Home from './Home';
import Route from '././Containers/Route/Route'
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import MyRoutes from "./Containers/MyRoutes/MyRoutes";
import CreateLocation from "./Containers/CreateLocation/CreateLocation";

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
        path: '/my-routes',
        element: <MyRoutes/>
    },
    {
        path: '/CreateLocation',
        element: <CreateLocation/>
    }
]);
   
   const RouterProviderWrapper = ({ children }) => (
    <RouterProvider router={router}>{children}</RouterProvider>
   );
   
export { RouterProviderWrapper };