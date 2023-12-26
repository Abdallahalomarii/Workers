import React, { useState } from 'react';
import axios from 'axios';
import Cookies from 'js-cookie';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../AuthContext/AuthContext';
import { Button, Card, Form } from 'react-bootstrap';




function Login() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [isLoggedIn, setLoggedIn] = useState(false);
    const [errors, setErrors] = useState({});

    const { setAuthData } = useAuth();

    const navigate = useNavigate();

    const vaildateForm = () =>
    {
        const errors = {};
        if (!username.trim()) {
            errors.username = 'Username is required';
        }
        if (!password.trim()) {
            errors.password = 'Password is required';
        }
        setErrors(errors);
        return Object.keys(errors).length === 0;
    }

    const signIn = async (e) => {
        e.preventDefault();

        if (vaildateForm()) {
            try {
                const response = await axios.post('https://localhost:7230/api/User/Login', { username, password });
                const token = response.data.token;
                Cookies.set('token', token, { expires: 7, secure: true });
                setLoggedIn(true);
                setAuthData(username);
                navigate('/');
                // Navigate to a protected route or show a success message
            } catch (error) {
                if (error.response && error.response.status === 401) {
                    // Unauthorized (invalid username or password)
                    setErrors({
                        username: 'Username is not exist',
                        password: 'Password not matching'
                    });
                } else {
                    // Handle other errors
                    console.error('An error occurred:', error);
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
                            <h3 className="text-center mb-4">Login</h3>
                            <Form onSubmit={signIn}>
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
                                        type="password"
                                        placeholder="Password"
                                        value={password}
                                        onChange={(e) => setPassword(e.target.value)}
                                    />
                                    {errors.password && <div className="text-danger">{errors.password}</div>}

                                </Form.Group>

                                

                                <Button variant="primary" type="submit" className="mx-2">
                                    Login
                                </Button>

                            </Form>

                            
                        </Card.Body>
                    </Card>
                </div>
            </div>
        </div>

    );
}

export default Login;
