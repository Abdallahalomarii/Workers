import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useParams } from 'react-router-dom';
import { Card, Container, Row, Col, Button } from 'react-bootstrap';

function ListWorkShop() {
    const { workerId } = useParams();
    const [workshops, setWorkshops] = useState([]);

    const fetchWorkshopsData = async () => {
        try {
            const response = await axios.get(`https://localhost:7230/api/Workshops/worker/${workerId}`);
            if (response.status === 200) {
                setWorkshops(response.data);
            }
        }
        catch (error) {
            console.log(error);
        }
    }


    useEffect(() => {
        fetchWorkshopsData();
    }, []);

    return (
        <Container className="mt-4">
            <h2>Workshops List</h2>
            <Row>
                {workshops.map(workshop => (
                    <Col key={workshop.id} lg={4} md={6} className="mb-4">
                        <Card>
                            <Card.Body>
                                <Card.Title>{workshop.workshop_Name}</Card.Title>
                                <Card.Text>{workshop.description}</Card.Text>
                            </Card.Body>
                            <Card.Footer>
                                <Button>
                                    Go to Review
                                </Button>
                            </Card.Footer>
                        </Card>
                    </Col>
                ))}
            </Row>
        </Container>
    );
}

export default ListWorkShop;
