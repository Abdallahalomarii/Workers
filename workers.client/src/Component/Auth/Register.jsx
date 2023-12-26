import React, { useState } from 'react';
import { Button, Card, Form } from 'react-bootstrap';
import { FaFacebook, FaGoogle, FaTwitter } from 'react-icons/fa';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function Register() {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [phoneNumber, setPhoneNumber] = useState('');
    const [password, setPassword] = useState('');
    const [location, setLocation] = useState('');
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

        if (!phoneNumber.trim()) {
            errors.phoneNumber = 'Phone number is required';
        }

        if (!password.trim()) {
            errors.password = 'Password is required';
        }

        if (!location.trim()) {
            errors.location = 'Location is required';
        }

        setErrors(errors);

        return Object.keys(errors).length === 0;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (validateForm()) {
            try {
                const response = await axios.post('https://localhost:7230/api/User/RegisterUser', { username, password, phoneNumber, email, location })
                if (response.status === 200) {
                    setUsername('');
                    setPassword('');
                    setEmail('');
                    setPhoneNumber('');
                    setLocation('');
                    setSucess('Signed up successfully!');
                    navigate('/login');
                }
            }
            catch (error) {
                if (error.response && error.response.status === 400) {
                    setErrors({
                        username: 'Username has been taken'
                    });
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
                            <h3 className="text-center mb-4">Sign Up</h3>
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
                                        placeholder="Email"
                                        value={email}
                                        onChange={(e) => setEmail(e.target.value)}
                                    />
                                    {errors.email && <div className="text-danger">{errors.email}</div>}

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
                                        placeholder="Location"
                                        value={location}
                                        onChange={(e) => setLocation(e.target.value)}
                                    />
                                    {errors.location && <div className="text-danger">{errors.location}</div>}

                                </Form.Group>

                                <Button variant="primary" type="submit" className="mx-2">
                                    Sign Up
                                </Button>
                               
                            </Form>

                            <div className="division text-center">
                                <span className="or-line">OR</span>
                            </div>

                            <a href="/registerWorker" className="mr-2 btn btn-secondary">
                                Register as Worker
                            </a>
                            {success && <div className="text-success my-3">
                                { success }
                            </div>}
                        </Card.Body>
                    </Card>
                </div>
            </div>
        </div>

    );
};

export default Register;