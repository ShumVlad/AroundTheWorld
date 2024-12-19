import React, { useState } from 'react';
import './registration.css';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Registration = () => {
  const [name, setName] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [passwordError, setPasswordError] = useState('');
  const [registrationSuccess, setRegistrationSuccess] = useState(false);
  const navigate = useNavigate();

  const handleSave = (e) => {
    e.preventDefault();

    if (password !== confirmPassword) {
      setPasswordError('Passwords do not match');
      return;
    }

    const url = 'https://localhost:7160/api/Identity/register-traveler';
    const data = {
      Username: name,
      Email: email,
      Password: password
    };
    axios.post(url, data)
      .then((result) => {
        setRegistrationSuccess(true);
      })
      .catch((error) => {
        console.log(error);
      });
  };

  return (
    <div className='aroundTheWorld__registration'>
      {registrationSuccess ? (
        <div className="success-message">
          Now you can login and choose your first trip
        </div>
      ) : (
        <>
          <input
            type="text"
            placeholder="Your User Name"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <input
            type="email"
            placeholder="Your Email Address"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
          <input
            type="password"
            placeholder="Confirm Password"
            value={confirmPassword}
            onChange={(e) => {
              setConfirmPassword(e.target.value);
              if (e.target.value !== password) {
                setPasswordError('Passwords do not match');
              } else {
                setPasswordError('');
              }
            }}
          />
          {passwordError && <div className="error-message">{passwordError}</div>}
          <button type="button" onClick={handleSave}>Register</button>
        </>
      )}
    </div>
  );
};

export default Registration;
