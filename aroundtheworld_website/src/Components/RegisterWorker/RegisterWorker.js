import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';

const RegisterWorker = React.forwardRef((props, ref) => {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    
    const navigate = useNavigate();

    const handleSave = (e) => {
        e.preventDefault();
      
        const url = 'https://localhost:7160/api/Identity/register-worker'; 
        const data = {
          Username: name,
          Email: email,
          Password: password,
          CompanyId: props.data.CompanyId
        }
        
        axios.post(url, data)
        .then((result) => {
          navigate("/");
        })
        .catch((error) => {
          console.log(error);
        });
    }

    if (!props.data) {
        return null;
    }

    return (
        <div ref={ref}>
            <div className='aroundTheWorld__registerWorker'>
                <input type="name" placeholder="User Name" onChange={(e) => setName(e.target.value)}></input>
                <input type="email" placeholder="Email Address" onChange={(e) => setEmail(e.target.value)}></input>
                <input type="password" placeholder="password" onChange={(e) => setPassword(e.target.value)}></input>
                <button type="button" onClick={(e) => handleSave(e)}>Register Worker</button>
            </div>
        </div>
    );
});

export default RegisterWorker;
