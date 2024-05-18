import React, {useState} from 'react'
import './login.css';
import axios from "axios";
import {useNavigate} from 'react-router-dom'

const Login = () => {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const navigate=useNavigate();
    const hadleSave =(e) => {
      e.preventDefault();
    
      const url ='https://localhost:7160/api/Identity/Login'; 
      const data = {
        Email: email,
        Password: password
      }
      
      axios.post(url, data)
      .then((result) => {
        const dt = result.data;
        navigate("/"+dt.userId)
      })
      .catch((error) =>{
        console.log(error);
      })
  }

  return (
    <div className='goalMe__login'>
       <input type="email" placeholder="Your Email Address" onChange={(e) => setEmail(e.target.value)}></input>
       <input type="password" placeholder="password" onChange={(e) => setPassword(e.target.value)}></input>
       <button type="button"  onClick={(e) => hadleSave(e)}>Login</button>
       <p>Forgot your password?</p>
       
    </div>
  )
}

export default Login
