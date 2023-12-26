import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';

function WorkerDetails() {
    const navigate = useNavigate();
    const { workerId } = useParams();
    const [workerDetails, setWorkerDetails] = useState({});

    const fetchWorkerDetails = async () => {
        try {
            const response = await axios.get(`https://localhost:7230/api/IndustrialWorkers/${workerId}`);
            if (response.status === 200) {
                setWorkerDetails(response.data);
            }
        } catch (error) {
            console.log(error);
        }
    };

    useEffect(() => {
        fetchWorkerDetails();
    }, []);

    const goToWrodkshops = () => {
        navigate(`/workshops/${workerId}`);
    }
    return (
        <div className="container mt-4">
            <h2 className="mb-4">Worker Details</h2>
            <div className="card">
                <div className="card-body">
                    <p className="card-text">
                        <strong>Name:</strong> {workerDetails.name}
                    </p>
                    <p className="card-text">
                        <strong>Location:</strong> {workerDetails.location}
                    </p>
                    <p className="card-text">
                        <strong>Phone Number:</strong> {workerDetails.phoneNumber}
                    </p>
                    <p className="card-text">
                        <strong>Price:</strong> {workerDetails.pricePerHour}<em className="text-success">$</em>
                    </p>
                    {workerDetails.isActive && <span className="rounded rounded-5 p-1 bg-success"></span>}
                </div>
                <button className="btn btn-secondary p-2 m-1 col-2 " onClick={() => goToWrodkshops()}>
                workshops
                </button>
            </div>
        </div>
    );
}

export default WorkerDetails;
