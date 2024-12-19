import React, {useState} from 'react'
import axios from "axios";
import {useNavigate} from 'react-router-dom'

const RegisterCompany = () => {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [imageUrl, setImageUrl] = useState('');
    const [description, setDescription] = useState('');
    const [address, setAddress] = useState('');
    const navigate=useNavigate();
    const hadleSave =(e) => {
      e.preventDefault();
    
      const url ='https://localhost:7160/api/Company/Add'; 
      const data = {
        Id: " ",
        Name: name,
        Email: email,
        ImageUrl: imageUrl,
        description: description,
        Address: address
      }
      
      axios.post(url, data)
      .then((result) => {
        const dt = result.data;
        navigate('/');
      })
      .catch((error) =>{
        console.log(error);
      })
  }

  return (
    <div className='aroundTheWorld_register-company'>
       <input type="name" placeholder="Company's Name" onChange={(e) => setName(e.target.value)}></input>
       <input type="email" placeholder="Company's Email" onChange={(e) => setEmail(e.target.value)}></input>
       <input type="description" placeholder="Add description to your company" onChange={(e) => setDescription(e.target.value)}></input>
       <input type="imageUrl" placeholder="Url to the Avatar picture" onChange={(e) => setImageUrl(e.target.value)}></input>
       <input type="address" placeholder="Company's Address" onChange={(e) => setAddress(e.target.value)}></input>
       <button type="button"  onClick={(e) => hadleSave(e)}>Registrate</button>
    </div>
  )
}

export default RegisterCompany
