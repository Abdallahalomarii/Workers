import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function Workers() {
    const navigate = useNavigate();
    const [workers, setWorkers] = useState([]);

    const fetchWorkers = async () => {
        try {
            const response = await axios.get('https://localhost:7230/api/IndustrialWorkers');
            if (response.status === 200) {
                setWorkers(response.data);
            }
        } catch (error) {
            console.log(error);
        }
    };

    useEffect(() => {
        fetchWorkers();
    }, []);

    const goToDetails = (workerId) => {
        // Navigate to the worker details page with the worker ID
        navigate(`/WorkerDetails/${workerId}`);
    };

    return (
        <div className="container mt-4">
            <h2>Workers</h2>
            <div className="row">
                {workers.map(worker => (
                    <div key={worker.id} className="col-md-4 mb-4">
                        <div className="card">
                            <div className="card-body">
                                <h5 className="card-title">{worker.name}</h5>
                                <p className="card-text">Location: {worker.location}</p>
                                <p className="card-text">Phone Number: {worker.phoneNumber}</p>
                                <p className="card-text">Price Per Hour: ${worker.pricePerHour}</p>
                                <p className="card-text">Active: {worker.isActive ? 'Yes' : 'No'}</p>
                                <p className="card-text">Rating: {worker.rate}/10</p>
                                <button
                                    onClick={() => goToDetails(worker.id)}
                                    className="btn btn-primary"
                                >
                                    View Details
                                </button>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
}

export default Workers;
