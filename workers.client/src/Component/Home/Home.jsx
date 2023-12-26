import React from 'react';
import { Container } from 'react-bootstrap';
import backgroundImage from "../Home/bg.jpg"; // Adjust the path to your background image
import Header from '../Header';

const Home = () => {
    return (
        <div className="full-width-background position-relative">
            <div className="full-width-background-image min-vh-100 d-flex align-items-center justify-content-center text-align-center" style={{
                backgroundImage: `url(${backgroundImage})`,
                backgroundSize: 'cover',
                backgroundPosition: 'center',
                position: 'relative', // Add this to make sure the child div is positioned relative to this div
            }}>
                {/* Overlay with opacity */}
                <div className="position-absolute top-0 start-0 w-100 h-100" style={{ backgroundColor: 'rgba(0, 0, 0, 0.75)', inset: "0px", zIndex: '10' }}></div>

                <div className="position-relative mx-auto my-auto d-flex  flex-column p-2" style={{ zIndex: '20' }}>
                    <Container className="text-center text-white">
                        <h1 className="display-4">Workers</h1>
                        <p className="lead">workers application where you can find the best services.</p>
                    </Container>
                </div>
            </div>
        </div>
    );
};

export default Home;
