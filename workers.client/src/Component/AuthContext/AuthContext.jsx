// AuthContext.js
import { createContext, useContext, useState } from 'react';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [username, setUsername] = useState('');

    const setAuthData = (newUsername) => {
        setUsername(newUsername);
    };

    return (
        <AuthContext.Provider value={{ username, setAuthData }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    return useContext(AuthContext);
};