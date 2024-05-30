import React, { useContext, useState } from 'react';
import { AuthContext } from './AuthProvider';
import RegisterGuide from './RegisterGuide';
import RegisterWorker from './RegisterWorker';

const CompanyProfile = ({ companyId }) => {
    const { authState } = useContext(AuthContext);
    const [showRegisterGuide, setShowRegisterGuide] = useState(false);
    const [showRegisterWorker, setShowRegisterWorker] = useState(false);
    const [company, setCompany] = useState([]);
    useEffect(() => {
        getCompanyInfo(page);
    }, [page]);

    const getCompanyInfo = async () => {
        try {
            const response = await axios.get('https://localhost:7160/api/Company/Get', { companyId });
            setCompany(prevLocations => [...prevLocations, ...response.data]);
        } catch (error) {
            console.error('There was an error!', error);
        }
    };
   
    return (      
        <div className="company-profile">
            <h1>{company.name}</h1>
            <p><strong>Description:</strong> {company.description}</p>
            <p><strong>Email:</strong> {company.email}</p>
            <img src={company.imageUrl} alt={`${company.name} Logo`} />
            <p><strong>Address:</strong> {company.address}</p>

            {authState.userRole === 'Worker' && (
                <div>
                    <button onClick={() => setShowRegisterGuide(!showRegisterGuide)}>
                        {showRegisterGuide ? 'Hide' : 'Add Guide'}
                    </button>
                    <button onClick={() => setShowRegisterWorker(!showRegisterWorker)}>
                        {showRegisterWorker ? 'Hide' : 'Add Worker'}
                    </button>
                </div>
            )}

            {showRegisterGuide && <RegisterGuide />}
            {showRegisterWorker && <RegisterWorker />}
        </div>
    );
};

export default CompanyProfile;
