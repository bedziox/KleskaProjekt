import { ChangeEvent, useState, FormEvent } from 'react';
import { Button, Modal, Form, FormLabel } from 'react-bootstrap';


function Register() {
    const [showModal, setShowModal] = useState<boolean>(false);
    const [formData, setFormData] = useState<{
        email: string,
        password: string,
        repeatPassword: string
    }>({
        email: '',
        password: '',
        repeatPassword: ''
    });
    
    const handleShowModal = () => {
        setShowModal(true);
    }

    const handleCloseModal = () => {
        setShowModal(false);
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
            <Button onClick={handleShowModal}>
                Zarejestruj się
            </Button>
            <Modal show={showModal} onHide={handleCloseModal}>
            <Modal.Header className="text-center" closeButton>
                <Modal.Title className="w-100">Rejestracja</Modal.Title>
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
                        Zarejestruj się
                    </Button>
                </Form>
            </Modal.Body>
            </Modal>
        </>
      );
  }
  
  export default Register;