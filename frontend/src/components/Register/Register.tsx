import { ChangeEvent, useState, FormEvent } from 'react';
import { Button, Modal, Form, Input } from 'antd';
import './Register.scss';

function Register() {
    const [showModal, setShowModal] = useState<boolean>(false);
    const [formData, setFormData] = useState<{
        email: string;
        password: string;
        repeatPassword: string;
    }>({
        email: '',
        password: '',
        repeatPassword: ''
    });

    const handleShowModal = () => {
        setShowModal(true);
    };

    const handleCloseModal = () => {
        setShowModal(false);
    };

    const handleChangeForm = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
    };

    const handleSubmitForm = (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log('Form Data:', formData);
    };

    return (
        <>
            <Button type="primary" onClick={handleShowModal}>
                Zarejestruj się
            </Button>
            <Modal
                title="Rejestracja"
                open={showModal}
                onCancel={handleCloseModal}
                footer={null}
                className='registerModal'
            >
                <Form layout="vertical" onSubmitCapture={handleSubmitForm}>
                    <Form.Item
                        label="Adres E-mail"
                        name="email"
                        rules={[
                            {
                                required: true,
                                message: 'Proszę podać adres e-mail',
                            },
                        ]}
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
                        rules={[
                            {
                                required: true,
                                message: 'Proszę podać hasło',
                            },
                        ]}
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
                            {
                                required: true,
                                message: 'Proszę powtórzyć hasło',
                            },
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

                    <Form.Item>
                        <Button type="primary" htmlType="submit" block>
                            Zarejestruj się
                        </Button>
                    </Form.Item>
                </Form>
            </Modal>
        </>
    );
}

export default Register;
