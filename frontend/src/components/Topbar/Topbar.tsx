import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import {Button} from 'antd';
import Navbar from 'react-bootstrap/Navbar';
import AuthModal from '../Auth/AuthModal';
import React, { useRef } from 'react';

const Topbar: React.FC = () => {
  const authModalRef = useRef<{ showModal: () => void }>(null);
  
  const openAuthModal = () => {
    authModalRef.current?.showModal();
  };

  return (
      <Navbar bg="dark" variant="dark" expand="lg" fixed="top">
        <Container>
          <Navbar.Brand href="/">KleskaProjekt</Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
            <Nav className="me-auto">
              <Nav.Link href="/">Strona główna</Nav.Link>
            </Nav>
            <Nav>
              <Button type="primary" onClick={openAuthModal}>
                Zaloguj
              </Button>
              <AuthModal ref={authModalRef} defaultMode="login" />
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    )
  }

export default Topbar;