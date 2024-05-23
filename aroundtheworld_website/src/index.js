import React from "react";
import ReactDOM from "react-dom/client";
import App from './App';
import { RouterProviderWrapper as RouterProvider } from "./RouterPage";
import './index.css';
import { AuthProvider } from './constext/AuthContext';

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
    <AuthProvider>
        <RouterProvider />
    </AuthProvider>
);
