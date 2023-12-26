import React, { useState } from 'react';
import { Button, Card, Form } from 'react-bootstrap';
import { FaFacebook, FaGoogle, FaTwitter } from 'react-icons/fa';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function RegisterWorker() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [email, setEmail] = useState('');
    const [name, setName] = useState('');
    const [location, setLocation] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [pricePerHour, setPricePerHour] = useState(0);
    const [errors, setErrors] = useState({});
    const [success, setSucess] = useState('');

    const navigate = useNavigate();


    const validateForm = () => {
        const errors = {};

        if (!username.trim()) {
            errors.username = 'Username is required';
        }

        if (!email.trim()) {
            errors.email = 'Email is required';
        } else if (!/\S+@\S+\.\S+/.test(email)) {
            errors.email = 'Email is invalid';
        }

        if (!password.trim()) {
            errors.password = 'Password is required';
        }

        if (!name.trim()) {
            errors.name = 'Name is required';
        }

        if (!location.trim()) {
            errors.location = 'Location is required';
        }

        if (!phoneNumber.trim()) {
            errors.phoneNumber = 'Phone number is required';
        }

        if (!pricePerHour.trim()) {
            errors.pricePerHour = 'pricePerHour is required';
        }

        setErrors(errors);

        return Object.keys(errors).length === 0;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (validateForm()) {
            try {
                const response = await axios.post('https://localhost:7230/api/User/RegisterWorker', { username, email, password, phoneNumber, location })
                if (response.status === 200) {
                    console.log(response.data);
                    const userId = response.data.id;
                    console.log(userId);
                    const config = {
                        headers: {
                            'Authorization': `Bearer ${response.data.token}`
                        },
                    };
                    const workerResponse = await axios.post('https://localhost:7230/api/IndustrialWorkers', { name, userId, location, phoneNumber, pricePerHour }, config);
                    if (workerResponse.status === 200) {
                        setSucess('Worker Had been added successfully!');
                    }
                    else {
                        setSucess('Worker Had not been added successfully!');

                    }

                }
            }
            catch (error) {
                if (error.response && error.response.status === 400) {
                    setErrors({
                        name: 'Something went wrong'
                    });
                    console.log(error.response);
                }
                else {
                    console.error('An error occured:', error);
                }
            }

        }
    };

    return (
        <div className="container mt-5">
            <div className="row justify-content-center">
                <div className="col-md-6 col-sm-12">
                    <Card className="text-center">
                        <Card.Body>
                            <h3 className="text-center mb-4">Join us as Worker!</h3>
                            <Form onSubmit={handleSubmit}>
                                <Form.Group className="mb-3">

                                    <Form.Control
                                        type="text"
                                        placeholder="Username"
                                        value={username}
                                        onChange={(e) => setUsername(e.target.value)}
                                    />
                                    {errors.username && <div className="text-danger">{errors.username}</div>}

                                </Form.Group>

                                <Form.Group className="mb-3">

                                    <Form.Control
                                        type="email"
                                        placeholder="email"
                                        value={email}
                                        onChange={(e) => setEmail(e.target.value)}
                                    />
                                    {errors.email && <div className="text-danger">{errors.email}</div>}

                                </Form.Group>

                                <Form.Group className="mb-3">

                                    <Form.Control
                                        type="password"
                                        placeholder="Password"
                                        value={password}
                                        onChange={(e) => setPassword(e.target.value)}
                                    />
                                    {errors.password && <div className="text-danger">{errors.password}</div>}

                                </Form.Group>
                                <Form.Group className="mb-3">

                                    <Form.Control
                                        type="text"
                                        placeholder="Name"
                                        value={name}
                                        onChange={(e) => setName(e.target.value)}
                                    />
                                    {errors.name && <div className="text-danger">{errors.name}</div>}

                                </Form.Group>

                                <Form.Group className="mb-3">

                                    <Form.Control
                                        type="phone"
                                        placeholder="Phone Number"
                                        value={phoneNumber}
                                        onChange={(e) => setPhoneNumber(e.target.value)}
                                    />
                                    {errors.phoneNumber && <div className="text-danger">{errors.phoneNumber}</div>}

                                </Form.Group>

                                <Form.Group className="mb-3">

                                    <Form.Control
                                        type="text"
                                        placeholder="Location"
                                        value={location}
                                        onChange={(e) => setLocation(e.target.value)}
                                    />
                                    {errors.location && <div className="text-danger">{errors.location}</div>}

                                </Form.Group>

                                <Form.Group className="mb-3">

                                    <Form.Control
                                        type="number"
                                        placeholder="Price Per Hour"
                                        value={pricePerHour}
                                        onChange={(e) => setPricePerHour(e.target.value)}
                                    />
                                    {errors.pricePerHour && <div className="text-danger">{errors.pricePerHour}</div>}

                                </Form.Group>

                                <Button variant="primary" type="submit" className="mr-2">
                                    Sign Up
                                </Button>
                            </Form>

                            {success && <div className="text-success my-3">
                                {success}
                            </div>}
                        </Card.Body>
                    </Card>
                </div>
            </div>
        </div>

    );
};

export default RegisterWorker;