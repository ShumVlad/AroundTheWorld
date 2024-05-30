import React from 'react';

const RegisterGuide = React.forwardRef((props, ref) => {
    if (!props.data) {
        return null;
    }
    const [name, setName] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    
    const hadleSave =(e) => {
        e.preventDefault();
      
        const url ='https://localhost:7160/api/Identity/register-guide'; 
        const data = {
          Username: name,
          Email: email,
          Password: password,
          CompanyId: props.data.CompanyId
        }
        
        axios.post(url, data)
        .then((result) => {
          const dt = result.data;
          navigate("/")
        })
        .catch((error) =>{
          console.log(error);
        })
    }

    return (
        <div ref={ref}>
            <div className='aroundTheWorld__registerWorker'>
                <input type="name" placeholder="User Name" onChange={(e) => setName(e.target.value)}></input>
                <input type="email" placeholder="Email Address" onChange={(e) => setEmail(e.target.value)}></input>
                <input type="password" placeholder="password" onChange={(e) => setPassword(e.target.value)}></input>
                <button type="button"  onClick={(e) => hadleSave(e)}>Register Worker</button>
            </div>
        </div>
    );
});

export default RegisterGuide;