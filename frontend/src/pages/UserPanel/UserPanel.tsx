import axios from 'axios'
import React, { useState } from 'react';

const UserPanel: React.FC = () => {
    const [userData, setUserData] = useState<{
        email: string;
        firstName: string;
        lastName: string;
        phoneNumber: {
            countryCode: string;
            number: string;
        }
    }>({
        email: '',
        firstName: '',
        lastName: '',
        phoneNumber: {
            countryCode: '',
            number: ''
        }
    });

    return (
        <>
        <div>
            <h1>Twoje dane:</h1>
            <p>
                Imię: {userData.firstName}
            </p>
            <p>
                Nazwisko: {userData.lastName}
            </p>
            <p>
                Adres e-mail: {userData.email}
            </p>
            <p>
                Numer telefonu: {userData.phoneNumber.countryCode} {userData.phoneNumber.number}
            </p>
        </div>
        </>
    );
}

export default UserPanel;
