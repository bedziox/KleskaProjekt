import { ChangeEvent, useState, FormEvent } from 'react';
import { Button, Modal, Form, Input } from 'antd';
import './Login.scss';

function Login() {
    const [showModal, setShowModal] = useState<boolean>(true);
    const [formData, setFormData] = useState<{
        email: string;
        password: string;
        repeatPassword: string;
    }>({
        email: '',
        password: '',
        repeatPassword: ''
    });

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
                    <Form.Item>
                        <Button type="primary" htmlType="submit" block>
                            Zaloguj się
                        </Button>
                    </Form.Item>
                </Form>
        </>
    );
}

export default Login;
