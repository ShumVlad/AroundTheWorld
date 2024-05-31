import React, { useContext, useState, useEffect } from 'react';
import axios from 'axios';
import { useParams } from "react-router-dom";
import { AuthContext } from '../../context/AuthContext';
import RegisterGuide from '../../Components/RegisterGuide/RegisterGuide';
import RegisterWorker from '../../Components/RegisterWorker/RegisterWorker';
import Navbar from '../../Components/navbar/Navbar'

const CompanyProfile = () => {
    const { authState } = useContext(AuthContext);
    const [showRegisterGuide, setShowRegisterGuide] = useState(false);
    const [showRegisterWorker, setShowRegisterWorker] = useState(false);
    const [company, setCompany] = useState({});
    const { companyId } = useParams();

    useEffect(() => {
        getCompanyInfo();
    }, [companyId]);

    const getCompanyInfo = async () => {
        try {
            const response = await axios.get('https://localhost:7160/api/Company/Get', { params: { companyId } });
            setCompany(response.data);
        } catch (error) {
            console.error('There was an error!', error);
        }
    };

    return (
        <div className="company-profile">
            <Navbar/>
            <h1>{company.name}</h1>
            <p><strong>Description:</strong> {company.description}</p>
            <p><strong>Email:</strong> {company.email}</p>
            <img src={company.imageUrl} alt={`${company.name} Logo`} />
            <p><strong>Address:</strong> {company.address}</p>

            {authState.userRole === 'Worker' && authState.companyId === companyId && (
                <div>
                    <button onClick={() => setShowRegisterGuide(!showRegisterGuide)}>
                        {showRegisterGuide ? 'Hide' : 'Add Guide'}
                    </button>
                    <button onClick={() => setShowRegisterWorker(!showRegisterWorker)}>
                        {showRegisterWorker ? 'Hide' : 'Add Worker'}
                    </button>
                </div>
            )}

            {showRegisterGuide && <RegisterGuide data={{ CompanyId: companyId }} />}
            {showRegisterWorker && <RegisterWorker data={{ CompanyId: companyId }} />}
        </div>
    );
};

export default CompanyProfile;
