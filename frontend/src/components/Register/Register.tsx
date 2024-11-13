import { ChangeEvent, useState, FormEvent } from 'react';
import { Button, Modal, Form, Input } from 'antd';
import axios from 'axios';
import './Register.scss';

function Register() {
    const [showModal, setShowModal] = useState<boolean>(false);
    const [formData, setFormData] = useState<{
        firstName: string;
        lastName: string;
        email: string;
        password: string;
        repeatPassword: string;
        phoneNumber: {
            countryCode: string;
            number: string;
        };
    }>({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        repeatPassword: '',
        phoneNumber: {
            countryCode: '',
            number: '',
        }
    });

    const handleShowModal = () => {
        setShowModal(true);
    };

    const handleCloseModal = () => {
        setShowModal(false);
    };

    const handleChangeForm = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        if (name === "countryCode" || name === "number") {
            setFormData({
                ...formData,
                phoneNumber: {
                    ...formData.phoneNumber,
                    [name]: value
                }
            });
        } else {
            setFormData({
                ...formData,
                [name]: value
            });
        }
    };

    const handleSubmitForm = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        
        const userDto = {
            firstName: formData.firstName,
            lastName: formData.lastName,
            email: formData.email,
            password: formData.password,
            phoneNumber: {
                countryCode: formData.phoneNumber.countryCode,
                number: formData.phoneNumber.number
            }
        };

        try {
            await axios.post('http://localhost:8080/auth/register', userDto);
            console.log('Form data sent successfully:', userDto);
            setShowModal(false);
        } catch (error) {
            console.error('Error submitting form data:', error);
        }
    };

    return (
        <>
                <Form layout="vertical" onSubmitCapture={handleSubmitForm}>
                    <Form.Item
                        label="Imię"
                        name="firstName"
                        rules={[{ required: true, message: 'Proszę podać imię' }]}
                    >
                        <Input
                            placeholder="Imię"
                            name="firstName"
                            value={formData.firstName}
                            onChange={handleChangeForm}
                        />
                    </Form.Item>

                    <Form.Item
                        label="Nazwisko"
                        name="lastName"
                        rules={[{ required: true, message: 'Proszę podać nazwisko' }]}
                    >
                        <Input
                            placeholder="Nazwisko"
                            name="lastName"
                            value={formData.lastName}
                            onChange={handleChangeForm}
                        />
                    </Form.Item>

                    <Form.Item
                        label="Adres E-mail"
                        name="email"
                        rules={[{ required: true, message: 'Proszę podać adres e-mail' }]}
                    >
                        <Input
                            type="email"
                            placeholder="Adres E-mail"
                            name="email"
                            value={formData.email}
                            onChange={handleChangeForm}
                            autoFocus
                        />
                    </Form.Item>

                    <Form.Item
                        label="Hasło"
                        name="password"
                        rules={[{ required: true, message: 'Proszę podać hasło' }]}
                    >
                        <Input.Password
                            placeholder="Hasło"
                            name="password"
                            value={formData.password}
                            onChange={handleChangeForm}
                        />
                    </Form.Item>

                    <Form.Item
                        label="Powtórz hasło"
                        name="repeatPassword"
                        rules={[
                            { required: true, message: 'Proszę powtórzyć hasło' },
                            ({ getFieldValue }) => ({
                                validator(_, value) {
                                    if (!value || getFieldValue('password') === value) {
                                        return Promise.resolve();
                                    }
                                    return Promise.reject(new Error('Hasła muszą być takie same!'));
                                },
                            }),
                        ]}
                    >
                        <Input.Password
                            placeholder="Powtórz hasło"
                            name="repeatPassword"
                            value={formData.repeatPassword}
                            onChange={handleChangeForm}
                        />
                    </Form.Item>

                    <Form.Item
                        label="Kod kraju telefonu"
                        name="countryCode"
                        rules={[{ required: true, message: 'Proszę podać kod kraju' }]}
                    >
                        <Input
                            placeholder="Kod kraju telefonu"
                            name="countryCode"
                            value={formData.phoneNumber.countryCode}
                            onChange={handleChangeForm}
                        />
                    </Form.Item>

                    <Form.Item
                        label="Numer telefonu"
                        name="number"
                        rules={[{ required: true, message: 'Proszę podać numer telefonu' }]}
                    >
                        <Input
                            placeholder="Numer telefonu"
                            name="number"
                            value={formData.phoneNumber.number}
                            onChange={handleChangeForm}
                        />
                    </Form.Item>

                    <Form.Item>
                        <Button type="primary" htmlType="submit" block>
                            Zarejestruj się
                        </Button>
                    </Form.Item>
                </Form>
        </>
    );
}

export default Register;
