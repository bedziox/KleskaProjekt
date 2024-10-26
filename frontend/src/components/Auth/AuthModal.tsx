import React, { useState, useImperativeHandle, forwardRef } from 'react';
import { Modal, Button, Form, Input, Typography } from 'antd';
import './AuthModal.scss'

const { Text } = Typography;

interface AuthModalProps {
  defaultMode?: "login" | "register";
}

const AuthModal = forwardRef(({ defaultMode = "login" }: AuthModalProps, ref) => {
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [isRegister, setIsRegister] = useState(defaultMode === "register");
  const [fade, setFade] = useState(false)

  useImperativeHandle(ref, () => ({
    showModal: () => {
      setIsModalVisible(true);
      setIsRegister(defaultMode === "register");
    },
  }));


  const handleCancel = () => {
    setIsModalVisible(false);
    setIsRegister(defaultMode === "register");
  };

  const handleToggleAuth = () => {
    setFade(true);
    setTimeout(() => {
      setIsRegister(!isRegister);
      setFade(false);
    }, 300);
  };

  const onFinish = (values: { email: string; password: string; confirmPassword?: string }) => {
    console.log("Submitted values:", values);
    handleCancel();
  };

  return (
    <Modal
      title={isRegister ? "Rejestracja" : "Logowanie"}
      visible={isModalVisible}
      onCancel={handleCancel}
      footer={null}
    >
        <div className={`fade-container ${fade ? "fade-out" : "fade-in"}`}>
      <Form name="authForm" onFinish={onFinish} layout="vertical">
        <Form.Item
          label="E-mail"
          name="email"
          rules={[{ required: true, message: 'Podaj e-mail!' }]}
        >
          <Input />
        </Form.Item>
        <Form.Item
          label="Hasło"
          name="password"
          rules={[{ required: true, message: 'Podaj hasło!' }]}
        >
          <Input.Password />
        </Form.Item>
        {isRegister && (
          <Form.Item
            label="Powtórz hasło"
            name="confirmPassword"
            rules={[
              { required: true, message: 'Potwierdź hasło!' },
              ({ getFieldValue }) => ({
                validator(_, value) {
                  if (!value || getFieldValue('password') === value) {
                    return Promise.resolve();
                  }
                  return Promise.reject(new Error('Hasła się różnią!'));
                },
              }),
            ]}
          >
            <Input.Password />
          </Form.Item>
        )}
        <Form.Item>
          <Button type="primary" htmlType="submit">
            {isRegister ? "Zarejestruj się" : "Zaloguj się"}
          </Button>
        </Form.Item>
      </Form>
      </div>
      <Text type="secondary">
        {isRegister ? "Masz już konto?" : "Nie masz konta?"}{" "}
        <Button type="link" onClick={handleToggleAuth}>
          {isRegister ? "Zaloguj się!" : "Zarejestruj się!"}
        </Button>
      </Text>
    </Modal>
  );
});

export default AuthModal;
