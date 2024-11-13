import { ChangeEvent, useState, FormEvent } from 'react';
import { Button, Modal, Form, Input } from 'antd';
import './Login.scss';
import axios from 'axios';
import { useAuth } from '../../context/AuthContext.tsx';

function Login() {
    const [showModal, setShowModal] = useState<boolean>(true);
    const [formData, setFormData] = useState<{
        email: string;
        password: string;
    }>({
        email: '',
        password: '',
    });

    const handleChangeForm = (e: ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
    };

    const { login } = useAuth();

    const handleSubmitForm = async (e: FormEvent<HTMLFormElement>) => {
        e.preventDefault();
        console.log('Form Data:', formData);
        const res = await axios.post("http://localhost:8080/auth/login", formData)
        if (res.status === 200) {
            console.log(res)
            login(res)
        } else {
            console.error(res)
        }
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
