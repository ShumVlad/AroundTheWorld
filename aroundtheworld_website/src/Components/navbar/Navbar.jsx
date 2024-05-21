import React, {useState} from 'react';
import Login from '../login/Login';
import Registration from '../registration/Registration';
import {useNavigate} from 'react-router-dom'
import './navbar.css';

const Navbar = () => {
    const [toggleMenu, setToggleMenu] = useState(false);
    const [loginContainer, setLoginContainer] = useState(true);
    function Navigate(source)
    {
        useNavigate(source);
    }
  return (
    <div className='goalMe__navbar'>
      <div className='goalMe__navbar-links'>
        <a className="nav-link" onClick={()=>Navigate('/my-routes')}>My Routes</a>
      </div>
      <div className="goalMe__navbar-menu">
        {toggleMenu
          ? <button color="#fff" size={27} onClick={() => setToggleMenu(false)}>Sing up / Sing In</button>
          : <button color="#fff" size={27} onClick={() => setToggleMenu(true)}>Sing up / Sing In</button>}
        {toggleMenu && (
          <div className="goalMe__navbar-menu_container scale-up-center">
            {loginContainer ? <Login/> : <Registration/>}
            <p onClick={() => loginContainer ? setLoginContainer(false) : setLoginContainer(true)}>Not a member?</p>
          </div>  
        )}
      </div>
    </div>
  )
}

export default Navbar
