import React, { useState, useContext } from 'react';
import axios from "axios";
import { AuthContext } from '../../context/AuthContext';
import './login.css';

const Login = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const { setAuthState } = useContext(AuthContext);  // Use the context

    const handleSave = (e) => {
        e.preventDefault();

        const url = 'https://localhost:7160/api/Identity/Login';
        const data = {
            Email: email,
            Password: password
        };

        axios.post(url, data)
            .then((result) => {
                const { userId, token, userName, userRole, companyId } = result.data;
                setAuthState({ userId, token, userName, userRole, companyId }); 
                // navigate("/" + userId);  // Uncomment if you want to navigate after login
                console.log(result.data);
            })
            .catch((error) => {
                console.log(error);
            });
    }

    return (
        <div className='goalMe__login'>
            <input type="email" placeholder="Your Email Address" onChange={(e) => setEmail(e.target.value)}></input>
            <input type="password" placeholder="password" onChange={(e) => setPassword(e.target.value)}></input>
            <button type="button" onClick={(e) => handleSave(e)}>Login</button>
            <p>Forgot your password?</p>
        </div>
    );
}

export default Login;
