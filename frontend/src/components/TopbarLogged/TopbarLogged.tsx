import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import {Button} from 'antd';
import Navbar from 'react-bootstrap/Navbar';
import React, { useRef } from 'react';
import { Avatar, Space } from 'antd';

const TopbarLogged: React.FC = () => {
  const authModalRef = useRef<{ showModal: () => void }>(null);
  

  return (
      <Navbar bg="dark" variant="dark" expand="lg" fixed="top">
        <Container>
          <Navbar.Brand href="/home"  style={{ display: 'flex', alignItems: 'center', justifyContent: 'center', gap: '8px' }}>Cześć, user! <Avatar style={{ backgroundColor: '#ccc', color: '#000' }}>U</Avatar></Navbar.Brand>
          <Navbar.Toggle aria-controls="basic-navbar-nav" />
          <Navbar.Collapse id="basic-navbar-nav">
          <Nav  className="ms-auto">
              <Nav.Link href="/myaccount">Moje konto</Nav.Link>
              <Nav.Link href="#">Zobacz ogłoszenia</Nav.Link>
            </Nav>
            <Nav>
              <Button type="primary">
                Dodaj ogłoszenie
              </Button>
            </Nav>
          </Navbar.Collapse>
        </Container>
      </Navbar>
    )
  }

export default TopbarLogged;