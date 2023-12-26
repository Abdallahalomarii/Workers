import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { useAuth } from './AuthContext/AuthContext';
import Cookies from 'js-cookie';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';


function Header() {
    const { username } = useAuth();
    const navigate = useNavigate();
    const logout = async () => {
        const token = Cookies.get('token');
        const config = {
            headers: {
                'Authorization': `Bearer ${token}`,
            },
        };
        if (token) {
            try {
                const response = await axios.post('https://localhost:7230/api/User/Logout', config);
                if (response.status === 200) {
                    console.log('Logged out successfully!');
                    Cookies.remove('token');
                    window.location.reload();
                }
            } catch (error) {
                console.log(error);
            }
        }

    };

    return (
        <>
            <Navbar expand="lg" variant="dark" className="bg-dark">
                <Container>
                    <Navbar.Brand href="/">Workers</Navbar.Brand>
                    <Navbar.Toggle aria-controls="basic-navbar-nav" />
                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="ml-auto">
                            <Nav.Link href="/">Home</Nav.Link>
                            {username && <Nav.Link href="/fetch">Work List</Nav.Link>}
                        </Nav>
                        {username ? (
                            <Nav className="ml-auto">
                                <Nav.Link onClick={logout}>Logout</Nav.Link>
                            </Nav>
                        ) : (
                            <Nav className="ml-auto">
                                <Nav.Link href="/register">Sign Up</Nav.Link>
                                <Nav.Link href="/login">Sign In</Nav.Link>
                            </Nav>
                        )}
                        <Nav>
                            <Nav.Link href="/workers" >Workers</Nav.Link>
                        </Nav>
                    </Navbar.Collapse>
                    {username && <span className="navbar-text text-light">Welcome, {username}!</span>}
                </Container>
            </Navbar>

        </>
    );
}

export default Header;
