import React, {useState} from 'react'
import './registration.css';
import axios from "axios";
import {useNavigate} from 'react-router-dom'

const Registration = () => {
    const [name, setName] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const navigate=useNavigate();
    const hadleSave =(e) => {
      e.preventDefault();
    
      const url ='https://localhost:7044/api/Identity/Registration'; 
      const data = {
        Username: name,
        Email: email,
        Password: password
      }
      
      axios.post(url, data)
      .then((result) => {
        const dt = result.data;
        navigate("/my-trips/"+dt.userId+"/"+dt.userroles[0])
      })
      .catch((error) =>{
        console.log(error);
      })
  }

  return (
    <div className='goalMe__registration'>
       <input type="name" placeholder="Your User Name" onChange={(e) => setName(e.target.value)}></input>
       <input type="email" placeholder="Your Email Address" onChange={(e) => setEmail(e.target.value)}></input>
       <input type="password" placeholder="password" onChange={(e) => setPassword(e.target.value)}></input>
       <button type="button"  onClick={(e) => hadleSave(e)}>Registrate</button>
    </div>
  )
}

export default Registration
