import React, { createContext, useState, useEffect } from 'react';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [authState, setAuthState] = useState({
        userId: '',
        userName: '',
        token: '',
        userRole: '',
        companyId: ''
    });

    // Initialize state from localStorage if available
    useEffect(() => {
        console.log("Get")
        console.log(authState);
        const storedAuthState = localStorage.getItem('authState');
        if (storedAuthState) {
            setAuthState(JSON.parse(storedAuthState));
        }
    }, []);

    // Save authState to localStorage whenever it changes
    useEffect(() => {
        console.log("Save")
        console.log(authState);
        localStorage.setItem('authState', JSON.stringify(authState));
    }, [authState]);

    return (
        <AuthContext.Provider value={{ authState, setAuthState }}>
            {children}
        </AuthContext.Provider>
    );
};
