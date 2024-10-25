import { ChangeEvent, useState, FormEvent } from 'react';
import { Button, Modal, Form, FormLabel } from 'react-bootstrap';

function Login() {
    const [showLoginModal, setShowLoginModal] = useState<boolean>(false);
    const [formData, setFormData] = useState<{
        email: string,
        password: string,
        repeatPassword: string
    }>({
        email: '',
        password: '',
        repeatPassword: ''
    });
    
    const handleShowLoginModal = () => {
        setShowLoginModal(true);
    }

    const handleCloseLoginModal = () => {
        setShowLoginModal(false);
    }

    const handleChangeForm = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
    }

    const handleSubmitForm = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log('Form Data:', formData);
      };


    return (
        <>
            <Button onClick={handleShowLoginModal}>
                Zaloguj się
            </Button>
            <Modal show={showLoginModal} onHide={handleCloseLoginModal}>
            <Modal.Header className="text-center" closeButton>
                <Modal.Title className="w-100">Logowanie</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmitForm}>
                    <Form.Group controlId="formEmail" className="mb-3">
                        <FormLabel>Adres E-mail: </FormLabel>
                        <Form.Control
                        type="text"
                        placeholder="Adres E-mail"
                        name="email"
                        value={formData.email}
                        onChange={handleChangeForm}
                        autoFocus
                        />
                    </Form.Group>
                    <Form.Group controlId="formPassword" className="mb-3">
                        <FormLabel>Hasło: </FormLabel>
                        <Form.Control
                        type="password"
                        placeholder="Hasło"
                        name="password"
                        value={formData.password}
                        onChange={handleChangeForm}
                        />
                    </Form.Group>
                    <Form.Group controlId="formRepeatPassword" className="mb-3">
                        <FormLabel>Powtórz hasło: </FormLabel>
                        <Form.Control
                        type="repeatPassword"
                        placeholder="Hasło"
                        name="repeatPassword"
                        value={formData.repeatPassword}
                        onChange={handleChangeForm}
                        />
                    </Form.Group>
                    <Button type="submit" className="m-auto">
                        Zaloguj się
                    </Button>
                </Form>
            </Modal.Body>
            </Modal>
        </>
      );
  }
  
  export default Login;