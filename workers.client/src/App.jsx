import Login from './Component/Auth/Login';
import React, { useEffect } from 'react';
import { Routes, Route } from 'react-router-dom';
import Fetch from './Component/Fetch';
import Home from './Component/Home/Home';
import Register from './Component/Auth/Register';
import Header from './Component/Header';
import RegisterWorker from './Component/Auth/RegisterWorker';
import Cookies from 'js-cookie';
import axios from 'axios';
import { useAuth } from './Component/AuthContext/AuthContext';
import Workers from './Component/Workers/Workers';
import WorkerDetails from './Component/Workers/WorkerDetails';
import ListWorkShop from './Component/Workers/WorkShops/ListWorkShop';

function App() {
    const token = Cookies.get('token');

    const { setAuthData } = useAuth();
        const loadUsername = async () => {
            const config = {
                headers: {
                    'Authorization': `Bearer ${token}`,
                },
            };

            const response = await axios.get('https://localhost:7230/api/User/Profile', config);
            setAuthData(response.data.userName);
        }
        
    
    useEffect(() => {
        if (token)
            loadUsername();
    }, []);
    return (
        <>
                        <Header />
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
                <Route path="/fetch" element={<Fetch />} />
                <Route path="/registerWorker" element={<RegisterWorker />} />
                <Route path="/Workers" element={<Workers />} />
                <Route path="/WorkerDetails/:workerId" element={<WorkerDetails />} />
                <Route path="/workshops/:workerId" element={<ListWorkShop /> } />
            </Routes>
        </>
    )
}

export default App;