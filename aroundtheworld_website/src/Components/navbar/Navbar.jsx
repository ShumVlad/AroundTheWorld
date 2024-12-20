import React, { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Login from '../login/Login';
import Registration from '../registration/Registration';
import { AuthContext } from '../../context/AuthContext';
import './navbar.css';

const Navbar = () => {
    const [toggleMenu, setToggleMenu] = useState(false);
    const [loginContainer, setLoginContainer] = useState(true);
    const { authState } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleNavigate = (source) => {
        navigate(source);
    };

    const handleProfileClick = () => {
        if (authState.userRole === 'Worker') {
            navigate(`/company/${authState.companyId}`);
        } else {
            console.log(authState);
        }
    };

    return (
        <div className='goalMe__navbar'>
            <div className='goalMe__navbar-links'>
                <a className="nav-link" onClick={() => handleNavigate('/')}>AroundTheWorld</a>
                <a className="nav-link" onClick={() => handleNavigate('/my-routes')}>Routes</a>
                <a className="nav-link" onClick={() => handleNavigate('/rent-items')}>Rent Items</a>
            </div>
            <div className="goalMe__navbar-menu">
                {authState.userId ? (
                    <button color="#fff" size={27} onClick={handleProfileClick}>
                        {authState.userName}
                    </button>
                ) : (
                    <>
                        {toggleMenu ? (
                            <button className="menu-button" onClick={() => setToggleMenu(false)}>
                                Sign up / Sign In
                            </button>
                        ) : (
                            <button className="menu-button" onClick={() => setToggleMenu(true)}>
                                Sign up / Sign In
                            </button>
                        )}
                        {toggleMenu && (
                            <div className="goalMe__navbar-menu_container scale-up-center">
                                {loginContainer ? <Login /> : <Registration />}
                                {loginContainer ? (
                                    <p onClick={() => setLoginContainer(false)}>Not a member?</p>
                                ) : (
                                    <p onClick={() => setLoginContainer(true)}>Already have an account?</p>
                                )}
                            </div>
                        )}
                    </>
                )}
            </div>
        </div>
    );
}

export default Navbar;
